namespace ReportsOutput
{
    public interface IDocumentCellStyle
    {
        bool HorizlontalCenter
        { get; set; } 

        bool VerticalCenter
        { get; set; }

        byte[] Color
        { get; set; }

        IDocumentFont IDocumentFont
        { get; }

        void SetFont(IDocumentFont iDocumentFont);
    }
}