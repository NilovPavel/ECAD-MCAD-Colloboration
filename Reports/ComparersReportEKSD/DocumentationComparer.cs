using System;
using System.Collections.Generic;

class DocumentationComparer : IComparer<RecordESKD>
{
    public int Compare(RecordESKD x, RecordESKD y)
    {
        if (x.Обозначение.Equals(y.Обозначение))
        {
            if (x.КодДокумента.Equals(y.КодДокумента))
                return x.Наименование.CompareTo(y.Наименование);
            else
                return x.КодДокумента.CompareTo(y.КодДокумента);
        }
        else
            return x.Обозначение.CompareTo(y.Обозначение);
    }
}