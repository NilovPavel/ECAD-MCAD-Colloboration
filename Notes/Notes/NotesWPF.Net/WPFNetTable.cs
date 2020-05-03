using INotes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace NotesWPF
{
    class WPFNetTable : AbstractTable
    {
        private DataGrid table;
        public ObservableCollection<DocumentData> Collection
        { get; private set; }

        public WPFNetTable(string[] configurationNames, Notes notes, object table) : base(configurationNames, notes, table)
        {
        }

        protected override void AddVariantColumns()
        {
        }

        protected override void IntializeColumns()
        {
            /*this.columnSettings = new WPFNetColumnParametersSettings(this.iNotesRazdel.GetColumnNames(), this.defaultNotesReader);
            foreach (IColumn iColumn in this.columnSettings.Columns)
                this.table.Columns.Add((DataGridColumn)iColumn);*/
        }

        protected override void SetTableObject(object table)
        {
            this.table = (DataGrid) table;
        }

        protected override void WriteData(object collection)
        {
            //this.table.ItemsSource = (IEnumerable)collection;
            //this.table.DataContext;
            //this.table.DataContext = collection;
        }

        protected override void WriteVariantData()
        {
            return;
        }
    }
}
