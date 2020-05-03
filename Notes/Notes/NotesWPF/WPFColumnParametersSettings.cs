using System;
using INotes;

namespace NotesWPF
{
    internal class WPFColumnParametersSettings : AbstractHeaderColumnSettings
    {
        public WPFColumnParametersSettings(string[] columnNames, DefaultNotesReader defaultNotesReader) : base(columnNames, defaultNotesReader)
        {
        }

        protected override IColumn GetIColumn(string columnName, DefaultNotesReader defaultNotesReader)
        {
            return new WPFParameterColumn(columnName, defaultNotesReader);
        }
    }
}