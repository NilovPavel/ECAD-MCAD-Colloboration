using System;
using System.Collections.Generic;

class EriEqualityComparer : IEqualityComparer<Eri>
{
    bool IEqualityComparer<Eri>.Equals(Eri x, Eri y)
    {
        return x.FullEriName.Equals(y);
    }

    int IEqualityComparer<Eri>.GetHashCode(Eri obj)
    {
        return 0;
    }
}