using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000023 RID: 35
	[CompilerGenerated]
	[Guid("E267CA95-2ECD-4832-BEF0-BE3C396E34DC")]
	[TypeIdentifier]
	[ComImport]
	public interface IBalloonOptions
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004B RID: 75
		// (set) Token: 0x0600004C RID: 76
		[DispId(1)]
		int Style { [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600004D RID: 77
		void _VtblGap1_4();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		[DispId(4)]
		int UpperTextContent { [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000050 RID: 80
		void _VtblGap2_4();

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000051 RID: 81
		// (set) Token: 0x06000052 RID: 82
		[DispId(7)]
		string LowerText { [DispId(7)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(7)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }
	}
}
