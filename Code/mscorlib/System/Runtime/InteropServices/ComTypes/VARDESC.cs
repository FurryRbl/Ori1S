using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes a variable, constant, or data member.</summary>
	// Token: 0x02000411 RID: 1041
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04001339 RID: 4921
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x0400133A RID: 4922
		public string lpstrSchema;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x0400133B RID: 4923
		public VARDESC.DESCUNION desc;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x0400133C RID: 4924
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x0400133D RID: 4925
		public short wVarFlags;

		/// <summary>Defines how to marshal a variable.</summary>
		// Token: 0x0400133E RID: 4926
		public VARKIND varkind;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x02000412 RID: 1042
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x0400133F RID: 4927
			[FieldOffset(0)]
			public IntPtr lpvarValue;

			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04001340 RID: 4928
			[FieldOffset(0)]
			public int oInst;
		}
	}
}
