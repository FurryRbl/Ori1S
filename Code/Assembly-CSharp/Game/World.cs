using System;

namespace Game
{
	// Token: 0x0200014A RID: 330
	public class World
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003E06D File Offset: 0x0003C26D
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0003E079 File Offset: 0x0003C279
		public static RuntimeGameWorldArea CurrentArea
		{
			get
			{
				return GameWorld.Instance.CurrentArea;
			}
			set
			{
				GameWorld.Instance.CurrentArea = value;
			}
		}

		// Token: 0x020004CA RID: 1226
		public static class Events
		{
			// Token: 0x06002136 RID: 8502 RVA: 0x00091D0F File Offset: 0x0008FF0F
			public static WorldEventsRuntime Find(WorldEvents worldEvents)
			{
				return WorldEventsManager.Instance.Find(worldEvents);
			}
		}
	}
}
