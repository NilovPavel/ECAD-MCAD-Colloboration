using PCB;
using System.Collections.Generic;
using System;
using AltiumLayerSpace;

namespace BoardSpace
{
    class AltiumBody : IBody
    {
        private IPCB_BoardRegion iPCB_BoardRegion;
        private IPCB_LayerStack ilayerStack;
        private AltiumLayerStack layerStack;
        private AltiumLayerStackManager altiumLayerStackManager;
        private IContour iAltiumContour;

        private void Initialization()
        {
            this.ilayerStack = this.iPCB_BoardRegion.GetState_LayerStack();
            this.layerStack = new AltiumLayerStack(this.ilayerStack);
            this.iAltiumContour = new AltiumContour(this.iPCB_BoardRegion);
        }

        bool IBody.GetIsFlex()
        {
            return this.ilayerStack.GetState_IsFlex();
        }

        double IBody.GetHeightFirstLayer()
        {
            return this.altiumLayerStackManager.GetHeightFirstLayer(this.layerStack.GetID());
        }

        double IBody.GetTotalHeight()
        {
            return this.altiumLayerStackManager.GetHeightByLayerStackID(this.layerStack.GetID());
        }

        IContour IBody.GetIContour()
        {
            return this.iAltiumContour;
        }

        IBendingLine[] IBody.GetBendingLines()
        {
            string parameters = string.Empty;
            ((IPCB_BoardRegion)this.iPCB_BoardRegion).Export_ToParameters(ref parameters);
            List<string> bendingLines = new List<string>(parameters.Split('|'));
            bendingLines.RemoveAll(x => x.IndexOf("BENDINGLINECOUNT") != -1);
            bendingLines.RemoveAll(x => x.IndexOf("BENDINGLINE")==-1);
            IBendingLine[] bendingLinesInterfaces = new IBendingLine[bendingLines.Count];
            for (int i = 0; i < bendingLines.Count; i++)
                bendingLinesInterfaces[i] = new AltiumBendingLineFromString(bendingLines[i], this.iAltiumContour);
            return bendingLinesInterfaces;
        }

        /*IBendingLine[] IBody.GetBendingLines()
        {
            //int bendingLineCount = ((IPCB_BoardRegion)this.iPCB_BoardRegion).GetState_BendingLinesCount();
            IBendingLine[] bendingLinesInterfaces = new IBendingLine[((IPCB_BoardRegion)this.iPCB_BoardRegion).GetState_BendingLinesCount()];
            for (int i = 0; i < bendingLinesInterfaces.Length; i++)
                bendingLinesInterfaces[i] = new AltiumBendingLine(((IPCB_BoardRegion)this.iPCB_BoardRegion).GetBendingLines(i));
            return bendingLinesInterfaces;
        }*/

        public AltiumBody(IPCB_BoardRegion iPCB_BoardRegion, ref AltiumLayerStackManager altiumLayerStackManager)
        {
            this.iPCB_BoardRegion = iPCB_BoardRegion;
            this.altiumLayerStackManager = altiumLayerStackManager;
            this.Initialization();
        }

        public void Unapproximation(IContour boardOutLineContour)
        {
            this.iAltiumContour = new AltiumUnapproximationContour(this.iAltiumContour, boardOutLineContour);
        }
    }
}
