using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> instead.</summary>
	// Token: 0x02000371 RID: 881
	[Obsolete]
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the Cdecl calling convention is used for a method.</summary>
		// Token: 0x040010BC RID: 4284
		CC_CDECL = 1,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x040010BD RID: 4285
		CC_PASCAL,
		/// <summary>Indicates that the Mscpascal calling convention is used for a method.</summary>
		// Token: 0x040010BE RID: 4286
		CC_MSCPASCAL = 2,
		/// <summary>Indicates that the Macpascal calling convention is used for a method.</summary>
		// Token: 0x040010BF RID: 4287
		CC_MACPASCAL,
		/// <summary>Indicates that the Stdcall calling convention is used for a method.</summary>
		// Token: 0x040010C0 RID: 4288
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x040010C1 RID: 4289
		CC_RESERVED,
		/// <summary>Indicates that the Syscall calling convention is used for a method.</summary>
		// Token: 0x040010C2 RID: 4290
		CC_SYSCALL,
		/// <summary>Indicates that the Mpwcdecl calling convention is used for a method.</summary>
		// Token: 0x040010C3 RID: 4291
		CC_MPWCDECL,
		/// <summary>Indicates that the Mpwpascal calling convention is used for a method.</summary>
		// Token: 0x040010C4 RID: 4292
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.CALLCONV" /> enumeration.</summary>
		// Token: 0x040010C5 RID: 4293
		CC_MAX
	}
}
