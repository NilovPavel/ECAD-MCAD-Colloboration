using System;
using System.Collections.Generic;

public class PassiveEriComparer : IComparer<Eri>
{
    public int Compare(Eri x, Eri y)
    {
        if (x.EriName.Equals(y.EriName))
        {
            if (x.Nominal == y.Nominal)
            {
                if (x.Voltage == y.Voltage)
                {
                    if (x.Admission == y.Admission)
                    {
                        if (x.Body.Equals(y.Body))
                            return x.FullEriName.CompareTo(y.FullEriName);
                        else
                            return x.Body.CompareTo(y.Body);
                    }
                    else
                        return x.Admission.CompareTo(y.Admission);
                }
                else
                    return x.Voltage.CompareTo(y.Voltage);
            }
            else
                return x.Nominal.CompareTo(y.Nominal);
        }
        else
            return x.EriName.CompareTo(y.EriName);
    }
}