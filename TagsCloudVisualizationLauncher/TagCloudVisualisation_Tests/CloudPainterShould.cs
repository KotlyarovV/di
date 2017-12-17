using System.Collections.Generic;
using System.Drawing;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;
using YandexMystem.Wrapper.Enums;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class CloudPainterShould
    {
        private CloudPainter cloudPainter;
        private Mock<ICloudLayouter> cloudLayouterMock;
        private Mock<IFormatter> formatterMock;
        private Mock<IWordExtractor> wordExtractorMock;
        private Mock<IFilter> filterMock;
        private Mock<IAnalysator> analysatorMock;
        private Mock<ITextVisualisator> textVisualisatorMock;
        private Mock<ITextCleaner> textCleanerMock;

        [SetUp]
        public void SetUp()
        {
            wordExtractorMock = new Mock<IWordExtractor>();
            filterMock = new Mock<IFilter>();
            formatterMock = new Mock<IFormatter>();
            analysatorMock = new Mock<IAnalysator>();
            textVisualisatorMock = new Mock<ITextVisualisator>();
            cloudLayouterMock = new Mock<ICloudLayouter>();
            textCleanerMock = new Mock<ITextCleaner>();

            cloudPainter = new CloudPainter(
                wordExtractorMock.Object,
                formatterMock.Object,
                filterMock.Object,
                analysatorMock.Object,
                cloudLayouterMock.Object,
                textVisualisatorMock.Object,
                textCleanerMock.Object
            );
        }

        private void GetMockedBitmap()
        {
            var text = "один, два, три";
            var stringWords = new[] {"один", "два", "три"};
            var wordsEnumerator = stringWords.GetEnumerator();

            var words = new[]
            {
                new Word("один", "один", GramPartsEnum.Numeral),
                new Word("два", "два", GramPartsEnum.Numeral),
                new Word("три", "три", GramPartsEnum.Numeral),
            };

            textCleanerMock
                .Setup(cleaner => cleaner.RemoveSigns(It.IsAny<string>()))
                .Returns("один  два  три");

            wordExtractorMock
                .Setup(wordExtractor => wordExtractor.ExtractWords(It.IsAny<string>()))
                .Returns(words);
            
            filterMock
                .Setup(filter => filter.IsNecessaryPartOfSpeech(It.IsAny<Word>()))
                .Returns(true);

            formatterMock
                .Setup(formatter => formatter.GetOriginal(It.IsAny<Word>()))
                .Returns(() =>
                {
                    wordsEnumerator.MoveNext();
                    return wordsEnumerator.Current.ToString();
                });

            analysatorMock.Setup(analysator => analysator.GetWeights(It.IsAny<IReadOnlyCollection<string>>()));

            textVisualisatorMock
                .Setup(textVisualisator =>
                    textVisualisator.CreateTextImages(It.IsAny<Dictionary<string, double>>()))
                .Returns(textVisualisatorMock.Object);

            textVisualisatorMock
                .Setup(textVisualisator => textVisualisator.SetColors())
                .Returns(textVisualisatorMock.Object);

            textVisualisatorMock
                .Setup(textVisualisator => textVisualisator.SetFontSizes(
                    It.IsAny<double>(),
                    It.IsAny<double>())
                )
                .Returns(textVisualisatorMock.Object);

            textVisualisatorMock
                .Setup(textVisualisator => textVisualisator.SetFontTipe(It.IsAny<string>()))
                .Returns(textVisualisatorMock.Object);

            textVisualisatorMock
                .Setup(textVisualisator => textVisualisator.GetStringImages())
                .Returns(new List<TextImage>(new []
                {
                    new TextImage("один")
                    {
                        Color = Color.Black,
                        FontSize = 10,
                        FontType = "Arial",
                        Size = new Size(30, 10)
                    },

                    new TextImage("два")
                    {
                        Color = Color.Black,
                        FontSize = 10,
                        FontType = "Arial",
                        Size = new Size(30, 10)
                    },

                    new TextImage("три")
                    {
                        Color = Color.Black,
                        FontSize = 10,
                        FontType = "Arial",
                        Size = new Size(30, 10)
                    }
                }));


            cloudLayouterMock.Setup(cloudLayouter => cloudLayouter.PutNextRectangle(It.IsAny<Size>()));
            cloudPainter.GetBitmap(text);
        }

        [Test]
        public void GetBitmap_SplitterCalled()
        {
            GetMockedBitmap();
            wordExtractorMock
                .Verify(wordExtractor => wordExtractor.ExtractWords(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetBitmap_FilterCalled()
        {
            GetMockedBitmap();
            filterMock.Verify(filter => filter.IsNecessaryPartOfSpeech(It.IsAny<Word>()), Times.AtLeastOnce);
        }

        [Test]
        public void GetBitmap_FormatterCalled()
        {
            GetMockedBitmap();
            formatterMock
                .Verify(formatter => formatter.GetOriginal(It.IsAny<Word>()), Times.AtLeastOnce);
        }

        [Test]
        public void GetBitmap_AnalysatorCalled()
        {
            GetMockedBitmap();
            analysatorMock
                .Verify(analysator => analysator.GetWeights(It.IsAny<IReadOnlyCollection<string>>()), Times.Once);
        }

        [Test]
        public void GetBitmap_VisualisatorCalled()
        {
            GetMockedBitmap();
            textVisualisatorMock.VerifyAll();
        }

        [Test]
        public void GetBitmap_CircularCloudCalled()
        {
            GetMockedBitmap();
            cloudLayouterMock
                .Verify(cloudLayouter => cloudLayouter.PutNextRectangle(It.IsAny<Size>()), Times.AtLeastOnce);
        }
    }
}
