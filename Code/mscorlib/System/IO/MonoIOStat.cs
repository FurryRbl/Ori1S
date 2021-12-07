using System;

namespace System.IO
{
	// Token: 0x0200024B RID: 587
	internal struct MonoIOStat
	{
		// Token: 0x04000B7F RID: 2943
		public string Name;

		// Token: 0x04000B80 RID: 2944
		public FileAttributes Attributes;

		// Token: 0x04000B81 RID: 2945
		public long Length;

		// Token: 0x04000B82 RID: 2946
		public long CreationTime;

		// Token: 0x04000B83 RID: 2947
		public long LastAccessTime;

		// Token: 0x04000B84 RID: 2948
		public long LastWriteTime;
	}
}
