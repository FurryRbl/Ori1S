using System;

namespace Game
{
	// Token: 0x020000AE RID: 174
	public class Events
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001F770 File Offset: 0x0001D970
		public static GameScheduler Scheduler
		{
			get
			{
				return GameController.Instance.GameScheduler;
			}
		}
	}
}
