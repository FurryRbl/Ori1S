using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AB RID: 683
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x04000F23 RID: 3875
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x04000F24 RID: 3876
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x04000F25 RID: 3877
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x04000F26 RID: 3878
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x04000F27 RID: 3879
		Renamed = 8
	}
}
