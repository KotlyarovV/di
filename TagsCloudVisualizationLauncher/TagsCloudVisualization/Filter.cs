using System.Linq;
using YandexMystem.Wrapper.Enums;


namespace TagsCloudVisualization
{
    public class Filter
    {
        public bool IsNecessaryPartOfSpeech(GramPartsEnum[] excludedGramParts, Word word)
        {
            if (word.InitialForm == null || word.GramPart == null) return false;
            return !excludedGramParts.Contains(word.GramPart.GetValueOrDefault());
        }
    }
}
