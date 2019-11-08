using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksActiveDocSpace
{
    class SolidworksActiveDoc
    {
        public SldWorks App
        { get; private set; }

        public ModelDoc2 ModelDoc
        { get; private set; }

        public FeatureManager FeatureManager
        {
            get
            {
                return this.ModelDoc.FeatureManager;
            }
        }

        public SketchManager SketchManager
        {
            get
            {
                return this.ModelDoc.SketchManager;
            }
        }

        private void Initialization()
        {
            this.App = (SldWorks) Marshal.GetActiveObject("SldWorks.Application");
            this.ModelDoc = this.App.ActiveDoc;
            /*this.FeatureManager = this.ModelDoc.FeatureManager;
            this.SketchManager = this.ModelDoc.SketchManager;*/
        }

        public SolidworksActiveDoc()
        {
            this.Initialization();
        }
    }
}
