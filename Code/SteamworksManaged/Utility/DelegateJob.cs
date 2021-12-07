using System;

namespace ManagedSteam.Utility
{
	// Token: 0x0200007B RID: 123
	internal class DelegateJob : IJob
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00007AAF File Offset: 0x00005CAF
		public DelegateJob() : this(null, null)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00007AB9 File Offset: 0x00005CB9
		public DelegateJob(DelegateJob.Method create, DelegateJob.Method destroy)
		{
			this.CreateMethod = create;
			this.DestroyMethod = destroy;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00007ACF File Offset: 0x00005CCF
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00007AD7 File Offset: 0x00005CD7
		public DelegateJob.Method CreateMethod { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00007AE0 File Offset: 0x00005CE0
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public DelegateJob.Method DestroyMethod { get; set; }

		// Token: 0x0600040F RID: 1039 RVA: 0x00007AF1 File Offset: 0x00005CF1
		public void Create()
		{
			if (this.CreateMethod != null)
			{
				this.CreateMethod();
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00007B06 File Offset: 0x00005D06
		public void Destroy()
		{
			if (this.DestroyMethod != null)
			{
				this.DestroyMethod();
			}
		}

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000412 RID: 1042
		public delegate void Method();
	}
}
