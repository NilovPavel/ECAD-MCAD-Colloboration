using System;
using System.Collections.Generic;

internal class RecordESKDPositionEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        if (!x.РазделСп.Equals(y.РазделСп))
            return false;
        return x.Fitted == y.Fitted
            && x.Обозначение.Equals(y.Обозначение)
            && x.Наименование.Equals(y.Наименование);
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}