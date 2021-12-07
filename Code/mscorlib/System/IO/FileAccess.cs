using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Defines constants for read, write, or read/write access to a file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023B RID: 571
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileAccess
	{
		/// <summary>Read access to the file. Data can be read from the file. Combine with Write for read/write access.</summary>
		// Token: 0x04000B03 RID: 2819
		Read = 1,
		/// <summary>Write access to the file. Data can be written to the file. Combine with Read for read/write access.</summary>
		// Token: 0x04000B04 RID: 2820
		Write = 2,
		/// <summary>Read and write access to the file. Data can be written to and read from the file.</summary>
		// Token: 0x04000B05 RID: 2821
		ReadWrite = 3
	}
}
