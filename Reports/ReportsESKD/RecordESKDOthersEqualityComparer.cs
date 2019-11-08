using System;
using System.Collections.Generic;

internal class RecordESKDOthersEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        return x.Fitted == y.Fitted
               && x.PartNumber.Equals(y.PartNumber)
               && x.Наименование.Equals(y.Наименование)
               && Designator.GetSymbolsFromDesignator(x.Designator).Equals(Designator.GetSymbolsFromDesignator(y.Designator));
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}