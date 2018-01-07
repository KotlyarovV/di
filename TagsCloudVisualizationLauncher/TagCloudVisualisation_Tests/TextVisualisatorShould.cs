using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class TextVisualisatorShould
    {
        private TextVisualisator textVisualisator;

        private readonly Color[] colors =
        {
            Color.Chartreuse,
            Color.Blue, 
            Color.BlueViolet
        };

        private readonly Dictionary<string, double> weights = new Dictionary<string, double>()
        {
            { "один", 0.1 },
            { "два", 0.2 },
            { "три", 0.3 },
            { "четыре", 0.4 }
        };

        [SetUp]
        public void SetUp()
        {
            textVisualisator = new TextVisualisator(colors);
        }

        [Test]
        public void SetFontSizes_CalculateFontSizes()
        {
            
            textVisualisator.CreateTextImages(weights);
            textVisualisator.SetFontSizes(10, 20);
            var expectedFontSizes = new []
            {
                10F,
                (float) (10 * 0.1 / 0.3 + 10),
                (float) (10 * 0.2 / 0.3 + 10),
                10 * 1 + 10
            };

            var fontSizes = textVisualisator
                .GetStringImages()
                .GetValueOrThrow()
                .Select(textImage => textImage.FontSize)
                .ToArray();
            expectedFontSizes.Should().Equal(fontSizes, (size1, size2) => Math.Abs(size1 - size2) < 0.00001);
        }

        [Test]
        public void SetFontType_FontTypeShouldBeSetted()
        {
            textVisualisator.CreateTextImages(weights);
            textVisualisator.SetFontSizes(10, 20);
            textVisualisator.SetFontTipe("Times New Roman");

            textVisualisator
                .GetStringImages()
                .GetValueOrThrow()
                .Should()
                .OnlyContain(x => x.Font.Name == "Times New Roman");
        }

        [Test]
        public void SetColors_DefineColors()
        {

            textVisualisator.CreateTextImages(weights);
            textVisualisator.SetFontSizes(10, 20);
            textVisualisator.SetColors();
            var expectedColors = new []
            {
                Color.Chartreuse,
                Color.Blue,
                Color.BlueViolet,
                Color.Chartreuse,
            };

            var colorsOfText = textVisualisator
                .GetStringImages()
                .GetValueOrThrow()
                .Select(textImage => textImage.Color)
                .ToArray();

            colorsOfText.Should().Equal(expectedColors);
        }

    }
}
