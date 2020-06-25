using System;

internal class PerechenAssemblyRecordESKD : RecordESKD
{
    private AssemblyComponentID rootAssemblyComponentID;

    private void Initialization()
    {
        this.CadID = this.rootAssemblyComponentID.UniqueID;
        this.Обозначение = this.rootAssemblyComponentID.Обозначение;
        this.PartNumber = string.Empty;
        this.Designator = this.rootAssemblyComponentID.Designator;
        this.Количество = 1;

        if (this.rootAssemblyComponentID.Designator.Contains(","))
            this.GetDesignatorAndCount();

        this.Fitted = true;
        this.Наименование = this.Обозначение + this.rootAssemblyComponentID.Наименование;
    }

    public PerechenAssemblyRecordESKD(AssemblyComponentID rootAssemblyComponentID)
    {
        this.rootAssemblyComponentID = rootAssemblyComponentID;
        this.Initialization();
    }

    private void GetDesignatorAndCount()
    {
        string repeatDesignator = this.rootAssemblyComponentID.Designator.Replace("Repeat(", string.Empty).Replace(")", string.Empty);
        string[] designators = repeatDesignator.Split(',');
        string baseDesignator = designators[0];
        double firstValue = double.Parse(designators[1]);
        double secondValue = double.Parse(designators[2]);
        this.Количество = secondValue - firstValue + 1;
        this.Designator = this.Количество == 1 ?
            (baseDesignator + firstValue + "," + baseDesignator + secondValue)
            :(baseDesignator + firstValue + "-" + baseDesignator + secondValue);
    }
}