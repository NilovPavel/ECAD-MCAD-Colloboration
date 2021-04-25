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
            return Math.Round(firstPoint.X, 3) == Math.Round(secondPoint.X, 3) && Math.Round(firstPoint.Y, 3) == Math.Round(secondPoint.Y, 3);
        }

        public int GetHashCode(Point obj)
        {
            return 0;
        }
    }
}
