using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchESKD
{
    class SearchESKDComparer : IEqualityComparer<DataESKD>
    {
        bool IEqualityComparer<DataESKD>.Equals(DataESKD x, DataESKD y)
        {
            switch (x.РазделСп)
            {
                case "Сборочные единицы":
                case "Детали":
                    return x.Обозначение.Equals(y.Обозначение) && x.Наименование.Equals(y.Наименование);
                case "Стандартные изделия":
                    return x.Наименование.Equals(y.Наименование);
                case "Прочие изделия":
                    return x.PartNumber.Equals(y.PartNumber);
            }
            return false;
        }

        int IEqualityComparer<DataESKD>.GetHashCode(DataESKD obj)
        {
            return 0;
        }
    }
}
