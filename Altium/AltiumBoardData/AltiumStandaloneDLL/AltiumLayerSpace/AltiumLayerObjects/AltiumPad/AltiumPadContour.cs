using System;
using PCB;
using System.Windows.Forms;
using Help;

internal class AltiumPadContour : IContour
{
    private IContour iContour;
    private IContour shapeContour;
    private IPCB_Pad iPcbPad;
    private IPCB_Pad2 iPcbPad2;
    private TShape shapeType;
    /*private double width;
    private double height;*/
    private double xSize;
    private double ySize;
    private double xLocation;
    private double yLocation;
    private double rotation;
    private int percentCornerRadius;

    public AltiumPadContour(IPCB_Pad iPcbPad)
    {
        this.iPcbPad = iPcbPad;
        this.iPcbPad2 = (IPCB_Pad2) this.iPcbPad;
        this.Initialization();
        this.GetShapes();
    }

    private void GetShapes()
    {
        switch (this.shapeType)
        {
            case TShape.eRounded:
                this.iContour = new AltiumPadRoundShape(this.iContour);
                break;
            case TShape.eRoundedRectangular:
                this.iContour = new AltiumPadRoundedRectangleShape(this.iContour);
                if (this.percentCornerRadius == 100)
                    this.iContour = new AltiumPadRoundShape(this.iContour);
                break;
            case TShape.eRectangular:
                this.iContour = new AltiumPadRectangleShape(this.iContour);
                break;
            case TShape.eOctagonal:
                this.iContour = new AltiumPadOctagonShape(this.iContour);
                break;
        }
    }

    private void Initialization()
    {
        //Сначала поворачивается медь, потом дырка
        this.shapeType = this.iPcbPad.GetState_ShapeOnLayer(this.iPcbPad.GetState_V7Layer());
        this.rotation = this.iPcbPad.GetState_Rotation();
        this.xLocation = new AltiumHelper().CoordToMMsX(this.iPcbPad.GetState_XLocation());
        this.yLocation = new AltiumHelper().CoordToMMsY(this.iPcbPad.GetState_YLocation());
        this.xSize = new AltiumHelper().CoordToMMs(this.iPcbPad.GetState_XSizeOnLayer(this.iPcbPad.GetState_V7Layer()));
        this.ySize = new AltiumHelper().CoordToMMs(this.iPcbPad.GetState_YSizeOnLayer(this.iPcbPad.GetState_V7Layer()));
        this.percentCornerRadius = this.iPcbPad2.GetState_CRPercentageOnLayer(this.iPcbPad.GetState_V7Layer());

        switch (this.shapeType)
        {
            case TShape.eRounded:
                this.percentCornerRadius = 100;
                break;
            case TShape.eRectangular:
                this.percentCornerRadius = 0;
                break;
            case TShape.eOctagonal:
                this.percentCornerRadius = 50;
                break;
        }
        this.iContour = new RoundRectangularShape(this.xLocation, this.yLocation, this.rotation, this.xSize, this.ySize, this.percentCornerRadius);
        /*MessageBox.Show(
            "shape = " + this.shapeType.ToString() + "\r\n"
            + "rotation = " + this.rotation + "\r\n"
            + "xSize = " + this.xSize + "\r\n"
            + "ySize = " + this.ySize + "\r\n"
            + "x = " + this.xLocation + "\r\n"
            + "y = " + this.yLocation + "\r\n"
            );*/
    }

    IArc[] IContour.GetIArcs()
    {
        return this.iContour.GetIArcs();
    }

    ILine[] IContour.GetILines()
    {
        return this.iContour.GetILines();
    }
}