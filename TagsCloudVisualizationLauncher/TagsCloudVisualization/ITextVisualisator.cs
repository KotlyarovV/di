using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextVisualisator
    {
        void SetFontSizes(double maxFont, double minFont);
        void CreateTextImages(Dictionary<string, double> weights);
        void SetFontTipe(string fontType);
        void SetColors();
        List<TextImage> GetStringImages();
    }
}
