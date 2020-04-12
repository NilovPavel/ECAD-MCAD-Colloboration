using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000031 RID: 49
	[CompilerGenerated]
	[Guid("83A33D80-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface ISheet
	{
		// Token: 0x06000097 RID: 151
		void _VtblGap1_36();

		// Token: 0x06000098 RID: 152
		[DispId(34)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetViews();
	}
}
