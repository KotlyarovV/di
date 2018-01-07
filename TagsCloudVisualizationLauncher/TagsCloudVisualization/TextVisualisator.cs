using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TextVisualisator : ITextVisualisator
    {
        private TextImage[] textImages;
        private Dictionary<string, double> weights;
        private readonly Color[] colors;

        public TextVisualisator(Color[] colors)
        {
            this.colors = colors;
        }

        public Result<ITextVisualisator> CreateTextImages(Dictionary<string, double> weights)
        {
            this.weights = weights;
            textImages = new TextImage[weights.Count];
            var i = 0;
            foreach (var weight in weights)
            {
                textImages[i] = new TextImage(weight.Key);
                i++;
            }
            return Result.Ok((ITextVisualisator) this);
        }


        public Result<ITextVisualisator> SetFontSizes(double minFont, double maxFont)
        {
            if (textImages.Length == 0)
                return Result.Fail<ITextVisualisator>("There was no words.");

            if (minFont <= 0 || maxFont <= 0)
                return Result.Fail<ITextVisualisator>("Font size can't be less then zero");

            var maxWeight = weights.Values.Max();
            var minWeight = weights.Values.Min();

            foreach (var textImage in textImages)
            {
                var fontSize = (weights[textImage.Text] > minWeight)
                    ? (maxFont - minFont) * (weights[textImage.Text] - minWeight) / (maxWeight - minWeight) + minFont
                    : minFont;
                textImage.FontSize = (float) fontSize;
            }
            return Result.Ok((ITextVisualisator) this);
        }


        public Result<ITextVisualisator> SetFontTipe(string fontType = "Arial")
        {
            foreach (var textImage in textImages)
            {
                var result = Result.Of(() => 
                        textImage.Font = new Font(fontType, textImage.FontSize, textImage.Style));

                if (!result.IsSuccess)
                    return Result.Fail<ITextVisualisator>("Can't set this font!");
            }
            return Result.Ok((ITextVisualisator)this);
        }
        
        public Result<ITextVisualisator> SetColors()
        {
            for (var i = 0; i < textImages.Length; i++)
            {
                textImages[i].Color = colors[i % colors.Length];
            }
            return Result.Ok((ITextVisualisator) this);
        }


        public Result<TextImage[]> GetStringImages()
        {
            var proposedSize = new Size(int.MaxValue, int.MaxValue);
            var flags = TextFormatFlags.NoPadding;
            foreach (var textImage in textImages)
            {
                var size = TextRenderer.MeasureText(textImage.Text,
                    textImage.Font, proposedSize, flags);
                textImage.Size = size;
            }
            return Result.Ok(textImages.ToArray());
        }        
    }
}
