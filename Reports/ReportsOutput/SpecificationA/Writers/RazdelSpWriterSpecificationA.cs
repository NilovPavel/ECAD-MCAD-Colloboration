using ReportsOutput.ExcelConstants;
using System;

namespace ReportsOutput
{
    public class RazdelSpWriterSpecificationA : IRazdelSpWriter
    {
        private IIteratorAction iteratorAction;
        private IDocument iDocument;
        private IDocumentCellStyle iDocumentCellStyle;
        private IDocumentFont iDocumentFont;
        private IPodRazdelSpWriter podrazdelSpWriter;
        private IElementWriter elementWriterSpecificationA;

        private void SetCellStyleSettings()
        {
            this.iDocumentCellStyle = this.iDocument.CellStyle;
            this.iDocumentFont = this.iDocumentCellStyle.IDocumentFont;

            this.iDocumentFont.Bold = true;
            this.iDocumentFont.Italic = true;
            this.iDocumentFont.UnderLine = true;

            this.iDocumentCellStyle.SetFont(this.iDocumentFont);

            this.iDocumentCellStyle.HorizlontalCenter = true;
            this.iDocumentCellStyle.VerticalCenter = true;
        }

        private void Initialization()
        {
            this.SetCellStyleSettings();
        }

        public RazdelSpWriterSpecificationA(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
        }

        private void WriteSpisokName(Spisok spisok)
        {
            this.iDocument.WriteCellValue(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, spisok.Name);
            this.iDocument.SetCellStyle(this.iDocument.CurrentSheet.NameCellColumn, this.iDocument.CurrentRowIndex, this.iDocumentCellStyle);
            this.iDocument.CurrentRowIndex++;
        }

        void IRazdelSpWriter.Write(Spisok spisok)
        {
            this.elementWriterSpecificationA = this.GetElementWriterSpecificationA(spisok.Name);
            this.podrazdelSpWriter = this.GetPodrazdelSpWriterSpecificationA(spisok.Name);

            IteratorSpisok iteratorSpisok = new IteratorSpisok(spisok);
            this.iteratorAction = new IteratorStringCount(this.iDocument);
            iteratorSpisok.SetIteratorAction(this.iteratorAction);
            iteratorSpisok.Iteration();

            if (this.iDocument.CurrentRowIndex + ((IteratorStringCount)this.iteratorAction).SpisokCountSpace >= this.iDocument.CurrentSheet.LastRowIndex)
                this.iDocument.NextSheet();

            this.WriteSpisokName(spisok);
            this.WriteElements(spisok);
            this.WriteRazdels(spisok);
            this.iDocument.CurrentRowIndex++;
        }

        private IPodRazdelSpWriter GetPodrazdelSpWriterSpecificationA(string name)
        {
            switch (name)
            {
                case "Стандартные изделия":
                    return new StandartPodrazdelWriter(this.iDocument);
                case "Прочие изделия":
                    return new OtherPodrazdelWriter(this.iDocument);
            }
            return new PodrazdelSpWriterSpecificationA(this.iDocument);
        }

        private void WriteRazdels(Spisok spisok)
        {
            if (spisok.Razdels.Count == 0)
                return;
            this.iDocument.CurrentRowIndex++;
            spisok.Razdels.ForEach(item => this.podrazdelSpWriter.Write(item));
        }

        private ElementWriterSpecificationA GetElementWriterSpecificationA(string name)
        {
            switch (name)
            {
                case "Документация":
                case "Комплекты":
                    return new DocumentsComplectsElementWriterSpecificationA(this.iDocument);
                case "Сборочные единицы":
                case "Детали":
                    return new AssemblyDetailsElementWriterSpecificationA(this.iDocument);
            }        
            return new ElementWriterSpecificationA(this.iDocument);
        }

        private void WriteElements(Spisok spisok)
        {
            if (spisok.Elements == null)
                return;
            this.iDocument.CurrentRowIndex++;
            Array.ForEach(spisok.Elements, item => this.elementWriterSpecificationA.Write(item, spisok));
        }
    }
}