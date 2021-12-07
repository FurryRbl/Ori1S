using System;

namespace UnityEngine
{
	// Token: 0x0200026D RID: 621
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		// Token: 0x060024CC RID: 9420 RVA: 0x00030190 File Offset: 0x0002E390
		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable) : base("java/lang/Runnable")
		{
			this.mRunnable = runnable;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000301A4 File Offset: 0x0002E3A4
		public void run()
		{
			this.mRunnable();
		}

		// Token: 0x040009CA RID: 2506
		private AndroidJavaRunnable mRunnable;
	}
}
