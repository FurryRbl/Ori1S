using System;
using System.Collections.Generic;
using Core;
using Game;

// Token: 0x020000D3 RID: 211
public class HoldRightCutsceneState : NormalBackwardsCutsceneState
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00026043 File Offset: 0x00024243
	public CharacterAnimationSystem Animation
	{
		get
		{
			return Characters.Sein.PlatformBehaviour.Visuals.Animation;
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00026059 File Offset: 0x00024259
	public SeinCutsceneBlocked CutsceneBlocked
	{
		get
		{
			return Characters.Sein.CutsceneBlocked;
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00026068 File Offset: 0x00024268
	public override void OnEnter()
	{
		base.OnEnter();
		this.Animation.PlayLoop(this.Normal, 210, null, false);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00026094 File Offset: 0x00024294
	public override void OnUpdate()
	{
		if (!Characters.Sein)
		{
			return;
		}
		if (Characters.Sein.Controller.InputLocked)
		{
			return;
		}
		if (this.CutsceneBlocked.IsNormal && !this.CutsceneBlocked.IsTransitionPlaying && this.Parent.CurrentStateTime > 0.5f)
		{
			foreach (Input.Button button in this.OnPressButtonList)
			{
				if (Input.GetButton(button).OnPressed || Input.NormalizedHorizontal < 0)
				{
					this.Parent.ChangeState(this.Next);
					break;
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

	// Token: 0x04000727 RID: 1831
	public List<Input.Button> OnPressButtonList = new List<Input.Button>
	{
		Input.Button.ButtonA
	};
}
