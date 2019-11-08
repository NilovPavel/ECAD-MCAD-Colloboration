namespace ReportsOutput
{
    public interface IDocumentFont
    {
        bool Bold
        { get; set; }

        bool Italic
        { get; set; }

        short FontColor
        { get; set; }

        bool UnderLine
        { get; set; }
    }
}