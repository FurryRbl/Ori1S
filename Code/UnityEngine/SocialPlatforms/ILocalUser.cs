using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002EA RID: 746
	public interface ILocalUser : IUserProfile
	{
		// Token: 0x0600269B RID: 9883
		void Authenticate(Action<bool> callback);

		// Token: 0x0600269C RID: 9884
		void LoadFriends(Action<bool> callback);

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600269D RID: 9885
		IUserProfile[] friends { get; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600269E RID: 9886
		bool authenticated { get; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600269F RID: 9887
		bool underage { get; }
	}
}
