using EDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltiumVariants
{
    class AltiumComponentData : IDataESKD
    {
        private IComponent iComponent;
        private Dictionary<string, string> properties;

        private string GetPropertieValue(string propertieName)
        {
            return this.properties.ContainsKey(propertieName) ? this.properties.Where(x => x.Key.Equals(propertieName)).FirstOrDefault().Value : null;
        }

        private void ReadProperties()
        {
            int parameterCount = this.iComponent.DM_ParameterCount();
            for (int i = 0; i < parameterCount; i++)
                if(!string.IsNullOrEmpty(this.iComponent.DM_Parameters(i).DM_Name()))
                    if (!this.properties.ContainsKey(this.iComponent.DM_Parameters(i).DM_Name()))
                        this.properties.Add(this.iComponent.DM_Parameters(i).DM_Name(), this.iComponent.DM_Parameters(i).DM_Value());
        }

        private void Initialization()
        {
            this.properties = new Dictionary<string, string>();
        }

        string IDataESKD.GetName()
        {
            return this.GetPropertieValue("Наименование");
        }

        string IDataESKD.GetObozn()
        {
            return this.GetPropertieValue("Обозначение");
        }

        string IDataESKD.GetPartNumber()
        {
            return this.GetPropertieValue("Part Number");
        }

        string IDataESKD.GetRazdelSP()
        {
            return string.IsNullOrEmpty(this.GetPropertieValue("Раздел")) ? "Прочие изделия" : this.GetPropertieValue("Раздел");
        }

        string IDataESKD.GetFormat()
        {
            return this.GetPropertieValue("Формат");
        }

        string IDataESKD.GetPrimechanie()
        {
            return this.GetPropertieValue("Примечание");
        }

        EskdSpecificationType IDataESKD.GetSpecificationType()
        {
            return EskdSpecificationType.general ;
        }

        void IDataESKD.SetSpecificationType(EskdSpecificationType eskdSpecificationType)
        {
            return;
        }

        public AltiumComponentData(IComponent iComponent)
        {
            this.iComponent = iComponent;
            this.Initialization();
            this.ReadProperties();
        }
    }
}
