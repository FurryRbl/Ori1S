using System;

namespace System.Reflection.Emit
{
	// Token: 0x020002C3 RID: 707
	internal struct MonoWin32Resource
	{
		// Token: 0x0600237E RID: 9086 RVA: 0x0007E904 File Offset: 0x0007CB04
		public MonoWin32Resource(int res_type, int res_id, int lang_id, byte[] data)
		{
			this.res_type = res_type;
			this.res_id = res_id;
			this.lang_id = lang_id;
			this.data = data;
		}

		// Token: 0x04000D71 RID: 3441
		public int res_type;

		// Token: 0x04000D72 RID: 3442
		public int res_id;

		// Token: 0x04000D73 RID: 3443
		public int lang_id;

		// Token: 0x04000D74 RID: 3444
		public byte[] data;
	}
}
