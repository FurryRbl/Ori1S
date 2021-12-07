using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x020002C2 RID: 706
	internal struct MonoResource
	{
		// Token: 0x04000D6B RID: 3435
		public byte[] data;

		// Token: 0x04000D6C RID: 3436
		public string name;

		// Token: 0x04000D6D RID: 3437
		public string filename;

		// Token: 0x04000D6E RID: 3438
		public ResourceAttributes attrs;

		// Token: 0x04000D6F RID: 3439
		public int offset;

		// Token: 0x04000D70 RID: 3440
		public Stream stream;
	}
}
