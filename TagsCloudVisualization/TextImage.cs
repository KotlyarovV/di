using System.Drawing;

namespace TagsCloudVisualization
{
    class TextImage
    {
        public readonly Size Size;
        public readonly string Text;
        public Color Color;
        public readonly Font Font;

        public TextImage(Size size, string text, Color color, Font font)
        {
            Size = size;
            Text = text;
            Color = color;
            Font = font;
        }
    }
}
