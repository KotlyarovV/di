using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using YandexMystem.Wrapper.Enums;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class FilterShould
    {
        private Filter filter;
        private readonly GramPartsEnum[] excludedGramPart =
        {
            GramPartsEnum.Conjunction,
            GramPartsEnum.NounPronoun,
            GramPartsEnum.Pretext,
            GramPartsEnum.Part,
            GramPartsEnum.PronounAdjective
        };

        [SetUp]
        public void SetUp()
        {
            filter = new Filter(excludedGramPart);
        }


        [Test]
        public void FilterWords_ExcludeByGramParts()
        {
            var words = new[]
            {
                new Word("один", "один", GramPartsEnum.Numeral),
                new Word("и", "и", GramPartsEnum.Conjunction),
                new Word("четыре", "четыре", GramPartsEnum.Numeral),
                new Word("снег", "снег", GramPartsEnum.Noun),
                new Word("он", "он", GramPartsEnum.NounPronoun),
                new Word("в", "в", GramPartsEnum.Pretext),
            };

            var expectedFilteredWords = new[]
            {
                new Word("один", "один", GramPartsEnum.Numeral),
                new Word("четыре", "четыре", GramPartsEnum.Numeral),
                new Word("снег", "снег", GramPartsEnum.Noun),
            };

            filter.FilterWords(words).Should().Equal(expectedFilteredWords);
        }

        [Test]
        public void FilterWords_ExcludeUnmeaning()
        {
            var words = new[]
            {
                new Word("один", "один", GramPartsEnum.Numeral),
                new Word("ааыы"),
                new Word("четыре", "четыре", GramPartsEnum.Numeral),
                new Word("шгрхф"),
                new Word("снег", "снег", GramPartsEnum.Noun),
                new Word("шгрхф")
            };

            var expectedFilteredWords = new[]
            {
                new Word("один", "один", GramPartsEnum.Numeral),
                new Word("четыре", "четыре", GramPartsEnum.Numeral),
                new Word("снег", "снег", GramPartsEnum.Noun),
            };

            filter.FilterWords(words).Should().Equal(expectedFilteredWords);
        }

    }
}
