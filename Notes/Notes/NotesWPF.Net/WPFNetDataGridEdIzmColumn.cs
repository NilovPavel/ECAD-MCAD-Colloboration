using System;
using INotes;
using System.Windows.Controls;

namespace NotesWPF
{
    internal class WPFNetDataGridEdIzmColumn : DataGridTextColumn, IColumn
    {

        string IColumn.ColumnName
        {
            get
            {
                return "Едизм";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}