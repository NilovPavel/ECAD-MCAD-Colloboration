public class DocumentationComplectsCombiner : SpecificationRazdelCombiner
{
    public DocumentationComplectsCombiner(RecordESKD[] sourceComponentCollection) : base(sourceComponentCollection)
    {
        this.iEqualityComparer = new RecordESKDDocumentationComplectsEqualityComparer();
    }
}