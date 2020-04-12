using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x02000033 RID: 51
	[CompilerGenerated]
	[Guid("83A33D50-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface IView
	{
		// Token: 0x0600009E RID: 158
		void _VtblGap1_270();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009F RID: 159
		[DispId(260)]
		DrawingComponent RootDrawingComponent { [DispId(260)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x060000A0 RID: 160
		void _VtblGap2_28();

		// Token: 0x060000A1 RID: 161
		[DispId(288)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool IsLightweight();

		// Token: 0x060000A2 RID: 162
		void _VtblGap3_1();

		// Token: 0x060000A3 RID: 163
		[DispId(290)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetLightweightToResolved();

		// Token: 0x060000A4 RID: 164
		void _VtblGap4_45();

		// Token: 0x060000A5 RID: 165
		[DispId(331)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetNotes();
	}
}
