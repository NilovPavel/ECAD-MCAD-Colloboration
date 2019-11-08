using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.SWRoutingLib;
using SolidworksBoardData.SolidworksVariantCAD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidworksBoardData
{
    class SpecificationVariantSolidworks : IVariant
    {
        private bool topLevel;
        private string variantName;
        private AssemblyDoc assemblyDoc;
        private ModelDoc2 assemblyModelDoc;
        private List<IComponentCAD> componentsCADCollection;
        private SpecificationVariantSolidworksCAD specificationVariantSolidworksCAD;

        public SpecificationVariantSolidworks(string variantName, AssemblyDoc assemblyDoc, SpecificationVariantSolidworksCAD specificationVariantSolidworksCAD)
        {
            this.variantName = variantName;
            this.assemblyDoc = assemblyDoc;
            this.specificationVariantSolidworksCAD = specificationVariantSolidworksCAD;
            this.Initialization();
            this.ReadCollectionCAD();
            this.CalcCollection();
        }

        private IComponentCAD GetComponentByDesignator(string designator)
        {
            IEnumerable<IComponentCAD> designatorElements = this.componentsCADCollection.Where(item => item.GetDesignator().Equals(designator));
            IComponentCAD component = designatorElements.Count() == 1
                ? designatorElements.FirstOrDefault()
                : designatorElements.Where(item => item.GetFitted()).FirstOrDefault()
                ?? designatorElements.FirstOrDefault();
            return component;
        }

        private List<IComponentCAD> GetDesignatorCollection()
        {
            string[] designators = this.componentsCADCollection.Where(item => !string.IsNullOrEmpty(item.GetDesignator())).Select(item => item.GetDesignator()).Distinct().ToArray();
            List<IComponentCAD> componentCollection = new List<IComponentCAD>();
            Array.ForEach(designators, item => componentCollection.Add(this.GetComponentByDesignator(item)));
            return componentCollection;
        }

        private List<IComponentCAD> GetWithoutDesignatorCollection()
        {
            return this.componentsCADCollection.Where(item => string.IsNullOrEmpty(item.GetDesignator())).ToList();
        }

        /*private void FeatureTree(Feature feature, int level)
        {
            for (int i = 0; i < level; i++)
                    Console.Write("-");
            Console.WriteLine(feature.Name + "\t" + feature.GetTypeName2());
            for (Feature swSubFeat = (Feature)feature.GetFirstSubFeature(); swSubFeat != null; swSubFeat = swSubFeat.GetNextSubFeature())
            {
                level++;
                this.FeatureTree(swSubFeat, level + 1);
            }
        }

        private void ReadComponent(Component2 component)
        {
            ModelDoc2 modelDoc = component.GetModelDoc2();
            RoutingComponentManager routManager = modelDoc.Extension.GetRoutingComponentManager();
            
            Console.WriteLine(component.Name2
                + "\t" + component.FirstFeature().GetTypeName2()
                + "\t" + ((swRouteComponentTypeID_e)routManager.GetComponentType()).ToString()
                + "\t" + routManager.GetRoutingComponentDescription()
                + "\t" + ((swRouteComponentTypeID_e)routManager.GetRouteComponentSubType()).ToString()
                + "\t" + ((swCPointConfig_e)routManager.GetCPointConfiguration()).ToString()
                );
            this.ReadComponent(component.GetModelDoc2());
        }

        private void ReadComponent(ModelDoc2 modelDoc)
        {
            for (Feature feature = modelDoc.FirstFeature(); feature != null; feature = (Feature)feature.GetNextFeature())
                this.FeatureTree(feature, 0);
        }*/

        private void RemoveCableFromComponents(List<Component2> components)
        {
            if (!this.assemblyDoc.IsRouteAssembly())
                return;
            components.RemoveAll(item => string.IsNullOrEmpty( item.ComponentReference));
        }

        private void ReadCollectionCAD()
        {
            ((ModelDoc2)this.assemblyDoc).ShowConfiguration2(variantName);
            List<Component2> components = ((object[])this.assemblyDoc.GetComponents(this.topLevel)).Cast<Component2>().ToList() ?? new List<Component2>();

            this.RemoveCableFromComponents(components);

            /*this.ReadComponent((ModelDoc2)this.assemblyDoc);
            IEnumerable<Component2> compons = components.Cast<Component2>();
            compons.ToList().ForEach(item => this.ReadComponent(item));
            Component2 componentCable = compons.Where(item => item.Name2.IndexOf("^") != -1).FirstOrDefault();
            this.ReadComponent(componentCable);*/

            Array.ForEach(components.ToArray(), item => this.componentsCADCollection.Add(new SolidworksComponentCAD(item)));

            List<IComponentCAD> montazhComponents = this.componentsCADCollection.Where(item =>
                                                            this.specificationVariantSolidworksCAD.ElectricalInstallationCollection
                                                            .Exists(id => id.Equals(item.GetUniqueID()))).ToList();
            montazhComponents.ForEach(x => x.GetIComponentData().SetSpecificationType(EskdSpecificationType.em));
        }

        public void CalcCollection()
        {
            List<IComponentCAD> designatorCollection = this.GetDesignatorCollection();
            List<IComponentCAD> withoutDesignatorCollection = this.GetWithoutDesignatorCollection();
            this.componentsCADCollection.Clear();
            this.componentsCADCollection.AddRange(designatorCollection);
            this.componentsCADCollection.AddRange(withoutDesignatorCollection);
        }

        private void Initialization()
        {
            this.topLevel = true;
            this.assemblyModelDoc = (ModelDoc2)this.assemblyDoc;
            this.componentsCADCollection = new List<IComponentCAD>();
        }

        IComponentCAD[] IVariant.GetCollection()
        {
            return this.componentsCADCollection.ToArray();
        }

        string IVariant.GetVariantName()
        {
            return this.variantName;
        }

        /*~SpecificationVariantSolidworks()
        {
            Console.WriteLine("Run variant destructor!");
            this.componentsCADCollection.Clear();
            this.componentsCADCollection = null;
        }*/
    }
}