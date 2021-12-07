using System;
using Core;

// Token: 0x02000354 RID: 852
public class StopAllSoundsAction : ActionMethod
{
	// Token: 0x06001856 RID: 6230 RVA: 0x000686EE File Offset: 0x000668EE
	public override void Perform(IContext context)
	{
		Core.SoundComposition.Manager.StopMusic();
		SoundPlayer.DestroyAll();
	}
}
