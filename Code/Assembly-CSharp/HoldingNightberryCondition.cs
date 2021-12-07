using System;
using Game;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class HoldingNightberryCondition : Condition
{
	// Token: 0x0600153E RID: 5438 RVA: 0x0005E914 File Offset: 0x0005CB14
	public override bool Validate(IContext context)
	{
		SeinCharacter sein = Characters.Sein;
		if (sein.Abilities.Carry && sein.Abilities.Carry.IsCarrying)
		{
			ICarryable currentCarryable = sein.Abilities.Carry.CurrentCarryable;
			if (currentCarryable != null)
			{
				return this.IsHolding == ((Component)currentCarryable).GetComponent<NightBerry>();
			}
		}
		return !this.IsHolding;
	}

	// Token: 0x04001269 RID: 4713
	public bool IsHolding = true;
}
