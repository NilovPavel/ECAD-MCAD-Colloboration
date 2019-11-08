using System;
using System.Collections.Generic;

internal class RecordESKDMaterialsEqualityComparer : IEqualityComparer<RecordESKD>
{
    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        if (x.ЕдИзм == null || y.ЕдИзм == null)
            return x.Fitted == y.Fitted && x.Наименование.Equals(y.Наименование);

        return x.Fitted == y.Fitted && x.Наименование.Equals(y.Наименование) && x.ЕдИзм.Equals(y.ЕдИзм);
            /*&& x.Количество == y.Количество*/
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}