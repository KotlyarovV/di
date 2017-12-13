using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Parameters
    {
        private static readonly Dictionary<string, ImageFormat> imageFormats = new Dictionary<string, ImageFormat>()
        {
            { "bmp",  ImageFormat.Bmp },
            { "png", ImageFormat.Png },
            { "jpeg", ImageFormat.Jpeg },
            { "gif", ImageFormat.Gif },
            { "exif", ImageFormat.Exif },
            { "emf", ImageFormat.Emf },
            { "icon", ImageFormat.Icon },
            { "wmf", ImageFormat.Wmf }
        };

        private static readonly ImageFormat standartFormat = ImageFormat.Png;

        public int Width { get; set; }
        public int Height { get; set; }

        public Size Size => new Size(Width, Height);
        public double FontSizeMin { get; set; }
        public double FontSizeMax { get; set; }
        public string FileName { get; set; }
        public string FontName { get; set; }
        public string ImageName { get; set; }

        public ImageFormat GetImageFormat()
        {
            var format = ImageName
                .Split('.')
                .Last()
                .ToLower();

            return (imageFormats.ContainsKey(format)) ? imageFormats[format] : standartFormat;
        }
    }
}
