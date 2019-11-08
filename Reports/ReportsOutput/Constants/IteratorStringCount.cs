using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOutput.ExcelConstants
{
    public class IteratorStringCount : IIteratorAction
    {
        private int count;
        private IDocument iDocument;
        private bool isEnd;

        public int SpisokCountSpace
        { get { return this.count; } }

        void IIteratorAction.ElementAction(RecordESKD recordESKD)
        {
            if (this.isEnd)
                return;

            this.isEnd = true;
            Razbivka razbivkaElement = new Razbivka(recordESKD.Наименование, this.iDocument.ITemplateDocument.NameLength);
            this.count += razbivkaElement.StringAllowLengthArray.Length;
        }

        void IIteratorAction.RazdelAction(Spisok spisok)
        {
            if (this.isEnd)
                return;

            Razbivka razbivka = new Razbivka(spisok.Name, this.iDocument.ITemplateDocument.NameLength);
            this.count += razbivka.StringAllowLengthArray.Length + 1;
        }

        private void Initialization()
        {
            this.count = 0;
            this.isEnd = false;
        }

        public IteratorStringCount(IDocument iDocument)
        {
            this.iDocument = iDocument;
            this.Initialization();
            
        }
    }
}
