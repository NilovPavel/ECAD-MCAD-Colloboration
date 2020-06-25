using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;

namespace ReportsOutput
{
    internal class SecondSheetPerechenXLS : IDocumentSheet
    {
        int[] IDocumentSheet.CountCellColumns
        {
            get
            {
                return new int[]{ (int)Alfavit.K };
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
                return 32 - 1;
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
                return (int)Alfavit.M;
            }
        }

        List<AddressParameterCell> IDocumentSheet.GetAddressesParameters()
        {
            List<AddressParameterCell> RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A,1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.J,34-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.D,35-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.E,35-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.F,35-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.I,35-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.P,36-1, "Лист")
            };
            return RowColumnValueCell;
        }
    }
}