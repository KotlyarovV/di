using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {      
        private Point BalancePoint(PointF point) =>
            new Point((int) Math.Floor(point.X), (int) Math.Ceiling(point.Y));

        public IEnumerable<Rectangle> GetCloudRectangles(
            IEnumerable<Size> sizes,
            Func<IEnumerable<PointF>> getSpiralPoints
            )
        {
            var pointEnumerator = getSpiralPoints().GetEnumerator();
            var rectangles = new List<Rectangle>();
            foreach (var size in sizes)
            {
                Point point;
                do
                {
                    pointEnumerator.MoveNext();
                    point = BalancePoint(pointEnumerator.Current);

                } while (rectangles.ContainPoint(point) ||
                         rectangles.IntersectRectangle(new Rectangle(point, size)));

                rectangles.Add(new Rectangle(point, size));
                yield return rectangles.Last();
            }
            pointEnumerator.Dispose();
        }
    }
}
