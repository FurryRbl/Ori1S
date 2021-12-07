using System;
using Core;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class SeinIdle : CharacterState, ISeinReceiver
{
	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x06001E97 RID: 7831 RVA: 0x00086D41 File Offset: 0x00084F41
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00086D53 File Offset: 0x00084F53
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00086D65 File Offset: 0x00084F65
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00086D7C File Offset: 0x00084F7C
	public TextureAnimationWithTransitions CurrentIdleAnimation
	{
		get
		{
			float num = this.Sein.PlatformBehaviour.Visuals.SpriteRotater.FeetAngle - this.PlatformMovement.GravityAngle;
			if (num > 30f)
			{
				return (!this.IsFacingLeft) ? this.IdleSlopeUpAnimation : this.IdleSlopeDownAnimation;
			}
			if (num < -30f)
			{
				return (!this.IsFacingLeft) ? this.IdleSlopeDownAnimation : this.IdleSlopeUpAnimation;
			}
			return this.IdleAnimation;
		}
	}

	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00086E08 File Offset: 0x00085008
	public int CurrentAnimationLayer
	{
		get
		{
			float value = this.Sein.PlatformBehaviour.Visuals.SpriteRotater.FeetAngle - this.PlatformMovement.GravityAngle;
			return (Math.Abs(value) <= 30f) ? 8 : 10;
		}
	}

	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00086E54 File Offset: 0x00085054
	public bool IsOnSlope
	{
		get
		{
			float num = this.Sein.PlatformBehaviour.Visuals.SpriteRotater.FeetAngle - this.PlatformMovement.GravityAngle;
			return num > 30f || num < -30f;
		}
	}

	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00086EA3 File Offset: 0x000850A3
	private bool IsFacingLeft
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft;
		}
	}

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06001E9E RID: 7838 RVA: 0x00086EBF File Offset: 0x000850BF
	public bool ShouldRunIdleAnimationPlay
	{
		get
		{
			return this.ShouldRunIdleAnimationKeepPlaying();
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x00086EC7 File Offset: 0x000850C7
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Idle = this;
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x00086EE4 File Offset: 0x000850E4
	public bool ShouldRunIdleAnimationKeepPlaying()
	{
		return this.PlatformMovement.IsOnGround && base.Active && this.PlatformMovement.LocalSpeedX == 0f;
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x00086F24 File Offset: 0x00085124
	public override void UpdateCharacterState()
	{
		if (this.ShouldRunIdleAnimationPlay)
		{
			this.m_idleTime += Time.deltaTime;
			if (!this.wasIdling)
			{
				this.m_idleTime = 0f;
				this.wasIdling = true;
			}
			if (this.m_shouldRunIdle == null)
			{
				this.m_shouldRunIdle = new Func<bool>(this.ShouldRunIdleAnimationKeepPlaying);
			}
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.CurrentIdleAnimation, this.CurrentAnimationLayer, this.m_shouldRunIdle, false);
			if (this.m_idleTime > this.YawnAnimationDelay)
			{
				this.Sein.Animation.Play(this.IdleYawnAnimation, 154, new Func<bool>(this.ShouldRunIdleAnimationKeepPlaying));
				this.m_idleTime = 0f;
				if (this.YawnSound)
				{
					Sound.Play(this.YawnSound.GetSound(null), base.transform.position, null);
				}
			}
		}
		else
		{
			this.wasIdling = false;
		}
	}

	// Token: 0x04001A69 RID: 6761
	public TextureAnimationWithTransitions IdleAnimation;

	// Token: 0x04001A6A RID: 6762
	public TextureAnimationWithTransitions IdleSlopeDownAnimation;

	// Token: 0x04001A6B RID: 6763
	public TextureAnimationWithTransitions IdleSlopeUpAnimation;

	// Token: 0x04001A6C RID: 6764
	public TextureAnimationWithTransitions IdleYawnAnimation;

	// Token: 0x04001A6D RID: 6765
	private bool wasIdling;

	// Token: 0x04001A6E RID: 6766
	private float m_idleTime;

	// Token: 0x04001A6F RID: 6767
	public float YawnAnimationDelay;

	// Token: 0x04001A70 RID: 6768
	public SoundProvider YawnSound;

	// Token: 0x04001A71 RID: 6769
	public SeinCharacter Sein;

	// Token: 0x04001A72 RID: 6770
	private Func<bool> m_shouldRunIdle;
}
