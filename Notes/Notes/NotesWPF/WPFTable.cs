using DevExpress.Xpf.Grid;
using INotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWPF
{
    class WPFTable : AbstractTable
    {
        private GridControl table;

        public WPFTable(string[] configurationNames, Notes notes, object table) : base(configurationNames, notes, table)
        {
        }

        protected override void IntializeColumns()
        {
            this.columnSettings = new WPFColumnParametersSettings(this.iNotesRazdel.GetColumnNames(), this.defaultNotesReader);
            foreach (IColumn iColumn in this.columnSettings.Columns)
                this.table.Columns.Add((GridColumn)iColumn);
            this.table.CustomUnboundColumnData += this.CustomUnboundColumnData;
        }

        private void CustomUnboundColumnData(object sender, GridColumnDataEventArgs eventArgs)
        {
            bool isSetData = eventArgs.IsSetData;
            if (isSetData)
            {
            }
            else
            {
                eventArgs.Value = this.tableCollection.GetVariantValue(eventArgs.ListSourceRowIndex, eventArgs.Column.GroupIndex);
            }
        }

        protected override void AddVariantColumns()
        {
            foreach (string columnName in this.configurationNames)
                this.table.Columns.Add(new GridColumn() { FieldName = columnName } );
        }

        protected override void SetTableObject(object table)
        {
            this.table = (GridControl) table;
        }

        protected override void WriteData(object collection)
        {
            this.table.DataContext = collection;
        }

        protected override void WriteVariantData()
        {
            /*for (int rowCount = 0; rowCount < 1; rowCount++)
            {
                for (int columnCount = 0; columnCount < this.configurationNames.Length; columnCount++)
                {
                    this.table.SetCellValue(rowCount, this.configurationNames[columnCount], this.tableCollection.GetVariantValue(rowCount, columnCount));
                }
            }*/
        }
    }
}
