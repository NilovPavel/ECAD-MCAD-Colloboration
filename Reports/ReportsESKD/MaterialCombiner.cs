using System.Collections.Generic;
using System.Linq;

public class MaterialCombiner : SpecificationRazdelCombiner
{
    protected override RecordESKD GetRecordESKD(RecordESKD recordESKD, IEnumerable<RecordESKD> records)
    {
        double countRecords = 0;
        records.ToList().ForEach(item => countRecords += item.Количество);
        recordESKD.Количество = countRecords;
        return recordESKD;
    }

    public MaterialCombiner(RecordESKD[] sourceComponentCollection) : base(sourceComponentCollection)
    {
        this.iEqualityComparer = new RecordESKDMaterialsEqualityComparer();
    }
}