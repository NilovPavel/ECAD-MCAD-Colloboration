using Help;
using PCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    class AltiumHoleContour : IContour
    {
        private IPCB_Pad2 pad;
        private double x, y, angle, height, width;
        private TExtendedHoleType holeType;
        private List<ILine> iLines;
        private List<IArc> iArcs;

        public bool NotDrill { get; private set; }

        public AltiumHoleContour(IPCB_Pad2 pad)
        {
            this.pad = pad;
            this.Initialization();
            if (this.NotDrill = (this.width == 0 && this.height == 0))
                return;
            this.CalcRectangle();
        }

        private void Initialization()
        {
            this.iLines = new List<ILine>();
            this.iArcs = new List<IArc>();

            double D = new AltiumHelper().CoordToMMs(this.pad.GetState_HoleSize());
            double Length = new AltiumHelper().CoordToMMs(this.pad.GetState_HoleWidth());

            this.x = new AltiumHelper().CoordToMMsX(this.pad.GetState_XLocation());
            this.y = new AltiumHelper().CoordToMMsY(this.pad.GetState_YLocation());

            //Ширина будет всегда меньше высоты
            this.height = D > Length ? D : Length;
            this.width = D > Length ? Length : D;
            
            this.holeType = (this.height == this.width && this.pad.GetState_HoleType() == TExtendedHoleType.eSlotHole) ? TExtendedHoleType.eRoundHole : this.pad.GetState_HoleType();

            //Угол поворота = угол заданного поворота отверстия + из соотношение (height:width? = 90/0?) + поворот медного пояска
            this.angle = this.pad.GetState_HoleRotation() + (double)(this.height > this.width ? 90 : 0) + this.pad.GetState_Rotation();
        }

        private void CalcRectangle()
        {
            switch (holeType)
            {
                case TExtendedHoleType.eRoundHole:
                    this.iArcs.Add(new HoleRound(this.x, this.y, this.height));
                    break;
                case TExtendedHoleType.eSquareHole:
                    if (this.width == 0)
                        this.width = this.height;
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle, this.width, this.height));
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle + 180, this.width, this.height));
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle + 90, this.height, this.width));
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle + 270, this.height, this.width));
                    break;
                case TExtendedHoleType.eSlotHole:
                    this.iArcs.Add(new HoleArc(this.x, this.y, this.angle, this.height, this.width));
                    this.iArcs.Add(new HoleArc(this.x, this.y, this.angle + 180, this.height, this.width));
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle, this.height - this.width, this.width));
                    this.iLines.Add(new HoleLine(this.x, this.y, this.angle + 180, this.height - this.width, this.width));
                    break;
            }
        }

        IArc[] IContour.GetIArcs()
        {
            return this.iArcs.ToArray();
        }

        ILine[] IContour.GetILines()
        {
            return this.iLines.ToArray();
        }
    }
}
