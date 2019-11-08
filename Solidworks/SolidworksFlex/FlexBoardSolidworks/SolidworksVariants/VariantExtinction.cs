using System.Collections.Generic;
using SolidWorks.Interop.sldworks;
using SolidWorksAssemblySpace;
using System.Linq;
using SolidWorks.Interop.swconst;
using System;

namespace SolidworksVariants
{
    class VariantExtinction
    {
        private Variant eachVariant;
        private SolidworksAssembly solidworksAssembly;
        private Dictionary<ComponentCAD, Component2> conformity;
        private Dictionary<Component2, string> componentConfiguration;
        private ModelDoc2 modelDoc;
        private SelectionMgr selectionManager;

        private void Initialization()
        {
            this.componentConfiguration = new Dictionary<Component2, string>();
            this.modelDoc = ((ModelDoc2)this.solidworksAssembly.SwAssembly);
            this.modelDoc.ClearSelection2(true);
            this.modelDoc.ShowConfiguration2(this.eachVariant.VariantName);
            this.selectionManager = this.modelDoc.ISelectionManager;
        }

        private void SetFootprints()
        {
            foreach (KeyValuePair<Component2, string> item in this.componentConfiguration)
            {
                this.modelDoc.ClearSelection2(true);
                bool status = this.modelDoc.Extension.SelectByID2(string.Concat(item.Key.Name2, "@", this.modelDoc.GetTitle()), "COMPONENT", 0, 0, 0, false, 0, null, 0);
                Component2 component = this.selectionManager.GetSelectedObject6(1, -1);
                if(!component.ReferencedConfiguration.Equals(item.Value))
                    component.ReferencedConfiguration = item.Value;
                this.modelDoc.ClearSelection2(true);
            }
            this.modelDoc.ClearSelection2(true);
            this.solidworksAssembly.SwAssembly.EditRebuild();
        }

        private void Extinction()
        {
            foreach (KeyValuePair<ComponentCAD, Component2> componentPair in this.conformity)
            {
                List<ComponentCAD> fittedAndEqDesign = this.eachVariant.ComponentCAD.Where(x => !string.IsNullOrEmpty(x.Designator)).Where(x => x.Designator.Equals(componentPair.Key.Designator) && x.Fitted == true).ToList();
                if (!fittedAndEqDesign.Contains(componentPair.Key, new SolidworksTreeESKDComparer()))
                    this.modelDoc.Extension.SelectByID2(string.Concat(componentPair.Value.Name2, "@", this.modelDoc.GetTitle()), "COMPONENT", 0, 0, 0, true, 0, null, 0);
                else
                {
                    ComponentCAD componentCAD = fittedAndEqDesign.FirstOrDefault();
                    this.componentConfiguration.Add(componentPair.Value, componentCAD.Configuration);
                }
            }

            this.modelDoc.EditSuppress2();
            this.modelDoc.ClearSelection2(true);
        }

        public VariantExtinction(Variant eachVariant, ref Dictionary<ComponentCAD, Component2> conformity, SolidworksAssembly solidworksAssembly)
        {
            this.eachVariant = eachVariant;
            this.conformity = conformity;
            this.solidworksAssembly = solidworksAssembly;
            this.Initialization();
            this.Extinction();
            this.SetFootprints();
        }
    }
}