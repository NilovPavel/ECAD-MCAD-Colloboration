using System;
using BoardSpace;
using PCB;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

public class AltiumLayerBase : ILayer
{
    //private IPCB_Board altiumBoard;
    private V7_LayerBase layerBase;
    private AltiumBoard altiumBoard;
    private AltiumLayer altiumLayer;

    /*public AltiumLayerBase(IPCB_Board altiumBoard, V7_LayerBase layerBase)
    {
        this.altiumBoard = altiumBoard;
        this.layerBase = layerBase;
        this.Initialization();
    }*/

    public AltiumLayerBase(AltiumBoard altiumBoard, V7_LayerBase layerBase)
    {
        this.altiumBoard = altiumBoard;
        this.layerBase = layerBase;
        this.Initialization();
    }

    private void Initialization()
    {
        /*string a = string.Join("\r\n", this.altiumBoard.AltiumLayerStackManager.Layers.Select( itemLayer => itemLayer.LayerName() + itemLayer.GetThickness()));

        MessageBox.Show(a);*/

        this.altiumLayer = this.altiumBoard.AltiumLayerStackManager.Layers.
            Find(itemLayer => (int)itemLayer.Type == (int)this.layerBase.DEBUGV6LAYER);
    }

    /*string ILayerPCB.GetLayerName()
    {
        return this.layerBase.DEBUGV6LAYER.ToString();
    }*/

    string ILayer.GetLayerName()
    {
        return this.layerBase.DEBUGV6LAYER.ToString();
    }

    int ILayer.GetLayerNumber()
    {
        return (int) this.layerBase.DEBUGV6LAYER;
    }

    /*IPad[] ILayerPCB.GetPads()
    {
        throw new System.NotImplementedException();
    }*/

    double ILayer.GetThickness()
    {
        return this.altiumLayer == null ? 0 : this.altiumLayer.GetThickness();
    }

    IPaint ILayer.GetPaint()
    {
        return new AltiumPaint(this.altiumBoard.Board, layerBase);
    }
}
