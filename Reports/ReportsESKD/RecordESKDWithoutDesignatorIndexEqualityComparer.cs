using System;
using System.Collections;
using System.Collections.Generic;

public class RecordESKDWithoutDesignatorIndexEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        return x.Fitted == y.Fitted
               && x.PartNumber.Equals(y.PartNumber)
               && x.Наименование.Equals(y.Наименование)
               && x.Обозначение.Equals(y.Обозначение)
               && x.РазделСп.Equals(y.РазделСп)
               && Designator.GetSymbolsFromDesignator(x.Designator).Equals(Designator.GetSymbolsFromDesignator(y.Designator));
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}