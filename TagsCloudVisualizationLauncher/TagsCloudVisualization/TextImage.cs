using System.Drawing;

namespace TagsCloudVisualization
{
    public class TextImage
    {
        public Size Size { get; set; }
        public readonly string Text;
        public Color Color { get; set; }
        public FontStyle Style { get; } = FontStyle.Regular;
        public float FontSize { get; set; }

        public Font Font { get; set; }
        public TextImage(string text) => Text = text;   
    }
}
