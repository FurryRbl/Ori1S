using System;
using Game;
using UnityEngine;

// Token: 0x020009D2 RID: 2514
public class HoldingTorchCondition : Condition
{
	// Token: 0x060036C2 RID: 14018 RVA: 0x000E5E70 File Offset: 0x000E4070
	public override bool Validate(IContext context)
	{
		SeinCharacter sein = Characters.Sein;
		if (sein.Abilities.Carry && sein.Abilities.Carry.IsCarrying)
		{
			ICarryable currentCarryable = sein.Abilities.Carry.CurrentCarryable;
			if (currentCarryable != null)
			{
				return this.IsHolding == ((Component)currentCarryable).GetComponent<MistTorch>();
			}
		}
		return !this.IsHolding;
	}

	// Token: 0x040031A9 RID: 12713
	public bool IsHolding = true;
}
