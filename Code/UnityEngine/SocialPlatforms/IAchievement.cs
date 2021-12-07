using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002ED RID: 749
	public interface IAchievement
	{
		// Token: 0x060026A5 RID: 9893
		void ReportProgress(Action<bool> callback);

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060026A6 RID: 9894
		// (set) Token: 0x060026A7 RID: 9895
		string id { get; set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060026A8 RID: 9896
		// (set) Token: 0x060026A9 RID: 9897
		double percentCompleted { get; set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060026AA RID: 9898
		bool completed { get; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060026AB RID: 9899
		bool hidden { get; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060026AC RID: 9900
		DateTime lastReportedDate { get; }
	}
}
