using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents the number of 100-nanosecond intervals since January 1, 1601. This structure is a 64-bit value.</summary>
	// Token: 0x020003EE RID: 1006
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the FILETIME.</summary>
		// Token: 0x040012B0 RID: 4784
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the FILETIME.</summary>
		// Token: 0x040012B1 RID: 4785
		public int dwHighDateTime;
	}
}
