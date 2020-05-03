using System;
using INotes;
using DevExpress.Xpf.Grid;
using DevExpress.Utils;
using DevExpress.Xpf.Editors.Settings;
using System.Linq;

namespace NotesWPF
{
    internal class WPFParameterColumn : GridColumn, IColumn
    {
        private DefaultNotesReader defaultNotesReader;

        public string ColumnName
        { get; set; }

        private void Settings()
        {
            switch(this.ColumnName)
            {
                case "Формат":
                    this.EditSettings = new ComboBoxEditSettings();
                    ((ComboBoxEditSettings)this.EditSettings).IsTextEditable = new bool?(false);
                    ((ComboBoxEditSettings)this.EditSettings).ItemsSource = new string[] 
                    {"А4","А3","А2","А1","А4, А3","А4, А3, А2","А4, А3, А2, А1","А4, А3, А1","А4, А2","А4, А2, А1","А3, А2","А3, А2, А1","А3, А1","А2, А1"};
                    break;
                case "Обозначение":
                    break;
                case "Наименование":
                    this.EditSettings = new ComboBoxEditSettings();
                    ((ComboBoxEditSettings)this.EditSettings).IsTextEditable = new bool?(false);
                    ((ComboBoxEditSettings)this.EditSettings).ItemsSource = this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray();
                    break;
                case "Примечание":
                    break;
                case "Едизм":
                    this.ReadOnly = true;
                    break;
                case "КодДокумента":
                    this.ReadOnly = true;
                    this.AllowEditing = DefaultBoolean.False;
                    break;
            }
        }

        private void Initialization()
        {
            this.Name = this.ColumnName;
            this.FieldName = this.ColumnName;
            this.AllowColumnFiltering = DefaultBoolean.False;
            this.AllowSorting = DefaultBoolean.False;
        }

        public WPFParameterColumn(string ColumnName, DefaultNotesReader defaultNotesReader)
        {
            this.ColumnName = ColumnName;
            this.defaultNotesReader = defaultNotesReader;
            this.Initialization();
            this.Settings();
        }
    }
}