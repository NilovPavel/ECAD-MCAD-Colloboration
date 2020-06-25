namespace ReportsOutput
{
    internal class DocumentsComplectsElementWriterSpecificationA : ElementWriterSpecificationA
    {
        protected override string GetOboznachenie(RecordESKD recordESKD, Spisok spisok)
        {
            return recordESKD.Обозначение + recordESKD.КодДокумента;
        }

        protected override string GetCount(RecordESKD recordESKD)
        {
            return "";
        }

        protected override string GetPositon(RecordESKD recordESKD)
        {
            return string.Empty;
        }

        public DocumentsComplectsElementWriterSpecificationA(IDocument iDocument) : base(iDocument)
        {
        }
    }
}