using System;
using INotes;
using System.Linq;
using System.Windows.Controls;

namespace NotesWPF
{
    internal class WPFNetColumnParametersSettings : AbstractHeaderColumnSettings
    {
        public WPFNetColumnParametersSettings(string[] columnNames, DefaultNotesReader defaultNotesReader) : base(columnNames, defaultNotesReader)
        {
        }

        protected override IColumn GetIColumn(string columnName, DefaultNotesReader defaultNotesReader)
        {
            switch (columnName)
            {
                case "Формат":
                    return new WPFNetDataGridFormatColumn();
                case "Обозначение":
                    return new WPFNetDataGridOboznColumn();
                case "Наименование":
                    return new WPFNetDataGridNameColumn(defaultNotesReader);
                case "Примечание":
                    return new WPFNetDataGridPrimechColumn();
                case "Едизм":
                    return new WPFNetDataGridEdIzmColumn();
                case "КодДокумента":
                    return new WPFNetDataGridCodeDocColumn();
            }
            return new WPFNetParameterColumn(columnName, defaultNotesReader);
        }
    }
}