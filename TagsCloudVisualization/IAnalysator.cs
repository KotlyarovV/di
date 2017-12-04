using System.Collections.Generic;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudVisualization
{
    interface IAnalysator
    {
        void AddExcludedGramPart(GramPartsEnum gramParts);
        List<string> PrepareWords(string text);
        Dictionary<string, int> GetFrequencies(List<string> words);
        Dictionary<string, double> GetWeights(Dictionary<string, int> frequencies);
    }
}
