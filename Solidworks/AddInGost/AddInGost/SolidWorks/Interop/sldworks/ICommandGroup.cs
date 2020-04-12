using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000024 RID: 36
	[CompilerGenerated]
	[Guid("FF545450-B559-400D-964C-A3811F209148")]
	[TypeIdentifier]
	[ComImport]
	public interface ICommandGroup
	{
		// Token: 0x06000053 RID: 83
		void _VtblGap1_3();

		// Token: 0x06000054 RID: 84
		[DispId(4)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool Activate();

		// Token: 0x06000055 RID: 85
		void _VtblGap2_15();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		[DispId(13)]
		bool HasToolbar { [DispId(13)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(13)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		[DispId(14)]
		bool HasMenu { [DispId(14)] [MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(14)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600005A RID: 90
		void _VtblGap3_4();

		// Token: 0x0600005B RID: 91
		[DispId(18)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int AddCommandItem2([MarshalAs(UnmanagedType.BStr)] [In] string Name, [In] int Position, [MarshalAs(UnmanagedType.BStr)] [In] string HintString, [MarshalAs(UnmanagedType.BStr)] [In] string ToolTip, [In] int ImageListIndex, [MarshalAs(UnmanagedType.BStr)] [In] string CallbackFunction, [MarshalAs(UnmanagedType.BStr)] [In] string EnableMethod, [In] int UserID, [In] int MenuTBOption);
	}
}
