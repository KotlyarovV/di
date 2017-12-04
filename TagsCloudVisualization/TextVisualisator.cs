using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            var maxWeight = weights.Values.Max();
            var minWeight = weights.Values.Min();
            foreach (var weight in weights)
            {
                var fontSize = (weight.Value > minWeight)
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

            foreach (var fontSize in fontSizes)
            {
                var font = new Font(fontTipe, fontSize.Item2, FontStyle.Regular);
                var size = TextRenderer.MeasureText(fontSize.Item1,
                    font, proposedSize, flags);
                var textImage = new TextImage(size, fontSize.Item1, Color.Black, font);
                result.Add(textImage);
            }

            return result;
        }
    }
}
