using System;
using UnityEngine;

// Token: 0x020004FE RID: 1278
[Serializable]
public class Kickback
{
	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x0600226A RID: 8810 RVA: 0x00096D4C File Offset: 0x00094F4C
	public float KickbackDuration
	{
		get
		{
			return this.KickbackCurve[this.KickbackCurve.length - 1].time;
		}
	}

	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x0600226B RID: 8811 RVA: 0x00096D79 File Offset: 0x00094F79
	// (set) Token: 0x0600226C RID: 8812 RVA: 0x00096D81 File Offset: 0x00094F81
	public Vector2 KickbackDirection { get; private set; }

	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x0600226D RID: 8813 RVA: 0x00096D8A File Offset: 0x00094F8A
	public float CurrentKickbackSpeed
	{
		get
		{
			if (this.m_kickbackTimeRemaining <= 0f)
			{
				return 0f;
			}
			return this.m_kickbackMultiplier * this.KickbackCurve.Evaluate(this.KickbackDuration - this.m_kickbackTimeRemaining);
		}
	}

	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x0600226E RID: 8814 RVA: 0x00096DC1 File Offset: 0x00094FC1
	public Vector2 KickbackVector
	{
		get
		{
			return this.CurrentKickbackSpeed * this.KickbackDirection;
		}
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x00096DD4 File Offset: 0x00094FD4
	public void ApplyKickback(float kickbackMultiplier)
	{
		this.m_kickbackMultiplier = kickbackMultiplier;
		this.m_kickbackTimeRemaining = this.KickbackDuration;
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x00096DE9 File Offset: 0x00094FE9
	public void ApplyKickback(float kickbackMultiplier, Vector2 kickbackDirection)
	{
		this.ApplyKickback(kickbackMultiplier);
		this.KickbackDirection = kickbackDirection.normalized;
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x00096DFF File Offset: 0x00094FFF
	public void AdvanceTime()
	{
		this.m_kickbackTimeRemaining -= Time.deltaTime;
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x00096E13 File Offset: 0x00095013
	public void Stop()
	{
		this.m_kickbackTimeRemaining = 0f;
	}

	// Token: 0x04001CDB RID: 7387
	public AnimationCurve KickbackCurve;

	// Token: 0x04001CDC RID: 7388
	private float m_kickbackTimeRemaining;

	// Token: 0x04001CDD RID: 7389
	private float m_kickbackMultiplier;
}
