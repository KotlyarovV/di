using System.Drawing;

namespace TagsCloudVisualization
{
    public class TextImage
    {
        public Size Size { get; set; }
        public readonly string Text;
        public Color Color { get; set; }
        private FontStyle style = FontStyle.Regular;
        public float FontSize { private get; set; }
        public string FontType { private get; set; }

        public Font Font => new Font(FontType, FontSize, style);

        public TextImage(string text)
        {
            Text = text;
        }
    }
}
