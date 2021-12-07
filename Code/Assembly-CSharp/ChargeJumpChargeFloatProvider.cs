using System;
using Game;

// Token: 0x02000428 RID: 1064
public class ChargeJumpChargeFloatProvider : FloatValueProvider
{
	// Token: 0x06001DAF RID: 7599 RVA: 0x000831F4 File Offset: 0x000813F4
	public override float GetFloatValue()
	{
		if (Characters.Sein && Characters.Sein.Abilities.ChargeJumpCharging && Characters.Sein.Abilities.ChargeJumpCharging.IsCharging)
		{
			this.m_value = Characters.Sein.Abilities.ChargeJumpCharging.ChargingValue;
		}
		return this.m_value;
	}

	// Token: 0x04001993 RID: 6547
	private float m_value;
}
