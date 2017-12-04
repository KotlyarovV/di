using System;
using System.Windows.Forms;
using Autofac;

namespace TagsCloudVisualization
{
    public partial class ParametersForm : Form
    {
        readonly Parameters parameters;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                GetParameters();
                var container = ConteinerConfigurator.ConfigureContainer(parameters);
                var cloudPainter = container.Resolve<CloudPainter>();
                var bitmap = cloudPainter.GetBitmap(parameters);
                Hide();
                var form1 = new Form1(bitmap);
                form1.Closed += (s, args) => Close();
                form1.Show();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GetParameters();
                var container = ConteinerConfigurator.ConfigureContainer(parameters);
                var cloudPainter = container.Resolve<CloudPainter>();
                var bitmap = cloudPainter.GetBitmap(parameters);
                cloudPainter.SaveBitmap(bitmap);

            }
            catch { }
        }
    }
}
