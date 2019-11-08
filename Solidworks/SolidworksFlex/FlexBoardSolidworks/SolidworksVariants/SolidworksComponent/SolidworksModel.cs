using System;
using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using SolidWorks.Interop.swconst;
using System.IO;

namespace SolidworksVariants
{
    class SolidworksModel
    {
        private SldWorks swApp;
        private string path;
        private ModelDoc2 modelDoc;

        public SolidworksModel(string path)
        {
            this.path = path;
            this.Initialization();
        }

        private void Initialization()
        {
            this.swApp = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
        }

        public void Close()
        {
            this.swApp.CloseDoc(this.modelDoc.GetPathName());
        }

        public ModelDoc2 GetModelDoc()
        {
            int errors = 0, warnings = 0;
            int type = (int)swDocumentTypes_e.swDocPART;
            switch (Path.GetExtension(path).ToLower())
            {
                case ".sldprt":
                    type = (int)swDocumentTypes_e.swDocPART;
                    break;
                case ".sldasm":
                    type = (int)swDocumentTypes_e.swDocASSEMBLY;
                    break;
            }
            this.modelDoc = this.swApp.OpenDoc6(path, type, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            return this.modelDoc;
        }
    }
}