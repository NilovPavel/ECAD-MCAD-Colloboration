namespace ReportsOutput
{
    internal class DocumentsComplectsElementWriterSpecificationA : ElementWriterSpecificationA
    {
        protected override string GetPositon(RecordESKD recordESKD)
        {
            return string.Empty;
        }

        public DocumentsComplectsElementWriterSpecificationA(IDocument iDocument) : base(iDocument)
        {
        }
    }
}