using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000030 RID: 48
	[CompilerGenerated]
	[Guid("83A33D59-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface ISelectionMgr
	{
		// Token: 0x06000091 RID: 145
		void _VtblGap1_35();

		// Token: 0x06000092 RID: 146
		[DispId(34)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int GetSelectedObjectCount2([In] int Mark);

		// Token: 0x06000093 RID: 147
		void _VtblGap2_2();

		// Token: 0x06000094 RID: 148
		[DispId(37)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetSelectionPoint2([In] int Index, [In] int Mark);

		// Token: 0x06000095 RID: 149
		void _VtblGap3_20();

		// Token: 0x06000096 RID: 150
		[DispId(57)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object GetSelectedObjectsComponent4([In] int Index, [In] int Mark);
	}
}
