namespace ReportsOutput
{
    internal class StandartPodrazdelWriter : PodrazdelSpWriterSpecificationA
    {
        public StandartPodrazdelWriter(IDocument iDocument) : base(iDocument)
        {
            this.elementWriterSpecificationA = new StandartElementWriterSpecificationA(this.iDocument);
        }

        protected override void WriteSpisokName(Spisok spisok)
        {
            if (spisok.Elements.Length == 1)
                return;
            string[] nameArray = ((StandartElementWriterSpecificationA)this.elementWriterSpecificationA).GetElementSplitArray(spisok.Name);
            for (int cellCount = 0; cellCount < nameArray.Length; cellCount++)
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex + cellCount, nameArray[cellCount]);
            this.iDocument.CurrentRowIndex += nameArray.Length;
        }
    }
}