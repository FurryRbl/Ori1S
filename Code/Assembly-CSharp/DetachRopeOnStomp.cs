using System;
using Game;
using UnityEngine;

// Token: 0x020006CF RID: 1743
public class DetachRopeOnStomp : MonoBehaviour, IDamageReciever, IAttackable, IStompAttackable
{
	// Token: 0x060029D0 RID: 10704 RVA: 0x000B41F9 File Offset: 0x000B23F9
	public void OnEnable()
	{
		Targets.Attackables.Add(this);
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x000B4206 File Offset: 0x000B2406
	public void OnDestroy()
	{
		Targets.Attackables.Remove(this);
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x000B4214 File Offset: 0x000B2414
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Stomp)
		{
			int num = 0;
			foreach (AttachToRope attachToRope in this.RopeAttachments)
			{
				if (attachToRope != null)
				{
					attachToRope.BreakAttachment();
					num++;
					if (num >= this.DetachmentsPerStomp)
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000B4276 File Offset: 0x000B2476
	// (set) Token: 0x060029D4 RID: 10708 RVA: 0x000B427E File Offset: 0x000B247E
	public Vector3 Position { get; private set; }

	// Token: 0x060029D5 RID: 10709 RVA: 0x000B4287 File Offset: 0x000B2487
	public bool CanBeChargeFlamed()
	{
		return false;
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x000B428A File Offset: 0x000B248A
	public bool CanBeChargeDashed()
	{
		return false;
	}

	// Token: 0x060029D7 RID: 10711 RVA: 0x000B428D File Offset: 0x000B248D
	public bool CanBeGrenaded()
	{
		return false;
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x000B4290 File Offset: 0x000B2490
	public bool CanBeStomped()
	{
		return true;
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x000B4293 File Offset: 0x000B2493
	public bool CanBeBashed()
	{
		return false;
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x000B4296 File Offset: 0x000B2496
	public bool CanBeSpiritFlamed()
	{
		return false;
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x000B4299 File Offset: 0x000B2499
	public bool IsStompBouncable()
	{
		return false;
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x000B429C File Offset: 0x000B249C
	public bool CanBeLevelUpBlasted()
	{
		return false;
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x000B429F File Offset: 0x000B249F
	public bool CountsTowardsSuperJumpAchievement()
	{
		return false;
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x000B42A2 File Offset: 0x000B24A2
	public bool IsDead()
	{
		return false;
	}

	// Token: 0x04002543 RID: 9539
	public int DetachmentsPerStomp = 1;

	// Token: 0x04002544 RID: 9540
	public AttachToRope[] RopeAttachments;
}
