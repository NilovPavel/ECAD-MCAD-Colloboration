using INotes;
using System.Windows.Controls;
using System;
using System.Windows.Data;
using System.Windows;

namespace NotesWPF
{
    internal class WPFNetDataGridOboznColumn : DataGridTextColumn, IColumn
    {
        public WPFNetDataGridOboznColumn()
        {
            this.Header = this.ColumnName;
            this.Binding = new Binding(this.ColumnName);
        }

        public string ColumnName
        {
            get
            {
                return "Обозначение";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}