using System.Collections;
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

        [TestCaseSource(typeof(DataClass), nameof(DataClass.FormatToInitialForm))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.FormatToLowerCase))]
        public string FormatWordsTest(Word word)
        {
            return formatter.GetOriginal(word);
        }

        private class DataClass
        {
            public static IEnumerable FormatToLowerCase()
            {
                yield return new TestCaseData(new Word("Человек", "человек", GramPartsEnum.Noun))
                    .Returns("человек")
                    .SetName("Starts_From_Upper_Case");

                yield return new TestCaseData(new Word("большоЙ", "большой", GramPartsEnum.Adjective))
                    .Returns("большой")
                    .SetName("Ends_With_Upper_Case");

                yield return new TestCaseData(new Word("ХОДИТЬ", "ходить", GramPartsEnum.Verb))
                    .Returns("ходить")
                    .SetName("All_Is_UpperCase");
            }

            public static IEnumerable FormatToInitialForm()
            {
                yield return new TestCaseData(new Word("человека", "человек", GramPartsEnum.Noun))
                    .Returns("человек")
                    .SetName("Noun_To_Initial_Form");

                yield return new TestCaseData(new Word("большого", "большой", GramPartsEnum.Adjective))
                    .Returns("большой")
                    .SetName("Adjective_To_Initial_Form");

                yield return new TestCaseData(new Word("ходил", "ходить", GramPartsEnum.Verb))
                    .Returns("ходить")
                    .SetName("Verb_To_Initial_Form");
            }
        }
    }
    
}
