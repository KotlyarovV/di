using System.Drawing;

namespace TagsCloudVisualization
{
    public class Parameters
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size Size => new Size(Width, Height);
        public double FontSizeMin { get; set; }
        public double FontSizeMax { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
        public string FontName { get; set; }
        public string ImageName { get; set; }
    }
}
