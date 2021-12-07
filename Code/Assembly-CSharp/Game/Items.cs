using System;

namespace Game
{
	// Token: 0x020002DE RID: 734
	public static class Items
	{
		// Token: 0x0400135D RID: 4957
		public static NightBerry NightBerry;

		// Token: 0x0400135E RID: 4958
		public static MistTorch MistTorch;

		// Token: 0x0400135F RID: 4959
		public static LightTorch LightTorch;

		// Token: 0x04001360 RID: 4960
		public static AllContainer<ICarryable> Carryables = new AllContainer<ICarryable>();
	}
}
