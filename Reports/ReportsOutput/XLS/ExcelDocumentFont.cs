using NPOI.SS.UserModel;

namespace ReportsOutput
{
    public class ExcelDocumentFont : IDocumentFont
    {
        private IWorkbook workbook;
        private IFont font;

        public bool Bold
        { get; set; }

        public bool Italic
        { get; set; }

        public short FontColor
        { get; set; }

        public bool UnderLine
        { get; set; }

        private void Initialization()
        {
            this.font = this.workbook.CreateFont();
        }

        public ExcelDocumentFont(IWorkbook workbook)
        {
            this.workbook = workbook;
            this.Initialization();
        }

        public IFont GetFont(IFont font)
        {
            this.font.FontName = font.FontName;
            this.font.FontHeight = font.FontHeight;
            this.font.IsBold = this.Bold;
            this.font.IsItalic = this.Italic;
            this.font.Underline = this.UnderLine ? FontUnderlineType.Single : FontUnderlineType.None;
            return this.font;
        }
    }
}