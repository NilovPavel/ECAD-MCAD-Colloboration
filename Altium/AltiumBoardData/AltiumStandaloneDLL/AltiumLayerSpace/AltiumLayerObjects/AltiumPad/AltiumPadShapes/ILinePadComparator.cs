using System.Collections.Generic;

internal class ILinePadComparator : IEqualityComparer<ILine>
{
    bool IEqualityComparer<ILine>.Equals(ILine x, ILine y)
    {
        return ((x.GetStartPoint().X == y.GetStartPoint().X && x.GetStartPoint().Y == y.GetStartPoint().Y) &&
                (x.GetEndPoint().X == y.GetEndPoint().X && x.GetEndPoint().Y == y.GetEndPoint().Y))
                ||
                ((x.GetStartPoint().X == y.GetEndPoint().X && x.GetStartPoint().Y == y.GetEndPoint().Y) &&
                (x.GetEndPoint().X == y.GetStartPoint().X && x.GetEndPoint().Y == y.GetStartPoint().Y));
    }

    int IEqualityComparer<ILine>.GetHashCode(ILine obj)
    {
        return 0;
    }
}