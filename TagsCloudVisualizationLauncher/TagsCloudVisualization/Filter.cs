using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper.Enums;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public class Filter : IFilter
    {
        private readonly GramPartsEnum[] excludedGramParts;

        public Filter(GramPartsEnum[] excludedGramParts)
        {
            this.excludedGramParts = excludedGramParts;
        }

        private bool IsRightWord(Word word)
        {
            if (word.InitialForm == null || word.GramPart == null) return false;
            return !excludedGramParts.Contains(word.GramPart.GetValueOrDefault());
        }

        public IEnumerable<Word> FilterWords(IEnumerable<Word> words)
        {
            return words.Where(IsRightWord);
        }
    }
}
