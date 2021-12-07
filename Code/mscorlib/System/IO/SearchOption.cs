using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Specifies whether to search the current directory, or the current directory and all subdirectories. </summary>
	// Token: 0x0200024E RID: 590
	[ComVisible(true)]
	[Serializable]
	public enum SearchOption
	{
		/// <summary>Includes only the current directory in a search.</summary>
		// Token: 0x04000B8E RID: 2958
		TopDirectoryOnly,
		/// <summary>Includes the current directory and all the subdirectories in a search operation. This option includes reparse points like mounted drives and symbolic links in the search.</summary>
		// Token: 0x04000B8F RID: 2959
		AllDirectories
	}
}
