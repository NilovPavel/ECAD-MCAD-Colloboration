using System;
using System.Collections.Generic;

class AssemblyDetailsComparer : IComparer<RecordESKD>
{
    int IComparer<RecordESKD>.Compare(RecordESKD x, RecordESKD y)
    {
        return x.Обозначение.CompareTo(y.Обозначение);
    }
}