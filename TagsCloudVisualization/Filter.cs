using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper.Enums;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    class Filter : IFilter
    {
        private readonly GramPartsEnum[] excludedGramParts;

        public Filter(GramPartsEnum[] excludedGramParts)
        {
            this.excludedGramParts = excludedGramParts;
        }

        private bool IsRightWord(WordModel wordModel)
        {
            if (wordModel.Lexems.Count == 0) return false;
            var wordInformation = wordModel.Lexems.First();
            return !excludedGramParts.Contains(wordInformation.GramPart);
        }

        public IEnumerable<WordModel> FilterWords(IEnumerable<WordModel> words)
        {
            return words.Where(IsRightWord);
        }
    }
}
