using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Fclp.Internals.Extensions;

namespace TagsCloudVisualization
{
    class TextVisualisator : ITextVisualisator
    {
        private List<TextImage> textImages;
        private Dictionary<string, double> weights;
        private readonly Color[] colors;

        public TextVisualisator(Color[] colors)
        {
            this.colors = colors;
        }

        public void CreateTextImages(Dictionary<string, double> weights)
        {
            this.weights = weights;
            textImages = new List<TextImage>(); 
            weights.ForEach((x) => textImages.Add(new TextImage(x.Key)));
        }


        public void SetFontSizes(double maxFont, double minFont)
        {
            var maxWeight = weights.Values.Max();
            var minWeight = weights.Values.Min();

            foreach (var textImage in textImages)
            {
                var fontSize = (weights[textImage.Text] > minWeight)
                    ? maxFont * (weights[textImage.Text] - minWeight) / (maxWeight - minWeight) + minFont
                    : minFont;
                textImage.FontSize = (float) fontSize;
            }
        }

        public void SetFontTipe(string fontType = "Arial")
        {
            textImages.ForEach(textImage => textImage.FontType = fontType);
        }
        
        public void SetColors()
        {
            for (var i = 0; i < textImages.Count; i++)
            {
                textImages[i].Color = colors[i % colors.Length];
            }
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
