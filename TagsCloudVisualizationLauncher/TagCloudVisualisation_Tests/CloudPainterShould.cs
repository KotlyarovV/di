using System.Collections.Generic;
using System.Drawing;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;

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
            var words = new[] {"один", "два", "три"};

            textCleanerMock
                .Setup(cleaner => cleaner.RemoveSigns(It.IsAny<string>()))
                .Returns("один  два  три");

            wordExtractorMock.Setup(wordExtractor => wordExtractor.ExtractWords(It.IsAny<string>()));
            filterMock.Setup(filter => filter.FilterWords(It.IsAny<IEnumerable<Word>>()));
            formatterMock
                .Setup(formatter => formatter.FormatWords(It.IsAny<IEnumerable<Word>>()))
                .Returns(words);

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
            filterMock.Verify(filter => filter.FilterWords(It.IsAny<IEnumerable<Word>>()));
        }

        [Test]
        public void GetBitmap_FormatterCalled()
        {
            GetMockedBitmap();
            formatterMock
                .Verify(formatter => formatter.FormatWords(It.IsAny<IEnumerable<Word>>()), Times.Once);
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
