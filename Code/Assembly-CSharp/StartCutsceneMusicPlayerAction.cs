using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[Category("Cutscenes")]
public class StartCutsceneMusicPlayerAction : ActionMethod
{
	// Token: 0x0600092A RID: 2346 RVA: 0x0002789C File Offset: 0x00025A9C
	public override void Perform(IContext context)
	{
		CutsceneMusicPlayer cutsceneMusicPlayer = UnityEngine.Object.Instantiate<CutsceneMusicPlayer>(this.CutsceneMusicPlayer);
		cutsceneMusicPlayer.Cutscene = this.CutsceneController;
		cutsceneMusicPlayer.Play(this.PauseOnSuspend);
	}

	// Token: 0x04000764 RID: 1892
	public CutsceneMusicPlayer CutsceneMusicPlayer;

	// Token: 0x04000765 RID: 1893
	public CutsceneController CutsceneController;

	// Token: 0x04000766 RID: 1894
	public bool PauseOnSuspend;
}
