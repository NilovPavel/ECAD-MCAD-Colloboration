using System;
using SolidWorks.Interop.sldworks;

namespace SolidworksBoardSpace
{
    internal class SolidworksRigid
    {
        private Feature feature;
        private PartDoc partDoc;
        private double totalHeight;

        public SolidworksRigid(Feature feature, double totalHeight)
        {
            this.feature = feature;
            this.totalHeight = totalHeight;
            this.ExtrudeRigid();
        }

        public SolidworksRigid(Feature feature, double totalHeight, PartDoc partDoc)
        {
            this.feature = feature;
            this.totalHeight = totalHeight;
            this.partDoc = partDoc;
            this.ExtrudeRigid();
        }

        private void ExtrudeRigid()
        {
            //this.partDoc.FeatureExtrusion2(true, false, false, 0, 0, this.totalHeight, 0, false, false, false, false, 0, 0, false, false, false, false, true, true, true, 0, 0, false);
            this.partDoc.FeatureExtrusion2(true, false, false, 0, 0, this.totalHeight, 0, false, false, false, false, 0, 0, false, false, false);
        }
    }
}