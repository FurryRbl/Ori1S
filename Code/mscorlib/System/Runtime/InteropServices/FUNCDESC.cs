using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCDESC" /> instead.</summary>
	// Token: 0x02000390 RID: 912
	[Obsolete]
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x0400110D RID: 4365
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x0400110E RID: 4366
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x0400110F RID: 4367
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04001110 RID: 4368
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04001111 RID: 4369
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04001112 RID: 4370
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04001113 RID: 4371
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04001114 RID: 4372
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04001115 RID: 4373
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04001116 RID: 4374
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04001117 RID: 4375
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04001118 RID: 4376
		public short wFuncFlags;
	}
}
