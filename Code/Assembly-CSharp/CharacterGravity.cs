using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class CharacterGravity : CharacterState
{
	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060001B1 RID: 433 RVA: 0x0000781D File Offset: 0x00005A1D
	// (remove) Token: 0x060001B2 RID: 434 RVA: 0x00007836 File Offset: 0x00005A36
	public event Action<GravityPlatformMovementSettings> ModifyGravityPlatformMovementSettingsEvent = delegate(GravityPlatformMovementSettings A_0)
	{
	};

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000784F File Offset: 0x00005A4F
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000785C File Offset: 0x00005A5C
	public GravityPlatformMovementSettings CurrentSettings
	{
		get
		{
			return this.m_settings;
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007864 File Offset: 0x00005A64
	public GravityPlatformMovementSettings BaseSettings
	{
		get
		{
			return this.Settings;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000786C File Offset: 0x00005A6C
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00007878 File Offset: 0x00005A78
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (!base.Active)
		{
			return;
		}
		this.UpdateSettings();
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		this.PlatformMovement.GravityAngle = this.CurrentSettings.GravityAngle;
		if (!this.PlatformMovement.IsOnGround || !this.PlatformMovement.GroundRayHit)
		{
			float gravityStrength = this.CurrentSettings.GravityStrength;
			float a = Mathf.Max(-this.BaseSettings.MaxFallSpeed, localSpeed.y - gravityStrength * Time.deltaTime);
			localSpeed.y = Mathf.Min(a, localSpeed.y);
		}
		this.PlatformMovement.LocalSpeed = localSpeed;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000793C File Offset: 0x00005B3C
	public void UpdateSettings()
	{
		this.m_settings.CopyFrom(this.Settings);
		this.ModifyGravityPlatformMovementSettingsEvent(this.m_settings);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0000796C File Offset: 0x00005B6C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.BaseSettings.GravityAngle);
		ar.Serialize(ref this.BaseSettings.GravityStrength);
		ar.Serialize(ref this.BaseSettings.MaxFallSpeed);
	}

	// Token: 0x04000148 RID: 328
	private readonly GravityPlatformMovementSettings m_settings = new GravityPlatformMovementSettings();

	// Token: 0x04000149 RID: 329
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x0400014A RID: 330
	public GravityPlatformMovementSettings Settings;
}
