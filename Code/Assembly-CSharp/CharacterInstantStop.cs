using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class CharacterInstantStop : CharacterState
{
	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000BA1F File Offset: 0x00009C1F
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BA2C File Offset: 0x00009C2C
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000BA39 File Offset: 0x00009C39
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000BA42 File Offset: 0x00009C42
	public void LockForDuration(float duration)
	{
		this.m_lockTimeRemaining = duration;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000BA4B File Offset: 0x00009C4B
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_lockTimeRemaining);
		base.Serialize(ar);
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000BA60 File Offset: 0x00009C60
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		this.m_lockTimeRemaining -= Time.deltaTime;
		if (this.m_lockTimeRemaining > 0f)
		{
			return;
		}
		if (base.Active && this.PlatformMovement.IsOnGround && MoonMath.Float.Normalize(this.PlatformMovement.LocalSpeedX) != MoonMath.Float.Normalize(this.LeftRightMovement.HorizontalInput))
		{
			this.PlatformMovement.LocalSpeedX = 0f;
		}
	}

	// Token: 0x04000203 RID: 515
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x04000204 RID: 516
	private float m_lockTimeRemaining;
}
