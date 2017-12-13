using System;
using System.Drawing;
using System.Drawing.Imaging;
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
            var container = ConteinerConfigurator.ConfigureContainer(parameters);
            var cloudPainter = container.Resolve<ICloudPainter>();
            var inputStream = container.ResolveNamed<Stream>(ConteinerConfigurator.InputStreamName);

            string text;
            using (var reader = new StreamReader(inputStream, Encoding.Default))
            {
                text = reader.ReadToEnd();
            }
            
            var bitmap = cloudPainter.GetBitmap(
                    text,
                    parameters.Width,
                    parameters.Height,
                    parameters.FontSizeMin,
                    parameters.FontSizeMax
                );
            
            Hide();
            var form1 = new Form1(bitmap);
            form1.Closed += (s, args) => Close();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GetParameters();
                var container = ConteinerConfigurator.ConfigureContainer(parameters);
                var cloudPainter = container.Resolve<ICloudPainter>();
                var inputStream = container.ResolveNamed<Stream>(ConteinerConfigurator.InputStreamName);

                string text;
                using (var reader = new StreamReader(inputStream, Encoding.Default))
                {
                    text = reader.ReadToEnd();
                }

                var bitmap = cloudPainter.GetBitmap(
                    text,
                    parameters.Width,
                    parameters.Height,
                    parameters.FontSizeMin,
                    parameters.FontSizeMax
                );

                var outStream = container.ResolveNamed<Stream>(ConteinerConfigurator.OutStreamName);
                var imageFormat = parameters.GetImageFormat();
                bitmap.Save(outStream, imageFormat);
            }
            catch { }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
