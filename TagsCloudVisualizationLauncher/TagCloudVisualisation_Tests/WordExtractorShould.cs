using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using YandexMystem.Wrapper.Enums;

namespace TagCloudVisualisation_Tests
{
    class WordExtractorShould
    {
        private WordExtractor wordExtractor;

        [SetUp]
        public void SetUp()
        {
            wordExtractor = new WordExtractor();
        }

        private void CheckWords(string words, IEnumerable<Word> expectedExtractedWords)
        {
            var exctractedWords = wordExtractor.ExtractWords(words);
            exctractedWords
                .GetValueOrThrow()
                .Should()
                .Equal(expectedExtractedWords);
        }

        [Test]
        public void ExtractWords_GetSimpleWords()
        {
            var words = "Один два три четыре снег он";
            var expectedExtractedWords = new[]
            {
                new Word("Один", "один", GramPartsEnum.Numeral),
                new Word("два", "два", GramPartsEnum.Numeral),
                new Word("три", "три", GramPartsEnum.Numeral),
                new Word("четыре", "четыре", GramPartsEnum.Numeral),
                new Word("снег", "снег", GramPartsEnum.Noun),
                new Word("он", "он", GramPartsEnum.NounPronoun),
            };
            CheckWords(words, expectedExtractedWords);           
        }

        [Test]
        public void ExtractWords_GetUnmeaningWords()
        {
            var words = "ааыы шгрхф эьыы";
            var expectedExtractedWords = new[]
            {
                new Word("ааыы"),
                new Word("шгрхф"),
                new Word("эьыы"),
            };
            CheckWords(words, expectedExtractedWords);
        }
    }
}

