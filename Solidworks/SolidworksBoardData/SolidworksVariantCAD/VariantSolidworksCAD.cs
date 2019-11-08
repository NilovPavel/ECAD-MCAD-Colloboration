using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace SolidworksBoardData.SolidworksVariantCAD
{
    public class VariantSolidworksCAD
    {
        protected bool topLevel;
        protected bool readCoordinats;
        protected bool routingLoadStatus;
        protected AssemblyDoc assemblyModelDoc;
        protected string[] configurationNames;

        public VariantSolidworksCAD(AssemblyDoc assemblyModelDoc)
        {
            this.assemblyModelDoc = assemblyModelDoc;
            this.Initialization();
        }

        private void Initialization()
        {
            this.configurationNames = (string[])((ModelDoc2)this.assemblyModelDoc).GetConfigurationNames();

            if (!this.assemblyModelDoc.ResolveAllLightweight())
                this.assemblyModelDoc.ResolveAllLightWeightComponents(true);
        }
    }
}
