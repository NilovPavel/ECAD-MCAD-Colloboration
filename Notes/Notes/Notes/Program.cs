using INotes;
using NotesWF;
using NotesWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            INotesCAD iNotesCAD;

            INotesWindow iWindowNotes = new WFWindow();
            iWindowNotes.ShowDialog();

            iWindowNotes = new WPFWindow();
            iWindowNotes.ShowDialog();
        }
    }
}
