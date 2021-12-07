using System;
using Core;

// Token: 0x02000315 RID: 789
public class PreloadSceneAction : ActionMethod
{
	// Token: 0x06001756 RID: 5974 RVA: 0x00064D63 File Offset: 0x00062F63
	public override void Perform(IContext context)
	{
		Scenes.Manager.PreloadScene(this.Scene);
	}

	// Token: 0x0400140E RID: 5134
	[NotNull]
	public SceneMetaData Scene;
}
