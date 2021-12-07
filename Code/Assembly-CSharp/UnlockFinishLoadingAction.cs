using System;

// Token: 0x02000274 RID: 628
public class UnlockFinishLoadingAction : ActionMethod
{
	// Token: 0x060014F1 RID: 5361 RVA: 0x0005E01E File Offset: 0x0005C21E
	public override void Perform(IContext context)
	{
		InstantLoadScenesController.Instance.LockFinishingLoading = false;
	}
}
