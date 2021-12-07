using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
[Category("Unity")]
public class PlayAnimationAction : ActionMethod
{
	// Token: 0x06001734 RID: 5940 RVA: 0x00064483 File Offset: 0x00062683
	public override void Perform(IContext context)
	{
		this.Target.GetComponent<Animation>().Play();
	}

	// Token: 0x040013E8 RID: 5096
	[NotNull]
	public GameObject Target;
}
