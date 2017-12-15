using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextVisualisator
    {
        ITextVisualisator SetFontSizes(double minFont, double maxFont);
        ITextVisualisator CreateTextImages(Dictionary<string, double> weights);
        ITextVisualisator SetFontTipe(string fontType);
        ITextVisualisator SetColors();
        List<TextImage> GetStringImages();
    }
}
