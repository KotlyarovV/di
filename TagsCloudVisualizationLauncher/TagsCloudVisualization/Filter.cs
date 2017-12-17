using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper.Enums;


namespace TagsCloudVisualization
{
    public class Filter : IFilter
    {
        private readonly GramPartsEnum[] excludedGramParts;

        public Filter(GramPartsEnum[] excludedGramParts)
        {
            this.excludedGramParts = excludedGramParts;
        }

        public bool IsNecessaryPartOfSpeech(Word word)
        {
            if (word.InitialForm == null || word.GramPart == null) return false;
            return !excludedGramParts.Contains(word.GramPart.GetValueOrDefault());
        }
    }
}
