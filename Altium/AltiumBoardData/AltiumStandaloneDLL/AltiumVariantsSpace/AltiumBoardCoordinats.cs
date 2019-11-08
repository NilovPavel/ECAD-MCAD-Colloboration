using BoardSpace;
using System;

namespace AltiumVariantsSpace
{
    class AltiumBoardCoordinats : ICoordinats
    {
        private AltiumBoard iBoard;

        public AltiumBoardCoordinats(IBoardCAD iBoard)
        {
            this.iBoard = (AltiumBoard)iBoard;
        }

        double ICoordinats.GetAngle()
        {
            return 0;
        }

        double[] ICoordinats.GetQuaternion()
        {
            return new double[] { 0, 0, 0, 0 };
        }

        double ICoordinats.GetX()
        {
            return 0;
        }

        double ICoordinats.GetY()
        {
            return 0;
        }

        double ICoordinats.GetZ()
        {
            return 0;
        }
    }
}