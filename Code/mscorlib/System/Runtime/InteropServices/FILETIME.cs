using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FILETIME" /> instead.</summary>
	// Token: 0x0200038F RID: 911
	[Obsolete]
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the FILETIME.</summary>
		// Token: 0x0400110B RID: 4363
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the FILETIME.</summary>
		// Token: 0x0400110C RID: 4364
		public int dwHighDateTime;
	}
}
