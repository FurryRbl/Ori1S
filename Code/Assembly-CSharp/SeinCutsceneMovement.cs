using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class SeinCutsceneMovement : CharacterState, ISeinReceiver
{
	// Token: 0x170001EC RID: 492
	// (get) Token: 0x060008EC RID: 2284 RVA: 0x000267AA File Offset: 0x000249AA
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000267BC File Offset: 0x000249BC
	public void FixedUpdate()
	{
		if (this.Sein.IsSuspended)
		{
			return;
		}
		if (!base.Active)
		{
			return;
		}
		SpriteAnimatorWithTransitions animator = this.Sein.PlatformBehaviour.Visuals.Animation.Animator;
		TextureAnimation currentAnimation = this.Sein.PlatformBehaviour.Visuals.Animation.Animator.CurrentAnimation;
		if (currentAnimation.AnimationMetaData)
		{
			Vector2 vector = animator.CurrentAnimation.Speed / 30f * currentAnimation.AnimationMetaData.CameraData.GetSpeedAtTime(animator.CurrentAnimationTime);
			this.PlatformMovement.LocalSpeedX = vector.x * (float)((!this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft) ? 1 : -1) * this.Sein.PlatformBehaviour.Visuals.Sprite.transform.localScale.x;
		}
		else
		{
			this.PlatformMovement.LocalSpeedX = 0f;
		}
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000268D5 File Offset: 0x00024AD5
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x000268DE File Offset: 0x00024ADE
	public override void OnEnter()
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x000268E0 File Offset: 0x00024AE0
	public override void OnExit()
	{
	}

	// Token: 0x04000735 RID: 1845
	public SeinCharacter Sein;
}
