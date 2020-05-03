using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    abstract public class AbstractHeaderColumnSettings
    {
        private DefaultNotesReader defaultNotesReader;
        private string[] columnNames;
        private IColumn[] columns;

        public IColumn[] Columns
        { get { return this.columns; } }

        private void Initialization()
        {
            this.columns = new IColumn[this.columnNames.Length];
        }

        private void CreateCloumns()
        {
            for (int columnCount = 0; columnCount < this.columnNames.Length; columnCount++)
                this.columns[columnCount] = this.GetIColumn(this.columnNames[columnCount], this.defaultNotesReader);
        }

        abstract protected IColumn GetIColumn(string columnName, DefaultNotesReader defaultNotesReader);

        public AbstractHeaderColumnSettings(string[] columnNames, DefaultNotesReader defaultNotesReader)
        {
            this.columnNames = columnNames;
            this.defaultNotesReader = defaultNotesReader;
            this.Initialization();
            this.CreateCloumns();
        }
    }
}
