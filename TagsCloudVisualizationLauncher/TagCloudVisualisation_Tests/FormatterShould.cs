using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using YandexMystem.Wrapper.Enums;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class FormatterShould
    {
        private Formatter formatter;

        [SetUp]
        public void SetUp()
        {
            formatter = new Formatter();
        }

        [Test]
        public void FormatWords_FormatToInitialForm()
        {
            var words = new[]
            {
                new Word("человека", "человек", GramPartsEnum.Noun),
                new Word("большого", "большой", GramPartsEnum.Adjective),
                new Word("ходили", "ходить", GramPartsEnum.Verb)
            };

            var expectedFilteredWords = new[]
            {
                "человек", "большой", "ходить"
            };
            formatter.FormatWords(words).Should().Equal(expectedFilteredWords);
        }

        [Test]
        public void FormatWords_FormatToLowerCase()
        {
            var words = new[]
            {
                new Word("ЧеЛовеК", "человек", GramPartsEnum.Noun),
                new Word("БольШой", "большой", GramPartsEnum.Adjective),
                new Word("ХОДИть", "ходить", GramPartsEnum.Verb)
            };

            var expectedFilteredWords = new[]
            {
                "человек", "большой", "ходить"
            };
            formatter.FormatWords(words).Should().Equal(expectedFilteredWords);
        }

    }
}
