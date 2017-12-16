using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudPainter : ICloudPainter
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IAnalysator lexicAnalysator;
        private readonly ITextVisualisator textVisualisator;
        private readonly IWordExtractor wordExtractor;
        private readonly IFormatter formatter;
        private readonly IFilter filter;
        private readonly ITextCleaner textCleaner;

        public CloudPainter(
            IWordExtractor wordExtractor,
            IFormatter formatter,
            IFilter filter,
            IAnalysator lexicAnalysator,
            ICloudLayouter cloudLayouter,
            ITextVisualisator textVisualisator,
            ITextCleaner textCleaner
            )
        {
            this.cloudLayouter = cloudLayouter;
            this.lexicAnalysator = lexicAnalysator;
            this.textVisualisator = textVisualisator;
            this.wordExtractor = wordExtractor;
            this.formatter = formatter;
            this.filter = filter;
            this.textCleaner = textCleaner;
        }

        private IEnumerable<TextImage> GetStringImages(
            string text, 
            double minFont = 1.0, 
            double maxFont = 10.0, 
            string fontName = "Arial"
            )
        {
            var textWithoutSigns = textCleaner.RemoveSigns(text);
            var words = wordExtractor.ExtractWords(textWithoutSigns);
            var filteredWords = filter.FilterWords(words);

            var formattedWords = formatter
                .FormatWords(filteredWords)
                .ToArray();

            var weights = lexicAnalysator.GetWeights(formattedWords);
            
            var stringImages = textVisualisator
                .CreateTextImages(weights)
                .SetFontSizes(minFont, maxFont)
                .SetColors()
                .SetFontTipe(fontName)
                .GetStringImages();

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
            var textImages = GetStringImages(text, minFont, maxFont, fontName);
            textImages = textImages
                .OrderBy(stringImage => -stringImage.Size.Width * stringImage.Size.Height);
             
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
            foreach (var textImage in textImages)
            {
                var rectangle = cloudLayouter.PutNextRectangle(textImage.Size);
                TextRenderer.DrawText(
                    graphics, 
                    textImage.Text, 
                    textImage.Font,
                    rectangle.Location, 
                    textImage.Color, 
                    flags
                    );
            }

            return bitmap;
        }        
    }
}
