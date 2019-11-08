using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;

namespace FlexBoardSolidworks
{
    class SolidworksModelProperties
    {
        private ModelDoc2 model;
        private ModelDocExtension documentExtension;
        private CustomPropertyManager customPropertyManager;
        private string configurationName;

        private void Initialization()
        {
            this.configurationName = this.configurationName == null ? ((Configuration)this.model.GetActiveConfiguration()).Name : this.configurationName; 
            this.documentExtension = this.model.Extension;
            this.customPropertyManager = this.documentExtension.CustomPropertyManager[this.configurationName];
        }

        public string GetPropertieValue(string name)
        {
            return this.customPropertyManager.Get(name);   
        }

        public void AddPropertieValue(string name, string value)
        {
            this.customPropertyManager.Add3(name, (int)swCustomInfoType_e.swCustomInfoText, value, (int)swCustomPropertyAddOption_e.swCustomPropertyOnlyIfNew);
        }

        public void SetProperiteValue(string name, string value)
        {
            this.customPropertyManager.Set2(name, value);
        }

        public SolidworksModelProperties(ModelDoc2 model)
        {
            this.model = model;
            this.Initialization();
        }
    }
}
