using System;
using DevExpress.Xpf.Grid;
using INotes;

namespace NotesWPF
{
    internal class WPFColumn : IColumn
    {
        private GridColumn gridColumn;

        public GridColumn GridColumn
        { get { return this.gridColumn; } }

        public string Name
        { get; set; }

        private void Initialization()
        {
            this.gridColumn = new GridColumn();
            this.gridColumn.FieldName = this.Name;
            this.gridColumn.Header = this.Name;
            this.gridColumn.AllowColumnFiltering = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn.AllowSorting = DevExpress.Utils.DefaultBoolean.False;
        }

        public WPFColumn()
        {
            this.Initialization();
        }
    }
}