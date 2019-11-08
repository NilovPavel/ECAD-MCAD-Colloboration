using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using ReportsOutput.ExcelConstants;

namespace ReportsOutput.IExcelReports
{
    class SecondSheetSpecificationAXLS : IDocumentSheet
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
                return 35 - 1;
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

        /*protected override void Initialization()
        {
            this.RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A,1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.L,35-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.D,36-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.F,36-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.H,36-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.K,36-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.V,37-1, "Лист")
            };

            this.FormatCellColumn = (int)Alfavit.D;
            this.PositionCellColumn = (int)Alfavit.G;
            this.OboznCellColumn = (int)Alfavit.I;
            this.NameCellColumn = (int)Alfavit.N;
            this.PrimechCellColumn = (int)Alfavit.U;
            this.FirstRowIndex = 3 - 1;
            this.LastRowIndex = 35 - 1;
            this.CountCellArray = new int[] { (int)Alfavit.T };
        }*/

        List<AddressParameterCell> IDocumentSheet.GetAddressesParameters()
        {
            List<AddressParameterCell> RowColumnValueCell = new List<AddressParameterCell>
            {
                new AddressParameterCell((int)Alfavit.A,1-1, "Проект"),
                new AddressParameterCell((int)Alfavit.L,35-1, "Обозначение"),
                new AddressParameterCell((int)Alfavit.D,36-1, "Порядковый_номер_изменения_СП"),
                new AddressParameterCell((int)Alfavit.F,36-1, "Указания_изменение_СП"),
                new AddressParameterCell((int)Alfavit.H,36-1, "Номер_документа_изменение_СП"),
                new AddressParameterCell((int)Alfavit.K,36-1, "Дата_изменения_СП"),
                new AddressParameterCell((int)Alfavit.V,37-1, "Лист")
            };
            return RowColumnValueCell;
        }
    }
}
