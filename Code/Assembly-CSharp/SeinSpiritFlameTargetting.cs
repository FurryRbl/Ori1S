using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class SeinSpiritFlameTargetting : CharacterState, ISeinReceiver
{
	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001676F File Offset: 0x0001496F
	// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00016777 File Offset: 0x00014977
	public float Range { get; set; }

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00016780 File Offset: 0x00014980
	// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00016788 File Offset: 0x00014988
	public float MaxNumberOfTargets { get; set; }

	// Token: 0x060005B6 RID: 1462 RVA: 0x00016791 File Offset: 0x00014991
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.SpiritFlameTargetting = this;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000167AB File Offset: 0x000149AB
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x000167B4 File Offset: 0x000149B4
	public bool IsShootableTarget(IAttackable attackable)
	{
		ISpiritFlameAttackable spiritFlameAttackable = attackable as ISpiritFlameAttackable;
		if (spiritFlameAttackable == null)
		{
			return false;
		}
		if (!attackable.CanBeSpiritFlamed())
		{
			return false;
		}
		if (spiritFlameAttackable.RequiresSpiritFlameAbilityToTarget && !this.m_sein.PlayerAbilities.SpiritFlame.HasAbility)
		{
			return false;
		}
		float num = Vector3.Distance(attackable.Position, this.m_sein.Position);
		if (num > this.Range)
		{
			return false;
		}
		if (num > spiritFlameAttackable.SpiritFlameRange)
		{
			return false;
		}
		GameObject gameObject = (spiritFlameAttackable as Component).gameObject;
		return this.m_sein.Controller.RayTest(gameObject) || (this.m_sein.Controller.RayTest(this.m_sein.gameObject, new Vector2(0f, 0f), new Vector2(0f, 3f)) && this.m_sein.Controller.RayTest(gameObject, new Vector2(0f, 3f), new Vector2(0f, 0f)));
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000168D0 File Offset: 0x00014AD0
	public void UpdateClosestAttackables()
	{
		this.ClosestAttackables.Clear();
		if (!this.m_sein.Controller.CanMove || this.m_sein.Controller.IsBashing)
		{
			return;
		}
		Vector3 position = this.m_sein.Position;
		this.m_remainingSpiritFlameAttackables.Clear();
		for (int i = 0; i < Targets.Attackables.Count; i++)
		{
			IAttackable attackable = Targets.Attackables[i];
			float num = Vector3.Distance(attackable.Position, position);
			if (num < this.Range && this.IsShootableTarget(attackable))
			{
				this.m_remainingSpiritFlameAttackables.Add(attackable as ISpiritFlameAttackable);
			}
		}
		int num2 = 0;
		while ((float)num2 < this.MaxNumberOfTargets)
		{
			ISpiritFlameAttackable spiritFlameAttackable = null;
			float num3 = float.MaxValue;
			int num4 = int.MinValue;
			for (int j = 0; j < this.m_remainingSpiritFlameAttackables.Count; j++)
			{
				ISpiritFlameAttackable spiritFlameAttackable2 = this.m_remainingSpiritFlameAttackables[j];
				IAttackable attackable2 = spiritFlameAttackable2 as IAttackable;
				float num5 = Vector3.Distance(attackable2.Position, position);
				int spiritFlamePriority = spiritFlameAttackable2.SpiritFlamePriority;
				if (spiritFlamePriority > num4 || (num5 <= num3 && spiritFlamePriority == num4))
				{
					num3 = num5;
					num4 = spiritFlamePriority;
					spiritFlameAttackable = spiritFlameAttackable2;
				}
			}
			if (spiritFlameAttackable == null)
			{
				break;
			}
			this.m_remainingSpiritFlameAttackables.Remove(spiritFlameAttackable);
			this.ClosestAttackables.Add(spiritFlameAttackable);
			num2++;
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00016A54 File Offset: 0x00014C54
	public void UpdateTargetHighlight()
	{
		for (int i = 0; i < this.m_lastClosestAttackables.Count; i++)
		{
			ISpiritFlameAttackable spiritFlameAttackable = this.m_lastClosestAttackables[i];
			if (!this.ClosestAttackables.Contains(spiritFlameAttackable))
			{
				if (spiritFlameAttackable as Component != null)
				{
					spiritFlameAttackable.OnSpiritFlameDehighlight();
				}
			}
		}
		for (int j = 0; j < this.ClosestAttackables.Count; j++)
		{
			ISpiritFlameAttackable spiritFlameAttackable2 = this.ClosestAttackables[j];
			if (!this.m_lastClosestAttackables.Contains(spiritFlameAttackable2))
			{
				spiritFlameAttackable2.OnSpiritFlameHighlight();
			}
		}
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00016AFC File Offset: 0x00014CFC
	public override void UpdateCharacterState()
	{
		base.UpdateCharacterState();
		if (Characters.Ori == null)
		{
			return;
		}
		this.m_lastClosestAttackables.Clear();
		for (int i = 0; i < this.ClosestAttackables.Count; i++)
		{
			ISpiritFlameAttackable item = this.ClosestAttackables[i];
			this.m_lastClosestAttackables.Add(item);
		}
		this.UpdateClosestAttackables();
		this.UpdateTargetHighlight();
		ISpiritFlameAttackable spiritFlameAttackable = null;
		if (this.ClosestAttackables.Count > 0)
		{
			spiritFlameAttackable = this.ClosestAttackables[0];
		}
		if (spiritFlameAttackable != null)
		{
			float num = spiritFlameAttackable.OriDistanceFromAttackable;
			IAttackable attackable = spiritFlameAttackable as IAttackable;
			num = Mathf.Min(num, Vector2.Distance(Characters.Sein.Position, attackable.Position));
			Vector3 a = Vector3.ClampMagnitude(attackable.Position - Characters.Ori.TargetPosition - Characters.Ori.TargetOffset, 8f);
			a -= a.normalized * num;
			if (Characters.Ori)
			{
				Characters.Ori.TargetOffsetAttack = Characters.Ori.TargetOffsetAttack + Vector3.ClampMagnitude(a - Characters.Ori.TargetOffsetAttack, Time.deltaTime * 20f);
			}
		}
		else if (Characters.Ori)
		{
			Characters.Ori.TargetOffsetAttack = Characters.Ori.TargetOffsetAttack + Vector3.ClampMagnitude(-Characters.Ori.TargetOffsetAttack, Time.deltaTime * 15f);
		}
	}

	// Token: 0x0400046E RID: 1134
	public const float DefaultOriDistanceFromAttackable = 5f;

	// Token: 0x0400046F RID: 1135
	private readonly List<ISpiritFlameAttackable> m_lastClosestAttackables = new List<ISpiritFlameAttackable>();

	// Token: 0x04000470 RID: 1136
	public List<ISpiritFlameAttackable> ClosestAttackables = new List<ISpiritFlameAttackable>();

	// Token: 0x04000471 RID: 1137
	private SeinCharacter m_sein;

	// Token: 0x04000472 RID: 1138
	private readonly List<ISpiritFlameAttackable> m_remainingSpiritFlameAttackables = new List<ISpiritFlameAttackable>();
}
