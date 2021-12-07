using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000294 RID: 660
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x04000E80 RID: 3712
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x04000E81 RID: 3713
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x04000E82 RID: 3714
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x04000E83 RID: 3715
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x04000E84 RID: 3716
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x04000E85 RID: 3717
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x04000E86 RID: 3718
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x04000E87 RID: 3719
		Size = 8
	}
}
