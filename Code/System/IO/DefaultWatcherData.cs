using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x02000275 RID: 629
	internal class DefaultWatcherData
	{
		// Token: 0x040006F4 RID: 1780
		public FileSystemWatcher FSW;

		// Token: 0x040006F5 RID: 1781
		public string Directory;

		// Token: 0x040006F6 RID: 1782
		public string FileMask;

		// Token: 0x040006F7 RID: 1783
		public bool IncludeSubdirs;

		// Token: 0x040006F8 RID: 1784
		public bool Enabled;

		// Token: 0x040006F9 RID: 1785
		public bool NoWildcards;

		// Token: 0x040006FA RID: 1786
		public DateTime DisabledTime;

		// Token: 0x040006FB RID: 1787
		public Hashtable Files;
	}
}
