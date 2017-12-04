using System;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    interface ITextVisualisator
    {
        List<Tuple<string, float>> GetFontSizes(Dictionary<string, double> weights, double maxFont, double minFont);

        List<TextImage> GetStringImages(List<Tuple<string, float>> fontSizes, string fontName);
        List<TextImage> SetColors(List<TextImage> textImages);
    }
}
