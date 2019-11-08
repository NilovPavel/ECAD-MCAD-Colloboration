namespace ReportsOutput
{
    public interface ISheetPropertiesWriter
    {
        void WriteProperties(IDocumentSheet iDocumentSheet, ProjectProperties properties);
    }
}