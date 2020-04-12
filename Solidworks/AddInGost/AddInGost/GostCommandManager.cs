using System;
using SolidWorks.Interop.sldworks;

namespace AddInGost
{
	// Token: 0x02000008 RID: 8
	internal class GostCommandManager
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002F3B File Offset: 0x0000113B
		private void Initialization()
		{
			this.addInShowName = "ГОСТ";
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F48 File Offset: 0x00001148
		public GostCommandManager(CommandManager commandManager)
		{
			this.commandManager = commandManager;
			this.Initialization();
			this.AddRootGroup();
			this.AddCommandsGroup();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002F6C File Offset: 0x0000116C
		private void AddRootGroup()
		{
			int num = 0;
			this.commandGroup = this.commandManager.CreateCommandGroup2(0, this.addInShowName, this.addInShowName, "", -1, false, ref num);
			this.commandGroup.HasMenu = true;
			this.commandGroup.HasToolbar = true;
			this.commandGroup.Activate();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002FC6 File Offset: 0x000011C6
		private void AddCommandsGroup()
		{
			this.gostCommandGroup = new AbstractCommandGroup[]
			{
				new TextDocCommandGroup(this.addInShowName, this.commandManager),
				new DrawingCommandGroup(this.addInShowName, this.commandManager)
			};
		}

		// Token: 0x04000013 RID: 19
		private CommandManager commandManager;

		// Token: 0x04000014 RID: 20
		private string addInShowName;

		// Token: 0x04000015 RID: 21
		private AbstractCommandGroup[] gostCommandGroup;

		// Token: 0x04000016 RID: 22
		private CommandGroup commandGroup;
	}
}
