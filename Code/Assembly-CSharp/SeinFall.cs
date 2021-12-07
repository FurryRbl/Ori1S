using System;
using Core;
using UnityEngine;

// Token: 0x02000444 RID: 1092
public class SeinFall : CharacterState, ISeinReceiver
{
	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0008632E File Offset: 0x0008452E
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00086340 File Offset: 0x00084540
	public bool ShouldFallMovingAnimationPlay
	{
		get
		{
			return this.ShouldFallMovingAnimationKeepPlaying();
		}
	}

	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x06001E66 RID: 7782 RVA: 0x00086348 File Offset: 0x00084548
	public bool ShouldFallIdleAnimationPlay
	{
		get
		{
			return this.ShouldFallIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00086350 File Offset: 0x00084550
	// (set) Token: 0x06001E68 RID: 7784 RVA: 0x00086358 File Offset: 0x00084558
	public bool IsSuspended { get; set; }

	// Token: 0x06001E69 RID: 7785 RVA: 0x00086361 File Offset: 0x00084561
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Fall = this;
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0008637B File Offset: 0x0008457B
	public void Start()
	{
		this.PlatformMovement.OnLandOnGroundEvent += this.HandleLandOnGround;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x00086394 File Offset: 0x00084594
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.PlatformMovement.OnLandOnGroundEvent -= this.HandleLandOnGround;
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000863B4 File Offset: 0x000845B4
	public bool ShouldFallMovingAnimationKeepPlaying()
	{
		return this.Sein.Controller.CanMove && this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput != 0f && !this.PlatformMovement.IsOnWall && !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x00086418 File Offset: 0x00084618
	public bool ShouldFallIdleAnimationKeepPlaying()
	{
		return (!this.Sein.Controller.CanMove || this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput == 0f || this.PlatformMovement.IsOnWall) && !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x0008647C File Offset: 0x0008467C
	public override void UpdateCharacterState()
	{
		if (this.ShouldFallMovingAnimationPlay)
		{
			if (this.s_shouldFallKeepPlaying == null)
			{
				this.s_shouldFallKeepPlaying = new Func<bool>(this.ShouldFallMovingAnimationKeepPlaying);
			}
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.MovingAnimation, 0, this.s_shouldFallKeepPlaying, false);
		}
		if (this.ShouldFallIdleAnimationPlay)
		{
			if (this.s_shouldFallIdleKeepPlaying == null)
			{
				this.s_shouldFallIdleKeepPlaying = new Func<bool>(this.ShouldFallIdleAnimationKeepPlaying);
			}
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.IdleAnimation, 0, this.s_shouldFallIdleKeepPlaying, false);
		}
		if (this.m_ignoreLandTime > 0f)
		{
			this.m_ignoreLandTime -= Time.deltaTime;
			if (this.m_ignoreLandTime < 0f)
			{
				this.m_ignoreLandTime = 0f;
			}
		}
		this.HandleFalling();
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x00086570 File Offset: 0x00084770
	public void HandleFalling()
	{
		if (this.PlatformMovement.Falling && this.PlatformMovement.LocalSpeedY < -this.FallingSpeedForSound)
		{
			if (!this.m_hasPlayedFallSound)
			{
				SoundDescriptor sound = this.FallingSound.GetSound(null);
				if (sound != null)
				{
					Sound.Play(sound, this.PlatformMovement.Position, null);
					this.m_hasPlayedFallSound = true;
				}
			}
		}
		else
		{
			this.m_hasPlayedFallSound = false;
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x000865E8 File Offset: 0x000847E8
	public void HandleLandOnGround(Vector3 normal, Collider target)
	{
		if (this.PlatformMovement.LocalSpeedY > 0f)
		{
			return;
		}
		if (this.m_ignoreLandTime > 0f)
		{
			return;
		}
		Sound.Play(this.LandingSound.GetSoundForMaterial(SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider), null), this.PlatformMovement.Position, null);
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.LandingParticleEffect, this.Sein.PlatformBehaviour.PlatformMovement.FeetPosition, Quaternion.identity);
		gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeed));
		Vector2 v = this.Sein.PlatformBehaviour.PlatformMovement.WorldSpeed * this.GroundLandImpulsePerUnitsOfSpeed;
		if (v.magnitude > this.GroundLandMaxImpulse)
		{
			v = v.normalized * this.GroundLandMaxImpulse;
		}
		if (this.Sein.PlatformBehaviour.Force)
		{
			this.Sein.PlatformBehaviour.Force.ApplyGroundForce(v, ForceMode.Impulse);
		}
		this.m_ignoreLandTime = 0.3f;
	}

	// Token: 0x04001A20 RID: 6688
	public SoundProvider FallingSound;

	// Token: 0x04001A21 RID: 6689
	public float FallingSpeedForSound;

	// Token: 0x04001A22 RID: 6690
	public float GroundLandImpulsePerUnitsOfSpeed = 20f;

	// Token: 0x04001A23 RID: 6691
	public float GroundLandMaxImpulse = 100f;

	// Token: 0x04001A24 RID: 6692
	public TextureAnimationWithTransitions IdleAnimation;

	// Token: 0x04001A25 RID: 6693
	public GameObject LandingParticleEffect;

	// Token: 0x04001A26 RID: 6694
	public SurfaceToSoundProviderMap LandingSound;

	// Token: 0x04001A27 RID: 6695
	public TextureAnimationWithTransitions MovingAnimation;

	// Token: 0x04001A28 RID: 6696
	public SeinCharacter Sein;

	// Token: 0x04001A29 RID: 6697
	private bool m_hasPlayedFallSound;

	// Token: 0x04001A2A RID: 6698
	private float m_ignoreLandTime = 0.5f;

	// Token: 0x04001A2B RID: 6699
	private Func<bool> s_shouldFallKeepPlaying;

	// Token: 0x04001A2C RID: 6700
	private Func<bool> s_shouldFallIdleKeepPlaying;
}
