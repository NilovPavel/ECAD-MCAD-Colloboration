using BoardBuilder.KompasAssemblySpace;
using Kompas6API7;
using SearchESKD;
using BoardBuilder.KompasSearch;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using KompasComponentSpace;
using KompasComparersSpace;
using BoardBuilder.KompasVariantsSpace.KompasComponentSpace.KompasCoordinatSpace;
using Kompas6Constants3D;
using System;
using KompasVariantsSpace;

namespace BoardBuilder.KompasVariantsSpace
{
    class KompasVariants
    {
        private IApplication kompasApp;
        private VariantCAD variantCAD;
        private KompasAssembly kompasAssembly;
        private ISearchESKD iSearch;
        private Dictionary<ComponentCAD, IPart7> conformity;

        public List<ComponentCAD> TreeList
        { get; private set; }

        private void Initialization()
        {
            this.kompasApp = (IApplication)Marshal.GetActiveObject("KOMPAS.Application.7");
            this.TreeList = new List<ComponentCAD>();
            this.iSearch = new ProxySearch(new FileSystemKompasSearch());
            this.conformity = new Dictionary<ComponentCAD, IPart7>(new KompasTreeESKDComparer());
        }

        public KompasVariants(VariantCAD variantCAD, KompasAssembly kompasAssembly)
        {
            this.variantCAD = variantCAD;
            this.kompasAssembly = kompasAssembly;
            this.Initialization();
            this.CalcCollection();
            this.AddComponents();
            //if (this.variantCAD.variant.Length > 1)
            this.AddConfigurations();
            this.CreateComponentVariants();

        }

        private void CreateComponentVariants()
        {
            this.variantCAD.variant.ToList().ForEach(eachVariant => new VariantExtinction(eachVariant, ref this.conformity, this.kompasAssembly));
        }

        private void AddConfigurations()
        {
            IEmbodimentsManager embodimentsManager = (IEmbodimentsManager)this.kompasAssembly.AssemblyDocument;
            this.variantCAD.variant.ToList().ForEach(variant => 
            embodimentsManager.AddEmbodiment(1, false, variant.VariantName, null, null));
            embodimentsManager.DeleteEmbodiment(0);
        }

        private void AddComponents()
        {
            foreach (ComponentCAD componentCAD in this.TreeList)
            {
                string path = this.iSearch.GetPath(componentCAD.dataESKD);
                if (string.IsNullOrEmpty(path))
                    continue;

                Part7 part = this.kompasAssembly.AssemblyDocument.TopPart.Parts.AddFromFile(path, false, true);
                part.Name = componentCAD.dataESKD.PartNumber;
                this.conformity.Add(componentCAD, part);

                KompasCoordinats kompasCoordinats = new KompasCoordinats(part);
                kompasCoordinats.Transform(componentCAD.coordinats);
                
                this.kompasAssembly.AssemblyDocument.TopPart.Update();
            }
        }

        private void CalcCollection()
        {
            this.variantCAD.variant.ToList().ForEach(x => this.TreeList.AddRange(x.ComponentCAD));
            this.TreeList.RemoveAll(item => (new CorrectComponent(item)).IsUnCorrectElement);
            this.TreeList = this.TreeList.Distinct(new KompasTreeESKDComparer()).ToList();
            this.TreeList.Sort(new ESKDComparer());
        }
    }
}
