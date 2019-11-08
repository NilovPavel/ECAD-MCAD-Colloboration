using System;
using System.Collections.Generic;

class StandartElementComparer : IComparer<StandartElement>
{
    int IComparer<StandartElement>.Compare(StandartElement x, StandartElement y)
    {
        if (x.Name.Equals(y.Name))
        {
            if (Array.IndexOf(Enum.GetNames(typeof(StandartNames)), x.NameStandart) == Array.IndexOf(Enum.GetNames(typeof(StandartNames)), y.NameStandart))
            {
                if (x.NumberStandart == y.NumberStandart)
                {
                    if (x.Diametr == y.Diametr)
                    {
                        if (x.Length == y.Length)
                        {
                            return x.FullName.CompareTo(y.FullName);
                        }
                        else
                            return x.Length.CompareTo(y.Length);
                    }
                    else
                        return x.Diametr.CompareTo(y.Diametr);
                }
                else
                    return x.NumberStandart - y.NumberStandart;
            }
            else
                return Array.IndexOf(Enum.GetNames(typeof(StandartNames)), x.NameStandart) - Array.IndexOf(Enum.GetNames(typeof(StandartNames)), y.NameStandart);
        }
        else
            return x.Name.CompareTo(y.Name);
    }
}