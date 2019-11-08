using System;
using System.Collections.Generic;

class RecordESKDPositionEqualityComparer : IEqualityComparer<RecordESKD>
{
    /*bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        if (!x.РазделСп.Equals(y.РазделСп))
            return false;
        return x.Fitted == y.Fitted
            && x.Обозначение.Equals(y.Обозначение)
            && x.Наименование.Equals(y.Наименование)
            ;
    }*/

    bool IEqualityComparer<RecordESKD>.Equals(RecordESKD x, RecordESKD y)
    {
        if (!x.РазделСп.Equals(y.РазделСп))
            return false;

        bool equalESKD = this.EqualESKDParameters(x, y);

        return equalESKD;
    }

    bool EqualESKDParameters(RecordESKD x, RecordESKD y)
    {
        switch (x.РазделСп)
        {
            case "Документация":
            case "Сборочные единицы":
            case "Детали":
            case "Комплекты":
                return x.Обозначение.Equals(y.Обозначение) && x.Наименование.Equals(y.Наименование);
            case "Стандартные изделия":
                return x.Наименование.Equals(y.Наименование);
            case "Прочие изделия":
                return x.PartNumber.Equals(y.PartNumber);
        }
        return false;
    }

    int IEqualityComparer<RecordESKD>.GetHashCode(RecordESKD obj)
    {
        return 0;
    }
}