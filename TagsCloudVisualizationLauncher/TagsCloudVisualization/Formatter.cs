using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public class Formatter : IFormatter
    {
        private string GetInitialWordForm(WordModel word) => word.Lexems.First().Lexeme;

        public IEnumerable<string> FormatWords(IEnumerable<WordModel> words)
        {
            return words
                .Select(GetInitialWordForm)
                .Select(word => word.ToLower());
        }
    }
}
