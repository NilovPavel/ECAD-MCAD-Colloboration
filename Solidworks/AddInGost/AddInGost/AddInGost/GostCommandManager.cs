using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System;

namespace AddInGost
{
    internal class GostCommandManager
    {
        private CommandManager commandManager;
        private string addInShowName;
        private AbstractCommandGroup[] gostCommandGroup;
        private CommandGroup commandGroup;

        private void Initialization()
        {
            this.addInShowName = "ГОСТ";
        }


        public GostCommandManager(CommandManager commandManager)
        {
            this.commandManager = commandManager;
            this.Initialization();
            this.AddRootGroup();
            this.AddCommandsGroup();
        }

        private void AddRootGroup()
        {
            int error = 0;
            this.commandGroup = this.commandManager.CreateCommandGroup2(0, this.addInShowName, this.addInShowName, "", -1, false, ref error);
            this.commandGroup.HasMenu = true;
            this.commandGroup.HasToolbar = true;
            this.commandGroup.Activate();
        }

        private void AddCommandsGroup()
        {
            this.gostCommandGroup = new AbstractCommandGroup[]
            {
                new TextDocCommandGroup(this.addInShowName, this.commandManager),
                new DrawingCommandGroup(this.addInShowName, this.commandManager)
            };
        }
    }
}