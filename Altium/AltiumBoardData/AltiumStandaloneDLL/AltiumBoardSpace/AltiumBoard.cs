using AltiumLayerSpace;
using DXP;
using EDP;
using Help;
using PCB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace BoardSpace
{
    class AltiumBoard : IBoardCAD
    {
        private IPCB_Board board;
        private IClient client;
        private List<IPCB_Region> regionAltium;
        private AltiumLayerStackManager altiumLayerStackManager;
        private AltiumContour boardOutLineContour;

        public AltiumLayerStackManager AltiumLayerStackManager
        { get { return this.altiumLayerStackManager; } }

        public Point PointMin
        { get; private set; }

        public Point PointMax
        { get; private set; }

        private void Initialization()
        {
            this.regionAltium = new List<IPCB_Region>();
            AltiumHelper.SetBoard(this);
        }

        public AltiumBoard(IDocument document)
        {
            this.board = this.GetBoardFromDocumentPCB(document);
            this.Initialization();
            this.CalcOrigin();
            this.GetRegions();
        }

        public AltiumBoard(IPCB_Board board)
        {
            this.board = board;
            this.Initialization();
            this.CalcOrigin();
            this.GetRegions();
        }

        private IPCB_Board GetBoardFromDocumentPCB(IDocument document)
        {
            this.client = DXP.GlobalVars.Client;
            if (!this.client.StartServer("PCB"))
                MessageBox.Show("Servermodule PCB is not run", "Don't can read board!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            IServerModule serverModule = this.client.GetServerModuleByName("PCB");
            this.client.OpenDocumentShowOrHide("PCB", document.DM_FullPath(), false);
            IPCB_ServerInterface pcbServerInterface = (IPCB_ServerInterface)serverModule;
            return pcbServerInterface.GetPCBBoardByPath(document.DM_FullPath());
        }

        private void GetBoardOutline()
        {
            this.boardOutLineContour = new AltiumContour(this.board.GetState_BoardOutline());
        }

        private void CalcOrigin()
        {
            double minX = 0, minY = 0, maxX = 0, maxY = 0;
            IContour icontour = new AltiumContour(this.board.GetState_BoardOutline());
            Contour contour = new Contour(icontour);
            contour.GetExtremums(ref minX, ref minY, ref maxX, ref maxY);
            this.PointMin = new Point { X = minX, Y = minY };
            this.PointMax = new Point { X = maxX, Y = maxY };
        }

        public List<IPCB_Component> GetBoardComponents()
        {
            List<IPCB_Component> pcbComponents = new List<IPCB_Component>();
            IPCB_BoardIterator iterator = board.BoardIterator_Create();
            iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.eComponentObject));
            iterator.AddFilter_LayerSet(PCBConstant.V6AllLayersSet);
            iterator.AddFilter_Method(TIterationMethod.eProcessAll);
            for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
                pcbComponents.Add((IPCB_Component)pcbObject);
            this.board.BoardIterator_Destroy(ref iterator);
            return pcbComponents;
        }

        private void GetRegions()
        {
            IPCB_BoardIterator iterator = this.board.BoardIterator_Create();
            iterator.AddFilter_ObjectSet(new PCB.TObjectSet(TObjectId.eRegionObject));
            iterator.AddFilter_LayerSet(PCBConstant.V6AllLayersSet);
            iterator.AddFilter_Method(TIterationMethod.eProcessAll);
            for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
            {
                IPCB_BoardRegion region = (IPCB_BoardRegion)pcbObject;
                if (region.GetState_Kind() == TRegionKind.eRegionKind_BoardCutout)
                    this.regionAltium.Add(region);
            }
            this.board.BoardIterator_Destroy(ref iterator);
        }

        IBody[] IBoardCAD.GetBodies()
        {
            this.altiumLayerStackManager = new AltiumLayerStackManager(this.board.GetState_MasterStack());
            List<IBody> bodies = new List<IBody>();
            for (int i = 0; i < this.regionAltium.Count; i++)
                if (new AltiumHelper().isBoardRegion(this.regionAltium[i]))
                {
                    bodies.Add(new AltiumBody((IPCB_BoardRegion)this.regionAltium[i], ref this.altiumLayerStackManager));
                }

            return bodies.ToArray();
        }

        Point IBoardCAD.GetOrigin()
        {
            return new Point
            {
                X = new AltiumHelper().CoordToMMsX(this.board.GetState_XOrigin()),
                Y = new AltiumHelper().CoordToMMsY(this.board.GetState_YOrigin())
            };
        }

        IVia[] IBoardCAD.GetVias()
        {
            List<IVia> vias = new List<IVia>();
            IPCB_BoardIterator iterator = this.board.BoardIterator_Create();
            iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.eViaObject));
            iterator.AddFilter_LayerSet(PCB.PCBConstant.V6AllLayersSet);
            iterator.AddFilter_Method(PCB.TIterationMethod.eProcessAll);
            for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
                vias.Add(new AltiumVia((IPCB_Via)pcbObject));
            this.board.BoardIterator_Destroy(ref iterator);
            return vias.ToArray();
        }

        ICutOut[] IBoardCAD.GetCutouts()
        {
            List<IPCB_Region> boardRegions = this.regionAltium.FindAll(x => x.GetState_Kind() == TRegionKind.eRegionKind_BoardCutout);//new List<IPCB_BoardRegion>();
            List<ICutOut> cutes = new List<ICutOut>();
            for (int i = 0; i < boardRegions.Count; i++)
                if (!new AltiumHelper().isBoardRegion(this.regionAltium[i]))
                    cutes.Add(new AltiumCutout(this.regionAltium[i]));
            return cutes.ToArray();
        }

        IHolePad[] IBoardCAD.GetHolePads()
        {
            List<IHolePad> iPads = new List<IHolePad>();
            IPCB_BoardIterator iterator = this.board.BoardIterator_Create();
            iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.ePadObject));
            iterator.AddFilter_LayerSet(PCB.PCBConstant.V6AllLayersSet);
            iterator.AddFilter_Method(PCB.TIterationMethod.eProcessAll);
            for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
                iPads.Add(new AltiumHolePad((IPCB_Pad2)pcbObject));
            iPads.RemoveAll(x => x.GetIContour() == null);
            this.board.BoardIterator_Destroy(ref iterator);
            return iPads.ToArray();
        }
    }
}