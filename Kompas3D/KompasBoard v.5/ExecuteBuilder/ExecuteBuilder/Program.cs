using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteBuilder
{
    class Program
    {
        static void Main()
        {
            new Builder(@"E:\Работа\Sources\CAD\Altium\AltiumBoardData\XMLs\PCB_Test_IDXPrjPCB_bak.xml");
            //new Builder(@"E:\Работа\Sources\CAD\Altium\AltiumBoardData\XMLs\Board_kompas.xml");
        }
    }
}
