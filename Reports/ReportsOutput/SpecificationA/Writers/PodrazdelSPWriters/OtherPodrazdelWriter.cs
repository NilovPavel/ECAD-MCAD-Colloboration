namespace ReportsOutput
{
    internal class OtherPodrazdelWriter : PodrazdelSpWriterSpecificationA
    {
        public OtherPodrazdelWriter(IDocument iDocument) : base(iDocument)
        {
            this.iDocument = iDocument;
            this.elementWriterSpecificationA = new OtherElementWriterSpecificationA(this.iDocument);
        }
    }
}