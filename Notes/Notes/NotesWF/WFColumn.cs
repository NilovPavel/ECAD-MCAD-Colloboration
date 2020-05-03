using System;
using INotes;
using System.Windows.Forms;

namespace NotesWF
{
    internal class WFColumn : IColumn
    {
        private DataGridViewColumn dataGridViewColumn;

        public DataGridViewColumn DataGridViewColumn
        { get { return this.dataGridViewColumn; } }

        public string Name
        { get; set; }

        private void Initialization()
        {
            this.dataGridViewColumn.CellTemplate = new DataGridViewTextBoxCell();
            this.dataGridViewColumn = new DataGridViewColumn();
            this.dataGridViewColumn.Name = this.Name;
            this.dataGridViewColumn.HeaderText = this.Name;
            this.dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public WFColumn()
        {
            this.Initialization();
        }
    }
}