using System;

namespace System.IO
{
	// Token: 0x0200028E RID: 654
	internal class KeventFileData
	{
		// Token: 0x060016D4 RID: 5844 RVA: 0x0003EABC File Offset: 0x0003CCBC
		public KeventFileData(FileSystemInfo fsi, DateTime LastAccessTime, DateTime LastWriteTime)
		{
			this.fsi = fsi;
			this.LastAccessTime = LastAccessTime;
			this.LastWriteTime = LastWriteTime;
		}

		// Token: 0x04000777 RID: 1911
		public FileSystemInfo fsi;

		// Token: 0x04000778 RID: 1912
		public DateTime LastAccessTime;

		// Token: 0x04000779 RID: 1913
		public DateTime LastWriteTime;
	}
}
