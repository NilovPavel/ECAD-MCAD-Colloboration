using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public class MaterialTableCollection : AbstractTableCollection
    {
        private ObservableCollection<MaterialData> materialData;
        private ObservableCollection<double[]> variantCollection;

        public ObservableCollection<MaterialData> MaterialData
        {
            get { return this.materialData; }
        }

        public ObservableCollection<double[]> VariantCollection
        {
            get { return this.variantCollection; }
        }

        public MaterialTableCollection() : base()
        {
        }

        public override void GetCollection(Notes notes, IParser iParser, DefaultNotesReader defaultNotesReader)
        {
            foreach (string noteString in notes.RazdelNotesCollection)
            {
                MaterialData materialDataElement = new MaterialData(defaultNotesReader)
                {
                    Наименование = iParser.GetParameterValue(noteString, "Наименование"),
                    Едизм = iParser.GetParameterValue(noteString, "Едизм"),
                    Примечание = iParser.GetParameterValue(noteString, "Примечание")
                };
                this.materialData.Add(materialDataElement);
                defaultNotesReader.AddCodes(materialDataElement.Наименование, materialDataElement.Едизм);
            }
        }

        protected override void Initialization()
        {
            this.iNotesRazdel = new NotesMaterials();
            this.materialData = new ObservableCollection<MaterialData>();
            this.variantCollection = new ObservableCollection<double[]>();
        }

        public override void GetVariantCollection(string[] configurationNames, Notes notes, IParser iParser)
        {
            foreach (string noteString in notes.RazdelNotesCollection)
            {
                double[] doubleValues = new double[configurationNames.Length];
                for (int variantCount = 0; variantCount < notes.RazdelNotesCollection.Length; variantCount++)
                    doubleValues[variantCount] = iParser.GetCount(noteString, variantCount);
                this.variantCollection.Add(doubleValues);
            }
        }

        public override object GetVariantValue(int row, int column)
        {
            return this.variantCollection[row][column];
        }
    }
}
