using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Formatter : IFormatter
    {
        
        public IEnumerable<string> FormatWords(IEnumerable<Word> words)
        {
            return words
                .Select(word => word.InitialForm)
                .Select(word => word.ToLower());
        }
    }
}
