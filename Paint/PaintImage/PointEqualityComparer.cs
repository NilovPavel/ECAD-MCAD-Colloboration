using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintImage
{
    class PointEqualityComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point firstPoint, Point secondPoint)
        {
            return Math.Round(firstPoint.X, 2) == Math.Round(secondPoint.X, 2) && Math.Round(firstPoint.Y, 2) == Math.Round(secondPoint.Y, 2);
        }

        public int GetHashCode(Point obj)
        {
            return 0;
        }
    }
}
