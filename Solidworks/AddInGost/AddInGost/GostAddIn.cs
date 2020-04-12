using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;

namespace AddInGost
{
	// Token: 0x02000006 RID: 6
	[Guid("f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf")]
	[ComVisible(true)]
	public class GostAddIn : ISwAddin
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002D6C File Offset: 0x00000F6C
		bool ISwAddin.ConnectToSW(object ThisSW, int Cookie)
		{
			this.swApp = (SolidWorks.Interop.sldworks.SldWorks)ThisSW;
			this.swApp.SetAddinCallbackInfo(0, this, Cookie);
			this.sessionCookie = Cookie;
			this.gostCommandManager = new GostCommandManager(this.swApp.GetCommandManager(this.sessionCookie));
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002DB8 File Offset: 0x00000FB8
		bool ISwAddin.DisconnectFromSW()
		{
			GC.Collect();
			this.swApp = null;
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DC8 File Offset: 0x00000FC8
		[ComRegisterFunction]
		private static void RegisterAssembly(Type t)
		{
			string subkey = "SOFTWARE\\SolidWorks\\AddIns\\f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf";
			RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subkey);
			registryKey.SetValue(null, 1);
			registryKey.SetValue("Title", "ГОСТ");
			registryKey.SetValue("Description", "For Roman");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E14 File Offset: 0x00001014
		[ComUnregisterFunction]
		private static void UnregisterAssembly(Type t)
		{
			string subkey = "SOFTWARE\\SolidWorks\\AddIns\\f0206a7e-bbec-4841-8f6f-5a1bffc3bfbf";
			Registry.LocalMachine.DeleteSubKey(subkey);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E32 File Offset: 0x00001032
		public void Specification()
		{
			TextDocCommandGroup.Specification(this.swApp);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E3F File Offset: 0x0000103F
		public void ManualPositions()
		{
			DrawingCommandGroup.ManualPositions(this.swApp);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E4C File Offset: 0x0000104C
		public void UpdatePositions()
		{
			DrawingCommandGroup.UpdatePositions(this.swApp);
		}

		// Token: 0x04000009 RID: 9
		private SolidWorks.Interop.sldworks.SldWorks swApp;

		// Token: 0x0400000A RID: 10
		private int sessionCookie;

		// Token: 0x0400000B RID: 11
		private GostCommandManager gostCommandManager;
	}
}
