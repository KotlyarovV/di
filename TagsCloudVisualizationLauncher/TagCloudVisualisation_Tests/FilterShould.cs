using System.Collections;
using System.Linq;
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
            filter = new Filter();
        }

        [TestCaseSource(typeof(DataClass), nameof(DataClass.DefineNotNeededGramParts))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.NotExcludeSimpleWords))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.ExcludeUnmeaning))]
        public bool FilterWordsTest(Word word)
        {
            return filter.IsNecessaryPartOfSpeech(excludedGramPart, word);
        }

        private class DataClass
        {
            public static IEnumerable DefineNotNeededGramParts()
            {
                yield return new TestCaseData(new Word("и", "и", GramPartsEnum.Conjunction))
                    .Returns(false)
                    .SetName("Exclude_Conjuction");

                yield return new TestCaseData(new Word("он", "он", GramPartsEnum.NounPronoun))
                    .Returns(false)
                    .SetName("Exclude_NounProNoun");

                yield return new TestCaseData(new Word("в", "в", GramPartsEnum.Pretext))
                    .Returns(false)
                    .SetName("Exclude_Pretext");
            }

            public static IEnumerable ExcludeUnmeaning()
            {
                yield return new TestCaseData(new Word("ааыы"))
                    .Returns(false)
                    .SetName("Exclude_Unmeaning_Word");
            }

            public static IEnumerable NotExcludeSimpleWords()
            {
                yield return new TestCaseData(new Word("один", "один", GramPartsEnum.Numeral))
                    .Returns(true)
                    .SetName("Not_Exclude_Numeral");

                yield return new TestCaseData(new Word("снег", "снег", GramPartsEnum.Noun))
                    .Returns(true)
                    .SetName("Not_Exclude_Noun");

                yield return new TestCaseData(new Word("большой", "большой", GramPartsEnum.Adjective))
                    .Returns(true)
                    .SetName("Not_Exclude_Adjective");
            }
        }

    }

    
}
