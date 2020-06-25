using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;

namespace ReportsOutput
{
    internal class FirstSheetPerechenXLS : IDocumentSheet
    {
        int[] IDocumentSheet.CountCellColumns
        {
            get
            {
                return new int[] { (int)Alfavit.L };
            }
        }

        int IDocumentSheet.FirstRowIndex
        {
            get
            {
                return 3 - 1;
            }
        }

        int IDocumentSheet.FormatCellColumn
        {
            get
            {
                return 0;
            }
        }

        int IDocumentSheet.LastRowIndex
        {
            get
            {
                return 26 - 1;
            }
        }

        int IDocumentSheet.NameCellColumn
        {
            get
            {
                return (int)Alfavit.G;
            }
        }

        int IDocumentSheet.OboznCellColumn
        {
            get
            {
                return 0;
            }
        }

        int IDocumentSheet.PositionCellColumn
        {
            get
            {
                return (int)Alfavit.D;
            }
        }

        int IDocumentSheet.PrimechCellColumn
        {
            get
            {
                return (int)Alfavit.O;
            }
        }

        List<AddressParameterCell> IDocumentSheet.GetAddressesParameters()
        {
            List<AddressParameterCell> RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A,1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.J,30-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.J,33-1, "Наименование"),
                new AddressParameterCell((int)Alfavit.N,34-1, "Литера"),
                new AddressParameterCell((int)Alfavit.O,34-1, "Литера2"),
                new AddressParameterCell((int)Alfavit.P,34-1, "Литера3"),
                new AddressParameterCell((int)Alfavit.D,30-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.E,30-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.F,30-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.I,30-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.D,35-1, "Характер_работы"),
                new AddressParameterCell((int)Alfavit.Q,34-1, "Лист"),
                new AddressParameterCell((int)Alfavit.R,34-1, "Листов"),
                new AddressParameterCell((int)Alfavit.F,35-1, "п_Доп_графа"),
                new AddressParameterCell((int)Alfavit.F,36-1, "п_Н_контр"),
                new AddressParameterCell((int)Alfavit.F,34-1, "п_Пров"),
                new AddressParameterCell((int)Alfavit.F,33-1, "п_Разраб"),
                new AddressParameterCell((int)Alfavit.F,37-1, "п_Утв")
            };
            return RowColumnValueCell;
        }
    }
}