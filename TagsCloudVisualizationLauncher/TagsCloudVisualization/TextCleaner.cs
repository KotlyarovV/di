using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class TextCleaner : ITextCleaner
    {
        private readonly char[] replacedChars;

        public TextCleaner(char[] replacedChars)
        {
            this.replacedChars = replacedChars;
        }

        public string RemoveSigns(string text)
        {
            foreach (var replacedChar in replacedChars)
            {
                text = text.Replace(replacedChar, ' ');
            }
            return text;
        }
    }
}
