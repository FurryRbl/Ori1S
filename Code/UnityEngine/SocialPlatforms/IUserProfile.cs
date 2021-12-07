using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002EC RID: 748
	public interface IUserProfile
	{
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060026A0 RID: 9888
		string userName { get; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060026A1 RID: 9889
		string id { get; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060026A2 RID: 9890
		bool isFriend { get; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060026A3 RID: 9891
		UserState state { get; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060026A4 RID: 9892
		Texture2D image { get; }
	}
}
