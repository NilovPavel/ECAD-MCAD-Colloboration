using SearchESKD;
using SolidWorksAssemblySpace;
using System.Collections.Generic;
using System.Linq;
using System;
using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using FlexBoardSolidworks.SolidworksVariants.SolidworksComponent;
using System.Collections;

namespace SolidworksVariants
{
    class SolidWorksVariants
    {
        private SldWorks swApp;
        private SolidworksAssembly solidworksAssembly;
        private VariantCAD variantCAD;
        private ISearchESKD isearch;
        private Dictionary<ComponentCAD,Component2> conformity;

        public List<ComponentCAD> TreeList
        { get; private set; }

        private void Initialization()
        {
            this.swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            this.TreeList = new List<ComponentCAD>();
            this.isearch = new ProxySearchSolidworksPDM();
            this.conformity = new Dictionary<ComponentCAD, Component2>(new SolidworksTreeESKDComparer());
        }

        private void CalcCollection()
        {
            this.variantCAD.variant.ToList().ForEach(x => this.TreeList.AddRange(x.ComponentCAD));
            this.TreeList.RemoveAll(item => (new CorrectComponent(item)).IsUnCorrectElement);
            this.TreeList = this.TreeList.Distinct(new SolidworksTreeESKDComparer()).ToList();
            this.TreeList.Sort(new ESKDComparer());
            this.solidworksAssembly.SwAssembly.LargeDesignReviewTransparencyLevelEnabled = true;
        }

        private void AddGroupUniqueComponent(SolidworksModel solidworksModel, ComponentCAD uniqueComponent)
        {
            ModelDoc2 modelDoc = solidworksModel.GetModelDoc();
            if (modelDoc == null)
                return;
            IEqualityComparer<ComponentCAD> comparerEKSD = new ESKDComparer();
            IEnumerable<ComponentCAD> collectionCurrentESKD = this.TreeList.Where(item => comparerEKSD.Equals(item, uniqueComponent));
            foreach (ComponentCAD currentESKDItem in collectionCurrentESKD)
            {
                SolidworksComponent componentInAssem = new SolidworksComponent(currentESKDItem);
                componentInAssem.InsertComponent(modelDoc, this.solidworksAssembly);
                this.conformity.Add(currentESKDItem, componentInAssem.Component);
            }
            solidworksModel.Close();
        }

        private void AddComponents()
        {
            IEqualityComparer<ComponentCAD> comparerEKSD = new ESKDComparer();
            IEnumerable<ComponentCAD> uniqueESKDComponentsCAD = this.TreeList.Distinct(comparerEKSD);
            foreach (ComponentCAD uniqueESKDComponentCAD in uniqueESKDComponentsCAD)
            {
                string path = this.isearch.GetPath(uniqueESKDComponentCAD.dataESKD);
                if (string.IsNullOrEmpty(path))
                    continue;
                SolidworksModel solidworksModel = new SolidworksModel(path);
                this.AddGroupUniqueComponent(solidworksModel, uniqueESKDComponentCAD);
            }
        }

        public SolidWorksVariants(VariantCAD variantCAD, SolidworksAssembly solidworksAssembly)
        {
            this.variantCAD = variantCAD;
            this.solidworksAssembly = solidworksAssembly;
            this.Initialization();
            this.CalcCollection();
            this.AddComponents();
            this.AddConfigurations();
            this.CreateComponentVariants();
        }

        private void CreateComponentVariants()
        {
            this.variantCAD.variant.ToList().ForEach(eachVariant => new VariantExtinction(eachVariant, ref this.conformity, this.solidworksAssembly));
        }

        private void AddConfigurations()
        {
            this.variantCAD.variant.ToList().ForEach(variant => this.solidworksAssembly.SwAssembly.AddComponentConfiguration(variant.VariantName, string.Empty, string.Empty, 0));
        }
    }
}