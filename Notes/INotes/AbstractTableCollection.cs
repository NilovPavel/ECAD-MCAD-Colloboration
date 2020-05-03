using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public abstract class AbstractTableCollection
    {
        protected INotesRazdel iNotesRazdel;

        public AbstractTableCollection()
        {
            this.Initialization();
        }

        protected abstract void Initialization();
        public abstract void GetCollection(Notes notes, IParser notesParser, DefaultNotesReader defaultNotesReader);
        public abstract void GetVariantCollection(string[] configurationNames, Notes notes, IParser iParser);
        public abstract object GetVariantValue(int row, int column);
    }
}
