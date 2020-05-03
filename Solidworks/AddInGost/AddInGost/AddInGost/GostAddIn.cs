using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;

namespace AddInGost
{
    [Guid("f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf"), ComVisible(true)]
    public partial class  GostAddIn : ISwAddin
    {
        private SldWorks swApp;
        private int sessionCookie;
        private GostCommandManager gostCommandManager;
        #region SolidWorks connection

        bool ISwAddin.ConnectToSW(object ThisSW, int Cookie)
        {
            //MessageBox.Show("1");
            this.swApp = (SldWorks)ThisSW;
            this.swApp.SetAddinCallbackInfo(0, this, Cookie);
            this.sessionCookie = Cookie;
            this.gostCommandManager = new GostCommandManager(this.swApp.GetCommandManager(this.sessionCookie));
            return true;
        }

        bool ISwAddin.DisconnectFromSW()
        {
            GC.Collect();
            this.swApp = null;
            return true;
        }

        #endregion

        #region com regiser-unregister function
        [ComRegisterFunction]
        private static void RegisterAssembly(Type t)
        {
            string path = @"SOFTWARE\SolidWorks\AddIns\f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf";
            RegistryKey key = Registry.LocalMachine.CreateSubKey(path);

            key.SetValue(null, 1);
            key.SetValue("Title", "ГОСТ");
            key.SetValue("Description", "For Roman");
        }

        [ComUnregisterFunction]
        private static void UnregisterAssembly(Type t)
        {
            string path = @"SOFTWARE\SolidWorks\AddIns\f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf";
            Registry.LocalMachine.DeleteSubKey(path);
        }

        #endregion
    }
}
