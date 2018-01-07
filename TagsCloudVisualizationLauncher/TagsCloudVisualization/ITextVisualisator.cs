using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextVisualisator
    {
        Result<ITextVisualisator> SetFontSizes(double minFont, double maxFont);
        Result<ITextVisualisator> CreateTextImages(Dictionary<string, double> weights);
        Result<ITextVisualisator> SetFontTipe(string fontType);
        Result<ITextVisualisator> SetColors();
        Result<TextImage[]> GetStringImages();
    }
}
