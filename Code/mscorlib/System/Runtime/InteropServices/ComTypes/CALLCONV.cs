using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the calling convention used by a method described in a METHODDATA structure.</summary>
	// Token: 0x020003E7 RID: 999
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the C declaration (CDECL) calling convention is used for a method. </summary>
		// Token: 0x0400128C RID: 4748
		CC_CDECL = 1,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x0400128D RID: 4749
		CC_PASCAL,
		/// <summary>Indicates that the MSC Pascal (MSCPASCAL) calling convention is used for a method.</summary>
		// Token: 0x0400128E RID: 4750
		CC_MSCPASCAL = 2,
		/// <summary>Indicates that the Macintosh Pascal (MACPASCAL) calling convention is used for a method.</summary>
		// Token: 0x0400128F RID: 4751
		CC_MACPASCAL,
		/// <summary>Indicates that the standard calling convention (STDCALL) is used for a method.</summary>
		// Token: 0x04001290 RID: 4752
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04001291 RID: 4753
		CC_RESERVED,
		/// <summary>Indicates that the standard SYSCALL calling convention is used for a method.</summary>
		// Token: 0x04001292 RID: 4754
		CC_SYSCALL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) CDECL calling convention is used for a method.</summary>
		// Token: 0x04001293 RID: 4755
		CC_MPWCDECL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) PASCAL calling convention is used for a method.</summary>
		// Token: 0x04001294 RID: 4756
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> enumeration.</summary>
		// Token: 0x04001295 RID: 4757
		CC_MAX
	}
}
