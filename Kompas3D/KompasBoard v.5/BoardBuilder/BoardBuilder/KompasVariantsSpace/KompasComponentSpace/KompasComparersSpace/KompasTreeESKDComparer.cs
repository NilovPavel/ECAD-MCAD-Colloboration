using System;
using System.Collections.Generic;

namespace KompasComparersSpace
{
    class KompasTreeESKDComparer : IEqualityComparer<ComponentCAD>
    {
        bool IEqualityComparer<ComponentCAD>.Equals(ComponentCAD x, ComponentCAD y)
        {
            if (!x.dataESKD.РазделСп.Equals(y.dataESKD.РазделСп) || !x.Designator.Equals(y.Designator))
                return false;

            bool equalESKD = this.EqualESKDParameters(x, y);
            
            return equalESKD && x.coordinats.X == y.coordinats.X && x.coordinats.Y == y.coordinats.Y && x.coordinats.Z == y.coordinats.Z;
        }

        bool EqualESKDParameters(ComponentCAD x, ComponentCAD y)
        {
            switch (x.dataESKD.РазделСп)
            {
                case "Сборочные единицы":
                case "Детали":
                    return x.dataESKD.Обозначение.Equals(y.dataESKD.Обозначение) && x.dataESKD.Наименование.Equals(y.dataESKD.Наименование);
                case "Стандартные изделия":
                case "Материалы":
                    return x.dataESKD.Наименование.Equals(y.dataESKD.Наименование);
                case "Прочие изделия":
                    return x.dataESKD.PartNumber.Equals(y.dataESKD.PartNumber);
            }
            return false;
        }

        int IEqualityComparer<ComponentCAD>.GetHashCode(ComponentCAD obj)
        {
            return 0;
        }
    }
}