using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INotes;
using System.Windows.Forms;
using System.Windows.Controls;

namespace NotesWF
{
    class WFTable : AbstractTable
    {
        private DataGridView table;

        public DefaultNotesReader DefaultNotesReader
        {
            get { return this.defaultNotesReader; }
        }

        public WFTable(string[] configurationNames, Notes notes, DataGridView table) : base(configurationNames, notes, table)
        {
        }

        protected override void IntializeColumns()
        {
            this.columnSettings = new WFColumnParametersSettings(this.iNotesRazdel.GetColumnNames(), this.defaultNotesReader);
            foreach (IColumn iColumn in this.columnSettings.Columns)
                this.table.Columns.Add((DataGridViewColumn)iColumn);
        }

        protected override void AddVariantColumns()
        {
            foreach (string columnName in this.configurationNames)
                //this.table.Columns.Add(new DataGridViewColumn() { Name = columnName, CellTemplate = new DataGridViewCheckBoxCell() { TrueValue = CheckState.Checked } });
                //this.table.Columns.Add(new VariantColumn(this.columnDataType, columnName, this.iNotesRazdel));
                this.table.Columns.Add(new DataGridViewCheckBoxColumn() { Name = columnName, HeaderText = columnName, DataPropertyName = columnName, TrueValue = CheckState.Indeterminate });
                //this.table.Columns.Add(new DataGridBoolColumn() );

            this.SetVariantData();
        }

        private void SetVariantData()
        {
            string[] columnNames = this.iNotesRazdel.GetColumnNames();
            foreach (DataGridViewRow row in this.table.Rows)
            {
                for (int columnCount = columnNames.Length; columnCount < columnNames.Length + this.configurationNames.Length; columnCount++)
                {
                    row.Cells[columnCount].Value = true;
                }
            }
        }

        protected override void SetTableObject(object table)
        {
            this.table = (DataGridView) table;
        }

        protected override void WriteData(object collection)
        {
            this.table.DataSource = collection;
        }

        protected override void WriteVariantData()
        {
            throw new NotImplementedException();
        }
    }
}
