using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class SeinSpriteRotationController : CharacterState, ISeinReceiver
{
	// Token: 0x0600183B RID: 6203 RVA: 0x00067C6E File Offset: 0x00065E6E
	public void BeginTiltLeftRightInAir(float duration)
	{
		this.m_tiltLeftRightTimer = duration;
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x00067C77 File Offset: 0x00065E77
	public void BeginTiltUpDownInAir(float duration)
	{
		this.m_tiltUpDownTimer = duration;
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x0600183D RID: 6205 RVA: 0x00067C80 File Offset: 0x00065E80
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x0600183E RID: 6206 RVA: 0x00067C92 File Offset: 0x00065E92
	public SeinCrouch Crouch
	{
		get
		{
			return this.Sein.Abilities.Crouch;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x0600183F RID: 6207 RVA: 0x00067CA4 File Offset: 0x00065EA4
	public SeinStomp Stomp
	{
		get
		{
			return this.Sein.Abilities.Stomp;
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x06001840 RID: 6208 RVA: 0x00067CB6 File Offset: 0x00065EB6
	public SeinBashAttack BashAttack
	{
		get
		{
			return this.Sein.Abilities.Bash;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x06001841 RID: 6209 RVA: 0x00067CC8 File Offset: 0x00065EC8
	public bool IsStomping
	{
		get
		{
			return this.Stomp && this.Stomp.Active;
		}
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x00067CF3 File Offset: 0x00065EF3
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x00067CFC File Offset: 0x00065EFC
	private void UpdateUnderwaterRotation()
	{
		this.HeadAngle = 0f;
		this.FeetAngle = 0f;
		this.CenterAngle = this.Sein.Abilities.Swimming.SwimAngle + (float)((!this.Sein.Controller.FaceLeft) ? 0 : 180);
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x00067D5C File Offset: 0x00065F5C
	private void UpdateCinematicRotation()
	{
		if (this.PlatformMovement.IsOnGround)
		{
			this.m_groundAngle = Mathf.LerpAngle(this.m_groundAngle, this.PlatformMovement.GroundAngle, 0.1f);
		}
		else
		{
			this.m_groundAngle = Mathf.LerpAngle(this.m_groundAngle, 0f, 0.1f);
		}
		this.FeetAngle = this.m_groundAngle;
		this.HeadAngle = 0f;
		this.CenterAngle = 0f;
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x00067DDC File Offset: 0x00065FDC
	private void UpdateRegularRotation()
	{
		if (this.m_tiltLeftRightTimer > 0f)
		{
			this.m_tiltLeftRightTimer = Mathf.Max(this.m_tiltLeftRightTimer - Time.deltaTime, 0f);
		}
		if (this.m_tiltUpDownTimer > 0f)
		{
			this.m_tiltUpDownTimer = Mathf.Max(this.m_tiltUpDownTimer - Time.deltaTime, 0f);
		}
		this.CenterAngle = 0f;
		this.HeadAngle = 0f;
		this.FeetAngle = 0f;
		if (this.PlatformMovement.HasWallLeft)
		{
			if (!this.PlatformMovement.WallLeft.WasOn)
			{
				this.m_wallLeftAngle = ((!this.PlatformMovement.WallLeftRayHit) ? this.PlatformMovement.GravityAngle : this.PlatformMovement.WallLeftAngle);
			}
			else if (this.PlatformMovement.WallLeftRayHit)
			{
				this.m_wallLeftAngle = Mathf.LerpAngle(this.m_wallLeftAngle, this.PlatformMovement.WallLeftAngle, 0.2f);
			}
			if (this.Sein.Abilities.Swimming && this.Sein.Abilities.Swimming.IsSwimming)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else if (this.PlatformMovement.IsOnGround && this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else
			{
				this.FeetAngle = Mathf.Max(0f, this.m_wallLeftAngle);
				this.HeadAngle = Mathf.Min(0f, this.m_wallLeftAngle);
			}
		}
		else if (this.PlatformMovement.HasWallRight)
		{
			if (!this.PlatformMovement.WallRight.WasOn)
			{
				this.m_wallRightAngle = ((!this.PlatformMovement.WallRightRayHit) ? this.PlatformMovement.GravityAngle : this.PlatformMovement.WallRightAngle);
			}
			else if (this.PlatformMovement.WallRightRayHit)
			{
				this.m_wallRightAngle = Mathf.LerpAngle(this.m_wallRightAngle, this.PlatformMovement.WallRightAngle, 0.2f);
			}
			if (this.Sein.Abilities.Swimming && this.Sein.Abilities.Swimming.IsSwimming)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else if (this.PlatformMovement.IsOnGround && !this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else
			{
				this.HeadAngle = Mathf.Max(0f, this.m_wallRightAngle);
				this.FeetAngle = Mathf.Min(0f, this.m_wallRightAngle);
			}
		}
		else if (this.PlatformMovement.IsOnGround)
		{
			if (this.Sein.Controller.IsAimingGrenade)
			{
				this.m_groundAngle = this.PlatformMovement.GroundAngle;
			}
			else if (!this.PlatformMovement.Ground.WasOn)
			{
				this.m_groundAngle = ((!this.PlatformMovement.GroundRayHit) ? this.PlatformMovement.GravityAngle : this.PlatformMovement.GroundAngle);
			}
			else if (this.PlatformMovement.GroundRayHit)
			{
				this.m_groundAngle = Mathf.LerpAngle(this.m_groundAngle, this.PlatformMovement.GroundAngle, 0.2f);
			}
			if (this.Sein.Abilities.Swimming && this.Sein.Abilities.Swimming.IsSwimming)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else if (this.PlatformMovement.IsOnCeiling && this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft == this.PlatformMovement.CeilingNormal.x > 0f)
			{
				this.FeetAngle = this.PlatformMovement.GravityAngle;
			}
			else
			{
				this.FeetAngle = this.m_groundAngle;
			}
		}
		else
		{
			this.FeetAngle = this.PlatformMovement.GravityAngle;
			if (this.m_tiltLeftRightTimer > 0f)
			{
				this.CenterAngle -= Mathf.Atan2(this.PlatformMovement.LocalSpeedX, 12f) * 57.29578f * 0.5f * Mathf.Clamp01(this.m_tiltLeftRightTimer);
			}
			if (this.m_tiltUpDownTimer > 0f)
			{
				this.CenterAngle += (float)((!this.Sein.FaceLeft) ? 1 : -1) * Mathf.Atan2(this.PlatformMovement.LocalSpeedY, 12f) * 57.29578f * 0.5f * Mathf.Clamp01(this.m_tiltUpDownTimer);
			}
		}
		if (this.Sein.Abilities.StandingOnEdge && this.Sein.Abilities.StandingOnEdge.StandingOnEdge)
		{
			this.FeetAngle = this.PlatformMovement.GravityAngle;
		}
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x0006836C File Offset: 0x0006656C
	public override void UpdateCharacterState()
	{
		if (this.CinematicRotation)
		{
			this.UpdateCinematicRotation();
		}
		else if (this.Sein.Controller.IsDashing)
		{
			this.UpdateDashingRotation();
		}
		else if (this.Sein.Controller.IsSwimming && this.Sein.Abilities.Swimming.IsUnderwater && !this.Sein.Controller.IsBashing && !this.Sein.Controller.IsStomping)
		{
			this.UpdateUnderwaterRotation();
		}
		else
		{
			this.UpdateRegularRotation();
		}
		this.UpdateRotation();
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x00068420 File Offset: 0x00066620
	public void UpdateDashingRotation()
	{
		this.FeetAngle = (this.HeadAngle = (this.CenterAngle = 0f));
		if (this.Sein.IsOnGround)
		{
			this.FeetAngle = this.Sein.Abilities.Dash.SpriteRotation;
		}
		else
		{
			this.CenterAngle = this.Sein.Abilities.Dash.SpriteRotation;
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x00068498 File Offset: 0x00066698
	public void UpdateRotation()
	{
		if (this.FeetTransform)
		{
			this.FeetTransform.eulerAngles = new Vector3(0f, 0f, this.FeetAngle);
		}
		if (this.HeadTransform)
		{
			this.HeadTransform.localEulerAngles = new Vector3(0f, 0f, this.HeadAngle);
		}
		if (this.CenterTransform)
		{
			this.CenterTransform.localEulerAngles = new Vector3(0f, 0f, this.CenterAngle);
		}
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x00068538 File Offset: 0x00066738
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.FeetAngle);
		ar.Serialize(ref this.CenterAngle);
		ar.Serialize(ref this.m_ceilingAngle);
		ar.Serialize(ref this.m_groundAngle);
		ar.Serialize(ref this.m_localPosition);
		ar.Serialize(ref this.m_wallLeftAngle);
		ar.Serialize(ref this.m_wallRightAngle);
		if (ar.Reading)
		{
			this.UpdateRotation();
		}
	}

	// Token: 0x040014CF RID: 5327
	public Transform FeetTransform;

	// Token: 0x040014D0 RID: 5328
	public Transform HeadTransform;

	// Token: 0x040014D1 RID: 5329
	public Transform CenterTransform;

	// Token: 0x040014D2 RID: 5330
	public bool CinematicRotation;

	// Token: 0x040014D3 RID: 5331
	public float FeetAngle;

	// Token: 0x040014D4 RID: 5332
	public float HeadAngle;

	// Token: 0x040014D5 RID: 5333
	public float CenterAngle;

	// Token: 0x040014D6 RID: 5334
	public SeinCharacter Sein;

	// Token: 0x040014D7 RID: 5335
	private float m_ceilingAngle;

	// Token: 0x040014D8 RID: 5336
	private float m_groundAngle;

	// Token: 0x040014D9 RID: 5337
	private Vector2 m_localPosition;

	// Token: 0x040014DA RID: 5338
	private float m_wallLeftAngle;

	// Token: 0x040014DB RID: 5339
	private float m_wallRightAngle;

	// Token: 0x040014DC RID: 5340
	private float m_tiltLeftRightTimer;

	// Token: 0x040014DD RID: 5341
	private float m_tiltUpDownTimer;
}
