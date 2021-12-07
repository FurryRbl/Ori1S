using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002EE RID: 750
	public interface IAchievementDescription
	{
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060026AD RID: 9901
		// (set) Token: 0x060026AE RID: 9902
		string id { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060026AF RID: 9903
		string title { get; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060026B0 RID: 9904
		Texture2D image { get; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060026B1 RID: 9905
		string achievedDescription { get; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060026B2 RID: 9906
		string unachievedDescription { get; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060026B3 RID: 9907
		bool hidden { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060026B4 RID: 9908
		int points { get; }
	}
}
