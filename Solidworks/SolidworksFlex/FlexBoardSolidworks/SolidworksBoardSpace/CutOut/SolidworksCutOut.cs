using System;
using SolidWorks.Interop.sldworks;
using SolidworksActiveDocSpace;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using System.Linq;

namespace SolidworksBoardSpace
{
    class SolidworksCutOut
    {
        private Contour contour;
        private SketchManager sketchManager;
        private ModelDoc2 modelDoc;
        private Feature feature;
        private FeatureManager featureManager;

        private void Initializaion()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
            this.feature = (Feature)this.sketchManager.ActiveSketch;
        }

        private void CreateContour()
        {
            this.SelectPlane();
            this.sketchManager.AddToDB = true;
            this.sketchManager.InsertSketch(true);
            new SolidworksContour(this.contour);
            this.sketchManager.InsertSketch(true);
            this.sketchManager.AddToDB = false;
        }

        private void CreateContours()
        {
            this.SelectPlane();
            this.sketchManager.InsertSketch(true);
            new SolidworksContour(this.contour);
            this.sketchManager.InsertSketch(true);
        }

        private void SelectPlane()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
                if (swFeat.GetTypeName() == "RefPlane" && ((double[])((RefPlane)swFeat.GetSpecificFeature2()).Transform.ArrayData).SequenceEqual(new double[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 }))        //Слабое место ("Спереди"), нужно по-другому определить базовую плоскость
                    swFeat.Select2(false, 0);
        }

        public SolidworksCutOut(Contour contour)
        {
            this.contour = contour;
            this.Initializaion();
            this.CreateContour();
            this.CreateBody();
            this.modelDoc.ClearSelection();
        }

        private void CreateBody()
        {
            Feature feature = this.featureManager.FeatureCut3(true, false, true, (int)swEndConditions_e.swEndCondThroughNext, (int)swEndConditions_e.swEndCondUpToSurface, 1, 1, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
            /*if (feature == null)
                ;*/
        }
    }



    /*class SolidworksCutOut
    {
        private Contour contour;
        private SketchManager sketchManager;
        private ModelDoc2 modelDoc;
        private Feature feature;
        private FeatureManager featureManager;
        private List<Contour> contours;
        private string v;

        private void Initializaion()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
            this.feature = (Feature)this.sketchManager.ActiveSketch;
        }

        private void CreateContour()
        {
            this.SelectPlane();
            this.sketchManager.AddToDB = true;
            this.sketchManager.InsertSketch(true);
            new SolidworksContour(this.contour);
            this.sketchManager.InsertSketch(true);
            this.sketchManager.AddToDB = false;
        }

        private void CreateContours()
        {
            this.SelectPlane();
            //this.sketchManager.AddToDB = true;
            this.sketchManager.InsertSketch(true);
            new SolidworksContour(this.contour);
            this.sketchManager.InsertSketch(true);
            //this.sketchManager.AddToDB = false;
        }

        private void SelectPlane()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
                if (swFeat.GetTypeName() == "RefPlane" && swFeat.Name.Equals("Спереди"))        //Слабое место ("Спереди"), нужно по-другому определить базовую плоскость
                    swFeat.Select2(false, 0);
        }

        public SolidworksCutOut(Contour contour)
        {
            this.contour = contour;
            this.Initializaion();
            this.CreateContour();
            this.CreateBody();
            this.modelDoc.ClearSelection();
        }

        public SolidworksCutOut(List<Contour> contours, string v)
        {
            this.contours = contours;
            this.v = v;
            this.Initializaion();
            this.contours.ForEach(item => new SolidworksContour(item));
            this.CreateBody();
            this.modelDoc.ClearSelection();
        }

        private void CreateBody()
        {
            Feature feature = this.featureManager.FeatureCut3(true, false, true, (int)swEndConditions_e.swEndCondThroughNext, (int)swEndConditions_e.swEndCondUpToSurface, 1, 1, false, false, false, false, 0, 0, false, false, false, false, false, true, true, true, true, false, 0, 0, false);
        }
    }*/

    /*class SolidworksCutOut
    {
        private CutOut cutOut;
        protected SketchManager sketchManager;
        protected ModelDoc2 modelDoc;
        protected Feature feature;
        protected FeatureManager featureManager;

        protected void BuildBody()
        {
        }

        private void Initializaion()
        {
            SolidworksActiveDoc swActiveDoc = new SolidworksActiveDoc();
            this.modelDoc = swActiveDoc.ModelDoc;
            this.sketchManager = swActiveDoc.SketchManager;
            this.featureManager = swActiveDoc.FeatureManager;
            this.feature = (Feature)this.sketchManager.ActiveSketch;
        }

        private void CreateContour()
        {
            this.SelectPlane();
            this.sketchManager.InsertSketch(true);
            //this.modelDoc.ClearSelection();
            new SolidworksContour(this.cutOut.contour);
            this.sketchManager.InsertSketch(true);
        }

        private void SelectPlane()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
                if (swFeat.GetTypeName() == "RefPlane" && swFeat.Name.Equals("Спереди"))        //Слабое место ("Спереди"), нужно по-другому определить базовую плоскость
                    swFeat.Select2(false, 0);

            //this.modelDoc.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swDisplayPlanes, false);
        }

        public SolidworksCutOut(CutOut cutOut)
        {
            this.cutOut = cutOut;
            this.Initializaion();
            this.CreateContour();
            this.BuildBody();
            this.modelDoc.ClearSelection();
        }
    }*/
}