using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TextVisualisator : ITextVisualisator
    {
        private List<TextImage> textImages;
        private Dictionary<string, double> weights;
        private readonly Color[] colors;

        public TextVisualisator(Color[] colors)
        {
            this.colors = colors;
            weights = new Dictionary<string, double>();
            textImages = new List<TextImage>();
        }

        public ITextVisualisator CreateTextImages(Dictionary<string, double> weights)
        {
            this.weights = weights;
            textImages = new List<TextImage>();
            foreach (var weight in weights)
            {
                textImages.Add(new TextImage(weight.Key));
            }
            return this;
        }


        public ITextVisualisator SetFontSizes(double maxFont, double minFont)
        {
            if (weights.Count == 0) return this;

            var maxWeight = weights.Values.Max();
            var minWeight = weights.Values.Min();

            foreach (var textImage in textImages)
            {
                var fontSize = (weights[textImage.Text] > minWeight)
                    ? maxFont * (weights[textImage.Text] - minWeight) / (maxWeight - minWeight) + minFont
                    : minFont;
                textImage.FontSize = (float) fontSize;
            }
            return this;
        }

        public ITextVisualisator SetFontTipe(string fontType = "Arial")
        {
            foreach (var textImage in textImages)
            {
                textImage.FontType = fontType;
            }
            return this;
        }
        
        public ITextVisualisator SetColors()
        {
            for (var i = 0; i < textImages.Count; i++)
            {
                textImages[i].Color = colors[i % colors.Length];
            }
            return this;
        }


        public List<TextImage> GetStringImages()
        {
            var proposedSize = new Size(int.MaxValue, int.MaxValue);
            var flags = TextFormatFlags.NoPadding;
            foreach (var textImage in textImages)
            {
                var size = TextRenderer.MeasureText(textImage.Text,
                    textImage.Font, proposedSize, flags);
                textImage.Size = size;
            }
            return textImages;
        }
    }
}
