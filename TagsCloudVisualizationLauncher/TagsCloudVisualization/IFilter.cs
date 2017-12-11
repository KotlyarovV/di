using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public interface IFilter
    {
        IEnumerable<WordModel> FilterWords(IEnumerable<WordModel> words);
    }
}
