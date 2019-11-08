using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using System.Linq;

namespace ReportsOutput
{
    public abstract class AbstractDocumentXLS : IDocument
    {
        protected ProjectProperties projectProperties;
        protected IWorkbook workbook;
        private ISheet sheet;

        public int CurrentRowIndex { get; set; }
        public int CurrentSheetIndex { get; set; }
        public abstract ITemplateDocument ITemplateDocument { get; }
        public IDocumentSheet CurrentSheet { get; protected set; }

        public IDocumentCellStyle CellStyle
        {
            get
            {
                return new ExcelDocumentCellStyle(this.workbook);
            }
        }

        public IDocumentSheetPropertiesWriter IDocumentSheetPropertiesWriter
        {
            get
            {
                return new ExcelSheetWriter((IDocument)this, projectProperties);
            }
        }

        private void Initialization()
        {
            ResourceTemplate resourceTemplate = new ResourceTemplate(this.ITemplateDocument.GetResourceName(), this.ITemplateDocument.GetFileName());
            this.workbook = new HSSFWorkbook(resourceTemplate.Stream);
            this.sheet = this.workbook.GetSheetAt(0);
            this.CurrentSheetIndex++;
            this.CurrentSheet = this.ITemplateDocument.IDocumentFirstSheet;
            this.CurrentRowIndex = this.CurrentSheet.FirstRowIndex;
            this.IDocumentSheetPropertiesWriter.WriteSheetProperties();
        }

        void IDocument.NextSheet()
        {
            this.sheet = this.workbook.CloneSheet(1);
            this.CurrentSheet = this.ITemplateDocument.IDocumentSecondSheet;
            this.CurrentRowIndex = this.CurrentSheet.FirstRowIndex;
            this.CurrentSheetIndex++;
            this.IDocumentSheetPropertiesWriter.WriteSheetProperties();
        }

        void IDocument.WriteCellValue(int column, int row, string value)
        {
            ICell cell = this.sheet.GetRow(row).GetCell(column);
            cell.SetCellValue(value);
        }

        void IDocument.WriteDocument(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    this.workbook.Write(fileStream);
                }
                catch
                { }
            }
        }

        void IDocument.MergedRegion(int firstRow, int lastRow, int firstColumn, int lastColumn)
        {
            for (int mergedRegionCount = this.sheet.NumMergedRegions - 1; mergedRegionCount > 0; mergedRegionCount--)
            {
                CellRangeAddress mergedRegion = this.sheet.GetMergedRegion(mergedRegionCount);
                if(mergedRegion.IsInRange(firstRow, firstColumn) || mergedRegion.IsInRange(lastRow, lastColumn))
                    this.sheet.RemoveMergedRegion(mergedRegionCount);
            }
            this.sheet.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstColumn, lastColumn));
        }

        void IDocument.SetCellStyle(int column, int row, IDocumentCellStyle iCellStyle)
        {
            ICell iCell = this.sheet.GetRow(row).GetCell(column);
            iCell.CellStyle = ((ExcelDocumentCellStyle)iCellStyle).GetCellStyle(iCell.CellStyle);
        }

        void IDocument.WriteCountToFirstSheet()
        {
            IDocumentSheet firstSheet = this.ITemplateDocument.IDocumentFirstSheet;
            AddressParameterCell cellAddress = firstSheet.GetAddressesParameters().Where(item => item.Parametr.Equals("Листов")).FirstOrDefault();
            this.sheet = this.workbook.GetSheetAt(0);
            ((IDocument)this).WriteCellValue(cellAddress.Column, cellAddress.Row, this.workbook.NumberOfSheets.ToString());
        }

        private void WriteLRI()
        {
            if (this.workbook.NumberOfSheets <= 3)
            {
                this.workbook.RemoveSheetAt(1);
                return;
            }
            this.sheet = this.workbook.CloneSheet(1);
            this.workbook.RemoveSheetAt(1);
            this.workbook.SetSheetName(this.workbook.NumberOfSheets - 1 , "ЛРИ");

            this.CurrentSheet = this.ITemplateDocument.IDocumentLRISheet;
            this.CurrentSheetIndex++;
            this.IDocumentSheetPropertiesWriter.WriteSheetProperties();
        }

        private void RenameAllTwoSheets()
        {
            for (int sheetsCount = 2; sheetsCount < this.workbook.NumberOfSheets && !this.sheet.SheetName.Equals("ЛРИ"); sheetsCount++)
                this.workbook.SetSheetName(sheetsCount, (sheetsCount).ToString());
        }

        void IDocument.RemoveUnusedSheets()
        {
            this.workbook.RemoveSheetAt(1);
            this.RenameAllTwoSheets();
            this.WriteLRI();
        }

        public abstract void WriteDocumentProperties();

        public AbstractDocumentXLS(ProjectProperties projectProperties)
        {
            this.projectProperties = projectProperties;
            this.Initialization();
        }
    }
}