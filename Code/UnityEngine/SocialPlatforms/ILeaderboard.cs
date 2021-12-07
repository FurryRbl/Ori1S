using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002F3 RID: 755
	public interface ILeaderboard
	{
		// Token: 0x060026BF RID: 9919
		void SetUserFilter(string[] userIDs);

		// Token: 0x060026C0 RID: 9920
		void LoadScores(Action<bool> callback);

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060026C1 RID: 9921
		bool loading { get; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060026C2 RID: 9922
		// (set) Token: 0x060026C3 RID: 9923
		string id { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060026C4 RID: 9924
		// (set) Token: 0x060026C5 RID: 9925
		UserScope userScope { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060026C6 RID: 9926
		// (set) Token: 0x060026C7 RID: 9927
		Range range { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060026C8 RID: 9928
		// (set) Token: 0x060026C9 RID: 9929
		TimeScope timeScope { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060026CA RID: 9930
		IScore localUserScore { get; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060026CB RID: 9931
		uint maxRange { get; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060026CC RID: 9932
		IScore[] scores { get; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060026CD RID: 9933
		string title { get; }
	}
}
