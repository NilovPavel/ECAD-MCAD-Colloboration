using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExcelIteratorAction : IIteratorAction
{
    private IWorkbook workbook;
    private ISheet sheet;
    private IRow row;
    private string fileName;

    public ExcelIteratorAction()
    {
        this.Initialization();
    }

    protected void CreateNewBook()
    {
        this.workbook = new HSSFWorkbook();
        this.sheet = this.workbook.CreateSheet("Report");
        using (FileStream fileStream = new FileStream(this.fileName, FileMode.Create, FileAccess.Write))
        {
            this.workbook.Write(fileStream);
            this.workbook.Close();
        }
    }

    protected void Initialization()
    {
        this.fileName = @"D:\Test\BoardData\ReportData\Report.xls";
        if (!File.Exists(this.fileName))
            this.CreateNewBook();
        if (this.workbook != null)
            return;
        using (FileStream fileStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read))
        {
            this.workbook = new HSSFWorkbook(fileStream);
            this.sheet = this.workbook.GetSheet("Report");
        }
        
    }

    void IIteratorAction.ElementAction(RecordESKD recordESKD)
    {
        this.row = this.sheet.CreateRow(this.sheet.LastRowNum+1);
        ICell cell = this.row.CreateCell(0);
        cell.SetCellValue(recordESKD.Формат);
        cell = this.row.CreateCell(1);
        cell.SetCellValue(recordESKD.Обозначение);
        cell = this.row.CreateCell(2);
        cell.SetCellValue(recordESKD.Fitted ? recordESKD.Наименование : "-" + recordESKD.Наименование);
        cell = this.row.CreateCell(3);
        cell.SetCellValue(recordESKD.Количество);
        cell = this.row.CreateCell(4);
        cell.SetCellValue(recordESKD.Designator);
    }

    void IIteratorAction.RazdelAction(Spisok spisok)
    {
        this.row = this.sheet.CreateRow(this.sheet.LastRowNum + 2);
        ICell cell = this.row.CreateCell(3);
        cell.SetCellValue(spisok.Name);
    }

    void IIteratorAction.Close()
    {
        using (FileStream fileStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Write))
        {
            this.workbook.Write(fileStream);
        }
    }
}