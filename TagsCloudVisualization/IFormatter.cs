using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public interface IFormatter
    {
        IEnumerable<string> FormatWords(IEnumerable<WordModel> words);
    }
}
