using System;

// Token: 0x020009D6 RID: 2518
public class MistyWoodsKuroController : SaveSerialize, ISuspendable
{
	// Token: 0x1700087D RID: 2173
	// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000E5FC3 File Offset: 0x000E41C3
	public bool IsHunting
	{
		get
		{
			return this.CurrentState == MistyWoodsKuroController.State.Hunting;
		}
	}

	// Token: 0x060036CA RID: 14026 RVA: 0x000E5FD0 File Offset: 0x000E41D0
	public new void Awake()
	{
		SuspensionManager.Register(this);
		this.KuroAnimator.OnAnimationEndEvent += this.OnAnimationEnded;
		this.KuroAnimator.OnAnimationLoopEvent += this.PlayIdleSound;
		if (this.IdleSound)
		{
			this.IdleSound.Play();
		}
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x000E602C File Offset: 0x000E422C
	public void OnAnimationEnded(TextureAnimation animation)
	{
		if (animation == this.KuroAnimations.Notice.Animation)
		{
			this.ChangeState(MistyWoodsKuroController.State.Hunting);
		}
		if (animation == this.KuroAnimations.Returning.Animation)
		{
			if (this.GameplayController.IsHidden)
			{
				this.ChangeState(MistyWoodsKuroController.State.Hidden);
			}
			else
			{
				this.ChangeState(MistyWoodsKuroController.State.Visible);
			}
		}
	}

	// Token: 0x060036CC RID: 14028 RVA: 0x000E6099 File Offset: 0x000E4299
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060036CD RID: 14029 RVA: 0x000E60A7 File Offset: 0x000E42A7
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.UpdateState();
	}

	// Token: 0x060036CE RID: 14030 RVA: 0x000E60BC File Offset: 0x000E42BC
	public void ChangeState(MistyWoodsKuroController.State state)
	{
		switch (this.CurrentState)
		{
		case MistyWoodsKuroController.State.Hidden:
			if (this.IdleSound)
			{
				this.IdleSound.Stop();
			}
			break;
		}
		this.CurrentState = state;
		switch (this.CurrentState)
		{
		case MistyWoodsKuroController.State.Hidden:
			this.KuroAnimator.SetAnimation(this.KuroAnimations.Idle, false);
			break;
		case MistyWoodsKuroController.State.Visible:
			this.KuroAnimator.SetAnimation(this.KuroAnimations.Notice, false);
			break;
		case MistyWoodsKuroController.State.Hunting:
			this.KuroAnimator.SetAnimation(this.KuroAnimations.FlyUp, false);
			break;
		case MistyWoodsKuroController.State.Returning:
			this.KuroAnimator.SetAnimation(this.KuroAnimations.Returning, false);
			if (this.ReturningSound)
			{
				this.ReturningSound.Play();
			}
			break;
		}
	}

	// Token: 0x060036CF RID: 14031 RVA: 0x000E61F0 File Offset: 0x000E43F0
	public void PlayIdleSound(TextureAnimation notUsed)
	{
		if (this.CurrentState == MistyWoodsKuroController.State.Hidden && this.IdleSound)
		{
			this.IdleSound.Play();
		}
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x000E6223 File Offset: 0x000E4423
	public void UpdateState()
	{
	}

	// Token: 0x060036D1 RID: 14033 RVA: 0x000E6225 File Offset: 0x000E4425
	public void OnHide()
	{
		this.DebugLog("OnHide");
		if (this.CurrentState == MistyWoodsKuroController.State.Visible)
		{
			this.ChangeState(MistyWoodsKuroController.State.Hidden);
		}
		if (this.CurrentState == MistyWoodsKuroController.State.Hunting)
		{
			this.ChangeState(MistyWoodsKuroController.State.Returning);
		}
	}

	// Token: 0x060036D2 RID: 14034 RVA: 0x000E6258 File Offset: 0x000E4458
	public void OnVisible()
	{
		this.DebugLog("OnVisible");
		if (this.CurrentState == MistyWoodsKuroController.State.Hidden)
		{
			this.ChangeState(MistyWoodsKuroController.State.Visible);
		}
		if (this.CurrentState == MistyWoodsKuroController.State.Hunting)
		{
			this.ChangeState(MistyWoodsKuroController.State.Returning);
		}
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x000E6295 File Offset: 0x000E4495
	public void KillPlayer()
	{
		this.DebugLog("KillPlayer");
		this.ChangeState(MistyWoodsKuroController.State.KillPlayer);
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x000E62AC File Offset: 0x000E44AC
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.ChangeState((MistyWoodsKuroController.State)ar.Serialize(0));
		}
		else
		{
			ar.Serialize((int)this.CurrentState);
		}
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x000E62E3 File Offset: 0x000E44E3
	private void DebugLog(string str)
	{
	}

	// Token: 0x1700087E RID: 2174
	// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000E62E5 File Offset: 0x000E44E5
	// (set) Token: 0x060036D7 RID: 14039 RVA: 0x000E62ED File Offset: 0x000E44ED
	public bool IsSuspended { get; set; }

	// Token: 0x040031AF RID: 12719
	public MistyWoodsKuroGameplayController GameplayController;

	// Token: 0x040031B0 RID: 12720
	public SpriteAnimatorWithTransitions KuroAnimator;

	// Token: 0x040031B1 RID: 12721
	public MistyWoodsKuroController.Animations KuroAnimations;

	// Token: 0x040031B2 RID: 12722
	public SoundSource IdleSound;

	// Token: 0x040031B3 RID: 12723
	public SoundSource ReturningSound;

	// Token: 0x040031B4 RID: 12724
	public MistyWoodsKuroController.State CurrentState;

	// Token: 0x020009D7 RID: 2519
	[Serializable]
	public class Animations
	{
		// Token: 0x040031B6 RID: 12726
		public TextureAnimationWithTransitions Idle;

		// Token: 0x040031B7 RID: 12727
		public TextureAnimationWithTransitions Notice;

		// Token: 0x040031B8 RID: 12728
		public TextureAnimationWithTransitions FlyUp;

		// Token: 0x040031B9 RID: 12729
		public TextureAnimationWithTransitions Returning;
	}

	// Token: 0x020009D8 RID: 2520
	public enum State
	{
		// Token: 0x040031BB RID: 12731
		Hidden,
		// Token: 0x040031BC RID: 12732
		Visible,
		// Token: 0x040031BD RID: 12733
		Hunting,
		// Token: 0x040031BE RID: 12734
		KillPlayer,
		// Token: 0x040031BF RID: 12735
		Returning,
		// Token: 0x040031C0 RID: 12736
		SequenceFinished
	}
}
