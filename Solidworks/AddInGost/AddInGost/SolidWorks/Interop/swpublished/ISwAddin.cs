using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.swpublished
{
	// Token: 0x0200003D RID: 61
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DA306A0D-EAC5-4406-8610-B1DA805D9270")]
	[TypeIdentifier]
	[ComImport]
	public interface ISwAddin
	{
		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool ConnectToSW([MarshalAs(UnmanagedType.IDispatch)] [In] object ThisSW, [In] int Cookie);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool DisconnectFromSW();
	}
}
