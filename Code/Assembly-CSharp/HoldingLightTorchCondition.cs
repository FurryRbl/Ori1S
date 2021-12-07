using System;
using Game;
using UnityEngine;

// Token: 0x020009D1 RID: 2513
public class HoldingLightTorchCondition : Condition
{
	// Token: 0x060036C0 RID: 14016 RVA: 0x000E5DE8 File Offset: 0x000E3FE8
	public override bool Validate(IContext context)
	{
		SeinCharacter sein = Characters.Sein;
		if (sein.Abilities.Carry && sein.Abilities.Carry.IsCarrying)
		{
			ICarryable currentCarryable = sein.Abilities.Carry.CurrentCarryable;
			if (currentCarryable != null)
			{
				return this.IsHolding == ((Component)currentCarryable).GetComponent<LightTorch>();
			}
		}
		return !this.IsHolding;
	}

	// Token: 0x040031A8 RID: 12712
	public bool IsHolding = true;
}
