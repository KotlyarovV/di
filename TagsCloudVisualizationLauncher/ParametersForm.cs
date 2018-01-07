using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
{
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


        private void GetParameters()
        {
            parameters.Width = int.Parse(height.Text);
            parameters.Height = int.Parse(width.Text);
            parameters.FileName = fileinput.Text;
            parameters.FontSizeMax = double.Parse(maxfontsize.Text);
            parameters.FontSizeMin = double.Parse(minfontsize.Text);
            parameters.ImageName = outputfile.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            GetParameters();
            var cloudBuilder = new CloudBuilder();
            var bitmapResult = cloudBuilder.TryBuildCloud(parameters);
            
            Hide();
            var form1 = new Form1(bitmapResult.GetValueOrThrow());
            form1.Closed += (s, args) => Close();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetParameters();
            
            var cloudBuilder = new CloudBuilder();
            var bitmapResult = cloudBuilder.TryBuildCloud(parameters);

            if (!bitmapResult.IsSuccess)
            {
                Console.WriteLine(bitmapResult.Error);
                return;
            }

            var outPuter = new ImageOutputer();
            var saveResult = outPuter.SaveImage(
                parameters,
                bitmapResult.GetValueOrThrow()
            );            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
