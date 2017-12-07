using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;


namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class CircularCloudLayouterShould
    {
        private Point startPoint;
        private Size sizeMin;
        private CircularCloudLayouter circularCloud;
        private List<Rectangle> cloudRectangles;

        private List<Rectangle> InitializationOfRandomRectangles(int numberOfPoints)
        {
            var visualisator = new Visualisator(circularCloud);
            cloudRectangles = visualisator.GenerateRandomRectangles(numberOfPoints);
            return cloudRectangles;
        }

        [SetUp]
        public void SetUp()
        {
            startPoint = new Point(1, 1);
            circularCloud = new CircularCloudLayouter(new ArchimedeanSpiral(startPoint));
            sizeMin = new Size(1, 1);
            cloudRectangles = new List<Rectangle>();
        }
        
        
        [Test]
        public void SetFirstMinRectangle_SetRectangleUpperCentre()
        {
            var rectangle = circularCloud.PutNextRectangle(sizeMin);
            cloudRectangles.Add(rectangle);
            rectangle.Should().Be(new Rectangle(1, 2, 1, 1));
        }

        [Test]
        public void SetTwoRectangle_SetSecondUpperFirst()
        {
            cloudRectangles.Add(circularCloud.PutNextRectangle(sizeMin));
            var secondRectangle = circularCloud.PutNextRectangle(sizeMin);
            cloudRectangles.Add(secondRectangle);
            secondRectangle.Should().Be(new Rectangle(1, 3, 1, 1));
        }

        [Test]
        public void SetThreeRectangle_SetInStraightAngel()
        {
            cloudRectangles.Add(circularCloud.PutNextRectangle(sizeMin));
            cloudRectangles.Add(circularCloud.PutNextRectangle(sizeMin));

            var thirdRectangle = circularCloud.PutNextRectangle(sizeMin);
            cloudRectangles.Add(thirdRectangle);
            thirdRectangle.Should().Be(new Rectangle(0, 3, 1, 1));
        }        
        
        [Test]
        public void TwoBigRectangles_IsNotInterspected()
        {
            var bigSize = new Size(100, 100);
            cloudRectangles.Add(circularCloud.PutNextRectangle(bigSize));
            cloudRectangles.Add(circularCloud.PutNextRectangle(bigSize));
            cloudRectangles.AnyIntersected().Should().BeFalse();
        }
        
            
        [TestCase(-1, 0, TestName = "negative number and zero in size")]
        [TestCase(-1, -10, TestName = "two negative numbers in size")]
        [TestCase(0, 0, TestName = "two zeros in size")]
        [TestCase(0, 5, TestName = "zero and positive number in size")]
        [TestCase(5, -10, TestName = "negative and positive number in size")]
        public void ZeroOrNegativeNumbersInSize_ThrowsArgumentException(int width, int height)
        {
            Assert.Throws<ArgumentException>(() => circularCloud.PutNextRectangle(new Size(width, height)));
        }

        [TestCase(100, TestName = "hundred of rectangles")]
        [TestCase(200, TestName = "two hundred of rectangles")]
        [TestCase(300, TestName = "three hundred of rectangles")]
        public void BigRandomCloud_IsCircle(int numberOfPoints)
        {
            var rectangles = InitializationOfRandomRectangles(numberOfPoints);

            var sortedByXRectangles = rectangles.OrderBy(x => x.Location.X);
            var mostLeftPoint = sortedByXRectangles.First().Location;
            var mostRightPoint = sortedByXRectangles.Last().Location;

            var sortedByYRectangles = rectangles.OrderBy(x => (x.Location.Y));
            var mostTopPoint = sortedByYRectangles.Last().Location;
            var mostBottomPoint = sortedByYRectangles.First().Location;
            
            var points = new [] {mostBottomPoint, mostLeftPoint, mostRightPoint, mostTopPoint};
            var radiuses = points.Select(point => startPoint.GetDistance(point)).ToArray();

            for (int i = 0; i < radiuses.Length; i++)
                for (int j = 0; j < radiuses.Length; j++)
                {
                    Assert.True(radiuses[j] / radiuses[i] > 0.7);
                }
        }
         
    

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var visualisator = new Visualisator(circularCloud);
                var testName = TestContext.CurrentContext.Test.Name;
                var filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "failed_tests_pictures");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                var saver = new Saver();
                var bitmap = visualisator.GetBitmap(cloudRectangles);
                saver.SaveBitmap(Path.Combine(filePath, testName + ".jpg"), bitmap);
                Console.WriteLine(string.Format("Tag cloud visualization saved to file {0}.jpg", testName));
            }
        }
        
    }
    
}
