using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" /> instead.</summary>
	// Token: 0x020003E0 RID: 992
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04001241 RID: 4673
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04001242 RID: 4674
		public string lpstrSchema;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04001243 RID: 4675
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04001244 RID: 4676
		public short wVarFlags;

		/// <summary>Defines how a variable should be marshaled.</summary>
		// Token: 0x04001245 RID: 4677
		public VarEnum varkind;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" /> instead.</summary>
		// Token: 0x020003E1 RID: 993
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04001246 RID: 4678
			[FieldOffset(0)]
			public IntPtr lpvarValue;

			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04001247 RID: 4679
			[FieldOffset(0)]
			public int oInst;
		}
	}
}
