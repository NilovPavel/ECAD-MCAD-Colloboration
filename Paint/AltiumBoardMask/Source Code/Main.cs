using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DXP;
using EDP;

namespace CSharpPlugin
{
    public interface IPluginFactory
    {
        object InvokePluginFactory(IClient a);
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class PluginFactory : IPluginFactory
    {
        public object InvokePluginFactory(IClient a)
        {
            PluginServerModule serverModule = new PluginServerModule(a, "AltiumBoardMask");
            return serverModule;
        }
    }

    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class PluginServerModule : ServerModule
    {
        public PluginServerModule(IClient argClient, string argModuleName)
            : base(argClient, argModuleName)
        {
        }

        protected override void InitializeCommands()
        {
            ((CommandLauncher)CommandLauncher).RegisterCommand("Screen", this.Command_BoardData, this.GetState_BoardData);
        }

        protected override IServerDocument NewDocumentInstance(string kind, string fileName)
        {
            return null;
        }

        private void GetState_BoardData(IServerDocumentView argContext, ref string argParameters, ref bool argEnabled, ref bool argChecked, ref bool argVisible, ref string argCaption, ref string argImageFile)
        {
            new Commands().GetState_Screen(argContext, ref argParameters, ref argEnabled, ref argChecked, ref argVisible, ref argCaption, ref argImageFile);
            return;
        }
        
        private void Command_BoardData(IServerDocumentView argView, ref string argParameters)
        {
            new Commands().Command_Screen(argView, ref argParameters);
            return;
        }
    }
}
