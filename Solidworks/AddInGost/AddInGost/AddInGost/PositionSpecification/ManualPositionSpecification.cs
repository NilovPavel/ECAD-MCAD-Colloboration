using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PositionSpecification
{
    class ManualPositionSpecification : AbstractPositions
    {
        private SelectionMgr selectManager;
        private Mouse mouse;

        public ManualPositionSpecification(ModelDoc2 swModel, Assembly assembly) : base(swModel, assembly)
        {
        }

        protected override void SetConcreteBehavior()
        {
            this.mouse = ((ModelView)this.swModel.ActiveView).GetMouse();
            this.selectManager = this.swModel.SelectionManager;
            this.AttachSWEvents();
        }

        private void AttachSWEvents()
        {
            this.drawing.UserSelectionPostNotify += this.clickDrawingComponent;
            this.mouse.MouseRBtnDownNotify += this.rightMouseClick;
        }

        private int rightMouseClick(int X, int Y, int WParam)
        {
            this.drawing.UserSelectionPostNotify -= this.clickDrawingComponent;
            this.mouse.MouseRBtnDownNotify -= this.rightMouseClick;
            return 0;
        }

        private Component2 GetCurrentComponentFromDrawing()
        {
            DrawingComponent dc = this.selectManager.GetSelectedObjectsComponent4(1, -1);
            if (this.selectManager.GetSelectedObjectCount2(-1) == 2)
                dc = this.selectManager.GetSelectedObjectsComponent4(2, -1); ;
            Component2 component = dc == null ? null : (Component2)dc.Component;
            return component;
        }

        private int clickDrawingComponent()
        {
            SolidWorks.Interop.sldworks.View activeDrawingView = this.GetActiveDrawingView();
            if (activeDrawingView == null)
                return 0;
            DrawingComponent currentRootComponent;
            string currentAssemblyConfigurationName = this.GetCurrentAssemblyConfiguration((SolidWorks.Interop.sldworks.View)this.drawing.ActiveDrawingView, out currentRootComponent);
            Component2 currentComponent = this.GetCurrentComponentFromDrawing();
            string position = this.GetPositionFromComponent(currentComponent, activeDrawingView);
            this.InsertPosition(currentComponent, position);
            return 0;
        }

        private void InsertPosition(Component2 currentComponent, string position)
        {
            ModelDocExtension swCurrentComponentExtension = ((ModelDoc2)currentComponent.GetModelDoc2()).Extension;
            double[] point = (double[])this.selectManager.GetSelectionPoint2(1, -1);

            BalloonOptions BomBalloonParams = this.GetBallonOptions(swCurrentComponentExtension);
            BomBalloonParams.LowerText = position;

            Note swNote = default(Note);
            swNote = (Note)swModel.Extension.InsertBOMBalloon2(BomBalloonParams);
            swNote.SetText(position);

            this.swModel.GraphicsRedraw2();
            this.swModel.ClearSelection2(true);
        }

        private SolidWorks.Interop.sldworks.View GetActiveDrawingView()
        {
            SolidWorks.Interop.sldworks.View activeDrawingView = this.drawing.ActiveDrawingView;

            //Вид становится равен null, если происходит автосохранение сборки
            if (activeDrawingView == null)
            {
                this.swModel.ForceRebuild3(true);
                activeDrawingView = this.drawing.ActiveDrawingView;
            }

            if (activeDrawingView == null)
                MessageBox.Show("DrawingView is not selected. Please, click to component in drawing again.", "DrawingView is null", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return activeDrawingView;
        }
    }
}
