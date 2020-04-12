using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000015 RID: 21
	[CompilerGenerated]
	[ComEventInterface(typeof(DDrawingDocEvents), typeof(DDrawingDocEvents))]
	[TypeIdentifier("83A33D31-27C5-11CE-BFD4-00400513BB57", "SolidWorks.Interop.sldworks.DDrawingDocEvents_Event")]
	[ComImport]
	public interface DDrawingDocEvents_Event
	{
		// Token: 0x0600003A RID: 58
		void _VtblGap1_100();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003B RID: 59
		// (remove) Token: 0x0600003C RID: 60
		event DDrawingDocEvents_UserSelectionPostNotifyEventHandler UserSelectionPostNotify;
	}
}
