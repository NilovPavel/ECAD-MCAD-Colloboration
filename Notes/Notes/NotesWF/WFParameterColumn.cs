using INotes;
using System.Windows.Forms;
using System;
using System.Linq;
using System.ComponentModel;

namespace NotesWF
{
    internal class WFParameterColumn : DataGridViewColumn, IColumn
    {
        public string ColumnName
        { get; set; }

        private DefaultNotesReader defaultNotesReader;

        private void Initialization()
        {
            this.Name = this.ColumnName;
            this.HeaderText = this.ColumnName;
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.CellTemplate = new DataGridViewTextBoxCell();
            this.DataPropertyName = this.ColumnName;
        }

        public WFParameterColumn(string ColumnName, DefaultNotesReader defaultNotesReader)
        {
            this.ColumnName = ColumnName;
            this.defaultNotesReader = defaultNotesReader;
            this.Initialization();
            this.Settings();
        }

        private void Settings()
        {
            switch (this.ColumnName)
            {
                case "Формат":
                    this.CellTemplate = new DataGridViewComboBoxCell();
                    ((DataGridViewComboBoxCell)this.CellTemplate).Items.AddRange(new string[]
                    {"А4","А3","А2","А1","А4, А3","А4, А3, А2","А4, А3, А2, А1","А4, А3, А1","А4, А2","А4, А2, А1","А3, А2","А3, А2, А1","А3, А1","А2, А1"});
                    break;
                case "Обозначение":
                    break;
                case "Наименование":
                    this.CellTemplate = new DataGridViewComboBoxCell();
                    ((DataGridViewComboBoxCell)this.CellTemplate).Items.AddRange(this.defaultNotesReader.SectionPair.Select(item => item.Name).ToArray());
                    break;
                case "Примечание":
                    break;
                case "Едизм":
                    this.HeaderText = "Единица измерения";
                    this.ReadOnly = true;
                    break;
                case "КодДокумента":
                    this.HeaderText = "Код документа";
                    this.ReadOnly = true;
                    break;
            }
        }
    }
}