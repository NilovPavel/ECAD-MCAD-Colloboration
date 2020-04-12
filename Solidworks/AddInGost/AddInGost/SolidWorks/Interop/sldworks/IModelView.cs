using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200002D RID: 45
	[CompilerGenerated]
	[Guid("83A33D4C-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IModelView
	{
		// Token: 0x06000087 RID: 135
		void _VtblGap1_95();

		// Token: 0x06000088 RID: 136
		[DispId(68)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		Mouse GetMouse();
	}
}
