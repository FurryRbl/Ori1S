using System;

// Token: 0x02000455 RID: 1109
public class SeinPushAgainstWall : CharacterState, ISeinReceiver
{
	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00087624 File Offset: 0x00085824
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00087636 File Offset: 0x00085836
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00087648 File Offset: 0x00085848
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x06001EBB RID: 7867 RVA: 0x0008765F File Offset: 0x0008585F
	public bool ShouldPushAgainstWallAnimationPlay
	{
		get
		{
			return this.ShouldPushAgainstWallAnimationKeepPlaying();
		}
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x00087667 File Offset: 0x00085867
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.PushAgainstWall = this;
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x00087681 File Offset: 0x00085881
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x0008768C File Offset: 0x0008588C
	public override void UpdateCharacterState()
	{
		if (base.Active && this.ShouldPushAgainstWallAnimationPlay)
		{
			if (this.m_shouldPushKeepPlaying == null)
			{
				this.m_shouldPushKeepPlaying = new Func<bool>(this.ShouldPushAgainstWallAnimationKeepPlaying);
			}
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.PushAgainstWallAnimation, 27, this.m_shouldPushKeepPlaying, false);
		}
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x000876F8 File Offset: 0x000858F8
	public bool ShouldPushAgainstWallAnimationKeepPlaying()
	{
		if (this.Sein.Abilities.GrabBlock.IsGrabbing)
		{
			return true;
		}
		if (this.PlatformMovement.IsOnGround && this.PlatformMovement.GroundRayHit)
		{
			if (this.PlatformMovement.HasWallLeft && this.SpriteMirror.FaceLeft)
			{
				return true;
			}
			if (this.PlatformMovement.HasWallRight && !this.SpriteMirror.FaceLeft)
			{
				return true;
			}
			if (this.PlatformMovement.IsOnCeiling)
			{
				if (this.PlatformMovement.CeilingNormal.x > 0f && this.SpriteMirror.FaceLeft)
				{
					return true;
				}
				if (this.PlatformMovement.CeilingNormal.x < 0f && !this.SpriteMirror.FaceLeft)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04001A87 RID: 6791
	public TextureAnimationWithTransitions PushAgainstWallAnimation;

	// Token: 0x04001A88 RID: 6792
	public SeinCharacter Sein;

	// Token: 0x04001A89 RID: 6793
	private Func<bool> m_shouldPushKeepPlaying;
}
