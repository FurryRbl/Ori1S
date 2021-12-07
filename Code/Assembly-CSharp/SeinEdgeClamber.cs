using System;
using Core;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class SeinEdgeClamber : CharacterState, ISeinReceiver
{
	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x06001E5C RID: 7772 RVA: 0x00085FE5 File Offset: 0x000841E5
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00085FF7 File Offset: 0x000841F7
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x00086009 File Offset: 0x00084209
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.EdgeClamber = this;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x00086024 File Offset: 0x00084224
	public override void UpdateCharacterState()
	{
		if (!base.Active)
		{
			return;
		}
		if (this.m_isEdgeClambering)
		{
			if (!this.PlatformMovement.IsOnWall)
			{
				this.m_isEdgeClambering = false;
			}
		}
		else if (this.PlatformMovement.IsOnWall && !this.PlatformMovement.HeadAgainstWall && this.PlatformMovement.FeetAgainstWall && ((this.PlatformMovement.HasWallLeft && this.Sein.Input.NormalizedHorizontal < 0) || (this.PlatformMovement.HasWallRight && this.Sein.Input.NormalizedHorizontal > 0)) && this.PlatformMovement.LocalSpeedY > 0f)
		{
			if (this.PlatformMovement.HasWallLeft && this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.WallLeftCollider && this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.WallLeftCollider.GetComponent<NonEdgeClamberble>())
			{
				return;
			}
			if (this.PlatformMovement.HasWallRight && this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.WallRightCollider && this.Sein.PlatformBehaviour.PlatformMovementListOfColliders.WallRightCollider.GetComponent<NonEdgeClamberble>())
			{
				return;
			}
			this.PerformEdgeClamber();
		}
		base.UpdateCharacterState();
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x000861AC File Offset: 0x000843AC
	public void PerformEdgeClamber()
	{
		this.Sein.PlatformBehaviour.Visuals.Animation.Play(this.EdgeClamberAnimation, 10, new Func<bool>(this.ShouldAnimationKeepPlaying));
		this.m_isEdgeClambering = true;
		if (this.PlatformMovement.LocalSpeedY < 9f)
		{
			this.PlatformMovement.LocalSpeedY = 9f;
		}
		if (this.PlatformMovement.HasWallLeft)
		{
			this.PlatformMovement.LocalSpeedX = Mathf.Min(this.PlatformMovement.LocalSpeedX, this.Sein.PlatformBehaviour.LeftRightMovement.Settings.Ground.MaxSpeed * -0.65f);
		}
		else
		{
			this.PlatformMovement.LocalSpeedX = Mathf.Max(this.PlatformMovement.LocalSpeedX, this.Sein.PlatformBehaviour.LeftRightMovement.Settings.Ground.MaxSpeed * 0.65f);
		}
		if (this.EdgeClamberSound)
		{
			Sound.Play(this.EdgeClamberSound.GetSound(null), base.transform.position, null);
		}
		this.Sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x000862ED File Offset: 0x000844ED
	public bool ShouldAnimationKeepPlaying()
	{
		return !this.PlatformMovement.IsOnGround;
	}

	// Token: 0x04001A1C RID: 6684
	public TextureAnimationWithTransitions EdgeClamberAnimation;

	// Token: 0x04001A1D RID: 6685
	public SoundProvider EdgeClamberSound;

	// Token: 0x04001A1E RID: 6686
	public SeinCharacter Sein;

	// Token: 0x04001A1F RID: 6687
	private bool m_isEdgeClambering;
}
