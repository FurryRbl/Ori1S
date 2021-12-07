using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information about how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000408 RID: 1032
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		/// <summary>Represents a pointer to a value that is being passed between processes.</summary>
		// Token: 0x040012E9 RID: 4841
		public IntPtr lpVarValue;

		/// <summary>Represents bitmask values that describe the structure element, parameter, or return value.</summary>
		// Token: 0x040012EA RID: 4842
		public PARAMFLAG wParamFlags;
	}
}
