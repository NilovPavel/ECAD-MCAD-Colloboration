using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Microsoft.CSharp.RuntimeBinder;
using PositionSpecification;
using SolidWorks.Interop.sldworks;

namespace AddInGost
{
	// Token: 0x02000005 RID: 5
	internal class DrawingCommandGroup : AbstractCommandGroup
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002B76 File Offset: 0x00000D76
		public DrawingCommandGroup(string addInName, CommandManager commandManager) : base(addInName, commandManager)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B80 File Offset: 0x00000D80
		protected override void AddCommands()
		{
			int num = 0;
			this.commandGroup.AddCommandItem2("Расстановка позиций", num++, "Расстановка позиций", "Расстановка позиций", AbstractCommandGroup.count, "ManualPositions", "", AbstractCommandGroup.count++, this.menuToolbarOption);
			this.commandGroup.AddCommandItem2("Обновление позиций", num++, "Обновление позиций", "Обновление позиций", AbstractCommandGroup.count, "UpdatePositions", "", AbstractCommandGroup.count++, this.menuToolbarOption);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C13 File Offset: 0x00000E13
		protected override string GetGroupCommandName()
		{
			return "Чертеж";
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static void ManualPositions(SolidWorks.Interop.sldworks.SldWorks swApp)
		{
			if (DrawingCommandGroup.<>o__3.<>p__0 == null)
			{
				DrawingCommandGroup.<>o__3.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(SolidWorks.Interop.sldworks.ModelDoc2), typeof(DrawingCommandGroup)));
			}
			SolidWorks.Interop.sldworks.ModelDoc2 modelDoc = DrawingCommandGroup.<>o__3.<>p__0.Target(DrawingCommandGroup.<>o__3.<>p__0, swApp.ActiveDoc);
			string fileName = new OpenAssemblyXMLDialog(modelDoc).FileName;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Assembly));
			Assembly assembly;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				assembly = (Assembly)xmlSerializer.Deserialize(fileStream);
			}
			new ManualPositionSpecification(modelDoc, assembly);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static void UpdatePositions(SolidWorks.Interop.sldworks.SldWorks swApp)
		{
			if (DrawingCommandGroup.<>o__4.<>p__0 == null)
			{
				DrawingCommandGroup.<>o__4.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(SolidWorks.Interop.sldworks.ModelDoc2), typeof(DrawingCommandGroup)));
			}
			SolidWorks.Interop.sldworks.ModelDoc2 modelDoc = DrawingCommandGroup.<>o__4.<>p__0.Target(DrawingCommandGroup.<>o__4.<>p__0, swApp.ActiveDoc);
			string fileName = new OpenAssemblyXMLDialog(modelDoc).FileName;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Assembly));
			Assembly assembly;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				assembly = (Assembly)xmlSerializer.Deserialize(fileStream);
			}
			new AutomaticalPositionSpecification(modelDoc, assembly);
		}
	}
}
