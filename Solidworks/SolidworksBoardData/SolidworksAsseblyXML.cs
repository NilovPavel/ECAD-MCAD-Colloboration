using IAssembly;
using Loadings;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksAssemblySpace;
using SolidworksBoardData.SolidworksVariantCAD.SolidworksComponent.Harness;
using System;
using System.Runtime.InteropServices;

namespace SolidworksBoardData
{
    public class SolidworksAsseblyXML : IAssemblyCAD
    {
        private string filePath;
        private SldWorks app;
        private ModelDoc2 assemblyModelDoc;

        private void Initialization()
        {
            this.app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            this.assemblyModelDoc = this.GetAssemblyDoc();
        }

        private ModelDoc2 GetAssemblyDoc()
        {
            int errors = 0;
            ModelDoc2 modelDocumentAssembly = this.app.OpenDocSilent(this.filePath, (int)swDocumentTypes_e.swDocASSEMBLY, ref errors);
            return modelDocumentAssembly;
        }

        public SolidworksAsseblyXML(string filePath)
        {
            this.filePath = filePath;
            this.Initialization();
        }

        IBoardCAD IAssemblyCAD.GetIBoardCAD()
        {
            return default(IBoardCAD);
        }

        IHierarchyCAD IAssemblyCAD.GetIHierarchyCAD()
        {
            return default(IHierarchyCAD);
        }

        INotesCAD IAssemblyCAD.GetINotesCAD()
        {
            return new SolidworksNotesCAD(this.assemblyModelDoc);
        }

        IProjectPropertiesCAD IAssemblyCAD.GetIProjectPropertiesCAD()
        {
            return new SolidworksProjectProperties(this.assemblyModelDoc);
        }

        IVariantCAD IAssemblyCAD.GetIVariantCAD()
        {
            return new SpecificationVariantSolidworksCAD((AssemblyDoc)this.assemblyModelDoc);
        }

        IHarnessCAD IAssemblyCAD.GetIHarnessCAD()
        {
            IHarnessCAD iHarnessCAD = default(IHarnessCAD);
            SolidworksAddInLoader addInLoader = new SolidworksAddInLoader();
            if (addInLoader.LoadAddin("sldrtadd.dll") && ((AssemblyDoc)this.assemblyModelDoc).IsRouteAssembly())
                iHarnessCAD = new SolidworksHarness((AssemblyDoc)this.assemblyModelDoc);
            return iHarnessCAD;
        }
    }
}