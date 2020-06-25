using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class RecordESKDDesignatorComparer : IComparer<RecordESKD>
{
    int IComparer<RecordESKD>.Compare(RecordESKD x, RecordESKD y)
    {
        return Designator.GetIndexFromDesignator(x.Designator) - Designator.GetIndexFromDesignator(y.Designator);
    }
}