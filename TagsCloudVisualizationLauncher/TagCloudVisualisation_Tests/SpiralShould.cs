using System;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    [TestFixture]
    class SpiralShould
    {
        private ArchimedeanSpiral spiral;
        private Point startPoint;
        private double spiralRadius;
        private double spiralAngle;
        private const double AngleStep = 0.1;

        [SetUp]
        public void SetUp()
        {
            startPoint = new Point(1, 1);
            spiral = new ArchimedeanSpiral(startPoint, AngleStep, spiralRadius);
        }


        /*
         * test based on http://hijos.ru/2011/03/09/arximedova-spiral/
         */
         
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void CheckSpiralPoints_MustBeCorrectlyDefined(int numberOfPoints)
        {
            for (var i = 0; i < numberOfPoints; i++)
            {
                spiralAngle += AngleStep;

                var spiralPoint = spiral.GetPoint();
                spiralPoint = new PointF(spiralPoint.X - startPoint.X, spiralPoint.Y - startPoint.Y);
                
                var sumXYSquares = Math.Pow(spiralPoint.X, 2) + Math.Pow(spiralPoint.Y, 2);
                var radiusAndAngleSquare = Math.Pow(spiralAngle * spiralRadius, 2);

                Assert.True(Math.Abs(sumXYSquares - radiusAndAngleSquare) < 0.000001);
            }
        }
        
    }
}
