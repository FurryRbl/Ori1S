using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
[Category("General")]
public class DestroyObjectAction : ActionMethod
{
	// Token: 0x0600166A RID: 5738 RVA: 0x00062A50 File Offset: 0x00060C50
	public override void Perform(IContext context)
	{
		InstantiateUtility.Destroy(this.ObjectToDestroy);
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x00062A5D File Offset: 0x00060C5D
	public override string GetNiceName()
	{
		return "Destroy " + ActionHelper.GetName(this.ObjectToDestroy);
	}

	// Token: 0x04001358 RID: 4952
	[NotNull]
	public GameObject ObjectToDestroy;
}
