using System;
using SolidWorks.Interop.sldworks;
using FlexBoardSolidworks;

namespace SolidworksBoardData.SolidworksVariantCAD
{
    class SolidworksDataESKD : IDataESKD
    {
        private Component2 component;
        private SolidworksModelProperties swModelProperties;
        private string format;
        private string obozn;
        private string name;
        private string partNumber;
        private string razdelSp;
        private EskdSpecificationType eskdSpecificationType;

        public SolidworksDataESKD(Component2 component)
        {
            this.component = component;
            this.Initialization();
        }

        private void Initialization()
        {
            ModelDoc2 modelDocComponent = this.component.GetModelDoc2();
            this.swModelProperties = new SolidworksModelProperties(modelDocComponent, this.component.ReferencedConfiguration);
            this.format = this.swModelProperties.GetPropertieValue("Формат");
            this.obozn = this.swModelProperties.GetPropertieValue("Обозначение");
            this.name = this.swModelProperties.GetPropertieValue("Наименование");
            this.partNumber = this.swModelProperties.GetPropertieValue("ERP_Part_Number");
            this.razdelSp = this.swModelProperties.GetPropertieValue("Раздел");
            modelDocComponent = null;
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
            return this.partNumber;
        }

        string IDataESKD.GetPrimechanie()
        {
            return string.Empty;
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