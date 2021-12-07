using System;

namespace UnityEngine
{
	// Token: 0x0200026C RID: 620
	public sealed class AndroidJavaException : Exception
	{
		// Token: 0x060024CA RID: 9418 RVA: 0x0003016C File Offset: 0x0002E36C
		internal AndroidJavaException(string message, string javaStackTrace) : base(message)
		{
			this.mJavaStackTrace = javaStackTrace;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x0003017C File Offset: 0x0002E37C
		public override string StackTrace
		{
			get
			{
				return this.mJavaStackTrace + base.StackTrace;
			}
		}

		// Token: 0x040009C9 RID: 2505
		private string mJavaStackTrace;
	}
}
