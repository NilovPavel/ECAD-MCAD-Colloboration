using System;
using System.Collections.Generic;

class StandartGroupComparer : IComparer<SpecificationRazdelStandartGroup>
{
    int IComparer<SpecificationRazdelStandartGroup>.Compare(SpecificationRazdelStandartGroup x, SpecificationRazdelStandartGroup y)
    {
        return ((IComparer<StandartElement>)new StandartElementComparer()).Compare(x.StandartElement, y.StandartElement);
    }
}