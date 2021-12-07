using System;

// Token: 0x02000661 RID: 1633
public class ClearSkipCutsceneAction : ActionMethod
{
	// Token: 0x060027CC RID: 10188 RVA: 0x000ACEF6 File Offset: 0x000AB0F6
	public override void Perform(IContext context)
	{
		SkipCutsceneController.Instance.Clear();
	}
}
