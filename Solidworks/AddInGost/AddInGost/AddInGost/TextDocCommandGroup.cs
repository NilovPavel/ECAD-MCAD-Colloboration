using System;
using SolidWorks.Interop.sldworks;
using System.Windows.Forms;
using SolidworksBoardData;
using System.Xml.Serialization;
using System.IO;
using ReportsOutput;

namespace AddInGost
{
    public class TextDocCommandGroup : AbstractCommandGroup
    {
        protected override string GetGroupCommandName()
        {
            return "Текстовая документация";
        }

        protected override void AddCommands()
        {
            int countCommand = 0;
            this.commandGroup.AddCommandItem2("Спецификация", countCommand++, "Спецификация", "Спецификация", count, "Specification", "", count++, menuToolbarOption);
        }

        public TextDocCommandGroup(string addInName, CommandManager commandManager) : base(addInName, commandManager)
        {
        }

        public static void Specification(SldWorks swApp)
        {
            IAssemblyCAD iAssemblyCAD = new SolidworksAsseblyXML(swApp);
            Assembly assembly = new Assembly(iAssemblyCAD);
            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));

            string filePathAssembly = ((SolidworksAsseblyXML)iAssemblyCAD).FilePath;
            string filePathXML = Path.GetDirectoryName(filePathAssembly) + @"\" + Path.GetFileNameWithoutExtension(filePathAssembly) + ".xml";

            SpecificationReport report = new SpecificationReport(assembly);

            AssemblyPositionXML assemblyPosition = new AssemblyPositionXML((SpecificationReport)report);
            assemblyPosition.WritePosition(filePathXML);
            if (report.Spisok.Count == 0)
                return;

            IDocument iDocuemnt = new SpecificationADocumentXLS(assembly.projectProperties);
            new SpecificationA(filePathXML, report, iDocuemnt);
        }
    }

    partial class GostAddIn
    {
        public void Specification()
        {
            TextDocCommandGroup.Specification(this.swApp);
        }
    }
}

