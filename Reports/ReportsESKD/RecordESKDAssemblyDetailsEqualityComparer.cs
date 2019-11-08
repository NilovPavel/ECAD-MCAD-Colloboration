using System;
using System.Collections.Generic;

public class RecordESKDAssemblyDetailsEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        return x.Fitted == y.Fitted
               && x.Обозначение.Equals(y.Обозначение)
               && Designator.GetSymbolsFromDesignator(x.Designator).Equals(Designator.GetSymbolsFromDesignator(y.Designator));
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}