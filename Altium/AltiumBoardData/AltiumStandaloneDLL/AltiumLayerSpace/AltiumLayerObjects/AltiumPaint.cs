using System;
using BoardSpace;
using PCB;
using System.Collections.Generic;

internal class AltiumPaint : IPaint
{
    private IPCB_Board iPcbBoard;
    private V7_LayerBase layerBase;
    private TV6_LayerSet layerSet;

    public AltiumPaint(IPCB_Board iPcbBoard, V7_LayerBase layerBase)
    {
        this.iPcbBoard = iPcbBoard;
        this.layerBase = layerBase;
        this.Initialization();
    }

    IPad[] IPaint.GetPads()
    {
        List<IPad> iPads = new List<IPad>();
        IPCB_BoardIterator iterator = this.iPcbBoard.BoardIterator_Create();
        iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.ePadObject));
        iterator.AddFilter_LayerSet(this.layerSet);
        iterator.AddFilter_Method(TIterationMethod.eProcessAll);
        for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
            iPads.Add(new AltiumLayerPad((IPCB_Pad)pcbObject));
        this.iPcbBoard.BoardIterator_Destroy(ref iterator);
        return iPads.ToArray();
    }

    IPolygon[] IPaint.GetPolygons()
    {
        List<IPolygon> pcbPolyGons = new List<IPolygon>();
        IPCB_BoardIterator iterator = this.iPcbBoard.BoardIterator_Create();
        iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.ePolyObject));
        iterator.AddFilter_LayerSet(this.layerSet);
        iterator.AddFilter_Method(TIterationMethod.eProcessAll);
        for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
            pcbPolyGons.Add(new AltiumLayerPolygon((IPCB_Polygon)pcbObject));
        this.iPcbBoard.BoardIterator_Destroy(ref iterator);
        return pcbPolyGons.ToArray();
    }

    IText[] IPaint.GetTexts()
    {
        List<IText> pcbTexts = new List<IText>();
        IPCB_BoardIterator iterator = this.iPcbBoard.BoardIterator_Create();
        iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.eTextObject));
        iterator.AddFilter_LayerSet(this.layerSet);
        iterator.AddFilter_Method(TIterationMethod.eProcessAll);
        for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
            pcbTexts.Add(new AltiumLayerText((IPCB_Text)pcbObject));
        this.iPcbBoard.BoardIterator_Destroy(ref iterator);
        return pcbTexts.ToArray();
    }

    ITracks[] IPaint.GetTracks()
    {
        AltiumTracksArray altiumTracksArray = new AltiumTracksArray();
        IPCB_BoardIterator iterator = this.iPcbBoard.BoardIterator_Create();
        iterator.AddFilter_ObjectSet(new PCB.TObjectSet(PCB.TObjectId.eArcObject, PCB.TObjectId.eTrackObject));
        iterator.AddFilter_LayerSet(this.layerSet);
        iterator.AddFilter_Method(TIterationMethod.eProcessAll);
        for (IPCB_Primitive pcbObject = iterator.FirstPCBObject(); pcbObject != null; pcbObject = iterator.NextPCBObject())
            altiumTracksArray.AddPrimitive(pcbObject);
        this.iPcbBoard.BoardIterator_Destroy(ref iterator);
        return altiumTracksArray.GetITracks();
    }

    private void Initialization()
    {
        this.layerSet = new TV6_LayerSet() { this.layerBase.DEBUGV6LAYER };
    }
}