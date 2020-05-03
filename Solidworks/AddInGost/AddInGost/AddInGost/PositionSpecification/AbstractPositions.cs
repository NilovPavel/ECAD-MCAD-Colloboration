using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PositionSpecification
{
    abstract class AbstractPositions
    {
        private Assembly assembly;
        protected ModelDoc2 swModel;
        protected DrawingDoc drawing;
        protected AssemblyDoc currentAssemblyDoc;
        protected DrawingComponent currentRootDrawingComponent;

        public AbstractPositions(ModelDoc2 swModel, Assembly assembly)
        {
            this.swModel = swModel;
            this.assembly = assembly;
            this.Intialization();
            if (this.drawing == null)
                return;
            this.SetConcreteBehavior();
        }

        protected BalloonOptions GetBallonOptions(ModelDocExtension swDocDocExt)
        {
            BalloonOptions BomBalloonParams;
            BomBalloonParams = ((BalloonOptions)(swDocDocExt.CreateBalloonOptions()));
            BomBalloonParams.Style = (int)swBalloonStyle_e.swBS_None;
            BomBalloonParams.UpperTextContent = (int)swBalloonTextContent_e.swBalloonTextCustom;
            return BomBalloonParams;
        }

        private string GetCorrectComponentName(string fullName, DrawingComponent rootComponent)
        {
            string rootComponentName = rootComponent.Component.Name2;
            string componentName = fullName;
            string[] splitComponentName = componentName.Contains('/') ? componentName.Split('/') : new string[] { componentName };
            componentName = splitComponentName.Length > 1 && splitComponentName[0].Contains(rootComponentName) ? splitComponentName[1] : splitComponentName[0];
            return componentName;
        }

        protected string GetPositionFromComponent(Component2 currentComponent, SolidWorks.Interop.sldworks.View currentView)
        {
            string position = "[N/A]";
            string assemblyConfigurationName = this.GetCurrentAssemblyConfiguration(currentView, out currentRootDrawingComponent);
            if(this.currentRootDrawingComponent.Component.Equals(currentComponent) || currentComponent == null)
                return position;
            string componentFirstLevelIdentifer = this.GetCorrectComponentName(currentComponent.Name2, this.currentRootDrawingComponent);
            position = this.GetElementPosition(componentFirstLevelIdentifer, assemblyConfigurationName).ToString();
            return position;
        }

        private int GetElementPosition(string componentFirstLevelIdentifer, string assemblyConfigurationName)
        {
            Variant variant = this.assembly.variantCAD.variant.Where(variantItem => variantItem.VariantName.Equals(assemblyConfigurationName)).FirstOrDefault();
            if (variant == null)
                return 0;
            ComponentCAD componentCAD = variant.ComponentCAD.Where(componentItem => componentItem.UniqueID.Equals(componentFirstLevelIdentifer)).FirstOrDefault();
            if (componentCAD == null)
                return 0;
            return componentCAD.dataESKD.Позиция;
        }

        protected string GetCurrentAssemblyConfiguration(SolidWorks.Interop.sldworks.View activeDrawingView, out DrawingComponent currentRootComponent)
        {
            currentRootComponent = activeDrawingView.RootDrawingComponent;
            ModelDoc2 assemblyComp = (ModelDoc2)currentRootComponent.Component.GetModelDoc2();
            this.currentAssemblyDoc = assemblyComp.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY ? (AssemblyDoc)assemblyComp : null;
            if (activeDrawingView.IsLightweight())
                activeDrawingView.SetLightweightToResolved();
            Configuration configuration = assemblyComp.GetActiveConfiguration();
            return configuration.Name;
        }

        abstract protected void SetConcreteBehavior();

        private void Intialization()
        {
            bool its_drw = this.swModel.GetType().Equals((int)swDocumentTypes_e.swDocDRAWING);
            if (!its_drw)
                MessageBox.Show("Данный документ не является чертежом!", "Ошибка инициализации", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            else this.drawing = (DrawingDoc)this.swModel;
        }
    }
}
