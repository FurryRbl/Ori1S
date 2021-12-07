using System;

// Token: 0x02000320 RID: 800
public class ResetDamageRecieversAction : ActionMethod
{
	// Token: 0x06001774 RID: 6004 RVA: 0x00065134 File Offset: 0x00063334
	public override void Perform(IContext context)
	{
		foreach (DamageReciever damageReciever in this.DamageRecievers)
		{
			damageReciever.SetHealth(damageReciever.MaxHealth);
			damageReciever.UpdateActive();
		}
	}

	// Token: 0x0400141E RID: 5150
	public DamageReciever[] DamageRecievers;
}
