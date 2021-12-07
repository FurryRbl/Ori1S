using System;
using Game;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class EntityTargetting : MonoBehaviour, IAttackable, IChargeFlameAttackable, IStompAttackable, IBashAttackable, ISpiritFlameAttackable, IChargeDashAttackable
{
	// Token: 0x17000372 RID: 882
	// (get) Token: 0x0600137E RID: 4990 RVA: 0x0005A449 File Offset: 0x00058649
	// (set) Token: 0x0600137F RID: 4991 RVA: 0x0005A451 File Offset: 0x00058651
	public bool IsSpiritFlameHighlighted { get; private set; }

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001380 RID: 4992 RVA: 0x0005A45A File Offset: 0x0005865A
	// (set) Token: 0x06001381 RID: 4993 RVA: 0x0005A462 File Offset: 0x00058662
	public bool IsBashHighlighted { get; private set; }

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001382 RID: 4994 RVA: 0x0005A46B File Offset: 0x0005866B
	// (set) Token: 0x06001383 RID: 4995 RVA: 0x0005A473 File Offset: 0x00058673
	public bool IsChargeDashHighlighted { get; private set; }

	// Token: 0x06001384 RID: 4996 RVA: 0x0005A47C File Offset: 0x0005867C
	public void OnValidate()
	{
		this.Entity = base.transform.FindComponentUpwards<Entity>();
		this.Entity.Targetting = this;
		this.m_highlightColors = this.Entity.gameObject.FindComponentsInChildren<IEntityHighlight>();
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x0005A4B1 File Offset: 0x000586B1
	public void Awake()
	{
		if (this.Entity == null)
		{
			this.OnValidate();
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001386 RID: 4998 RVA: 0x0005A4CA File Offset: 0x000586CA
	public bool IsOnScreen
	{
		get
		{
			return UI.Cameras.Current.IsOnScreen(this.Entity.Position);
		}
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x0005A4E1 File Offset: 0x000586E1
	public bool CanBeSpiritFlamed()
	{
		return this.Activated && this.CanSpiritFlame && this.IsOnScreen;
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x0005A502 File Offset: 0x00058702
	public void OnSpiritFlameHighlight()
	{
		this.IsSpiritFlameHighlighted = true;
		this.UpdateHighlighting();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x0005A511 File Offset: 0x00058711
	public void OnSpiritFlameDehighlight()
	{
		this.IsSpiritFlameHighlighted = false;
		this.UpdateHighlighting();
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x0600138A RID: 5002 RVA: 0x0005A520 File Offset: 0x00058720
	public int SpiritFlamePriority
	{
		get
		{
			return this.SpiritFlamePriorityNumber;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x0600138B RID: 5003 RVA: 0x0005A528 File Offset: 0x00058728
	public float OriDistanceFromAttackable
	{
		get
		{
			return 5f;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x0600138C RID: 5004 RVA: 0x0005A52F File Offset: 0x0005872F
	public float SpiritFlameRange
	{
		get
		{
			return float.PositiveInfinity;
		}
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x0005A536 File Offset: 0x00058736
	public bool CanBeBashed()
	{
		return this.Activated && this.CanBash && this.IsOnScreen;
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x0005A557 File Offset: 0x00058757
	public void OnEnable()
	{
		Targets.Attackables.Add(this);
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x0005A564 File Offset: 0x00058764
	public void OnDisable()
	{
		Targets.Attackables.Remove(this);
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x0005A572 File Offset: 0x00058772
	public void OnEnterBash()
	{
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x0005A574 File Offset: 0x00058774
	public void OnBashHighlight()
	{
		this.IsBashHighlighted = true;
		this.UpdateHighlighting();
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x0005A583 File Offset: 0x00058783
	public void OnBashDehighlight()
	{
		this.IsBashHighlighted = false;
		this.UpdateHighlighting();
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06001393 RID: 5011 RVA: 0x0005A592 File Offset: 0x00058792
	public int BashPriority
	{
		get
		{
			return 100;
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x0005A598 File Offset: 0x00058798
	public void UpdateHighlighting()
	{
		for (int i = 0; i < this.m_highlightColors.Length; i++)
		{
			IEntityHighlight entityHighlight = (IEntityHighlight)this.m_highlightColors[i];
			if (this.IsChargeDashHighlighted)
			{
				entityHighlight.SetToChargeDash();
			}
			else if (this.IsBashHighlighted)
			{
				entityHighlight.SetToBashHighlight();
			}
			else if (this.IsSpiritFlameHighlighted)
			{
				entityHighlight.SetToSpiritFlame();
			}
			else
			{
				entityHighlight.Reset();
			}
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001395 RID: 5013 RVA: 0x0005A614 File Offset: 0x00058814
	public bool RequiresSpiritFlameAbilityToTarget
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x0005A617 File Offset: 0x00058817
	public Vector3 GenerateSpiritFlameProjectileOffset(Vector3 position)
	{
		return this.SpiritFlameProjectileOffsetGenerator.GenerateSpiritFlameProjectileOffset(base.transform, position);
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001397 RID: 5015 RVA: 0x0005A62B File Offset: 0x0005882B
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x0005A638 File Offset: 0x00058838
	public bool CanBeStomped()
	{
		return this.CanStomp;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x0005A640 File Offset: 0x00058840
	public bool CountsTowardsSuperJumpAchievement()
	{
		return this.Entity is Enemy;
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x0005A650 File Offset: 0x00058850
	public bool CanBeChargeFlamed()
	{
		return this.CanChargeFlame;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x0005A658 File Offset: 0x00058858
	public bool CanBeChargeDashed()
	{
		return this.CanBeSpiritFlamed() && this.Entity is Enemy;
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x0005A676 File Offset: 0x00058876
	public bool CanBeGrenaded()
	{
		return this.CanBeChargeFlamed() && this.Activated;
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x0005A68C File Offset: 0x0005888C
	public bool CountsTowardsPowerOfLightAchievement()
	{
		return this.Entity is Enemy;
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x0005A69C File Offset: 0x0005889C
	public bool IsDead()
	{
		return this.Entity && this.Entity.DamageReciever && this.Entity.DamageReciever.NoHealthLeft;
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x0005A6D8 File Offset: 0x000588D8
	public bool IsStompBouncable()
	{
		return (this.Entity.DamageReciever.IgnoreDamageCondition == null || !this.Entity.DamageReciever.IgnoreDamageCondition(new Damage(0f, Vector3.down, base.transform.position, DamageType.Stomp, base.gameObject))) && this.Entity.DamageReciever.BounceOnStomp;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x0005A74D File Offset: 0x0005894D
	public bool CanBeLevelUpBlasted()
	{
		return this.CanLevelUpBlast || this.CanBeSpiritFlamed();
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x0005A763 File Offset: 0x00058963
	public void OnChargeDashHighlight()
	{
		this.IsChargeDashHighlighted = true;
		this.UpdateHighlighting();
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x0005A772 File Offset: 0x00058972
	public void OnChargeDashDehighlight()
	{
		this.IsChargeDashHighlighted = false;
		this.UpdateHighlighting();
	}

	// Token: 0x04001159 RID: 4441
	public Entity Entity;

	// Token: 0x0400115A RID: 4442
	[HideInInspector]
	[SerializeField]
	private Component[] m_highlightColors;

	// Token: 0x0400115B RID: 4443
	public bool Activated = true;

	// Token: 0x0400115C RID: 4444
	public bool CanSpiritFlame = true;

	// Token: 0x0400115D RID: 4445
	public bool CanBash = true;

	// Token: 0x0400115E RID: 4446
	public bool CanChargeFlame = true;

	// Token: 0x0400115F RID: 4447
	public bool CanStomp = true;

	// Token: 0x04001160 RID: 4448
	public bool CanLevelUpBlast;

	// Token: 0x04001161 RID: 4449
	public int SpiritFlamePriorityNumber;

	// Token: 0x04001162 RID: 4450
	public SpiritFlameProjectileOffsetGenerator SpiritFlameProjectileOffsetGenerator = new SpiritFlameProjectileOffsetGenerator();
}
