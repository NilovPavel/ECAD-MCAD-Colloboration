using ReportsOutput.ExcelConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace ReportsOutput.IExcelReports
{
    class FirstSheetSpecificationAXLS : IDocumentSheet
    {
        int[] IDocumentSheet.CountCellColumns
        {
            get
            {
                return new int[] { (int)Alfavit.T };
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
                return (int)Alfavit.D;
            }
        }

        int IDocumentSheet.LastRowIndex
        {
            get
            {
                return 28-1;
            }
        }

        int IDocumentSheet.NameCellColumn
        {
            get
            {
                return (int)Alfavit.N;
            }
        }

        int IDocumentSheet.OboznCellColumn
        {
            get
            {
                return (int)Alfavit.I;
            }
        }

        int IDocumentSheet.PositionCellColumn
        {
            get
            {
                return (int)Alfavit.G;
            }
        }

        int IDocumentSheet.PrimechCellColumn
        {
            get
            {
                return (int)Alfavit.U;
            }
        }

        List<AddressParameterCell> IDocumentSheet.GetAddressesParameters()
        {
            List<AddressParameterCell> RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A,1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.L,32-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.L,35-1, "Наименование"),
                new AddressParameterCell((int)Alfavit.P,36-1, "Литера"),
                new AddressParameterCell((int)Alfavit.Q,36-1, "Литера2"),
                new AddressParameterCell((int)Alfavit.R,36-1, "Литера3"),
                new AddressParameterCell((int)Alfavit.D,33-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.F,33-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.H,33-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.K,33-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.D,37-1, "Характер_работы"),
                new AddressParameterCell((int)Alfavit.S,36-1, "Лист"),
                new AddressParameterCell((int)Alfavit.V,36-1, "Листов"),
                new AddressParameterCell((int)Alfavit.H,37-1, "п_Доп_графа"),
                new AddressParameterCell((int)Alfavit.H,38-1, "п_Н_контр"),
                new AddressParameterCell((int)Alfavit.H,36-1, "п_Пров"),
                new AddressParameterCell((int)Alfavit.H,35-1, "п_Разраб"),
                new AddressParameterCell((int)Alfavit.H,39-1, "п_Утв")
            };
            return RowColumnValueCell;
        }
    }
}
