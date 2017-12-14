using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IFilter
    {
        IEnumerable<Word> FilterWords(IEnumerable<Word> words);
    }
}
