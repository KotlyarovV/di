using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models; 

namespace TagsCloudVisualization
{
    public class WordExtractor : Mysteam, IWordExtractor
    {
        public IEnumerable<Word> ExtractWords(string text)
        {
            return GetWords(text)
                .Select(wordModel => new Word(wordModel));
        }
    }
}
