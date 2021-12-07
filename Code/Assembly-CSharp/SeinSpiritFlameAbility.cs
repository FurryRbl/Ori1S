using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class SeinSpiritFlameAbility : CharacterState, ISeinReceiver
{
	// Token: 0x1700015E RID: 350
	// (get) Token: 0x0600056D RID: 1389 RVA: 0x000157EC File Offset: 0x000139EC
	public bool LockShootingSpiritFlame
	{
		get
		{
			return this.m_lockShootingSpiritFlameLocks.Count > 0;
		}
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000157FC File Offset: 0x000139FC
	public void AddLock(string lockName)
	{
		this.m_lockShootingSpiritFlameLocks.Add(lockName);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001580B File Offset: 0x00013A0B
	public void RemoveLock(string lockName)
	{
		this.m_lockShootingSpiritFlameLocks.Remove(lockName);
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001581A File Offset: 0x00013A1A
	public List<ISpiritFlameAttackable> ClosestAttackables
	{
		get
		{
			return this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables;
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00015831 File Offset: 0x00013A31
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.SpiritFlame = this;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001584B File Offset: 0x00013A4B
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00015854 File Offset: 0x00013A54
	public override void UpdateCharacterState()
	{
		if (!base.Active)
		{
			return;
		}
		if (this.m_sein.IsSuspended)
		{
			return;
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00015874 File Offset: 0x00013A74
	public void ThrowSpiritFlames(SpiritFlame spiritFlame)
	{
		if (Characters.Ori == null)
		{
			return;
		}
		for (int i = 0; i < this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables.Count; i++)
		{
			ISpiritFlameAttackable spiritFlameAttackable = this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables[i];
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(spiritFlame.Projectile, Characters.Ori.transform.position, Quaternion.identity);
			SpiritFlameProjectile component = gameObject.GetComponent<SpiritFlameProjectile>();
			component.AttackableTargetTransform = ((Component)spiritFlameAttackable).transform;
			component.SpiritFlame = spiritFlame;
			component.Sein = this.m_sein;
			component.StartPosition = Characters.Ori.Position;
			component.Damage = spiritFlame.Damage;
			component.StartTarget = Characters.Ori.transform;
			component.ImpactOffset = spiritFlameAttackable.GenerateSpiritFlameProjectileOffset(this.m_sein.Position);
			component.DoImpact = true;
			component.HasARealTarget = true;
		}
		if (this.ShootWhenNoTarget && this.m_sein.Abilities.SpiritFlameTargetting.ClosestAttackables.Count == 0)
		{
			GameObject gameObject2 = InstantiateUtility.Instantiate(this.TempTarget) as GameObject;
			Transform transform = gameObject2.transform;
			PlatformBehaviour platformBehaviour = this.m_sein.PlatformBehaviour;
			Vector3 b = new Vector3(1f - 2f * FixedRandom.Values[1], 1f - 2f * FixedRandom.Values[2], 0f) * 4.5f;
			transform.position = Characters.Ori.Position + 2f * platformBehaviour.PlatformMovement.GroundBinormal * (float)((!platformBehaviour.Visuals.SpriteMirror.FaceLeft) ? 1 : -1) + b;
			gameObject2.GetComponent<FollowPositionRotation>().SetTarget(Characters.Ori.transform);
			GameObject gameObject3 = (GameObject)InstantiateUtility.Instantiate(spiritFlame.Projectile);
			gameObject3.transform.position = Characters.Ori.transform.position;
			SpiritFlameProjectile component2 = gameObject3.GetComponent<SpiritFlameProjectile>();
			component2.AttackableTargetTransform = transform;
			component2.SpiritFlame = spiritFlame;
			component2.Sein = this.m_sein;
			component2.StartPosition = Characters.Ori.Position;
			component2.DoImpact = false;
			component2.StartTarget = Characters.Ori.transform;
			component2.HasARealTarget = false;
		}
	}

	// Token: 0x0400042C RID: 1068
	public GameObject TempTarget;

	// Token: 0x0400042D RID: 1069
	private readonly HashSet<string> m_lockShootingSpiritFlameLocks = new HashSet<string>();

	// Token: 0x0400042E RID: 1070
	public bool ShootWhenNoTarget = true;

	// Token: 0x0400042F RID: 1071
	private SeinCharacter m_sein;
}
