using INotes;
using NotesWF;
using NotesWPF;
using NotesWPF.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesWindow
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            INotesCAD notesMemory = new NotesMemory();
            ProjectProperties projectProperties = new ProjectProperties()
            {
                Propertie = new List<Propertie>()
                { new Propertie("Обозначение", "XXXX.123456.789") }
            };

            string[] configurationNames = new string[] { "-00", "-01"/*, "-02"*/ };

            foreach (string razdelName in notesMemory.GetRazdelNames())
            {
                Notes notes = new Notes(razdelName, notesMemory);

                INotesWindow iNotesWindow;
                /*iNotesWindow = new WFWindow(configurationNames, projectProperties, notes);
                iNotesWindow.ShowDialog();*/

                iNotesWindow = new WPFNetWindow(configurationNames, projectProperties, notes);
                iNotesWindow.ShowDialog();

                iNotesWindow = new WPFWindow(configurationNames, projectProperties, notes);
                iNotesWindow.ShowDialog();
            }
        }
    }
}
