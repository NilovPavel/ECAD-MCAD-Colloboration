using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200002C RID: 44
	[CompilerGenerated]
	[Guid("99F4D4AF-F268-4EE1-8C55-041F7BECF879")]
	[TypeIdentifier]
	[ComImport]
	public interface IModelDocExtension
	{
		// Token: 0x06000084 RID: 132
		void _VtblGap1_227();

		// Token: 0x06000085 RID: 133
		[DispId(222)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		BalloonOptions CreateBalloonOptions();

		// Token: 0x06000086 RID: 134
		[DispId(223)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		Note InsertBOMBalloon2([MarshalAs(UnmanagedType.Interface)] [In] BalloonOptions BalloonOptions);
	}
}
