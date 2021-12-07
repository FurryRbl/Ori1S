using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002EF RID: 751
	public interface IScore
	{
		// Token: 0x060026B5 RID: 9909
		void ReportScore(Action<bool> callback);

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060026B6 RID: 9910
		// (set) Token: 0x060026B7 RID: 9911
		string leaderboardID { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060026B8 RID: 9912
		// (set) Token: 0x060026B9 RID: 9913
		long value { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060026BA RID: 9914
		DateTime date { get; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060026BB RID: 9915
		string formattedValue { get; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060026BC RID: 9916
		string userID { get; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060026BD RID: 9917
		int rank { get; }
	}
}
