using System;
using Game;
using UnityEngine;

// Token: 0x02000635 RID: 1589
public class InventoryAbilityItem : MonoBehaviour
{
	// Token: 0x1700062A RID: 1578
	// (get) Token: 0x06002710 RID: 10000 RVA: 0x000AABFE File Offset: 0x000A8DFE
	public bool HasAbility
	{
		get
		{
			return Characters.Sein.PlayerAbilities.HasAbility(this.Ability);
		}
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x000AAC18 File Offset: 0x000A8E18
	public void OnEnable()
	{
		bool hasAbility = this.HasAbility;
		TransparencyAnimator component = base.GetComponent<TransparencyAnimator>();
		component.Initialize();
		if (hasAbility)
		{
			component.AnimatorDriver.GoToEnd();
		}
		else
		{
			component.AnimatorDriver.GoToStart();
		}
	}

	// Token: 0x040021AD RID: 8621
	public MessageProvider AbilityName;

	// Token: 0x040021AE RID: 8622
	public MessageProvider AbilityDescription;

	// Token: 0x040021AF RID: 8623
	public AbilityType Ability;
}
