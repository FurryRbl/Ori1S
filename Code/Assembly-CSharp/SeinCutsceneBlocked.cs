using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class SeinCutsceneBlocked : CharacterState, ISeinReceiver
{
	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x060008DB RID: 2267 RVA: 0x00026284 File Offset: 0x00024484
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00026296 File Offset: 0x00024496
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0002629F File Offset: 0x0002449F
	public void ChangeState(SeinCutsceneBlocked.State state)
	{
		this.CurrentState = state;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x000262A8 File Offset: 0x000244A8
	public void Normal()
	{
		this.ChangeState(SeinCutsceneBlocked.State.Normal);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x000262B1 File Offset: 0x000244B1
	public void Backwards()
	{
		this.ChangeState(SeinCutsceneBlocked.State.Backwards);
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000262BA File Offset: 0x000244BA
	public bool IsNormal
	{
		get
		{
			return this.CurrentState == SeinCutsceneBlocked.State.Normal;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000262C5 File Offset: 0x000244C5
	public bool IsBackwards
	{
		get
		{
			return this.CurrentState == SeinCutsceneBlocked.State.Backwards;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000262D0 File Offset: 0x000244D0
	public bool IsTransitionPlaying
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.Animation.Animator.IsTransitionPlaying;
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x000262F4 File Offset: 0x000244F4
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
			Vector2 speedAtTime = currentAnimation.AnimationMetaData.CameraData.GetSpeedAtTime(animator.CurrentAnimationTime);
			this.PlatformMovement.LocalSpeedX = speedAtTime.x * (float)((!this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft) ? 1 : -1) * this.Sein.PlatformBehaviour.Visuals.Sprite.transform.localScale.x;
			this.PlatformMovement.LocalSpeedY = speedAtTime.y * this.Sein.PlatformBehaviour.Visuals.Sprite.transform.localScale.y;
		}
		else
		{
			this.PlatformMovement.LocalSpeed = Vector3.zero;
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00026438 File Offset: 0x00024638
	public override void OnEnter()
	{
		base.OnEnter();
		this.PlatformMovement.KinematicMode = true;
		this.Sein.PlatformBehaviour.CapsuleController.EnableCollider(false);
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00026470 File Offset: 0x00024670
	public override void OnExit()
	{
		base.OnExit();
		this.PlatformMovement.KinematicMode = false;
		this.Sein.PlatformBehaviour.CapsuleController.EnableCollider(true);
	}

	// Token: 0x0400072E RID: 1838
	public SeinCharacter Sein;

	// Token: 0x0400072F RID: 1839
	public SeinCutsceneBlocked.State CurrentState;

	// Token: 0x020000DB RID: 219
	public enum State
	{
		// Token: 0x0400073A RID: 1850
		Normal,
		// Token: 0x0400073B RID: 1851
		Backwards
	}
}
