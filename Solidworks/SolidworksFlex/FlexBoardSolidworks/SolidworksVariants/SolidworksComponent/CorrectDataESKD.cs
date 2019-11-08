using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks.SolidworksVariants.SolidworksComponent
{
    class CorrectDataESKD
    {
        private DataESKD dataESKD;

        public bool IsUnCorrectESKD
        {
            get { return this.IsUnCorrectData(); }
        }

        private bool IsUnCorrectData()
        {
            if (string.IsNullOrEmpty(this.dataESKD.РазделСп)) return true;
            switch(this.dataESKD.РазделСп)
            {
                case "Сборочные единицы":
                case "Детали":
                    return string.IsNullOrEmpty(this.dataESKD.Обозначение) || string.IsNullOrEmpty(this.dataESKD.Наименование);
                case "Стандартные изделия":
                    return string.IsNullOrEmpty(this.dataESKD.Наименование);
                case "Прочие изделия":
                    return string.IsNullOrEmpty(this.dataESKD.PartNumber);
            }
            return true;
        }

        public CorrectDataESKD(DataESKD dataESKD)
        {
            this.dataESKD = dataESKD;
        }
    }
}
