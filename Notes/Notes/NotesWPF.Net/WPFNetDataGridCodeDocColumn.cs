using INotes;
using System.Windows.Controls;
using System;
using System.Windows.Data;

namespace NotesWPF
{
    internal class WPFNetDataGridCodeDocColumn : DataGridTextColumn, IColumn
    {
        public WPFNetDataGridCodeDocColumn()
        {
            this.Header = this.ColumnName;
            this.Binding = new Binding(this.ColumnName);
        }

        public string ColumnName
        {
            get
            {
                return "КодДокумента";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}