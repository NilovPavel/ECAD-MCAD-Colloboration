using BoardSpace;
using DXP;
using EDP;
using IAssembly;
using IProjectProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AltiumDebugDLL
{
    public class InterFaceKind : MarshalByRefObject
    {
        private void CreateFiles(string projectPath)
        {
            IAssemblyCAD assem_cad = new AltiumAssembly();
            Assembly board = new Assembly(assem_cad);

            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));

            using (FileStream fs = new FileStream(@"E:\Работа\Sources\CAD\Altium\AltiumBoardData\XMLs\" + Path.GetFileNameWithoutExtension(projectPath) + ".xml", FileMode.Create))
            {
                formatter.Serialize(fs, board);
            }

            using (FileStream fs = new FileStream(@"E:\Работа\Sources\CAD\Altium\AltiumBoardData\XMLs\" + Path.GetFileNameWithoutExtension(projectPath) + ".xml", FileMode.OpenOrCreate))
            {
                board = (Assembly)formatter.Deserialize(fs);
            }
        }

        public void Run()
        {
            this.CreateFiles(((EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace).DM_FocusedProject().DM_ProjectFullPath());

            /*string[] lines = System.IO.File.ReadAllLines(@"D:\TEST\BoardData\Altium\AltiumProjects.txt", Encoding.Default);

            foreach (string line in lines)
            {
                ((EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace).DM_OpenProject(line, true);
                this.CreateFiles(line);
                ((EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace).DM_CloseProject(line);
            }*/
        }
    }
}
