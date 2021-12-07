using System;
using System.Collections.Generic;
using Core;
using Game;

// Token: 0x020000DF RID: 223
public class PressACutsceneState : NormalBackwardsCutsceneState
{
	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000906 RID: 2310 RVA: 0x00026CDF File Offset: 0x00024EDF
	public CharacterAnimationSystem Animation
	{
		get
		{
			return Characters.Sein.PlatformBehaviour.Visuals.Animation;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000907 RID: 2311 RVA: 0x00026CF5 File Offset: 0x00024EF5
	public SeinCutsceneBlocked CutsceneBlocked
	{
		get
		{
			return Characters.Sein.CutsceneBlocked;
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00026D04 File Offset: 0x00024F04
	public override void OnEnter()
	{
		base.OnEnter();
		this.Animation.PlayLoop(this.Normal, 210, null, false);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00026D30 File Offset: 0x00024F30
	public override void OnUpdate()
	{
		if (Characters.Sein.Controller.InputLocked)
		{
			return;
		}
		if (this.CutsceneBlocked.IsNormal && !this.CutsceneBlocked.IsTransitionPlaying)
		{
			foreach (Input.Button button in this.OnPressButtonList)
			{
				if (Input.GetButton(button).OnPressed)
				{
					this.Parent.ChangeState(this.Next);
					return;
				}
			}
			if (Input.NormalizedHorizontal > 0)
			{
				this.Animation.PlayLoop(this.Backwards, 210, null, false);
				this.CutsceneBlocked.Backwards();
			}
		}
		if (this.CutsceneBlocked.IsBackwards && Input.NormalizedHorizontal <= 0)
		{
			this.Animation.PlayLoop(this.Normal, 210, null, false);
			this.CutsceneBlocked.Normal();
		}
	}

	// Token: 0x04000745 RID: 1861
	public List<Input.Button> OnPressButtonList = new List<Input.Button>
	{
		Input.Button.ButtonA
	};
}
