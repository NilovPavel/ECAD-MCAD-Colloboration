using Help;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSpace
{
    //Плохое решение, но по-другому не знаю как
    class AltiumBendingLineFromString : IBendingLine
    {
        private string bendingLineString;
        private IContour iAltiumContour;
        private string[] split_parameters;
        private double minX, minY, maxX, maxY;

        public AltiumBendingLineFromString(string bendingLineString)
        {
            this.bendingLineString = bendingLineString;
            this.bendingLineString = this.bendingLineString.Substring(this.bendingLineString.IndexOf("=") + 1, this.bendingLineString.Length - this.bendingLineString.IndexOf("=") - 1);
            this.split_parameters = this.bendingLineString.Split(';');
        }

        public AltiumBendingLineFromString(string bendingLineString, IContour iAltiumContour) : this(bendingLineString)
        {
            this.iAltiumContour = iAltiumContour;
            Contour contour = new Contour(this.iAltiumContour);
            contour.GetExtremums(ref minX, ref minY, ref maxX, ref maxY);
        }

        double IBendingLine.GetAngle()
        {
            return double.Parse(this.split_parameters[(int)BendingLineParameters.Angle].Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        }

        double IBendingLine.GetBendRadius()
        {
            return new AltiumHelper().CoordToMMs(Convert.ToInt32(this.split_parameters[(int)BendingLineParameters.Radius]));
        }

        Line IBendingLine.GetLine()
        {
            return new Line
            {
                Point1 = new Point
                {
                    X = new AltiumHelper().CoordToMMs(Convert.ToInt32(this.split_parameters[(int)BendingLineParameters.X1])) + this.minX,
                    Y = new AltiumHelper().CoordToMMs(Convert.ToInt32(this.split_parameters[(int)BendingLineParameters.Y1])) + this.minY
                },

                Point2 = new Point
                {
                    X = new AltiumHelper().CoordToMMs(Convert.ToInt32(this.split_parameters[(int)BendingLineParameters.X2])) + this.minX,
                    Y = new AltiumHelper().CoordToMMs(Convert.ToInt32(this.split_parameters[(int)BendingLineParameters.Y2])) + this.minY
                }
            };
        }

        short IBendingLine.GetFoldIndex()
        {
            return short.Parse(this.split_parameters[(int)BendingLineParameters.FoldIndex]);
        }
    }
}
