using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class SpiralShould
    {
        private ArchimedeanSpiral spiral;
        private Point startPoint;
        private double spiralRadius = 1;
        private double spiralAngle;
        private const double AngleStep = 0.1;

        [SetUp]
        public void SetUp()
        {
            startPoint = new Point(1, 1);
            spiral = new ArchimedeanSpiral();
        }


        /*
         * test based on http://hijos.ru/2011/03/09/arximedova-spiral/
         */
         
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void CheckSpiralPoints_MustBeCorrectlyDefined(int numberOfPoints)
        {

            var spiralPointsEnumerator = spiral
                .GetSpiralPoints(startPoint, AngleStep, spiralRadius)
                .GetEnumerator();

            spiralPointsEnumerator.MoveNext();

            spiralAngle = 0;
            for (var i = 0; i < numberOfPoints; i++)
            {

                spiralAngle += AngleStep;
                var spiralPoint = spiralPointsEnumerator.Current;
                spiralPoint = new PointF(spiralPoint.X - startPoint.X, spiralPoint.Y - startPoint.Y);
                
                var sumXYSquares = Math.Pow(spiralPoint.X, 2) + Math.Pow(spiralPoint.Y, 2);
                var radiusAndAngleSquare = Math.Pow(spiralAngle * spiralRadius, 2);

                Math.Abs(sumXYSquares - radiusAndAngleSquare).Should().BeLessThan(0.00001);
                spiralPointsEnumerator.MoveNext();
            }
            spiralPointsEnumerator.Dispose();
        }
        
    }
}
