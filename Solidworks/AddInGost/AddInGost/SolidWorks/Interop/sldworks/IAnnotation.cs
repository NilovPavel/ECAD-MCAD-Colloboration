using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000021 RID: 33
	[CompilerGenerated]
	[Guid("83A33DA9-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IAnnotation
	{
		// Token: 0x06000046 RID: 70
		void _VtblGap1_30();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000047 RID: 71
		// (set) Token: 0x06000048 RID: 72
		[DispId(28)]
		int Style { [DispId(28)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(28)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000049 RID: 73
		void _VtblGap2_41();

		// Token: 0x0600004A RID: 74
		[DispId(66)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetAttachedEntities3();
	}
}
