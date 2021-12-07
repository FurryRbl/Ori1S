using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002E8 RID: 744
	internal static class ActivePlatform
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000363A8 File Offset: 0x000345A8
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x000363C4 File Offset: 0x000345C4
		internal static ISocialPlatform Instance
		{
			get
			{
				if (ActivePlatform._active == null)
				{
					ActivePlatform._active = ActivePlatform.SelectSocialPlatform();
				}
				return ActivePlatform._active;
			}
			set
			{
				ActivePlatform._active = value;
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000363CC File Offset: 0x000345CC
		private static ISocialPlatform SelectSocialPlatform()
		{
			return new Local();
		}

		// Token: 0x04000BDB RID: 3035
		private static ISocialPlatform _active;
	}
}
