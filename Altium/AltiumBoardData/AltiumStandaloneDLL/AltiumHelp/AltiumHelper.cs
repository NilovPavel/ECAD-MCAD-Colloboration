using BoardSpace;
using PCB;
using System;

namespace Help
{
    class AltiumHelper
    {
        //public static IPCB_Board board;
        public static AltiumBoard Board
        { get; private set; }

        private int precision = 3;

        public static void SetBoard(AltiumBoard aBoard)
        {
            Board = aBoard;
        }

        public double GetHeightLayerById(uint Ord)
        {
            return Board.AltiumLayerStackManager.GetLayerHeightById(Ord);
        }

        public double GetHeightLayerByIdWithSelf(uint Ord)
        {
            return Board.AltiumLayerStackManager.GetLayerHeightByIdWithSelf(Ord);
        }

        public double CoordToMMs(int coords)
        {
            return Math.Round(EDP.Utils.CoordToMMs(coords), this.precision);
        }

        public double CoordToMMsX(int coords)
        {
            AltiumBoard board = Board;
            return board.PointMin == null ? this.CoordToMMs(coords) : Math.Round(EDP.Utils.CoordToMMs(coords), this.precision) - board.PointMin.X;
        }

        public double CoordToMMsY(int coords)
        {
            AltiumBoard board = Board;
            return board.PointMin == null ? this.CoordToMMs(coords) : Math.Round(EDP.Utils.CoordToMMs(coords), this.precision) - board.PointMin.Y;
        }

        public bool isBoardRegion(IPCB_Region region)
        {
            try
            {
                ((IPCB_BoardRegion)region).GetState_LayerStack();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
