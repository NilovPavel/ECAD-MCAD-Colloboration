using System;
using INotes;

namespace NotesWPF
{
    internal class WPFHeaderColumnSettings : AbstractHeaderColumnSettings
    {
        public WPFHeaderColumnSettings(string[] columnNames) : base(columnNames)
        {
        }

        protected override IColumn GetIColumn()
        {
            return new WPFColumn();
        }
    }
}