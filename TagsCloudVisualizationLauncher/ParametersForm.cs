using System;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
    enum Regim
    {
        Show,
        Save
    }


    public partial class ParametersForm : Form
    {
        private readonly Parameters parameters;
        public ParametersForm()
        {
            InitializeComponent();
            parameters = new Parameters();
        }

        private void ParametersForm_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private Result<Parameters> GetParametersResult(Regim regim)
        {
            if (fileinput.TextLength == 0 || (regim == Regim.Save && outputfile.TextLength == 0))
                return Result.Fail<Parameters>("One or more parameters was missed!");

            parameters.FileName = fileinput.Text;

            var digitParseResult = Result
                .Of(() => parameters.Width = int.Parse(height.Text))
                .Then(x => parameters.Height = int.Parse(width.Text))
                .Then(x => parameters.FontSizeMax = double.Parse(maxfontsize.Text))
                .Then(x => parameters.FontSizeMin = double.Parse(minfontsize.Text))
                .ReplaceError((str) => "Digits parameters was written incorrect!");
            
            return new Result<Parameters>(digitParseResult.Error, parameters);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            var parametersResult = GetParametersResult(Regim.Show);

            if (!parametersResult.IsSuccess)
            {
                errorMessage.Text = parametersResult.Error;
                return;
            }

            var cloudBuilder = new CloudBuilder();
            var bitmapResult = cloudBuilder.TryBuildCloud(parameters);
            if (!bitmapResult.IsSuccess)
            {
                errorMessage.Text = bitmapResult.Error;
            }
            else
            {
                Hide();
                var form1 = new Form1(bitmapResult.GetValueOrThrow());
                form1.Closed += (s, args) => Close();
                form1.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var parametersResult = GetParametersResult(Regim.Save);            

            var cloudBuilder = new CloudBuilder();
            var bitmapResult = cloudBuilder.TryBuildCloud(parametersResult);
            
            var outPuter = new ImageOutputer();
            var saveResult = outPuter.SaveImage(
                parametersResult,
                bitmapResult.GetValueOrThrow()
            );

            if (!saveResult.IsSuccess)
            {
                errorMessage.Text = bitmapResult.Error;
            }
            else errorMessage.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
