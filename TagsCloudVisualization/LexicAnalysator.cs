using System.Collections.Generic;
using System.Linq;
using Fclp.Internals.Extensions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    class LexicAnalysator : IAnalysator
    {
        private readonly Mysteam mysteam;

        public LexicAnalysator(Mysteam mysteam)
        {
            this.mysteam = mysteam;
        }

        private readonly List<GramPartsEnum> excludedGramParts = new List<GramPartsEnum>()
        {
            GramPartsEnum.Conjunction,
            GramPartsEnum.NounPronoun,
            GramPartsEnum.Pretext,
            GramPartsEnum.Part,
            GramPartsEnum.PronounAdjective
        };

        public void AddExcludedGramPart(GramPartsEnum gramParts) => excludedGramParts.Add(gramParts);
        
        private bool IsRightWord(WordModel wordModel)
        {
            if (wordModel.Lexems.Count == 0) return false;
            var wordInformation = wordModel.Lexems.First();
            return !excludedGramParts.Contains(wordInformation.GramPart);
        }

        private string GetInitialWordForm(WordModel word) => word.Lexems.First().Lexeme;
        
        public List<string> PrepareWords(string text)
        {
            var chars = new [] {'!', ',', '"', '\r', '\n'};
            chars.ForEach((c) => text = text.Replace(c, ' '));

            var words = mysteam.GetWords(text);
            return words
                .Where(IsRightWord)
                .Select(GetInitialWordForm)
                .Select(word => word.ToLower())
                .ToList();  
        }
        
        public Dictionary<string, int> GetFrequencies(List<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                {
                    frequencies[word] = 1;
                }
                else
                {
                    frequencies[word]++;
                }
            }
            return frequencies;
        }

        public Dictionary<string, double> GetWeights(Dictionary<string, int> frequencies)
        {
            var weights = new Dictionary<string, double>();
            var numberOfWords = frequencies.Count;
            frequencies.ForEach((pair) => weights.Add(pair.Key, pair.Value / (double)numberOfWords));
            
            return weights;
        }
    }
}
