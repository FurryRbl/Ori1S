using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Provides attributes for files and directories.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023C RID: 572
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileAttributes
	{
		/// <summary>The file's archive status. Applications use this attribute to mark files for backup or removal.</summary>
		// Token: 0x04000B07 RID: 2823
		Archive = 32,
		/// <summary>The file is compressed.</summary>
		// Token: 0x04000B08 RID: 2824
		Compressed = 2048,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000B09 RID: 2825
		Device = 64,
		/// <summary>The file is a directory.</summary>
		// Token: 0x04000B0A RID: 2826
		Directory = 16,
		/// <summary>The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and directories.</summary>
		// Token: 0x04000B0B RID: 2827
		Encrypted = 16384,
		/// <summary>The file is hidden, and thus is not included in an ordinary directory listing.</summary>
		// Token: 0x04000B0C RID: 2828
		Hidden = 2,
		/// <summary>The file is normal and has no other attributes set. This attribute is valid only if used alone.</summary>
		// Token: 0x04000B0D RID: 2829
		Normal = 128,
		/// <summary>The file will not be indexed by the operating system's content indexing service.</summary>
		// Token: 0x04000B0E RID: 2830
		NotContentIndexed = 8192,
		/// <summary>The file is offline. The data of the file is not immediately available.</summary>
		// Token: 0x04000B0F RID: 2831
		Offline = 4096,
		/// <summary>The file is read-only.</summary>
		// Token: 0x04000B10 RID: 2832
		ReadOnly = 1,
		/// <summary>The file contains a reparse point, which is a block of user-defined data associated with a file or a directory.</summary>
		// Token: 0x04000B11 RID: 2833
		ReparsePoint = 1024,
		/// <summary>The file is a sparse file. Sparse files are typically large files whose data are mostly zeros.</summary>
		// Token: 0x04000B12 RID: 2834
		SparseFile = 512,
		/// <summary>The file is a system file. The file is part of the operating system or is used exclusively by the operating system.</summary>
		// Token: 0x04000B13 RID: 2835
		System = 4,
		/// <summary>The file is temporary. File systems attempt to keep all of the data in memory for quicker access rather than flushing the data back to mass storage. A temporary file should be deleted by the application as soon as it is no longer needed.</summary>
		// Token: 0x04000B14 RID: 2836
		Temporary = 256
	}
}
