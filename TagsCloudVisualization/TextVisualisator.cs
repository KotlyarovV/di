using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class TextVisualisator : ITextVisualisator
    {

        public List<Tuple<string, float>> GetFontSizes(
            Dictionary<string, double> weights,
            double maxFont,
            double minFont)
        {
            var result = new List<Tuple<string, float>>();
            double maxWeight = weights.Values.Max();
            double minWeight = weights.Values.Min();
            foreach (var weight in weights)
            {
                double fontSize = (weight.Value > minWeight)
                    ? maxFont * (weight.Value - minWeight) / (maxWeight - minWeight) + minFont
                    : minFont;
                result.Add(Tuple.Create(weight.Key, (float) fontSize));
            }
            return result;
        }

        public List<TextImage> SetColors(List<TextImage> textImages)
        {
            var colors = new[] { Color.Blue, Color.Brown, Color.Coral, Color.DarkGreen, };
            for (var i = 0; i < textImages.Count; i++)
            {
                textImages[i].Color = colors[i % 4];
            }
            return textImages;
        }


        public List<TextImage> GetStringImages(
            List<Tuple<string, float>> fontSizes, string fontTipe)
        {
            var result = new List<TextImage>();
            var proposedSize = new Size(int.MaxValue, int.MaxValue);
            var flags = TextFormatFlags.NoPadding;
            
            for (int i = 0; i < fontSizes.Count; i++)
            {
                Font font = new Font(fontTipe, fontSizes[i].Item2, FontStyle.Regular);
                Size size = TextRenderer.MeasureText(fontSizes[i].Item1,
                    font, proposedSize, flags);
                var textImage = new TextImage(size, fontSizes[i].Item1, Color.Black, font);
                result.Add(textImage);
            }

            return result;
        }
    }
}
