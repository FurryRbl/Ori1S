using System;
using Core;

// Token: 0x02000660 RID: 1632
public class ClearScenesLoadedForCheckpointAction : ActionMethod
{
	// Token: 0x060027CA RID: 10186 RVA: 0x000ACED7 File Offset: 0x000AB0D7
	public override void Perform(IContext context)
	{
		Scenes.Manager.ClearKeepLoadedForCheckpoint();
		Scenes.Manager.UnloadScenesAtPosition(true);
	}
}
