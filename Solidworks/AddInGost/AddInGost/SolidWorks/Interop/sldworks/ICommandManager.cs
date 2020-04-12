using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000025 RID: 37
	[CompilerGenerated]
	[Guid("F61069CF-2E42-4AC4-A517-6A95B79E45EE")]
	[TypeIdentifier]
	[ComImport]
	public interface ICommandManager
	{
		// Token: 0x0600005C RID: 92
		void _VtblGap1_13();

		// Token: 0x0600005D RID: 93
		[DispId(14)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		CommandGroup CreateCommandGroup2([In] int UserID, [MarshalAs(UnmanagedType.BStr)] [In] string Title, [MarshalAs(UnmanagedType.BStr)] [In] string ToolTip, [MarshalAs(UnmanagedType.BStr)] [In] string Hint, [In] int Position, [In] bool IgnorePreviousVersion, [In] [Out] ref int Errors);
	}
}
