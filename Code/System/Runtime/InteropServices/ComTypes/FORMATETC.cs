using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents a generalized Clipboard format. </summary>
	// Token: 0x020004CF RID: 1231
	public struct FORMATETC
	{
		/// <summary>Specifies the particular clipboard format of interest.</summary>
		// Token: 0x04001BB5 RID: 7093
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration constants that indicates how much detail should be contained in the rendering.</summary>
		// Token: 0x04001BB6 RID: 7094
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		/// <summary>Specifies part of the aspect when the data must be split across page boundaries. </summary>
		// Token: 0x04001BB7 RID: 7095
		public int lindex;

		/// <summary>Specifies a pointer to a DVTARGETDEVICE structure containing information about the target device that the data is being composed for. </summary>
		// Token: 0x04001BB8 RID: 7096
		public IntPtr ptd;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration constants, which indicates the type of storage medium used to transfer the object's data. </summary>
		// Token: 0x04001BB9 RID: 7097
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
