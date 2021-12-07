using System;

// Token: 0x020004F5 RID: 1269
public class SpriteEntity : Entity
{
	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x06002233 RID: 8755 RVA: 0x00096459 File Offset: 0x00094659
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Animation.SpriteMirror;
		}
	}

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x06002234 RID: 8756 RVA: 0x00096466 File Offset: 0x00094666
	public SpriteAnimatorWithTransitions SpriteAnimator
	{
		get
		{
			return this.Animation.Animator;
		}
	}

	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x06002235 RID: 8757 RVA: 0x00096473 File Offset: 0x00094673
	// (set) Token: 0x06002236 RID: 8758 RVA: 0x00096480 File Offset: 0x00094680
	public bool FaceLeft
	{
		get
		{
			return this.SpriteMirror.FaceLeft;
		}
		set
		{
			this.SpriteMirror.FaceLeft = value;
		}
	}

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x06002237 RID: 8759 RVA: 0x0009648E File Offset: 0x0009468E
	public int FaceLeftSign
	{
		get
		{
			return (!this.SpriteMirror.FaceLeft) ? 1 : -1;
		}
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x000964A8 File Offset: 0x000946A8
	public override void Serialize(Archive ar)
	{
		if (this.Animation)
		{
			this.FaceLeft = ar.Serialize(this.FaceLeft);
		}
		base.Serialize(ar);
	}

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x06002239 RID: 8761 RVA: 0x000964E0 File Offset: 0x000946E0
	public bool IsFacingPlayer
	{
		get
		{
			if (this.FaceLeft)
			{
				if (base.PlayerIsToLeft)
				{
					return true;
				}
			}
			else if (!base.PlayerIsToLeft)
			{
				return true;
			}
			return false;
		}
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x00096518 File Offset: 0x00094718
	public void FacePlayer()
	{
		this.FaceLeft = (base.PositionToPlayerPosition.x < 0f);
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x00096540 File Offset: 0x00094740
	public void FaceStartPosition()
	{
		this.FaceLeft = (base.PositionToStartPosition.x < 0f);
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x00096568 File Offset: 0x00094768
	public void FaceAwayFromPlayer()
	{
		this.FaceLeft = (base.PositionToPlayerPosition.x > 0f);
	}

	// Token: 0x0600223D RID: 8765 RVA: 0x00096590 File Offset: 0x00094790
	public void PlayAnimationOnce(TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && this.Animation)
		{
			this.Animation.Play(anim, layer, null);
		}
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x000965C8 File Offset: 0x000947C8
	public void RestartAnimationLoop(TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && this.Animation)
		{
			this.Animation.RestartLoop(anim, layer, null);
		}
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x000965FF File Offset: 0x000947FF
	public void PlayAnimationLoop(TextureAnimationWithTransitions anim, int layer = 0)
	{
		if (anim && this.Animation)
		{
			this.Animation.PlayLoop(anim, layer, null, false);
		}
	}

	// Token: 0x04001CC2 RID: 7362
	public CharacterAnimationSystem Animation;
}
