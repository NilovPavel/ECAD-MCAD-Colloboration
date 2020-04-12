using Help;
using PCB;

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

    double IText.GetSize()
    {
        //Под вопросом корректность вычислений
        return new AltiumHelper().CoordToMMs(this.iPcbText.GetState_Y2Location() - this.iPcbText.GetState_Y1Location() - (this.iPcbText.GetState_YLocation() - this.iPcbText.GetState_Y1Location())) - 0.254;
        //return new AltiumHelper().CoordToMMs(this.iPcbText.GetState_MultilineTextHeight());
        //return new AltiumHelper().CoordToMMs(this.iPcbText.GetState_TTFTextHeight());
    }

    string IText.GetValue()
    {
        return this.iPcbText.GetState_Text();
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