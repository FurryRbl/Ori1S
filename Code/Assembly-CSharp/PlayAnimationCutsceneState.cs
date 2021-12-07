using System;
using Game;

// Token: 0x020000DD RID: 221
public class PlayAnimationCutsceneState : CutsceneState
{
	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000901 RID: 2305 RVA: 0x00026B51 File Offset: 0x00024D51
	public SeinCharacter Sein
	{
		get
		{
			return Characters.Sein;
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00026B58 File Offset: 0x00024D58
	public override void OnEnter()
	{
		if (this.IgnoreCollisions)
		{
			Characters.Sein.CutsceneBlocked.Enter();
		}
		else
		{
			Characters.Sein.CutsceneMovement.Enter();
		}
		if (this.Loop)
		{
			this.Sein.Animation.PlayLoop(this.AnimationToPlay, 210, null, false);
		}
		else
		{
			this.Sein.Animation.Play(this.AnimationToPlay, 210, null);
		}
		if (this.Facing == PlayAnimationCutsceneState.FacingMode.FaceLeft)
		{
			this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = false;
		}
		if (this.Facing == PlayAnimationCutsceneState.FacingMode.FaceRight)
		{
			this.Sein.PlatformBehaviour.Visuals.SpriteMirror.FaceLeft = true;
		}
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00026C2C File Offset: 0x00024E2C
	public override void OnExit()
	{
		if (this.IgnoreCollisions)
		{
			Characters.Sein.CutsceneBlocked.Exit();
		}
		else
		{
			Characters.Sein.CutsceneMovement.Exit();
		}
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00026C68 File Offset: 0x00024E68
	public override void OnUpdate()
	{
		if (this.Sein.Animation.Animator.AnimationEnded && this.StateWhenAnimationFinished)
		{
			this.Parent.ChangeState(this.StateWhenAnimationFinished);
		}
		base.OnUpdate();
	}

	// Token: 0x0400073C RID: 1852
	public TextureAnimationWithTransitions AnimationToPlay;

	// Token: 0x0400073D RID: 1853
	public bool Loop;

	// Token: 0x0400073E RID: 1854
	public PlayAnimationCutsceneState.FacingMode Facing;

	// Token: 0x0400073F RID: 1855
	public CutsceneState StateWhenAnimationFinished;

	// Token: 0x04000740 RID: 1856
	public bool IgnoreCollisions;

	// Token: 0x020000DE RID: 222
	public enum FacingMode
	{
		// Token: 0x04000742 RID: 1858
		DontChange,
		// Token: 0x04000743 RID: 1859
		FaceRight,
		// Token: 0x04000744 RID: 1860
		FaceLeft
	}
}
