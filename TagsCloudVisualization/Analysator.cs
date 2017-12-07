using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{    
    class Analysator : IAnalysator
    {       
        private Dictionary<string, int> GetFrequencies(IReadOnlyCollection<string> words)
        {
            return words
                .GroupBy(word => word, (word, wordsSame) => new {Key = word, Count = wordsSame.Count()})
                .ToDictionary(x => x.Key, x => x.Count);
        }

        public Dictionary<string, double> GetWeights(IReadOnlyCollection<string> words)
        {
            var frequencies = GetFrequencies(words);
            return frequencies
                .ToDictionary(pair => pair.Key, pair => pair.Value / (double) frequencies.Count);
        }
    }    
}
