using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsOutput
{
    public interface IDocument
    {
        int CurrentRowIndex { get; set; }
        int CurrentSheetIndex { get; set; }
        ITemplateDocument ITemplateDocument { get; }
        IDocumentSheet CurrentSheet { get; }
        IDocumentCellStyle CellStyle { get; }
        IDocumentSheetPropertiesWriter IDocumentSheetPropertiesWriter { get;}
        //ISheetPropertiesWriter SheetPropertiesWriter { get; }
        void MergedRegion(int firstRow, int secondRow, int firstColumn, int secondColumn);
        //void CreateDocument();
        void WriteDocument(string filePath);
        void SetCellStyle(int column, int row, IDocumentCellStyle iCellStyle);
        void WriteCellValue(int column, int row, string value);
        void NextSheet();
        void WriteCountToFirstSheet();
        void RemoveUnusedSheets();
        void WriteDocumentProperties();
    }
}