using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StandartElementsSort : IComparer<StandartElement>
{
    int IComparer<StandartElement>.Compare(StandartElement x, StandartElement y)
    {
        if (x.Name.Equals(y.Name))
        {
            if (Array.IndexOf(Constants.standarts_names, this.standart_name) == Array.IndexOf(Constants.standarts_names, srav.standart_name))
            {
                if (this.standart_oboznach == srav.standart_oboznach)
                {
                    if (this.diametr == srav.diametr)
                    {
                        if (this.length == srav.length)
                        {
                            return this.full_name.CompareTo(srav.full_name);
                        }
                        else
                            return this.length.CompareTo(srav.length);
                    }
                    else
                        return this.diametr.CompareTo(srav.diametr);
                }
                else
                    return this.standart_oboznach - srav.standart_oboznach;
            }
            else
                return Array.IndexOf(Constants.standarts_names, this.standart_name) - Array.IndexOf(Constants.standarts_names, srav.standart_name);
        }
        else
            return x.Name.CompareTo(y.Name);
    }
}