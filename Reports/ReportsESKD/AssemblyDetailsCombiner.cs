public class AssemblyDetailsCombiner : SpecificationRazdelCombiner
{
    public AssemblyDetailsCombiner(RecordESKD[] sourceComponentCollection) : base(sourceComponentCollection)
    {
        this.iEqualityComparer = new RecordESKDAssemblyDetailsEqualityComparer();
    }
}