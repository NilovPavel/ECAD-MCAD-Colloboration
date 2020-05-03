using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public class DocumentComplectTableCollection : AbstractTableCollection
    {
        private ObservableCollection<DocumentData> documentData;
        private ObservableCollection<bool[]> variantCollection;
        
        public ObservableCollection<DocumentData> DocumentData
        {
            get { return this.documentData; }
        }

        public DocumentComplectTableCollection() : base()
        {
        }

        public override void GetCollection(Notes notes, IParser iParser, DefaultNotesReader defaultNotesReader)
        {
            foreach (string noteString in notes.RazdelNotesCollection)
            {
                DocumentData documentDataElement = new DocumentData(defaultNotesReader)
                {
                    Формат = iParser.GetParameterValue(noteString, "Формат"),
                    Обозначение = iParser.GetParameterValue(noteString, "Обозначение"),
                    Наименование = iParser.GetParameterValue(noteString, "Наименование"),
                    КодДокумента = iParser.GetParameterValue(noteString, "КодДокумента"),
                    Примечание = iParser.GetParameterValue(noteString, "Примечание")
                };
                this.documentData.Add(documentDataElement);
                defaultNotesReader.AddCodes(documentDataElement.Наименование, documentDataElement.КодДокумента);
            }
        }

        protected override void Initialization()
        {
            this.iNotesRazdel = new NotesDocumentComplects();
            this.documentData = new ObservableCollection<DocumentData>();
            this.variantCollection = new ObservableCollection<bool[]>();
        }

        public override void GetVariantCollection(string[] configurationNames, Notes notes, IParser iParser)
        {
            foreach (string noteString in notes.RazdelNotesCollection)
            {
                bool[] boolValues = new bool[configurationNames.Length];
                for (int variantCount = 0; variantCount < notes.RazdelNotesCollection.Length; variantCount++)
                    boolValues[variantCount] = iParser.GetCount(noteString, variantCount) == 1;
                this.variantCollection.Add(boolValues);
            }
        }

        public override object GetVariantValue(int row, int column)
        {
            return this.variantCollection[row][column];
        }
    }
}
