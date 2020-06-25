using Help;
using PCB;
using System.Windows.Forms;

public class AltiumLayerText : IText
{
    private IPCB_Text iPcbText;

    public AltiumLayerText(IPCB_Text iPcbText)
    {
        this.iPcbText = iPcbText;
    }

    double IText.GetAngle()
    {
        return this.iPcbText.GetState_Rotation();
    }

    string IText.GetFontName()
    {
        return this.iPcbText.GetState_FontName();
    }

    double IText.GetHeight()
    {
        /*if(this.iPcbText.GetState_Text().Equals("VT1"))
        MessageBox.Show(
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangle().Y2 - this.iPcbText.BoundingRectangle().Y1) + "\n" +
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangleChildren().Y2 - this.iPcbText.BoundingRectangleChildren().Y1) + "\n" +
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangleForPainting().Y2 - this.iPcbText.BoundingRectangleForPainting().Y1) + "\n" +
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangleForSelection().Y2 - this.iPcbText.BoundingRectangleForSelection().Y1) + "\n" +
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangleChildren().Y2 - this.iPcbText.BoundingRectangleChildren().Y1) + "\n" +
            new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangleChildren().Y2 - this.iPcbText.BoundingRectangleChildren().Y1) + "\n" +
            );*/
        return new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangle().Y2 - this.iPcbText.BoundingRectangle().Y1);// this.iPcbText.GetState_InvRectHeight());// this.iPcbText.GetState_TTFTextHeight());//.GetState_MultilineTextHeight());
    }

    TypeJust IText.GetJustification()
    {
        TypeJust typeJust = 0;
        switch (this.iPcbText.GetState_MultilineTextAutoPosition())
        {
            case TTextAutoposition.eAutoPos_BottomCenter:
                typeJust = TypeJust.bottom;
                break;
            case TTextAutoposition.eAutoPos_CenterCenter:
                typeJust = TypeJust.center;
                break;
            case TTextAutoposition.eAutoPos_CenterLeft:
                typeJust = TypeJust.left;
                break;
            case TTextAutoposition.eAutoPos_BottomLeft:
                typeJust = TypeJust.lowerLeft;
                break;
            case TTextAutoposition.eAutoPos_BottomRight:
                typeJust = TypeJust.lowerRight;
                break;
            case TTextAutoposition.eAutoPos_CenterRight:
                typeJust = TypeJust.right;
                break;
            case TTextAutoposition.eAutoPos_TopCenter:
                typeJust = TypeJust.top;
                break;
            case TTextAutoposition.eAutoPos_TopLeft:
                typeJust = TypeJust.upperLeft;
                break;
            case TTextAutoposition.eAutoPos_TopRight:
                typeJust = TypeJust.upperRight;
                break;
        }
        return typeJust;
    }

    double IText.GetSize()
    {
        return new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangle().Y2 - this.iPcbText.BoundingRectangle().Y1);
    }

    string IText.GetValue()
    {
        return this.iPcbText.GetState_Text();
    }

    double IText.GetWidth()
    {
        return new AltiumHelper().CoordToMMs(this.iPcbText.BoundingRectangle().X2 - this.iPcbText.BoundingRectangle().X1);// GetState_InvRectWidth());//this.iPcbText.GetState_TTFTextHeight());// this.iPcbText.GetState_MultilineTextWidth());
    }

    double IText.GetX()
    {
        return new AltiumHelper().CoordToMMsX(this.iPcbText.GetState_XLocation());
    }

    double IText.GetY()
    {
        return new AltiumHelper().CoordToMMsY(this.iPcbText.GetState_YLocation());
    }
}