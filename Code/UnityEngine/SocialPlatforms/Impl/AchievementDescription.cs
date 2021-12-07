using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002E1 RID: 737
	public class AchievementDescription : IAchievementDescription
	{
		// Token: 0x0600262B RID: 9771 RVA: 0x00034D44 File Offset: 0x00032F44
		public AchievementDescription(string id, string title, Texture2D image, string achievedDescription, string unachievedDescription, bool hidden, int points)
		{
			this.id = id;
			this.m_Title = title;
			this.m_Image = image;
			this.m_AchievedDescription = achievedDescription;
			this.m_UnachievedDescription = unachievedDescription;
			this.m_Hidden = hidden;
			this.m_Points = points;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00034D84 File Offset: 0x00032F84
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" - ",
				this.title,
				" - ",
				this.achievedDescription,
				" - ",
				this.unachievedDescription,
				" - ",
				this.points,
				" - ",
				this.hidden
			});
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00034E08 File Offset: 0x00033008
		public void SetImage(Texture2D image)
		{
			this.m_Image = image;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x00034E14 File Offset: 0x00033014
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x00034E1C File Offset: 0x0003301C
		public string id { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00034E28 File Offset: 0x00033028
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00034E30 File Offset: 0x00033030
		public Texture2D image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00034E38 File Offset: 0x00033038
		public string achievedDescription
		{
			get
			{
				return this.m_AchievedDescription;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00034E40 File Offset: 0x00033040
		public string unachievedDescription
		{
			get
			{
				return this.m_UnachievedDescription;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00034E48 File Offset: 0x00033048
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00034E50 File Offset: 0x00033050
		public int points
		{
			get
			{
				return this.m_Points;
			}
		}

		// Token: 0x04000BB2 RID: 2994
		private string m_Title;

		// Token: 0x04000BB3 RID: 2995
		private Texture2D m_Image;

		// Token: 0x04000BB4 RID: 2996
		private string m_AchievedDescription;

		// Token: 0x04000BB5 RID: 2997
		private string m_UnachievedDescription;

		// Token: 0x04000BB6 RID: 2998
		private bool m_Hidden;

		// Token: 0x04000BB7 RID: 2999
		private int m_Points;
	}
}
