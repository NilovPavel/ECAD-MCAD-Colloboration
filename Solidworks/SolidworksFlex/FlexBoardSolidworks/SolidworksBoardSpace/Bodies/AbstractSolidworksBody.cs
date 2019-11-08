using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksActiveDocSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksBoardSpace
{
    abstract class AbstractSolidworksBody
    {
        protected SketchManager sketchManager;
        protected ModelDoc2 modelDoc;
        protected Feature absFeature;
        protected FeatureManager featureManager;

        public Body Body
        {get; private set;}

        abstract protected void BuildBody();

        private void Initializaion()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
            this.absFeature = (Feature)this.sketchManager.ActiveSketch;
        }

        private void CreateContour()
        {
            this.InsertPlane();
            this.absFeature.Select2(false, 0);
            this.sketchManager.InsertSketch(true);
            this.modelDoc.ClearSelection();
            new SolidworksContour(this.Body.contour);
            this.sketchManager.InsertSketch(true);
        }

        private void InsertPlane()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
                if (swFeat.GetTypeName() == "RefPlane" && ((double[])((RefPlane)swFeat.GetSpecificFeature2()).Transform.ArrayData).SequenceEqual(new double[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 }))        //Слабое место ("Спереди"), нужно по-другому определить базовую плоскость
                    swFeat.Select2(false, 0);

            this.absFeature = this.featureManager.InsertRefPlane((int)swRefPlaneReferenceConstraints_e.swRefPlaneReferenceConstraint_Distance, this.Body.originHeight / 1000, 0, 0, 0, 0);
            this.modelDoc.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swDisplayPlanes, false);
            this.modelDoc.ClearSelection();
        }

        public AbstractSolidworksBody(Body body)
        {
            this.Body = body;
            this.Initializaion();
            this.CreateContour();
            this.BuildBody();
            this.modelDoc.ClearSelection();
        }
    }
}
