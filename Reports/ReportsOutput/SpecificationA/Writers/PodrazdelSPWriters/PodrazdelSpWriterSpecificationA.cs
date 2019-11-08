using ReportsOutput.ExcelConstants;
using System;

namespace ReportsOutput
{
    public class PodrazdelSpWriterSpecificationA : IPodRazdelSpWriter
    {
        protected IDocument iDocument;
        protected IElementWriter elementWriterSpecificationA;
        private IIteratorAction iteratorAction;

        public PodrazdelSpWriterSpecificationA(IDocument iDocument)
        {
            this.iDocument = iDocument;
        }

        protected virtual void WriteSpisokName(Spisok spisok)
        {
            string[] nameArray = new Razbivka(spisok.Name, this.iDocument.ITemplateDocument.NameLength).StringAllowLengthArray;
            for (int cellCount = 0; cellCount < nameArray.Length; cellCount++)
                this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex + cellCount, nameArray[cellCount]);
            this.iDocument.CurrentRowIndex += nameArray.Length;
        }

        void IPodRazdelSpWriter.Write(Spisok spisok)
        {
            IteratorSpisok iteratorSpisok = new IteratorSpisok(spisok);
            this.iteratorAction = new IteratorStringCount(this.iDocument);
            iteratorSpisok.SetIteratorAction(this.iteratorAction);
            iteratorSpisok.Iteration();

            if (this.iDocument.CurrentRowIndex + ((IteratorStringCount)this.iteratorAction).SpisokCountSpace >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.WriteSpisokName(spisok);
            this.WriteElements(spisok);
            this.iDocument.CurrentRowIndex++;
        }

        private void WriteElements(Spisok spisok)
        {
            Array.ForEach(spisok.Elements, item => this.elementWriterSpecificationA.Write(item, spisok));
        }
    }
}