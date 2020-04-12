using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000028 RID: 40
	[CompilerGenerated]
	[Guid("F25E6093-1A6F-46D3-9866-860934DF611D")]
	[TypeIdentifier]
	[ComImport]
	public interface IDrawingComponent
	{
		// Token: 0x06000066 RID: 102
		void _VtblGap1_2();

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000067 RID: 103
		[DispId(3)]
		Component2 Component { [DispId(3)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
