using System;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
public class WormHoleManager : MonoBehaviour, ISuspendable
{
	// Token: 0x06002649 RID: 9801 RVA: 0x000A7E2D File Offset: 0x000A602D
	public MortarWormEnemy FindMortarWorm()
	{
		return base.GetComponentInChildren<MortarWormEnemy>();
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x000A7E38 File Offset: 0x000A6038
	public void Awake()
	{
		this.m_wormHoles = base.GetComponentsInChildren<WormHole>();
		MortarWormEnemy.OnMortarHide = (Action<MortarWormEnemy>)Delegate.Combine(MortarWormEnemy.OnMortarHide, new Action<MortarWormEnemy>(this.OnMortarHide));
		SuspensionManager.Register(this);
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x000A7E78 File Offset: 0x000A6078
	public void OnDestroy()
	{
		MortarWormEnemy.OnMortarHide = (Action<MortarWormEnemy>)Delegate.Remove(MortarWormEnemy.OnMortarHide, new Action<MortarWormEnemy>(this.OnMortarHide));
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x000A7EAB File Offset: 0x000A60AB
	public void OnMortarHide(MortarWormEnemy mortarWormEnemy)
	{
		if (mortarWormEnemy == this.m_mortarWormEnemy)
		{
			this.ResetHole();
		}
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x000A7EC4 File Offset: 0x000A60C4
	public void ResetHole()
	{
		this.m_timeBetweenHolesRemaining = 3f;
		for (int i = 0; i < 10; i++)
		{
			WormHole randomArrayItem = FixedRandom.GetRandomArrayItem<WormHole>(this.m_wormHoles, i);
			if (randomArrayItem != this.m_lastHole)
			{
				this.m_lastHole = randomArrayItem;
				this.m_mortarWormEnemy.Position = randomArrayItem.Position;
				this.m_mortarWormEnemy.Rotation = randomArrayItem.Rotation;
				this.m_mortarWormEnemy.WormHole = randomArrayItem;
				return;
			}
		}
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x000A7F44 File Offset: 0x000A6144
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_mortarWormEnemy == null)
		{
			this.m_mortarWormEnemy = this.FindMortarWorm();
		}
		if (this.m_mortarWormEnemy == null)
		{
			return;
		}
		if (this.m_timeBetweenHolesRemaining > 0f && this.m_mortarWormEnemy.IsHidden)
		{
			this.m_timeBetweenHolesRemaining -= Time.deltaTime;
			if (this.m_timeBetweenHolesRemaining <= 0f)
			{
				this.m_timeBetweenHolesRemaining = 0f;
				if (this.IsAggressive)
				{
					this.ResetHole();
					this.m_mortarWormEnemy.ForceEmerge();
				}
			}
		}
	}

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x0600264F RID: 9807 RVA: 0x000A7FF5 File Offset: 0x000A61F5
	// (set) Token: 0x06002650 RID: 9808 RVA: 0x000A7FFD File Offset: 0x000A61FD
	public bool IsSuspended { get; set; }

	// Token: 0x040020E1 RID: 8417
	private const float TimeBetweenHoles = 3f;

	// Token: 0x040020E2 RID: 8418
	private WormHole[] m_wormHoles;

	// Token: 0x040020E3 RID: 8419
	private MortarWormEnemy m_mortarWormEnemy;

	// Token: 0x040020E4 RID: 8420
	private float m_timeBetweenHolesRemaining;

	// Token: 0x040020E5 RID: 8421
	public bool IsAggressive;

	// Token: 0x040020E6 RID: 8422
	private WormHole m_lastHole;
}
