using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;

namespace ReportsOutput
{
    internal class LRISheetPerechenXLS : IDocumentSheet
    {
        int[] IDocumentSheet.CountCellColumns
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.FirstRowIndex
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.FormatCellColumn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.LastRowIndex
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.NameCellColumn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.OboznCellColumn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.PositionCellColumn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IDocumentSheet.PrimechCellColumn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        List<AddressParameterCell> IDocumentSheet.GetAddressesParameters()
        {
            List<AddressParameterCell> RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A, 1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.L, 35-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.D, 36-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.E, 36-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.G, 36-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.K, 36-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.S, 37-1, "Лист")
            };
            return RowColumnValueCell;
        }
    }
}