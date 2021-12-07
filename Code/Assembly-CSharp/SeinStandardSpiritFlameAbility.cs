using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class SeinStandardSpiritFlameAbility : CharacterState, ISeinReceiver
{
	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00016DE7 File Offset: 0x00014FE7
	public SpiritFlame CurrentSpiritFlame
	{
		get
		{
			return this.GetStandardSpiritFlame(this.OriLevel);
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00016DF5 File Offset: 0x00014FF5
	public int OriLevel
	{
		get
		{
			return this.m_sein.PlayerAbilities.OriStrength;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00016E07 File Offset: 0x00015007
	public bool LockShootingSpiritFlame
	{
		get
		{
			return this.m_sein.Abilities.SpiritFlame.LockShootingSpiritFlame;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x060005CA RID: 1482 RVA: 0x00016E1E File Offset: 0x0001501E
	public int MaxTargets
	{
		get
		{
			return this.m_sein.PlayerAbilities.SplitFlameTargets;
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00016E30 File Offset: 0x00015030
	public override void UpdateCharacterState()
	{
		if (this.m_sein.Controller.InputLocked)
		{
			return;
		}
		if (SeinAbilityRestrictZone.IsInside(SeinAbilityRestrictZoneMode.AllAbilities))
		{
			return;
		}
		bool flag = Core.Input.SpiritFlame.OnPressed && !Core.Input.SpiritFlame.Used;
		bool released = Core.Input.SpiritFlame.Released;
		bool flag2 = flag;
		if (flag)
		{
			if (Characters.Ori == null)
			{
				return;
			}
			this.StandardSpiritFlameShotCombo.UseShotDelay = false;
			this.m_timeOfBeforeLastShot = this.m_timeOfLastShot;
			this.m_timeOfLastShot = Time.time;
		}
		if (released)
		{
			this.UpdateTargetting();
			flag2 = false;
		}
		if (this.m_sein.PlayerAbilities.RapidFire.HasAbility)
		{
			this.StandardSpiritFlameShotCombo.CanShoot = true;
			if (this.m_isSpamming)
			{
				if (Time.time - this.m_timeOfLastSpam > 2f / this.SpamShotSpeed)
				{
					this.m_timeOfLastSpam = Time.time;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				if (Time.time - this.m_timeOfLastShot > 0.2f)
				{
					this.m_isSpamming = false;
				}
			}
			else if (flag2 && Time.time - this.m_timeOfBeforeLastShot < 0.2f)
			{
				this.m_timeOfLastSpam = Time.time;
				this.m_isSpamming = true;
			}
		}
		this.StandardSpiritFlameShotCombo.Update(Time.deltaTime);
		if (flag2)
		{
			Characters.Ori.ShootAnimation.Restart();
			if (this.StandardSpiritFlameShotCombo.CanShoot && !this.LockShootingSpiritFlame)
			{
				this.StandardSpiritFlameShotCombo.NumberOfShotsPerCombo = ((!this.m_sein.PlayerAbilities.QuickFlame.HasAbility) ? 2 : 3);
				SpiritFlame currentSpiritFlame = this.CurrentSpiritFlame;
				this.m_sein.Abilities.SpiritFlame.ThrowSpiritFlames(currentSpiritFlame);
				this.StandardSpiritFlameShotCombo.Shoot();
				Core.Input.SpiritFlame.Used = true;
			}
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00017028 File Offset: 0x00015228
	public SpiritFlame GetStandardSpiritFlame(int index)
	{
		if (index < 0)
		{
			index = 0;
		}
		if (index >= this.StandardSpiritFlames.Length)
		{
			index = this.StandardSpiritFlames.Length - 1;
		}
		return this.StandardSpiritFlames[index];
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x060005CD RID: 1485 RVA: 0x00017061 File Offset: 0x00015261
	public List<ISpiritFlameAttackable> ClosestAttackables
	{
		get
		{
			return this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables;
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00017078 File Offset: 0x00015278
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.StandardSpiritFlame = this;
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00017092 File Offset: 0x00015292
	public void UpdateTargetting()
	{
		this.m_sein.Abilities.SpiritFlameTargetting.MaxNumberOfTargets = (float)this.MaxTargets;
		this.m_sein.Abilities.SpiritFlameTargetting.Range = this.SpiritFlameRange;
	}

	// Token: 0x04000477 RID: 1143
	public ShotCombo StandardSpiritFlameShotCombo = new ShotCombo();

	// Token: 0x04000478 RID: 1144
	public SeinStandardSpiritFlameAbility.PoisonSettings Poison = new SeinStandardSpiritFlameAbility.PoisonSettings();

	// Token: 0x04000479 RID: 1145
	public SpiritFlame[] StandardSpiritFlames;

	// Token: 0x0400047A RID: 1146
	public float SpiritFlameRange = 8f;

	// Token: 0x0400047B RID: 1147
	public bool CanDamageOverTime;

	// Token: 0x0400047C RID: 1148
	private SeinCharacter m_sein;

	// Token: 0x0400047D RID: 1149
	private float m_timeOfLastShot;

	// Token: 0x0400047E RID: 1150
	private float m_timeOfBeforeLastShot;

	// Token: 0x0400047F RID: 1151
	private bool m_isSpamming;

	// Token: 0x04000480 RID: 1152
	private float m_timeOfLastSpam;

	// Token: 0x04000481 RID: 1153
	public float SpamShotSpeed = 10f;

	// Token: 0x02000087 RID: 135
	[Serializable]
	public class PoisonSettings
	{
		// Token: 0x04000482 RID: 1154
		public float DamageAmount = 4f;

		// Token: 0x04000483 RID: 1155
		public int DamageDuration = 4;

		// Token: 0x04000484 RID: 1156
		public GameObject PoisonEffect;
	}
}
