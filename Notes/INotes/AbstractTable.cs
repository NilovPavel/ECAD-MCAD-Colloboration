using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public abstract class AbstractTable
    {
        protected string[] configurationNames;
        protected Notes notes;
        protected IParser iParser;
        protected INotesRazdel iNotesRazdel;

        protected eColumnDataType columnDataType;
        protected DefaultNotesReader defaultNotesReader;
        protected AbstractTableCollection tableCollection;
        protected AbstractHeaderColumnSettings columnSettings;

        protected abstract void IntializeColumns();

        protected abstract void AddVariantColumns();

        protected abstract void SetTableObject(object table);

        protected abstract void WriteVariantData();

        protected abstract void WriteData(object collection);

        private void Intialization()
        {
            this.defaultNotesReader = new DefaultNotesReader(this.notes.RazdelName);

            switch (this.notes.RazdelName)
            {
                case "Документация":
                case "Документация ЭМ":
                case "Комплекты":
                case "Комплекты ЭМ":
                    this.iNotesRazdel = new NotesDocumentComplects();
                    this.tableCollection = new DocumentComplectTableCollection();
                    this.WriteData(((DocumentComplectTableCollection)tableCollection).DocumentData);
                    this.columnDataType = eColumnDataType.eBool;
                    break;
                case "Материалы":
                case "Материалы ЭМ":
                    this.iNotesRazdel = new NotesMaterials();
                    this.tableCollection = new MaterialTableCollection();
                    this.WriteData(((MaterialTableCollection)tableCollection).MaterialData);
                    this.columnDataType = eColumnDataType.eDouble;
                    break;
            }

            this.iParser = new NotesSharpParser(this.iNotesRazdel);
        }

        public AbstractTable(string[] configurationNames, Notes notes, object table)
        {
            this.configurationNames = configurationNames;
            this.notes = notes;
            this.SetTableObject(table);
            this.Intialization();
            this.IntializeColumns();
            this.GetData();
            this.AddVariantColumns();
            this.WriteVariantData();
        }

        private void GetData()
        {
            this.tableCollection.GetCollection(this.notes, this.iParser, this.defaultNotesReader);
            this.tableCollection.GetVariantCollection(this.configurationNames, this.notes, this.iParser);
        }
    }
}
