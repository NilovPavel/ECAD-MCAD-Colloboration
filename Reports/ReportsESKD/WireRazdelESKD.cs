public class WireRazdelESKD
{
    private Wire item;
    private RecordESKD recordESKD;

    public RecordESKD RecordESKD
    {
        get
        {
            return recordESKD;
        }
    }

    public WireRazdelESKD(Wire item)
    {
        this.item = item;
        this.recordESKD = new RecordESKD
        {
            CadID = this.item.SourceConnectorDesignator + ":" + this.item.SourceConnectorPin,
            Designator = string.Empty,
            PartNumber = this.item.Material,
            Fitted = true,
            ЕдИзм = "м",
            Количество = this.item.Length,
            Наименование = this.item.Material,
            Обозначение = string.Empty,
            РазделСп = "Материалы",
            Примечание = string.Empty,
            Формат = string.Empty
        };
    }

}
