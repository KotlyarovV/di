using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;

        public Form1(Bitmap bitmap)
        {
            InitializeComponent();
            Size = bitmap.Size;
            this.bitmap = bitmap;
            Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0,0);
        }
    }
}
