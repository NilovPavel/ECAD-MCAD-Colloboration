using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlexBoardSolidworks
{
    class CloseOpenDocuments
    {
        private SldWorks app;

        private void Close()
        {
            this.app.CloseAllDocuments(true);
            ModelDoc2 modelDoc;
            while ((modelDoc = this.app.ActiveDoc) != null)
            {
                this.app.CloseDoc(modelDoc.GetPathName());
                modelDoc = null;
            }
        }

        public CloseOpenDocuments()
        {
            this.app = (SldWorks) Marshal.GetActiveObject("SldWorks.Application");
            this.Close();
        }
    }
}
