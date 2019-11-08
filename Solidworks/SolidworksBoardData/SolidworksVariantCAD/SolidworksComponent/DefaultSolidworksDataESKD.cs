using System;
using SolidWorks.Interop.sldworks;
using System.IO;
using System.Text.RegularExpressions;

namespace SolidworksBoardData.SolidworksVariantCAD
{
    class DefaultSolidworksDataESKD : IDataESKD
    {
        private Component2 component;
        private string razdelSp;
        private string componentName;
        private string obozn;
        private string name;
        private string format;
        private EskdSpecificationType eskdSpecificationType;

        public DefaultSolidworksDataESKD(Component2 component)
        {
            this.component = component;
            this.componentName = this.component.GetPathName();
            this.Initialization();
        }

        private void Initialization()
        {
            this.razdelSp = this.GetRazdelSp();
            switch (this.razdelSp)
            {
                case "Сборочные единицы":
                case "Детали":
                    this.name = this.component.ReferencedConfiguration;
                    break;
                case "Стандартные изделия":
                case "Прочие изделия":
                    this.name = Path.GetFileNameWithoutExtension(this.componentName);
                    break;
            }
        }

        private string GetRazdelSp()
        {
            string pathName = Path.GetFileNameWithoutExtension(this.componentName);
            Match match = this.SearchDecimalNumber(pathName);
            if (match.Success)
            {
                this.obozn = match.Value;
                return Path.GetExtension(this.componentName).ToLower().Equals(".sldprt") ? "Детали" : "Сборочные единицы";
            }
            if (this.ExistStandart(pathName))
                return "Стандартные изделия";
            return "Прочие изделия";
        }

        private bool ExistStandart(string fileName)
        {
            string[] standartNames = new string[] { "DIN", "ISO", "ГОСТ", "ОСТ" };
            return Array.Exists(standartNames, item => fileName.IndexOf(item.ToUpper()) != -1);
        }

        private Match SearchDecimalNumber(string fileName)
        {
            Regex regex = new Regex("[а-яА-Я]{4}.\\d{6}.\\d{3}");
            return regex.Match(fileName);
        }

        string IDataESKD.GetFormat()
        {
            return this.format;
        }

        string IDataESKD.GetName()
        {
            return this.name;
        }

        string IDataESKD.GetObozn()
        {
            return this.obozn;
        }

        string IDataESKD.GetPartNumber()
        {
            return this.razdelSp.Equals("Прочие изделия") ? this.name : null;
        }

        string IDataESKD.GetPrimechanie()
        {
            return null;
        }

        string IDataESKD.GetRazdelSP()
        {
            return this.razdelSp;
        }

        EskdSpecificationType IDataESKD.GetSpecificationType()
        {
            return this.eskdSpecificationType;
        }

        void IDataESKD.SetSpecificationType(EskdSpecificationType eskdSpecificationType)
        {
            this.eskdSpecificationType = eskdSpecificationType;
        }
    }
}