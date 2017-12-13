using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;
using Moq;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class CircularCloudLayouterShould
    {
        private CircularCloudLayouter circularCloud;
        private readonly Mock<ISpiral> spiraMock = new Mock<ISpiral>(); 
       
        [SetUp]
        public void SetUp()
        {
            circularCloud = new CircularCloudLayouter(spiraMock.Object);
        }
        
        [Test]
        public void SetRectangleOnPoint_SetRectangleOnBalancedPoint()
        {
            spiraMock.Setup(spiral => spiral.GetPoint()).Returns(new PointF(0.5F, 0.6F));
            var size = new Size(1, 1);
            var rectangle = circularCloud.PutNextRectangle(size);
            rectangle.Location.Should().Be(new Point(0, 1));
        }

        [Test]
        public void SetTwoRectangle_DoNotSetRectangleOnInsertedPoint()
        {
            spiraMock.SetupSequence(spiral => spiral.GetPoint())
                .Returns(new PointF(0F, 1F))
                .Returns(new PointF(1F, 1F))
                .Returns(new PointF(3F, 1F));

            circularCloud.PutNextRectangle(new Size(2, 2));
            var secondRectangle = circularCloud.PutNextRectangle(new Size(1, 1));
            secondRectangle.Location.Should().Be(new Point(3, 1));
        }

        [Test]
        public void SetTwoRectangle_DoNotSetInterspectedRectangles()
        {
            spiraMock.SetupSequence(spiral => spiral.GetPoint())
                .Returns(new PointF(0F, 1F))
                .Returns(new PointF(1F, 1F))
                .Returns(new PointF(2F, 1F))
                .Returns(new PointF(3F, 1F))
                .Returns(new PointF(4F, 1F))
                .Returns(new PointF(5F, 1F))
                .Returns(new PointF(6F, 1F));

            var rectangles = new List<Rectangle>();
            var size = new Size(2, 2);

            rectangles.Add(circularCloud.PutNextRectangle(size));
            rectangles.Add(circularCloud.PutNextRectangle(size));
            rectangles.Add(circularCloud.PutNextRectangle(size));

            rectangles.AnyIntersected().Should().BeFalse();
        }


        [TestCase(-1, 0, TestName = "negative number and zero in size")]
        [TestCase(-1, -10, TestName = "two negative numbers in size")]
        [TestCase(0, 0, TestName = "two zeros in size")]
        [TestCase(0, 5, TestName = "zero and positive number in size")]
        [TestCase(5, -10, TestName = "negative and positive number in size")]
        public void ZeroOrNegativeNumbersInSize_ThrowsArgumentException(int width, int height)
        {
            Action getRectangle = () => circularCloud.PutNextRectangle(new Size(width, height));
            getRectangle.ShouldThrow<ArgumentException>();
        }
    }
    
}
