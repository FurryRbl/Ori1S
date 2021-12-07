using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002DF RID: 735
	public class UserProfile : IUserProfile
	{
		// Token: 0x0600260E RID: 9742 RVA: 0x00034A84 File Offset: 0x00032C84
		public UserProfile()
		{
			this.m_UserName = "Uninitialized";
			this.m_ID = "0";
			this.m_IsFriend = false;
			this.m_State = UserState.Offline;
			this.m_Image = new Texture2D(32, 32);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00034AC0 File Offset: 0x00032CC0
		public UserProfile(string name, string id, bool friend) : this(name, id, friend, UserState.Offline, new Texture2D(0, 0))
		{
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00034AD4 File Offset: 0x00032CD4
		public UserProfile(string name, string id, bool friend, UserState state, Texture2D image)
		{
			this.m_UserName = name;
			this.m_ID = id;
			this.m_IsFriend = friend;
			this.m_State = state;
			this.m_Image = image;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00034B04 File Offset: 0x00032D04
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" - ",
				this.userName,
				" - ",
				this.isFriend,
				" - ",
				this.state
			});
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x00034B64 File Offset: 0x00032D64
		public void SetUserName(string name)
		{
			this.m_UserName = name;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x00034B70 File Offset: 0x00032D70
		public void SetUserID(string id)
		{
			this.m_ID = id;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00034B7C File Offset: 0x00032D7C
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00034B88 File Offset: 0x00032D88
		public void SetIsFriend(bool value)
		{
			this.m_IsFriend = value;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x00034B94 File Offset: 0x00032D94
		public void SetState(UserState state)
		{
			this.m_State = state;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00034BA0 File Offset: 0x00032DA0
		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public string id
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x00034BB0 File Offset: 0x00032DB0
		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00034BB8 File Offset: 0x00032DB8
		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		protected string m_UserName;

		// Token: 0x04000BA9 RID: 2985
		protected string m_ID;

		// Token: 0x04000BAA RID: 2986
		protected bool m_IsFriend;

		// Token: 0x04000BAB RID: 2987
		protected UserState m_State;

		// Token: 0x04000BAC RID: 2988
		protected Texture2D m_Image;
	}
}
