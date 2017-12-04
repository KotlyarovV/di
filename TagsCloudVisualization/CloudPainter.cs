using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class CloudPainter
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IAnalysator lexicAnalysator;
        private readonly ITextVisualisator textVisualisator;
        private readonly IInputer inputer;
        private readonly ISaver saver;

        public CloudPainter(
            IAnalysator lexicAnalysator,
            ICloudLayouter cloudLayouter,
            ITextVisualisator textVisualisator,
            IInputer inputer,
            ISaver saver
        )
        {
            this.cloudLayouter = cloudLayouter;
            this.lexicAnalysator = lexicAnalysator;
            this.textVisualisator = textVisualisator;
            this.inputer = inputer;
            this.saver = saver;
        }

        public List<TextImage> GetStringImagesSorted(
            string text, 
            double minFont = 1.0, 
            double maxFont = 10.0, 
            string fontName = "Arial"
            )
        {
            var filtredWords = lexicAnalysator.PrepareWords(text);
            var frequencies = lexicAnalysator.GetFrequencies(filtredWords);
            var weights = lexicAnalysator.GetWeights(frequencies);

            var stringsWithFontsSizes = textVisualisator.GetFontSizes(weights, maxFont, minFont);
            var stringImages = textVisualisator.GetStringImages(stringsWithFontsSizes, fontName);
            stringImages = textVisualisator.SetColors(stringImages);

            stringImages = stringImages.OrderBy(stringImage => (-stringImage.Size.Width * stringImage.Size.Height)).ToList();
            return stringImages;
        }
        
        public void SetWordsOnGraphic(List<TextImage> textImages, Graphics graphics)
        {
            var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
            foreach (TextImage t in textImages)
            {
                var rectangle = cloudLayouter.PutNextRectangle(t.Size);
                TextRenderer.DrawText(graphics, t.Text, t.Font,
                    rectangle.Location, t.Color, flags);
            }
        }

        public Bitmap GetBitmap(Parameters parameters)
        {
            string text = inputer.GetText();
            var bitmap = new Bitmap(parameters.Width, parameters.Height);
            var graphics = Graphics.FromImage(bitmap);
            List<TextImage> textImages;
            if (parameters.FontName == null)
            {
                textImages = GetStringImagesSorted(text, parameters.FontSizeMin, parameters.FontSizeMax);
            }
            else
            {
                textImages = GetStringImagesSorted(text, parameters.FontSizeMin, parameters.FontSizeMax,
                    parameters.FontName);
            }
            SetWordsOnGraphic(textImages, graphics);
            return bitmap;
        }

        public void SaveBitmap(Bitmap bitmap) => saver.SaveBitmap(bitmap);
    }
}
