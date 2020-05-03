using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesWF
{
    class VariantColumn : DataGridViewColumn
    {
        private eColumnDataType columnDataType ;
        private string columnName;
        private INotesRazdel iNotesRazdel;

        private void SettingsColumns()
        {
            switch (this.columnDataType)
            {
                case eColumnDataType.eBool:
                    //this.CellTemplate = new CheckBoxCell();
                    this.CellTemplate = new DataGridViewCheckBoxCell();
                    //this.CellTemplate.Value = CheckState.Checked ;
                    //this.CellTemplate.Value = this.iNotesRazdel.GetDefaultValue().Equals("1");
                    break;
                case eColumnDataType.eDouble:
                    this.CellTemplate = new DataGridViewTextBoxCell();
                    //this.CellTemplate.Value = !this.iNotesRazdel.GetDefaultValue().Equals("0");
                    break;
            }
        }

        private void Initalization()
        {
            this.Name = this.columnName;
            this.HeaderText = this.columnName;
            this.DataPropertyName = this.columnName;
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.CellTemplate = new DataGridViewTextBoxCell();
        }

        public VariantColumn(eColumnDataType columnDataType, string columnName, INotesRazdel iNotesRazdel)
        {
            this.columnDataType = columnDataType;
            this.columnName = columnName;
            this.iNotesRazdel = iNotesRazdel;
            this.Initalization();
            this.SettingsColumns();
        }
    }
}
