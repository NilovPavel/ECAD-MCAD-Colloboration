using System;
using INotes;

namespace NotesWF
{
    internal class WFColumnParametersSettings : AbstractHeaderColumnSettings
    {
        public WFColumnParametersSettings(string[] columnNames, DefaultNotesReader defaultNotesReader) : base(columnNames, defaultNotesReader)
        {
        }

        protected override IColumn GetIColumn(string columnName, DefaultNotesReader defaultNotesReader)
        {
            return new WFParameterColumn(columnName, defaultNotesReader);
        }
    }
}