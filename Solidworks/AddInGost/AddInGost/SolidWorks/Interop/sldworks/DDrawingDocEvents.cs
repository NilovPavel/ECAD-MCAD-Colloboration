using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000014 RID: 20
	[CompilerGenerated]
	[Guid("83A33D34-37C5-11CE-BFD4-00400513BB57")]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	[TypeIdentifier]
	[ComImport]
	public interface DDrawingDocEvents
	{
		// Token: 0x06000038 RID: 56
		void _VtblGap1_50();

		// Token: 0x06000039 RID: 57
		[DispId(51)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int UserSelectionPostNotify();
	}
}
