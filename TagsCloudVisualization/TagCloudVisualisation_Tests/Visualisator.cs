using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagCloudVisualisation_Tests
{
    class Visualisator
    {
        private readonly CircularCloudLayouter circularCloud;
        private readonly SolidBrush brush = new SolidBrush(Color.LimeGreen);

        public Visualisator(CircularCloudLayouter circularCloudLayouter)
        {
            circularCloud = circularCloudLayouter;
        }

        private static readonly Random Random = new Random();

        private static void AddRandomSize(List<Size> sizeList, int minValue, int maxValue)
        {
            var x = Random.Next(minValue, maxValue);
            var y = Random.Next(minValue, maxValue);
            sizeList.Add(new Size(x, y));
        }

        private static void AddRandomSizes(List<Size> sizes, int number, int minSize = 10, int maxSize = 60)
        {
            for (var i = 0; i < number; i++)
            {
                AddRandomSize(sizes, minSize, maxSize);
            }
        }

        public List<Rectangle> GenerateRandomRectangles(int numberOfSizes)
        {
            var sizes = new List<Size>();
            var rectangles = new List<Rectangle>();
            AddRandomSizes(sizes, numberOfSizes);
            sizes.ForEach(s =>
            {
                rectangles.Add(circularCloud.PutNextRectangle(s));
            });
            return rectangles;
        }
        
        private void SetRectanglesOnGraphics(Graphics graphics, List<Rectangle> rectangles) =>
            rectangles.ForEach(rectangle => graphics.FillRectangle(brush, rectangle)); 

        private Size GetBitmapSize(List<Rectangle> rectangles)
        {
            var minX = rectangles.Min(rectangle => rectangle.X);
            var maxX = rectangles.Max(rectangle => rectangle.X + rectangle.Width);

            var minY = rectangles.Min(rectangle => rectangle.Y - rectangle.Height);
            var maxY = rectangles.Max(rectangle => rectangle.Y);
            
            return new Size(maxX - minX, maxY - minY);
        }

        public Bitmap GetBitmap(List<Rectangle> rectangles)
        {
            var size = GetBitmapSize(rectangles);
            var bitmap = new Bitmap(size.Width, size.Height);

            var graphics = Graphics.FromImage(bitmap);
            SetRectanglesOnGraphics(graphics, rectangles);
            return bitmap;
        }
        
    }
}
