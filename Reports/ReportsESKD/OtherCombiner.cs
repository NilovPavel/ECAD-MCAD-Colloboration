public class OtherCombiner : SpecificationRazdelCombiner
{
    public OtherCombiner(RecordESKD[] sourceComponentCollection) : base(sourceComponentCollection)
    {
        this.iEqualityComparer = new RecordESKDOthersEqualityComparer();
    }
}