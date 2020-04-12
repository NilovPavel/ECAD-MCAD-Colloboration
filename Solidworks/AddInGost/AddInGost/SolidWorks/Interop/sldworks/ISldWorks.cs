using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000032 RID: 50
	[CompilerGenerated]
	[Guid("83A33D22-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface ISldWorks
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000099 RID: 153
		[DispId(1)]
		object ActiveDoc { [DispId(1)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; }

		// Token: 0x0600009A RID: 154
		void _VtblGap1_147();

		// Token: 0x0600009B RID: 155
		[DispId(146)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool SetAddinCallbackInfo([In] int ModuleHandle, [MarshalAs(UnmanagedType.IDispatch)] [In] object AddinCallbacks, [In] int Cookie);

		// Token: 0x0600009C RID: 156
		void _VtblGap2_75();

		// Token: 0x0600009D RID: 157
		[DispId(220)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		CommandManager GetCommandManager([In] int Cookie);
	}
}
