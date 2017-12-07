using System.Collections.Generic;

namespace TagsCloudVisualization
{
    interface IAnalysator
    {
        Dictionary<string, double> GetWeights(IReadOnlyCollection<string> words);
    }
}
