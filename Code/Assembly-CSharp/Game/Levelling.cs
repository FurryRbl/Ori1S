using System;

namespace Game
{
	// Token: 0x02000453 RID: 1107
	public static class Levelling
	{
		// Token: 0x06001EAE RID: 7854 RVA: 0x00087220 File Offset: 0x00085420
		public static float CalculateLevelBasedMaxHealth(int level, float health)
		{
			return Characters.Sein.Level.CalculateLevelBasedMaxHealth(level, health);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00087233 File Offset: 0x00085433
		public static int CalculateLevelBasedDamageAmount(int level, int damage)
		{
			return (int)((float)damage + (float)(damage * level) * 0.5f);
		}
	}
}
