public class StandartsCombiner : SpecificationRazdelCombiner
{
    public StandartsCombiner(RecordESKD[] sourceComponentCollection) : base(sourceComponentCollection)
    {
        this.iEqualityComparer = new RecordESKDStandartsEqualityComparer();
    }
}