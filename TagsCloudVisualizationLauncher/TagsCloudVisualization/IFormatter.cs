using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IFormatter
    {
        IEnumerable<string> FormatWords(IEnumerable<Word> words);
    }
}
