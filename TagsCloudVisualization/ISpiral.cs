using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISpiral
    {
        Tuple<double, double> GetPoint();
        Point BalancePoint(Tuple<double, double> point);
        bool CheckBalancedPointOnSpiral(Point point);
        Point Center { get; }
    }
}
