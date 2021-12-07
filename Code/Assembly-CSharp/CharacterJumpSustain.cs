using System;
using Core;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class CharacterJumpSustain : CharacterState
{
	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x000081A4 File Offset: 0x000063A4
	public CharacterGravity CharacterGravity
	{
		get
		{
			return this.PlatformBehaviour.Gravity;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060001E5 RID: 485 RVA: 0x000081B1 File Offset: 0x000063B1
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000081BE File Offset: 0x000063BE
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x000081C7 File Offset: 0x000063C7
	public new void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x000081D0 File Offset: 0x000063D0
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.JumpStopDeceleration);
		ar.Serialize(ref this.JumpStopDecelerationMultiplier);
		ar.Serialize(ref this.m_amountOfSpeedToLose);
		base.Serialize(ar);
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00008208 File Offset: 0x00006408
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (base.Active)
		{
			Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
			float num = this.JumpStopDecelerationMultiplier * this.JumpStopDeceleration * Time.deltaTime;
			if (this.PlatformMovement.LocalSpeed.y <= 0f)
			{
				this.m_amountOfSpeedToLose = 0f;
			}
			if (!Core.Input.Jump.Pressed)
			{
				if (num < this.m_amountOfSpeedToLose)
				{
					localSpeed.y -= num;
					this.m_amountOfSpeedToLose -= num;
				}
				else
				{
					localSpeed.y -= this.m_amountOfSpeedToLose;
					this.m_amountOfSpeedToLose = 0f;
				}
			}
			this.PlatformMovement.LocalSpeed = localSpeed;
			this.m_amountOfSpeedToLose -= this.CharacterGravity.CurrentSettings.GravityStrength * Time.deltaTime;
			if (this.m_amountOfSpeedToLose < 0f)
			{
				this.m_amountOfSpeedToLose = 0f;
			}
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00008324 File Offset: 0x00006524
	public void SetAmountOfSpeedToLose(float speed, float jumpStopDecelerationMultiplier = 1f)
	{
		this.m_amountOfSpeedToLose = speed;
		this.JumpStopDecelerationMultiplier = jumpStopDecelerationMultiplier;
	}

	// Token: 0x04000188 RID: 392
	public float JumpStopDeceleration;

	// Token: 0x04000189 RID: 393
	public float JumpStopDecelerationMultiplier = 1f;

	// Token: 0x0400018A RID: 394
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x0400018B RID: 395
	private float m_amountOfSpeedToLose;
}
