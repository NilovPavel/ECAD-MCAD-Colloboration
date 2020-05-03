using SolidWorks.Interop.sldworks;
using Microsoft.CSharp;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using PositionSpecification;

namespace AddInGost
{
    class DrawingCommandGroup : AbstractCommandGroup
    {
        public DrawingCommandGroup(string addInName, CommandManager commandManager) : base(addInName, commandManager)
        {
        }

        protected override void AddCommands()
        {
            int countCommand = 0;
            this.commandGroup.AddCommandItem2("Расстановка позиций", countCommand++, "Расстановка позиций", "Расстановка позиций", count, "ManualPositions", "", count++, menuToolbarOption);
            this.commandGroup.AddCommandItem2("Обновление позиций", countCommand++, "Обновление позиций", "Обновление позиций", count, "UpdatePositions", "", count++, menuToolbarOption);
        }

        protected override string GetGroupCommandName()
        {
            return "Чертеж";
        }

        public static void ManualPositions(SldWorks swApp)
        {
            ModelDoc2 modelDoc = (ModelDoc2) swApp.ActiveDoc;
            OpenAssemblyXMLDialog fileAssemblyDialog = new OpenAssemblyXMLDialog(modelDoc);
            string fileNameXML = fileAssemblyDialog.FileName;

            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));
            Assembly assembly;

            using (FileStream fs = new FileStream(fileNameXML, FileMode.Open, FileAccess.Read))
            {
                assembly = (Assembly)formatter.Deserialize(fs);
            }

            new ManualPositionSpecification(modelDoc, assembly);
        }

        public static void UpdatePositions(SldWorks swApp)
        {
            ModelDoc2 modelDoc = (ModelDoc2)swApp.ActiveDoc;
            OpenAssemblyXMLDialog fileAssemblyDialog = new OpenAssemblyXMLDialog(modelDoc);
            string fileNameXML = fileAssemblyDialog.FileName;

            XmlSerializer formatter = new XmlSerializer(typeof(Assembly));
            Assembly assembly;

            using (FileStream fs = new FileStream(fileNameXML, FileMode.Open, FileAccess.Read))
            {
                assembly = (Assembly)formatter.Deserialize(fs);
            }

            new AutomaticalPositionSpecification(modelDoc, assembly);
        }
    }

    partial class GostAddIn
    {
        public void ManualPositions()
        {
            TextDocCommandGroup.Specification(this.swApp);
        }

        public void UpdatePositions()
        {
            DrawingCommandGroup.UpdatePositions(this.swApp);
        }
    }
}
