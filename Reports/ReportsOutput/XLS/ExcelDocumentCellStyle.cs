using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;

namespace ReportsOutput
{
    public class ExcelDocumentCellStyle : IDocumentCellStyle
    {
        private IWorkbook workbook;
        private ICellStyle currentCellStyle;
        private IDocumentFont iDocumentFont;

        public bool HorizlontalCenter
        { get; set; }

        public bool VerticalCenter
        { get; set; }

        public byte[] Color
        { get; set; }

        public IDocumentFont IDocumentFont
        { get { return new ExcelDocumentFont(this.workbook); } }

        private HSSFColor GetColor()
        {
            HSSFPalette XlPalette = ((HSSFWorkbook)this.workbook).GetCustomPalette();
            HSSFColor hssfColor = XlPalette.FindColor(this.Color[0], this.Color[1], this.Color[2]);
            hssfColor = hssfColor ?? XlPalette.FindSimilarColor(this.Color[0], this.Color[1], this.Color[2]);
            return hssfColor;
        }

        private void SetFont()
        {
            IFont font = this.currentCellStyle.GetFont(this.workbook);
            this.currentCellStyle.SetFont(((ExcelDocumentFont)this.iDocumentFont).GetFont(font));
        }

        private void SetColor()
        {
            HSSFColor hssfColor = this.GetColor();
            this.currentCellStyle.FillBackgroundColor = hssfColor.Indexed;
            this.currentCellStyle.FillForegroundColor = hssfColor.Indexed;
            this.currentCellStyle.FillPattern = FillPattern.SolidForeground;
        }

        public ICellStyle GetCellStyle(ICellStyle iCellStyle)
        {
            this.currentCellStyle = this.workbook.CreateCellStyle();
            this.currentCellStyle.CloneStyleFrom(iCellStyle);
            this.currentCellStyle.Alignment = this.HorizlontalCenter ? HorizontalAlignment.Center : HorizontalAlignment.Left;
            this.currentCellStyle.VerticalAlignment = this.VerticalCenter ? VerticalAlignment.Center : VerticalAlignment.Bottom;

            this.SetFont();

            this.SetColor();
            
            return currentCellStyle;
        }

        public void SetFont(IDocumentFont iDocumentFont)
        {
            this.iDocumentFont = iDocumentFont;
        }

        public ExcelDocumentCellStyle(IWorkbook workbook)
        {
            this.workbook = workbook;
            this.Color = new byte[] { 255, 255, 255 };
        }
    }
}