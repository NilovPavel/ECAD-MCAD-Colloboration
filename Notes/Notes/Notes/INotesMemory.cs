using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWindow
{
    class NotesMemory : INotesCAD
    {
        private NotesFile noteFile;

        string[] INotesCAD.GetRazdelNames()
        {
            return new string[] { "Документация", "Комплекты", "Материалы" };
        }

        string[] INotesCAD.ReadRazdelNotes(string razdelName)
        {
            return this.noteFile.GetLines(razdelName);
        }

        void INotesCAD.WriteNotes(Notes notes)
        {
            this.noteFile.WriteLines(notes.RazdelName, notes.RazdelNotesCollection);
        }

        private void Initialization()
        {
            this.noteFile = new NotesFile();
        }

        public NotesMemory()
        {
            this.Initialization();
        }
    }
}