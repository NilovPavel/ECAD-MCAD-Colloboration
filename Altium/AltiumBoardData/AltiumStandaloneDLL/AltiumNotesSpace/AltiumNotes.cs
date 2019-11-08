using DXP;
using EDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltiumNotesSpace
{
    class AltiumNotes : INotesCAD
    {
        private IProject project;

        string[] INotesCAD.GetRazdelNames()
        {
            return new string[] { "Документация", "Комплекты", "Материалы" };
        }

        string[] INotesCAD.ReadRazdelNotes(string razdelName)
        {
            List<string> razdelCollection = new List<string>();

            for (int i = 0; i < this.project.DM_ParameterCount(); i++)
                if (this.project.DM_Parameters(i).DM_Name().IndexOf(razdelName) != -1)
                    razdelCollection.Add(this.project.DM_Parameters(i).DM_Value());

            return razdelCollection.ToArray();
        }

        void INotesCAD.WriteNotes(Notes notes)
        {
            throw new NotImplementedException();
        }

        public AltiumNotes(IProject project)
        {
            this.project = project;
        }
    }
}
