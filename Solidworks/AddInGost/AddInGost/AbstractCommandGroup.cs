using System;
using SolidWorks.Interop.sldworks;

namespace AddInGost
{
	// Token: 0x02000007 RID: 7
	public abstract class AbstractCommandGroup
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002E61 File Offset: 0x00001061
		private void Settings()
		{
			this.commandGroup.HasMenu = true;
			this.commandGroup.HasToolbar = true;
			this.commandGroup.Activate();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E88 File Offset: 0x00001088
		private void Initialization()
		{
			this.cmdGroupErr = 0;
			this.menuToolbarOption = 3;
			ICommandManager commandManager = this.commandManager;
			int userID = AbstractCommandGroup.count++;
			string title = this.groupName;
			string groupCommandName = this.GetGroupCommandName();
			string hint = "";
			int position = AbstractCommandGroup.count;
			bool ignorePreviousVersion = false;
			int num = this.cmdGroupErr;
			this.commandGroup = commandManager.CreateCommandGroup2(userID, title, groupCommandName, hint, position, ignorePreviousVersion, ref num);
		}

		// Token: 0x06000028 RID: 40
		protected abstract string GetGroupCommandName();

		// Token: 0x06000029 RID: 41
		protected abstract void AddCommands();

		// Token: 0x0600002A RID: 42 RVA: 0x00002EE4 File Offset: 0x000010E4
		public AbstractCommandGroup(string addInName, CommandManager commandManager)
		{
			this.addInName = addInName;
			this.groupName = this.addInName + "\\" + this.GetGroupCommandName();
			this.commandManager = commandManager;
			this.Initialization();
			this.AddCommands();
			this.Settings();
		}

		// Token: 0x0400000C RID: 12
		private CommandManager commandManager;

		// Token: 0x0400000D RID: 13
		private int cmdGroupErr;

		// Token: 0x0400000E RID: 14
		private string addInName;

		// Token: 0x0400000F RID: 15
		private string groupName;

		// Token: 0x04000010 RID: 16
		protected CommandGroup commandGroup;

		// Token: 0x04000011 RID: 17
		protected static int count = 1;

		// Token: 0x04000012 RID: 18
		protected int menuToolbarOption;
	}
}
