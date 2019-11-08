using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using System.Linq;

namespace FlexBoardSolidworks
{
    enum RefPlanesNumeration
    {
        frontPlane = (int)swRefPlaneReferenceIndex_e.swRefPlaneReference_First,
        topPlane = (int)swRefPlaneReferenceIndex_e.swRefPlaneReference_Second,
        profilePlane = (int)swRefPlaneReferenceIndex_e.swRefPlaneReference_Third
    };

    class SolidworksComponentPrimitives
    {
        private ModelDoc2 modelDoc;
        private List<Feature> componentFeatures;
        private Component2 component;

        public RefPlane FrontFace
        { get; private set; }

        public RefPlane TopFace
        { get; private set; }

        public RefPlane RightFace
        { get; private set; }

        private void FeatureTree(Feature feature)
        {
            //Console.WriteLine(feature.GetTypeName2() + "\t" + feature.Name);
            this.componentFeatures.Add(feature);
            for (Feature swSubFeat = (Feature)feature.GetFirstSubFeature(); swSubFeat != null; swSubFeat = swSubFeat.GetNextSubFeature())
                this.FeatureTree(swSubFeat);
        }

        private void GetChildFeatures()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = swFeat.GetNextFeature())
                this.FeatureTree(swFeat);
        }

        private void Initialization()
        {
            this.modelDoc = (ModelDoc2)this.component.GetModelDoc();
            this.componentFeatures = new List<Feature>();
        }

        public string GetTopPlaneName()
        {
            Feature[] refPlanes = this.componentFeatures.Where(x => x.GetTypeName2().Equals("RefPlane")).ToArray();
            return refPlanes[(int)swRefPlaneReferenceIndex_e.swRefPlaneReference_Second].Name;
            /*Entity entity = (Entity)refPlanes[(int)swRefPlaneReferenceIndex_e.swRefPlaneReference_Second];
            entity.Select(true);*/
        }

        public void SelectFrontPlane(Component2 root)
        {
            Feature[] refPlanes = this.componentFeatures.Where(x => x.GetTypeName2().Equals("RefPlane")).ToArray();
            Feature frontPlane = refPlanes[(int)RefPlanesNumeration.topPlane];
            ((Entity)this.component.FeatureByName(frontPlane.Name)).Select(true);
            /*ModelDoc2 modelDoc = (ModelDoc2)root.GetModelDoc2();
            modelDoc.Extension.SelectByID2(frontPlane.Name + "@" + this.component.Name2 + "@" + root.Name2, "PLANE", 0, 0, 0, true, -1, null, (int)swSelectOption_e.swSelectOptionDefault);*/
        }

        public void SelectTopPlane(Component2 root)
        {
            Feature[] refPlanes = this.componentFeatures.Where(x => x.GetTypeName2().Equals("RefPlane")).ToArray();
            Feature frontPlane = refPlanes[(int)RefPlanesNumeration.frontPlane];
            ((Entity)this.component.FeatureByName(frontPlane.Name)).Select(true);
        }

        public void SelectGeneralAxis(Component2 root)
        {
            Feature axis = this.componentFeatures.Where(x => x.Name.Equals("Ось1") && x.GetTypeName2().Equals("RefAxis")).FirstOrDefault();
            if (axis == null)
                return;
            ((Entity)this.component.FeatureByName(axis.Name)).Select(true);
            //((Entity)axis).Select(true);
            /*ModelDoc2 modelDoc = (ModelDoc2)root.GetModelDoc2();
            modelDoc.Extension.SelectByID2(axis.Name + "@" + this.component.Name2 + "@" + root.Name2, "AXIS", 0, 0, 0, true, -1, null, (int)swSelectOption_e.swSelectOptionDefault);*/
        }

        /*public string GetOriginalPointName()
        {
            Feature feature = this.componentFeatures.Where(x => x.GetTypeName2().Equals("OriginProfileFeature")).First();
            return "Point1" + "@" + feature.Name;
            ISketch sketch = feature.GetSpecificFeature2();
            object[] points = sketch.GetSketchPoints2();
            ISketchPoint point = (ISketchPoint)points[0];
            point.Select(true);
        }*/

        /*public void SelectOriginalPoint(SelectionMgr modelContextSelectManager)
        {
            Feature origin = this.componentFeatures.Where(x => x.GetTypeName2().Equals("OriginProfileFeature")).First();
            //((Entity)feature).Select(true);
            Feature originPoint = this.component.FeatureByName(origin.Name);
            originPoint.Select(true);
            ISketch sketch = origin.GetSpecificFeature2();
            object[] points = sketch.GetSketchPoints2();
            ISketchPoint point = (ISketchPoint)points[0];
            SelectData swSelData = modelContextSelectManager.CreateSelectData();
            bool status = point.Select4(true, swSelData);
        }*/

        public void SelectOriginalPoint(Component2 root)
        {
            Feature origin = this.componentFeatures.Where(x => x.GetTypeName2().Equals("OriginProfileFeature")).First();
            //((Entity)this.component.FeatureByName(origin.Name)).Select(true);
            ModelDoc2 modelDoc = (ModelDoc2) root.GetModelDoc2();
            modelDoc.Extension.SelectByID2("Point1" + "@" + origin.Name + "@" + this.component.Name2 + "@" + root.Name2, "EXTSKETCHPOINT", 0, 0, 0, true, -1, null, (int)swSelectOption_e.swSelectOptionDefault);
        }

        /*public void SelectOriginalPoint(ModelDoc2 swAssemblyDoc)
        {
            Feature origin = this.componentFeatures.Where(x => x.GetTypeName2().Equals("OriginProfileFeature")).First();
            SelectionMgr selectMgr = (SelectionMgr)swAssemblyDoc.SelectionManager;
            swAssemblyDoc.Extension.SelectByID2("Point1" + "@" + origin.Name + "@" + this.component.Name2 + "@" + swAssemblyDoc.Name, "EXTSKETCHPOINT", 0, 0, 0, true, -1, null, (int)swSelectOption_e.swSelectOptionDefault);
        }*/

        /*public SketchPoint GetOriginalPoint()
        {
            Feature origin = this.componentFeatures.Where(x => x.GetTypeName2().Equals("OriginProfileFeature")).First();
            //((Entity)feature).Select(true);
            /*Feature originPoint = this.component.FeatureByName(origin.Name);
            originPoint.Select(true);
            
            Sketch sketch = origin.GetSpecificFeature2();
            object[] points = sketch.GetSketchPoints2();
            SketchPoint point = (SketchPoint)points[0];
            return point;*/
            /*SelectData swSelData = (SelectData)this.modelDoc.SelectionManager.CreateSelectData();
            bool status = point.Select4(true, swSelData);
        }*/

        public SolidworksComponentPrimitives(Component2 component)
        {
            this.component = component;
            this.Initialization();
            this.GetChildFeatures();
        }

        
    }
}
