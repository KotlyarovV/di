using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization;
using TagsCloudVisualization.Extensions;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class CircularCloudLayouterShould
    {
        private CircularCloudLayouter circularCloud;
        
        [SetUp]
        public void SetUp()
        {
            circularCloud = new CircularCloudLayouter();
        }
        
        [Test]
        public void SetRectangleOnPoint_SetRectangleOnBalancedPoint()
        {
            var sizes = new []{new Size(1, 1)};
            Func<IEnumerable<PointF>> getPoints = () => new[] {new PointF(0.5F, 0.6F)};
            var rectangles = circularCloud.GetCloudRectangles(sizes, getPoints);
            rectangles.First().Location.Should().Be(new Point(0, 1));
        }

        [Test]
        public void SetTwoRectangle_DoNotSetRectangleOnInsertedPoint()
        {
            Func<IEnumerable<PointF>> getPoints = () => new[]
            {
                new PointF(0F, 1F),
                new PointF(1F, 1F),
                new PointF(3F, 1F)
            };

            var sizes = new[]
            {
                new Size(2, 2),
                new Size(1, 1)
            };
            
            var rectangles = circularCloud.GetCloudRectangles(sizes, getPoints);
            rectangles.Last().Location.Should().Be(new Point(3, 1));
        }

        [Test]
        public void SetTwoRectangle_DoNotSetInterspectedRectangles()
        {
            Func<IEnumerable<PointF>> getPoints = () => new[]
            {
                new PointF(0F, 1F),
                new PointF(1F, 1F),
                new PointF(2F, 1F),
                new PointF(3F, 1F),
                new PointF(4F, 1F),
                new PointF(5F, 1F),
                new PointF(6F, 1F)

            };

            var sizes = new[]
            {
                new Size(2, 2),
                new Size(2, 2),
                new Size(2, 2)
            };

            var rectangles = circularCloud.GetCloudRectangles(sizes, getPoints);
            rectangles.AnyIntersected().Should().BeFalse();
        }
    }
    
}
