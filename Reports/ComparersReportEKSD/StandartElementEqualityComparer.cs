using System;
using System.Collections.Generic;

class StandartElementEqualityComparer : IEqualityComparer<StandartElement>
{
    bool IEqualityComparer<StandartElement>.Equals(StandartElement x, StandartElement y)
    {
        return x.Name.Equals(y.Name) && x.NameStandart.Equals(y.NameStandart) && x.NumberStandart == y.NumberStandart;
    }

    int IEqualityComparer<StandartElement>.GetHashCode(StandartElement obj)
    {
        return 0;
    }
}