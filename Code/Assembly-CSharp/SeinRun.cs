using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class SeinRun : CharacterState, ISeinReceiver
{
	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x060017A2 RID: 6050 RVA: 0x000658D8 File Offset: 0x00063AD8
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000658EA File Offset: 0x00063AEA
	public CharacterLeftRightMovement LeftRightMovement
	{
		get
		{
			return this.Sein.PlatformBehaviour.LeftRightMovement;
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000658FC File Offset: 0x00063AFC
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x00065913 File Offset: 0x00063B13
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
		this.Sein.Abilities.Run = this;
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0006592D File Offset: 0x00063B2D
	public TextureAnimator TextureAnimator
	{
		get
		{
			return this.Sein.PlatformBehaviour.Visuals.Animation.Animator.TextureAnimator;
		}
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x0006594E File Offset: 0x00063B4E
	public override void OnExit()
	{
		this.TextureAnimator.SpeedMultiplier = 1f;
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x00065960 File Offset: 0x00063B60
	public override void UpdateCharacterState()
	{
		if (this.m_shouldRunPlay == null)
		{
			this.m_shouldRunPlay = new Func<bool>(this.ShouldRunAnimationPlaying);
		}
		if (this.m_shouldJogPlay == null)
		{
			this.m_shouldJogPlay = new Func<bool>(this.ShouldJogAnimationPlay);
		}
		if (this.m_shouldWalkPlay == null)
		{
			this.m_shouldWalkPlay = new Func<bool>(this.ShouldWalkAnimationPlay);
		}
		bool flag = this.Sein.Controller.CanMove || this.Sein.Controller.IgnoreControllerInput;
		float horizontalInput = this.Sein.PlatformBehaviour.LeftRightMovement.HorizontalInput;
		if (!flag || !this.PlatformMovement.IsOnGround || this.PlatformMovement.LocalSpeedX == 0f)
		{
			this.TextureAnimator.SpeedMultiplier = 1f;
			return;
		}
		float value = Mathf.Abs(this.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedX) / this.Sein.PlatformBehaviour.LeftRightMovement.BaseSettings.Ground.MaxSpeed;
		if (Math.Abs(horizontalInput) > this.Sein.Controller.InputSettings.JogThreshold)
		{
			this.CurrentState = SeinRun.State.Run;
			this.TextureAnimator.SpeedMultiplier = this.Sein.Controller.AnimationSpeedSettings.RunAnimationSpeed.Evaluate(Mathf.InverseLerp(this.Sein.Controller.InputSettings.JogThreshold, 1f, value));
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.RunAnimation, 8, this.m_shouldRunPlay, false);
		}
		else if (Math.Abs(horizontalInput) > this.Sein.Controller.InputSettings.WalkThreshold)
		{
			this.CurrentState = SeinRun.State.Jog;
			this.TextureAnimator.SpeedMultiplier = this.Sein.Controller.AnimationSpeedSettings.JogAnimationSpeed.Evaluate(Mathf.InverseLerp(this.Sein.Controller.InputSettings.WalkThreshold, this.Sein.Controller.InputSettings.JogThreshold, value));
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.JogAnimation, 8, this.m_shouldJogPlay, false);
		}
		else
		{
			this.CurrentState = SeinRun.State.Walk;
			this.TextureAnimator.SpeedMultiplier = this.Sein.Controller.AnimationSpeedSettings.WalkAnimationSpeed.Evaluate(Mathf.InverseLerp(0f, this.Sein.Controller.InputSettings.WalkThreshold, value));
			this.Sein.PlatformBehaviour.Visuals.Animation.PlayLoop(this.WalkAnimation, 8, this.m_shouldWalkPlay, false);
		}
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x00065C34 File Offset: 0x00063E34
	public bool ShouldRunAnimationPlaying()
	{
		return this.PlatformMovement.IsOnGround && base.Active && this.CurrentState == SeinRun.State.Run && this.PlatformMovement.LocalSpeedX != 0f;
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x00065C80 File Offset: 0x00063E80
	public bool ShouldJogAnimationPlay()
	{
		return this.PlatformMovement.IsOnGround && base.Active && this.CurrentState == SeinRun.State.Jog && this.PlatformMovement.LocalSpeedX != 0f;
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x00065CCC File Offset: 0x00063ECC
	public bool ShouldWalkAnimationPlay()
	{
		return this.PlatformMovement.IsOnGround && base.Active && this.CurrentState == SeinRun.State.Walk && this.PlatformMovement.LocalSpeedX != 0f;
	}

	// Token: 0x04001442 RID: 5186
	public TextureAnimationWithTransitions RunAnimation;

	// Token: 0x04001443 RID: 5187
	public TextureAnimationWithTransitions JogAnimation;

	// Token: 0x04001444 RID: 5188
	public TextureAnimationWithTransitions WalkAnimation;

	// Token: 0x04001445 RID: 5189
	public SeinCharacter Sein;

	// Token: 0x04001446 RID: 5190
	private float m_horizontalInputDelay;

	// Token: 0x04001447 RID: 5191
	public SeinRun.State CurrentState;

	// Token: 0x04001448 RID: 5192
	private Func<bool> m_shouldRunPlay;

	// Token: 0x04001449 RID: 5193
	private Func<bool> m_shouldJogPlay;

	// Token: 0x0400144A RID: 5194
	private Func<bool> m_shouldWalkPlay;

	// Token: 0x02000456 RID: 1110
	public enum State
	{
		// Token: 0x04001A8B RID: 6795
		Run,
		// Token: 0x04001A8C RID: 6796
		Jog,
		// Token: 0x04001A8D RID: 6797
		Walk
	}
}
