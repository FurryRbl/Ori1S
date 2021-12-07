using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002DE RID: 734
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x000349F8 File Offset: 0x00032BF8
		public LocalUser()
		{
			this.m_Friends = new UserProfile[0];
			this.m_Authenticated = false;
			this.m_Underage = false;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00034A28 File Offset: 0x00032C28
		public void Authenticate(Action<bool> callback)
		{
			ActivePlatform.Instance.Authenticate(this, callback);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00034A38 File Offset: 0x00032C38
		public void LoadFriends(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadFriends(this, callback);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00034A48 File Offset: 0x00032C48
		public void SetFriends(IUserProfile[] friends)
		{
			this.m_Friends = friends;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00034A54 File Offset: 0x00032C54
		public void SetAuthenticated(bool value)
		{
			this.m_Authenticated = value;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00034A60 File Offset: 0x00032C60
		public void SetUnderage(bool value)
		{
			this.m_Underage = value;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x00034A6C File Offset: 0x00032C6C
		public IUserProfile[] friends
		{
			get
			{
				return this.m_Friends;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x00034A74 File Offset: 0x00032C74
		public bool authenticated
		{
			get
			{
				return this.m_Authenticated;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00034A7C File Offset: 0x00032C7C
		public bool underage
		{
			get
			{
				return this.m_Underage;
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private IUserProfile[] m_Friends;

		// Token: 0x04000BA6 RID: 2982
		private bool m_Authenticated;

		// Token: 0x04000BA7 RID: 2983
		private bool m_Underage;
	}
}
