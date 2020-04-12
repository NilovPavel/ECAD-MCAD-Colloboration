using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200001A RID: 26
	[CompilerGenerated]
	[ComEventInterface(typeof(DMouseEvents), typeof(DMouseEvents))]
	[TypeIdentifier("83A33D31-27C5-11CE-BFD4-00400513BB57", "SolidWorks.Interop.sldworks.DMouseEvents_Event")]
	[ComImport]
	public interface DMouseEvents_Event
	{
		// Token: 0x06000041 RID: 65
		void _VtblGap1_8();

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000042 RID: 66
		// (remove) Token: 0x06000043 RID: 67
		event DMouseEvents_MouseRBtnDownNotifyEventHandler MouseRBtnDownNotify;
	}
}
