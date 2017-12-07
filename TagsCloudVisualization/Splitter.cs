using System.Collections.Generic;
using Fclp.Internals.Extensions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    class Splitter : ISplitter
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
            replacedChars.ForEach((c) =>
            {
                text = text.Replace(c, ' ');
            });
            return mysteam.GetWords(text);
        }
    }
}
