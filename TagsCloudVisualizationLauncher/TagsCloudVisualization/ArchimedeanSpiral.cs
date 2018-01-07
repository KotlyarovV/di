using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedeanSpiral
    {
        /*
        * Realization of http://hijos.ru/2011/03/09/arximedova-spiral/ formulas
        */

        
        public IEnumerable<PointF> GetSpiralPoints(
            Point center,
            double spiralRadius = 1,
            double angleStep = 0.1
        )
        {
            var spiralAngle = 0.0;

            while (true)
            {
                spiralAngle += angleStep;

                var x = (float) (center.X + spiralRadius * spiralAngle * Math.Cos(spiralAngle));
                var y = (float) (center.Y + spiralRadius * spiralAngle * Math.Sin(spiralAngle));

                yield return new PointF(x, y);
            }
        }

    }
}
