using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
	// Token: 0x0200002B RID: 43
	[CompilerGenerated]
	[Guid("B90793FB-EF3D-4B80-A5C4-99959CDB6CEB")]
	[TypeIdentifier]
	[ComImport]
	public interface IModelDoc2
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000071 RID: 113
		// (set) Token: 0x06000073 RID: 115
		[DispId(65537)]
		object SelectionManager { [DispId(65537)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; [DispId(65537)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.IDispatch)] set; }

		// Token: 0x06000072 RID: 114
		void _VtblGap1_1();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000074 RID: 116
		// (set) Token: 0x06000076 RID: 118
		[DispId(65538)]
		object ActiveView { [DispId(65538)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.IDispatch)] get; [DispId(65538)] [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.IDispatch)] set; }

		// Token: 0x06000075 RID: 117
		void _VtblGap2_1();

		// Token: 0x06000077 RID: 119
		void _VtblGap3_78();

		// Token: 0x06000078 RID: 120
		[DispId(65608)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetPathName();

		// Token: 0x06000079 RID: 121
		[DispId(65609)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int GetType();

		// Token: 0x0600007A RID: 122
		void _VtblGap4_227();

		// Token: 0x0600007B RID: 123
		[DispId(65841)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.IDispatch)]
		object GetActiveConfiguration();

		// Token: 0x0600007C RID: 124
		void _VtblGap5_28();

		// Token: 0x0600007D RID: 125
		[DispId(65865)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GraphicsRedraw2();

		// Token: 0x0600007E RID: 126
		void _VtblGap6_366();

		// Token: 0x0600007F RID: 127
		[DispId(66227)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		bool ForceRebuild3([In] bool TopOnly);

		// Token: 0x06000080 RID: 128
		void _VtblGap7_35();

		// Token: 0x06000081 RID: 129
		[DispId(66263)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ClearSelection2([In] bool All);

		// Token: 0x06000082 RID: 130
		void _VtblGap8_45();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000083 RID: 131
		[DispId(66306)]
		ModelDocExtension Extension { [DispId(66306)] [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
