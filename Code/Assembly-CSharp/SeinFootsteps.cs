using System;
using Core;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class SeinFootsteps : CharacterState, ISeinReceiver
{
	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00086754 File Offset: 0x00084954
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00086766 File Offset: 0x00084966
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00086778 File Offset: 0x00084978
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0008678F File Offset: 0x0008498F
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Footsteps = this;
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x000867A9 File Offset: 0x000849A9
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x000867B4 File Offset: 0x000849B4
	public override void UpdateCharacterState()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (base.Active)
		{
			this.HandleFootstepEvents();
		}
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x000867E4 File Offset: 0x000849E4
	public void HandleFootstepEvents()
	{
		if (this.PlatformMovement.IsOnGround && !this.PlatformMovement.HasWallLeft && !this.PlatformMovement.HasWallRight && !this.PlatformMovement.IsOnCeiling && this.PlatformMovement.MovingHorizontally && this.ShouldPlayFootstepSounds)
		{
			this.m_nextStepTime -= Time.deltaTime * this.SoundsPerSecondOverSpeed.Evaluate(Mathf.Abs(this.PlatformMovement.LocalSpeedX));
			if (this.m_nextStepTime < 0f)
			{
				float time = Mathf.Abs(this.PlatformMovement.LocalSpeedX);
				SoundDescriptor soundForMaterial = this.FootstepsSounds.GetSoundForMaterial(this.Sein.PlatformBehaviour.GroundSurfaceMaterialType, null);
				soundForMaterial.Volume *= this.FootstepVolumeOverSpeed.Evaluate(time);
				Sound.Play(soundForMaterial, this.PlatformMovement.Position, null);
				this.m_nextStepTime = 1f;
			}
		}
		else
		{
			this.m_nextStepTime = 0f;
		}
	}

	// Token: 0x04001A2E RID: 6702
	public GameObject DustParticlesPrefab;

	// Token: 0x04001A2F RID: 6703
	public SurfaceToSoundProviderMap FootstepsSounds;

	// Token: 0x04001A30 RID: 6704
	public SeinCharacter Sein;

	// Token: 0x04001A31 RID: 6705
	public bool ShouldPlayFootstepSounds = true;

	// Token: 0x04001A32 RID: 6706
	public AnimationCurve SoundsPerSecondOverSpeed;

	// Token: 0x04001A33 RID: 6707
	public float MinSpeedForFootsteps = 10f;

	// Token: 0x04001A34 RID: 6708
	public AnimationCurve FootstepVolumeOverSpeed;

	// Token: 0x04001A35 RID: 6709
	private float m_nextStepTime;
}
