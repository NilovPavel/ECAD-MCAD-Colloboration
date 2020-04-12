using System;
using System.IO;
using System.Xml.Serialization;
using ReportsOutput;
using SolidWorks.Interop.sldworks;
using SolidworksBoardData;

namespace AddInGost
{
	// Token: 0x0200000A RID: 10
	public class TextDocCommandGroup : AbstractCommandGroup
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000030D9 File Offset: 0x000012D9
		protected override string GetGroupCommandName()
		{
			return "Текстовая документация";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030E0 File Offset: 0x000012E0
		protected override void AddCommands()
		{
			int num = 0;
			this.commandGroup.AddCommandItem2("Спецификация", num++, "Спецификация", "Спецификация", AbstractCommandGroup.count, "Specification", "", AbstractCommandGroup.count++, this.menuToolbarOption);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B76 File Offset: 0x00000D76
		public TextDocCommandGroup(string addInName, CommandManager commandManager) : base(addInName, commandManager)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003134 File Offset: 0x00001334
		public static void Specification(SolidWorks.Interop.sldworks.SldWorks swApp)
		{
			SolidworksAsseblyXML solidworksAsseblyXML = new SolidworksAsseblyXML(swApp);
			Assembly assembly = new Assembly(solidworksAsseblyXML);
			new XmlSerializer(typeof(Assembly));
			string filePath = ((SolidworksAsseblyXML)solidworksAsseblyXML).FilePath;
			string text = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".xml";
			SpecificationReport specificationReport = new SpecificationReport(assembly);
			new AssemblyPositionXML(specificationReport).WritePosition(text);
			if (specificationReport.Spisok.Count == 0)
			{
				return;
			}
			IDocument iDocument = new SpecificationADocumentXLS(assembly.projectProperties);
			new SpecificationA(text, specificationReport, iDocument);
		}
	}
}
