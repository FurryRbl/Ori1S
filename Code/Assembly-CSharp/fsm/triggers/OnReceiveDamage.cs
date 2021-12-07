using System;

namespace fsm.triggers
{
	// Token: 0x02000507 RID: 1287
	public class OnReceiveDamage : ITrigger
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x0009743A File Offset: 0x0009563A
		public OnReceiveDamage(Damage damage)
		{
			this.Damage = damage;
		}

		// Token: 0x04001CEF RID: 7407
		public readonly Damage Damage;
	}
}
