using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;

namespace AddInGost
{
    public abstract class AbstractCommandGroup
    {
        private CommandManager commandManager;
        private int cmdGroupErr;
        private string addInName;
        private string groupName;

        protected CommandGroup commandGroup;
        protected static int count = 1;
        protected int menuToolbarOption;

        private void Settings()
        {
            this.commandGroup.HasMenu = true;
            this.commandGroup.HasToolbar = true;
            this.commandGroup.Activate();
        }

        private void Initialization()
        {
            this.cmdGroupErr = 0;
            this.menuToolbarOption = (int)(swCommandItemType_e.swMenuItem | swCommandItemType_e.swToolbarItem);
            this.commandGroup = this.commandManager.CreateCommandGroup2(count++, this.groupName, this.GetGroupCommandName(), "", count, false, cmdGroupErr);
        }

        abstract protected string GetGroupCommandName();

        abstract protected void AddCommands();

        public AbstractCommandGroup(string addInName, CommandManager commandManager)
        {
            this.addInName = addInName;
            this.groupName = this.addInName + @"\" + this.GetGroupCommandName();
            this.commandManager = commandManager;
            this.Initialization();
            this.AddCommands();
            this.Settings();
        }
    }
}