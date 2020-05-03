using System;
using INotes;
using System.Windows.Controls;

namespace NotesWPF
{
    internal class WPFNetDataGridPrimechColumn : DataGridTextColumn, IColumn
    {
        string IColumn.ColumnName
        {
            get
            {
                return "Примечание";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}