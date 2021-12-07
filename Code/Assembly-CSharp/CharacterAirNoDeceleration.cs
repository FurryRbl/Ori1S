using System;

// Token: 0x0200034E RID: 846
public class CharacterAirNoDeceleration : CharacterState
{
	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06001831 RID: 6193 RVA: 0x00067B46 File Offset: 0x00065D46
	// (set) Token: 0x06001832 RID: 6194 RVA: 0x00067B4E File Offset: 0x00065D4E
	public bool NoDeceleration
	{
		get
		{
			return this.m_noDeceleration;
		}
		set
		{
			this.m_noDeceleration = value;
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06001833 RID: 6195 RVA: 0x00067B57 File Offset: 0x00065D57
	public CharacterLeftRightMovement CharacterLeftRightMovement
	{
		get
		{
			return this.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x06001834 RID: 6196 RVA: 0x00067B64 File Offset: 0x00065D64
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00067B71 File Offset: 0x00065D71
	public void Start()
	{
		base.Active = true;
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent += this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x00067B91 File Offset: 0x00065D91
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.CharacterLeftRightMovement.ModifyHorizontalPlatformMovementSettingsEvent -= this.ModifyHorizontalPlatformMovementSettings;
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x00067BB0 File Offset: 0x00065DB0
	public void ModifyHorizontalPlatformMovementSettings(HorizontalPlatformMovementSettings settings)
	{
		if (base.Active && this.NoDeceleration)
		{
			settings.Air.Decceleration = 0f;
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x00067BE4 File Offset: 0x00065DE4
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (this.PlatformMovement.IsOnGround)
		{
			this.NoDeceleration = false;
		}
		if (this.PlatformMovement.IsOnCeiling)
		{
			this.NoDeceleration = false;
		}
		if (this.CharacterLeftRightMovement.HorizontalInput != 0f)
		{
			this.NoDeceleration = false;
		}
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x00067C51 File Offset: 0x00065E51
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_noDeceleration);
		base.Serialize(ar);
	}

	// Token: 0x040014CD RID: 5325
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x040014CE RID: 5326
	private bool m_noDeceleration;
}
