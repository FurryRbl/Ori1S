using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x0200028F RID: 655
	internal class KeventData
	{
		// Token: 0x0400077A RID: 1914
		public FileSystemWatcher FSW;

		// Token: 0x0400077B RID: 1915
		public string Directory;

		// Token: 0x0400077C RID: 1916
		public string FileMask;

		// Token: 0x0400077D RID: 1917
		public bool IncludeSubdirs;

		// Token: 0x0400077E RID: 1918
		public bool Enabled;

		// Token: 0x0400077F RID: 1919
		public Hashtable DirEntries;

		// Token: 0x04000780 RID: 1920
		public kevent ev;
	}
}
