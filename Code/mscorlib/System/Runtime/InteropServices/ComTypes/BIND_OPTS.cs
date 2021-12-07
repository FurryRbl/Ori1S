using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Stores the parameters that are used during a moniker binding operation.</summary>
	// Token: 0x020003E6 RID: 998
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size, in bytes, of the BIND_OPTS structure.</summary>
		// Token: 0x04001287 RID: 4743
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04001288 RID: 4744
		public int grfFlags;

		/// <summary>Represents flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04001289 RID: 4745
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the GetTickCount function) that the caller specified to complete the binding operation.</summary>
		// Token: 0x0400128A RID: 4746
		public int dwTickCountDeadline;
	}
}
