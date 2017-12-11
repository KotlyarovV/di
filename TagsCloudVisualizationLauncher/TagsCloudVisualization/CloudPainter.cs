using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudPainter
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IAnalysator lexicAnalysator;
        private readonly ITextVisualisator textVisualisator;
        private readonly ISplitter splitter;
        private readonly IFormatter formatter;
        private readonly IFilter filter;

        public CloudPainter(
            ISplitter splitter,
            IFormatter formatter,
            IFilter filter,
            IAnalysator lexicAnalysator,
            ICloudLayouter cloudLayouter,
            ITextVisualisator textVisualisator
            )
        {
            this.cloudLayouter = cloudLayouter;
            this.lexicAnalysator = lexicAnalysator;
            this.textVisualisator = textVisualisator;
            this.splitter = splitter;
            this.formatter = formatter;
            this.filter = filter;
        }

        private List<TextImage> GetStringImagesSorted(
            string text, 
            double minFont = 1.0, 
            double maxFont = 10.0, 
            string fontName = "Arial"
            )
        {
            var words = splitter.Split(text);
            var filteredWords = filter.FilterWords(words);
            var formattedWords = formatter.FormatWords(filteredWords).ToList().AsReadOnly();
            var weights = lexicAnalysator.GetWeights(formattedWords);

            textVisualisator.CreateTextImages(weights);
            textVisualisator.SetFontSizes(maxFont, minFont);
            textVisualisator.SetColors();
            textVisualisator.SetFontTipe(fontName);

            var stringImages = textVisualisator.GetStringImages();

            stringImages = stringImages.OrderBy(stringImage => - stringImage.Size.Width * stringImage.Size.Height).ToList();
            return stringImages;
        }
        
        public Bitmap GetBitmap(
            string text, 
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
            )
        {
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            var textImages = GetStringImagesSorted(text, minFont, maxFont, fontName);

            var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
            foreach (var t in textImages)
            {
                var rectangle = cloudLayouter.PutNextRectangle(t.Size);
                TextRenderer.DrawText(graphics, t.Text, t.Font,
                    rectangle.Location, t.Color, flags);
            }

            return bitmap;
        }        
    }
}
