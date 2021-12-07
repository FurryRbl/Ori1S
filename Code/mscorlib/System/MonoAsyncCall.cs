using System;

namespace System
{
	// Token: 0x02000154 RID: 340
	internal class MonoAsyncCall
	{
		// Token: 0x04000538 RID: 1336
		private object msg;

		// Token: 0x04000539 RID: 1337
		private IntPtr cb_method;

		// Token: 0x0400053A RID: 1338
		private object cb_target;

		// Token: 0x0400053B RID: 1339
		private object state;

		// Token: 0x0400053C RID: 1340
		private object res;

		// Token: 0x0400053D RID: 1341
		private object out_args;

		// Token: 0x0400053E RID: 1342
		private long wait_event;
	}
}
