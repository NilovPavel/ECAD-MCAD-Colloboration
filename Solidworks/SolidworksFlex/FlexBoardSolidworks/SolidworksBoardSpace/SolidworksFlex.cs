using System;
using SolidWorks.Interop.sldworks;

namespace SolidworksBoardSpace
{
    internal class SolidworksFlex
    {
        private Feature feature;
        private PartDoc partDoc;
        private double totalHeight;
        private FeatureManager featureManager;
        //private CustomBendAllowance CBAObject;

        public SolidworksFlex(Feature feature, double totalHeight, PartDoc partDoc) 
        {
            this.feature = feature;
            this.totalHeight = totalHeight;
            this.partDoc = partDoc;
            this.featureManager = ((ModelDoc2)this.partDoc).FeatureManager;
            this.CreateCustomBendAllowance();
            this.ExtrudeFlex();
        }

        private CustomBendAllowance CreateCustomBendAllowance()
        {
            CustomBendAllowance CBAObject = this.featureManager.CreateCustomBendAllowance();
            CBAObject.Type = 2;
            CBAObject.KFactor = 0.5;
            return CBAObject;
        }

        private void ExtrudeFlex()
        {
            this.featureManager.InsertSheetMetalBaseFlange2(0.1, false, 0.004, 0.02, 0.01, false, 0, 0, 1, this.CreateCustomBendAllowance(), false, 2, 0.0001, 0.0001, 0.5, true, false, true, true);
        }
    }
}