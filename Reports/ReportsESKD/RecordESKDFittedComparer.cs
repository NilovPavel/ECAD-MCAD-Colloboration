using System;
using System.Collections;
using System.Collections.Generic;

public class RecordESKDFittedComparer : IComparer<RecordESKD>
{
    public int Compare(RecordESKD x, RecordESKD y)
    {
        return y.Fitted.CompareTo(x.Fitted);
    }
}