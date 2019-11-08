using System;
using System.Collections.Generic;

internal class RecordESKDDocumentationComplectsEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        return x.Fitted == y.Fitted
               && x.Наименование.Equals(y.Наименование)
               && x.Обозначение.Equals(y.Обозначение);
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}