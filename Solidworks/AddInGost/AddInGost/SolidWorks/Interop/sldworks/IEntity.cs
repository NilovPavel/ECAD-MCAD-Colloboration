using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200002A RID: 42
	[CompilerGenerated]
	[Guid("83A33D65-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IEntity
	{
		// Token: 0x0600006F RID: 111
		void _VtblGap1_8();

		// Token: 0x06000070 RID: 112
		[DispId(65545)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object GetComponent();
	}
}
