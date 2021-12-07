using System;
using Game;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class NormalBackwardsCutsceneState : CutsceneState
{
	// Token: 0x060008D8 RID: 2264 RVA: 0x000261E8 File Offset: 0x000243E8
	public override void OnEnter()
	{
		Characters.Sein.CutsceneBlocked.Enter();
		if (this.CutsceneMusicPlayer)
		{
			CutsceneMusicPlayer cutsceneMusicPlayer = UnityEngine.Object.Instantiate<CutsceneMusicPlayer>(this.CutsceneMusicPlayer);
			cutsceneMusicPlayer.Cutscene = this.Parent;
			cutsceneMusicPlayer.Play(false);
		}
		if (this.ActionOnEnter)
		{
			this.ActionOnEnter.Perform(null);
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0002624F File Offset: 0x0002444F
	public override void OnExit()
	{
		Characters.Sein.CutsceneBlocked.Exit();
		if (this.ActionOnExit)
		{
			this.ActionOnExit.Perform(null);
		}
	}

	// Token: 0x04000728 RID: 1832
	public TextureAnimationWithTransitions Normal;

	// Token: 0x04000729 RID: 1833
	public TextureAnimationWithTransitions Backwards;

	// Token: 0x0400072A RID: 1834
	public CutsceneState Next;

	// Token: 0x0400072B RID: 1835
	public ActionMethod ActionOnEnter;

	// Token: 0x0400072C RID: 1836
	public ActionMethod ActionOnExit;

	// Token: 0x0400072D RID: 1837
	public CutsceneMusicPlayer CutsceneMusicPlayer;
}
