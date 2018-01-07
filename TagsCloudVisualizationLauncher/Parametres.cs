using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudVisualizationLauncher
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

        
        public int Width { get; set; }
        public int Height { get; set; }

        public Size Size => new Size(Width, Height);
        public double FontSizeMin { get; set; }
        public double FontSizeMax { get; set; }
        public string FileName { get; set; }
        public string FontName { get; set; }
        public string ImageName { get; set; }

        public Result<ImageFormat> GetImageFormat()
        {
            var format = ImageName
                .Split('.')
                .Last()
                .ToLower();


            return (imageFormats.ContainsKey(format) && ImageName.Contains(".")) ? 
                Result.Ok(imageFormats[format]) : 
                Result.Fail<ImageFormat>("Can't define image format");
        }
    }
}
