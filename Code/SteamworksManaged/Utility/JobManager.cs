using System;
using System.Collections.Generic;

namespace ManagedSteam.Utility
{
	// Token: 0x020000BF RID: 191
	internal class JobManager
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x000091B3 File Offset: 0x000073B3
		public JobManager()
		{
			this.jobs = new List<IJob>();
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000574 RID: 1396 RVA: 0x000091C8 File Offset: 0x000073C8
		// (remove) Token: 0x06000575 RID: 1397 RVA: 0x00009200 File Offset: 0x00007400
		public event Action<IJob> PreCreateJob;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000576 RID: 1398 RVA: 0x00009238 File Offset: 0x00007438
		// (remove) Token: 0x06000577 RID: 1399 RVA: 0x00009270 File Offset: 0x00007470
		public event Action<IJob> PostCreateJob;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000578 RID: 1400 RVA: 0x000092A8 File Offset: 0x000074A8
		// (remove) Token: 0x06000579 RID: 1401 RVA: 0x000092E0 File Offset: 0x000074E0
		public event Action<IJob> PreDestroyJob;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x0600057A RID: 1402 RVA: 0x00009318 File Offset: 0x00007518
		// (remove) Token: 0x0600057B RID: 1403 RVA: 0x00009350 File Offset: 0x00007550
		public event Action<IJob> PostDestroyJob;

		// Token: 0x0600057C RID: 1404 RVA: 0x00009385 File Offset: 0x00007585
		public void AddJob(IJob job)
		{
			this.jobs.Add(job);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00009394 File Offset: 0x00007594
		public void RunCreateJobs()
		{
			foreach (IJob job in this.jobs)
			{
				if (this.PreCreateJob != null)
				{
					this.PreCreateJob(job);
				}
				job.Create();
				if (this.PostCreateJob != null)
				{
					this.PostCreateJob(job);
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00009410 File Offset: 0x00007610
		public void RunDestroyJobs()
		{
			foreach (IJob job in this.jobs)
			{
				if (this.PreDestroyJob != null)
				{
					this.PreDestroyJob(job);
				}
				job.Destroy();
				if (this.PostDestroyJob != null)
				{
					this.PostDestroyJob(job);
				}
			}
		}

		// Token: 0x0400034D RID: 845
		private List<IJob> jobs;
	}
}
