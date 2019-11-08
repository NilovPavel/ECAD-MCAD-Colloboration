using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using SolidworksBoardSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks.SolidworksBoardSpace
{
    class SolidworksGroupCutOut
    {
        private SketchManager sketchManager;
        private ModelDoc2 modelDoc;
        private Feature feature;
        private FeatureManager featureManager;
        private List<Contour> contours;
        private string nameCutOut;

        private void Initializaion()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
            this.feature = (Feature)this.sketchManager.ActiveSketch;
        }

        public SolidworksGroupCutOut(List<Contour> contours, string nameCutOut)
        {
            this.contours = contours;
            this.nameCutOut = nameCutOut;
            this.Initializaion();
            this.contours.ForEach(item => new SolidworksContour(item));
            this.CreateBody();
            this.modelDoc.ClearSelection();
        }

        private void CreateBody()
        {
            Feature feature = this.featureManager.FeatureCut3(true, false, true, (int)swEndConditions_e.swEndCondThroughNext, (int)swEndConditions_e.swEndCondUpToSurface, 0.0, 0.0, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            if (feature != null)
                feature.Name = this.nameCutOut;
        }
    }
}
