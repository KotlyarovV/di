using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    interface IFormatter
    {
        IEnumerable<string> FormatWords(IEnumerable<WordModel> words);
    }
}
