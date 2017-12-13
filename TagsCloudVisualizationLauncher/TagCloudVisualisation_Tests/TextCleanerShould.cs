using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class TextCleanerShould
    {
        private TextCleaner textCleaner;
        private readonly char[] extendedChars = {',', '.', ':', '!', '\n', '\t', '?'};

        [SetUp]
        public void SetUp()
        {
            textCleaner = new TextCleaner(extendedChars);
        }

        [TestCase("Один, два, три\t", "Один  два  три ", TestName = "Commas and tabulation symbol")]
        [TestCase("!Один ,два ..три\n", " Один  два   три ", TestName = "Signs and new line")]
        [TestCase("!Один ,два :.три?", " Один  два   три ", TestName = "All signs in text")]
        public void CleanString_DeleteSignst(string text, string result)
        {
            textCleaner.RemoveSigns(text).Should().Be(result);
        }

    }
}
