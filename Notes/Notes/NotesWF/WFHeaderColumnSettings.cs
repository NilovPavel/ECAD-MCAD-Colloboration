using System;
using INotes;

namespace NotesWF
{
    internal class WFHeaderColumnSettings : AbstractHeaderColumnSettings
    {
        public WFHeaderColumnSettings(string[] columnNames) : base(columnNames)
        {
        }

        protected override IColumn GetIColumn()
        {
            return new WFColumn();
        }
    }
}