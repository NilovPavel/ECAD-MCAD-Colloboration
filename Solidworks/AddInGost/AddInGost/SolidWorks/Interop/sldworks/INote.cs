using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200002F RID: 47
	[CompilerGenerated]
	[Guid("83A33D55-27C5-11CE-BFD4-00400513BB57")]
	[TypeIdentifier]
	[ComImport]
	public interface INote
	{
		// Token: 0x06000089 RID: 137
		void _VtblGap1_2();

		// Token: 0x0600008A RID: 138
		[DispId(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetText();

		// Token: 0x0600008B RID: 139
		void _VtblGap2_35();

		// Token: 0x0600008C RID: 140
		[DispId(39)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool SetText([MarshalAs(UnmanagedType.BStr)] [In] string Txt);

		// Token: 0x0600008D RID: 141
		void _VtblGap3_45();

		// Token: 0x0600008E RID: 142
		[DispId(85)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object GetAnnotation();

		// Token: 0x0600008F RID: 143
		void _VtblGap4_9();

		// Token: 0x06000090 RID: 144
		[DispId(94)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool IsBomBalloon();
	}
}
