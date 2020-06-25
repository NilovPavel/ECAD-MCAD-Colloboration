using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class PerechenDesignatorComparer : IComparer<string>
{
    int IComparer<string>.Compare(string x, string y)
    {

        if (x.Contains("-"))
            x = x.Substring(0, x.IndexOf('-'));

        if (y.Contains("-"))
            y = y.Substring(0, y.IndexOf('-'));

        return Designator.GetIndexFromDesignator(x.Trim()) - Designator.GetIndexFromDesignator(y.Trim());
    }
}