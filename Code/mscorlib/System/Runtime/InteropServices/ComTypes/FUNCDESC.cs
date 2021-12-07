using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines a function description.</summary>
	// Token: 0x020003EF RID: 1007
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x040012B2 RID: 4786
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x040012B3 RID: 4787
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x040012B4 RID: 4788
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x040012B5 RID: 4789
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x040012B6 RID: 4790
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x040012B7 RID: 4791
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x040012B8 RID: 4792
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x040012B9 RID: 4793
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x040012BA RID: 4794
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x040012BB RID: 4795
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x040012BC RID: 4796
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x040012BD RID: 4797
		public short wFuncFlags;
	}
}
