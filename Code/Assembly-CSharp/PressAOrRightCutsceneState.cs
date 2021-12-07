using System;
using System.Collections.Generic;
using Core;
using Game;

// Token: 0x020000E0 RID: 224
public class PressAOrRightCutsceneState : NormalBackwardsCutsceneState
{
	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x00026E73 File Offset: 0x00025073
	public CharacterAnimationSystem Animation
	{
		get
		{
			return Characters.Sein.PlatformBehaviour.Visuals.Animation;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x0600090C RID: 2316 RVA: 0x00026E89 File Offset: 0x00025089
	public SeinCutsceneBlocked CutsceneBlocked
	{
		get
		{
			return Characters.Sein.CutsceneBlocked;
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00026E98 File Offset: 0x00025098
	public override void OnEnter()
	{
		base.OnEnter();
		this.Animation.PlayLoop(this.Normal, 210, null, false);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00026EC4 File Offset: 0x000250C4
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
				if (Input.GetButton(button).OnPressed || Input.Left.OnPressed)
				{
					this.Parent.ChangeState(this.Next);
					break;
				}
			}
			if (this.OnPressButtonList.Count == 0)
			{
				this.Parent.ChangeState(this.Next);
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

	// Token: 0x04000746 RID: 1862
	public List<Input.Button> OnPressButtonList = new List<Input.Button>
	{
		Input.Button.ButtonA
	};
}
