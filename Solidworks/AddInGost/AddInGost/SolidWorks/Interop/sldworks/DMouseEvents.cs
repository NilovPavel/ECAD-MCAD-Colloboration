using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000019 RID: 25
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	[Guid("5266B813-FB5F-4831-AED1-A60AB431994A")]
	[TypeIdentifier]
	[ComImport]
	public interface DMouseEvents
	{
		// Token: 0x0600003F RID: 63
		void _VtblGap1_4();

		// Token: 0x06000040 RID: 64
		[DispId(5)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int MouseRBtnDownNotify(int X, int Y, int WParam);
	}
}
