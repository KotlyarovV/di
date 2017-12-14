using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public interface IWordExtractor
    {
        IEnumerable<Word> ExtractWords(string text);
    }
}
