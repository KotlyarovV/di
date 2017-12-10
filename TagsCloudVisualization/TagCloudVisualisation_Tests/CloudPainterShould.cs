using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;
using YandexMystem.Wrapper.Models;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class CloudPainterShould
    {
        private CloudPainter cloudPainter;
        private Mock<ICloudLayouter> cloudLayouterMock;
        private Mock<IFormatter> formatterMock;
        private Mock<ISplitter> splitterMock;
        private Mock<IFilter> filterMock;
        private Mock<IAnalysator> analysatorMock;
        private Mock<ITextVisualisator> textVisualisatorMock;

        [SetUp]
        public void SetUp()
        {
            splitterMock = new Mock<ISplitter>();
            filterMock = new Mock<IFilter>();
            formatterMock = new Mock<IFormatter>();
            analysatorMock = new Mock<IAnalysator>();
            textVisualisatorMock = new Mock<ITextVisualisator>();
            cloudLayouterMock = new Mock<ICloudLayouter>();

            cloudPainter = new CloudPainter(
                splitterMock.Object,
                formatterMock.Object,
                filterMock.Object,
                analysatorMock.Object,
                cloudLayouterMock.Object,
                textVisualisatorMock.Object
            );
        }

        private void GetMockedBitmap()
        {
            var text = "один два три";
            var words = new[] {"один", "два", "три"};

            splitterMock.Setup(splitter => splitter.Split(It.IsAny<string>()));
            filterMock.Setup(filter => filter.FilterWords(It.IsAny<IEnumerable<WordModel>>()));
            formatterMock
                .Setup(formatter => formatter.FormatWords(It.IsAny<IEnumerable<WordModel>>()))
                .Returns(words);

            analysatorMock.Setup(analysator => analysator.GetWeights(It.IsAny<IReadOnlyCollection<string>>()));

            textVisualisatorMock.Setup(textVisualisator =>
                textVisualisator.CreateTextImages(It.IsAny<Dictionary<string, double>>()));

            textVisualisatorMock.Setup(textVisualisator => textVisualisator.SetColors());
            textVisualisatorMock.Setup(textVisualisator => textVisualisator.SetFontSizes(
                It.IsAny<double>(),
                It.IsAny<double>())
            );
            textVisualisatorMock.Setup(textVisualisator => textVisualisator.SetFontTipe(It.IsAny<string>()));

            textVisualisatorMock.Setup(textVisualisator => textVisualisator.GetStringImages())
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
            splitterMock
                .Verify(splitter => splitter.Split(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetBitmap_FilterCalled()
        {
            GetMockedBitmap();
            filterMock.Verify(filter => filter.FilterWords(It.IsAny<IEnumerable<WordModel>>()));
        }

        [Test]
        public void GetBitmap_FormatterCalled()
        {
            GetMockedBitmap();
            formatterMock
                .Verify(formatter => formatter.FormatWords(It.IsAny<IEnumerable<WordModel>>()), Times.Once);
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
