using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000027 RID: 39
	[CompilerGenerated]
	[Guid("83A33D98-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IConfiguration
	{
		// Token: 0x06000063 RID: 99
		void _VtblGap1_2();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		[DispId(3)]
		string Name { [DispId(3)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(3)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }
	}
}
