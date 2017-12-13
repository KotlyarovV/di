using System.Collections.Generic;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models; 

namespace TagsCloudVisualization
{
    public class Splitter : Mysteam, ISplitter
    {
        public IEnumerable<WordModel> Split(string text)
        {
            return GetWords(text);
        }
    }
}
