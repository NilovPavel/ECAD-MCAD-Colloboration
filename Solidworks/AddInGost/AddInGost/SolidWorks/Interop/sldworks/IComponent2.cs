using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000026 RID: 38
	[CompilerGenerated]
	[Guid("655D6F2A-5441-45D1-8CBA-D35FB26988E4")]
	[TypeIdentifier]
	[ComImport]
	public interface IComponent2
	{
		// Token: 0x0600005E RID: 94
		void _VtblGap1_62();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		[DispId(59)]
		string Name2 { [DispId(59)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(59)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

		// Token: 0x06000061 RID: 97
		void _VtblGap2_67();

		// Token: 0x06000062 RID: 98
		[DispId(122)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object GetModelDoc2();
	}
}
