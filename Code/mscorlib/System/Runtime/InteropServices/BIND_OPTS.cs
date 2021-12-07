using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" /> instead.</summary>
	// Token: 0x0200036E RID: 878
	[Obsolete]
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size of the BIND_OPTS structure in bytes.</summary>
		// Token: 0x040010B3 RID: 4275
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x040010B4 RID: 4276
		public int grfFlags;

		/// <summary>Flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x040010B5 RID: 4277
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the GetTickCount function) the caller specified to complete the binding operation.</summary>
		// Token: 0x040010B6 RID: 4278
		public int dwTickCountDeadline;
	}
}
