using System;
using Game;
using UnityEngine;

// Token: 0x02000636 RID: 1590
public class InventoryDEAbilityConnector : MonoBehaviour
{
	// Token: 0x06002713 RID: 10003 RVA: 0x000AAC64 File Offset: 0x000A8E64
	public void OnEnable()
	{
		PlayerAbilities playerAbilities = Characters.Sein.PlayerAbilities;
		TransparencyAnimator component = base.GetComponent<TransparencyAnimator>();
		component.Initialize();
		if (playerAbilities.HasAbility(AbilityType.Grenade) && playerAbilities.HasAbility(AbilityType.ChargeJump))
		{
			component.AnimatorDriver.GoToEnd();
		}
		else
		{
			component.AnimatorDriver.GoToStart();
		}
	}
}
