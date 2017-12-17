using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class AnalysatorShould
    {
        private Analysator analysator;

        [SetUp]
        public void SetUp()
        {
            analysator = new Analysator();
        }

        [Test]
        public void GetWeights_GetSameWeights()
        {
            var words = new []
            {
                "один",
                "два",
                "три",
                "четыре",
                "пять",
                "шесть",
                "семь",
                "восемь",
                "девять",
                "десять"
            };

            var expectedWeights = new Dictionary<string, double>()
            {
                {"один", 0.1},
                {"два", 0.1},
                {"три", 0.1},
                {"четыре", 0.1},
                {"пять", 0.1},
                {"шесть", 0.1},
                {"семь", 0.1},
                {"восемь", 0.1},
                {"девять", 0.1},
                {"десять", 0.1}
            };

            analysator.GetWeights(words).Should().Equal(expectedWeights);
        }

        [Test]
        public void GetWeights_GetDifferentWeights()
        {
            var words = new[]
            {
                "один",
                "один",
                "один",
                "четыре",
                "четыре",
                "четыре",
                "семь",
                "семь",
                "девять",
                "десять"
            };

            var expectedWeights = new Dictionary<string, double>()
            {
                {"один", 0.3},
                {"четыре", 0.3},
                {"семь", 0.2},
                {"девять", 0.1},
                {"десять", 0.1}
            };

            analysator.GetWeights(words).Should().Equal(expectedWeights);
        }

        [Test]
        public void GetWeights_GetWeightOfRepeatedWords()
        {
            var words = new[]
            {
                "один",
                "один",
                "один",
                "один",
            };

            var expectedWeights = new Dictionary<string, double>()
            {
                {"один", 1},
            };

            analysator.GetWeights(words).Should().Equal(expectedWeights);
        }

        [Test]
        public void GetWeights_GetWeightOfOneWords()
        {
            var words = new[]
            {
                "один"
            };

            var expectedWeights = new Dictionary<string, double>()
            {
                {"один", 1},
            };

            analysator.GetWeights(words).Should().Equal(expectedWeights);
        }
    }
}
