using System.Collections.Generic;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models; 
using System.Linq;

namespace TagsCloudVisualization
{
    public class Splitter : ISplitter
    {
        private readonly Mysteam mysteam;
        private readonly char[] replacedChars;

        public Splitter(char[] replacedChars, Mysteam mysteam)
        {
            this.replacedChars = replacedChars;
            this.mysteam = mysteam;
        }

        public IEnumerable<WordModel> Split(string text)
        {
            foreach (var replacedChar in replacedChars)
            {
                text = text.Replace(replacedChar, ' ');
            }
            return mysteam.GetWords(text);
        }
    }
}
