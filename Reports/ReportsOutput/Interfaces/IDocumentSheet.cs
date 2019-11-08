using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsOutput
{
    public interface IDocumentSheet
    {
        int FormatCellColumn { get; }
        int PositionCellColumn { get;}
        int OboznCellColumn { get; }
        int NameCellColumn { get; }
        int PrimechCellColumn { get; }
        int[] CountCellColumns { get; }
        int FirstRowIndex { get; }
        int LastRowIndex { get; }

        List<AddressParameterCell> GetAddressesParameters();
    }
}