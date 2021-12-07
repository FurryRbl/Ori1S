using System;
using Game;

// Token: 0x020009CF RID: 2511
public class DestroyMistTorchAction : ActionMethod
{
	// Token: 0x060036BC RID: 14012 RVA: 0x000E5DAF File Offset: 0x000E3FAF
	public override void Perform(IContext context)
	{
		InstantiateUtility.Destroy(Items.MistTorch.gameObject);
	}
}
