using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000029 RID: 41
	[CompilerGenerated]
	[Guid("83A33D33-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IDrawingDoc
	{
		// Token: 0x06000068 RID: 104
		void _VtblGap1_122();

		// Token: 0x06000069 RID: 105
		[DispId(123)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int GetSheetCount();

		// Token: 0x0600006A RID: 106
		[DispId(124)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetSheetNames();

		// Token: 0x0600006B RID: 107
		void _VtblGap2_67();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006C RID: 108
		[DispId(190)]
		object ActiveDrawingView { [DispId(190)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x0600006D RID: 109
		void _VtblGap3_59();

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006E RID: 110
		[DispId(250)]
		Sheet Sheet { [DispId(250)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
