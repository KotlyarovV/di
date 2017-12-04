using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter : ICloudLayouter
    {
        public readonly List<Rectangle> Rectangles;
        public readonly ISpiral Spiral;
        public Point Center => Spiral.Center;
        
        public CircularCloudLayouter(ISpiral spiral)
        {
            Spiral = spiral;
            Rectangles = new List<Rectangle>();
        }

        private Point ChoosePoint()
        {
            Point point;
            do
            {
                var pointOnSpiral = Spiral.GetPoint();
                point = Spiral.BalancePoint(pointOnSpiral);
            }
            while (Rectangles.ContainPoint(point));
            return point;
        }

        private Rectangle ChooseRectangle(Size size, Point point)
        {
            var rectangle = new Rectangle(point, size);
            while (Rectangles.IntersectRectangle(rectangle))
            {
                point = ChoosePoint();
                rectangle = new Rectangle(point, size);
            }
            return rectangle;
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0) 
                throw new ArgumentException("Size have to be positive non zero number!");

            var point = ChoosePoint();
            var rectangle = ChooseRectangle(rectangleSize, point);
            Rectangles.Add(rectangle);
            return rectangle;
        }        
    }
}
