using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class HeatUpPlatformAction : ActionMethod
{
	// Token: 0x060016A0 RID: 5792 RVA: 0x00062EC4 File Offset: 0x000610C4
	public override void Perform(IContext context)
	{
		HeatUpPlatformAction.HeatupPlatformMethod method = this.Method;
		if (method != HeatUpPlatformAction.HeatupPlatformMethod.Activate)
		{
			if (method == HeatUpPlatformAction.HeatupPlatformMethod.Deactivate)
			{
				foreach (HeatUpPlatform heatUpPlatform in this.HeatUpPlatform.GetComponentsInChildren<HeatUpPlatform>())
				{
					heatUpPlatform.Activated = false;
				}
			}
		}
		else
		{
			foreach (HeatUpPlatform heatUpPlatform2 in this.HeatUpPlatform.GetComponentsInChildren<HeatUpPlatform>())
			{
				heatUpPlatform2.Activated = true;
			}
		}
	}

	// Token: 0x04001378 RID: 4984
	[NotNull]
	public GameObject HeatUpPlatform;

	// Token: 0x04001379 RID: 4985
	public HeatUpPlatformAction.HeatupPlatformMethod Method;

	// Token: 0x020002ED RID: 749
	public enum HeatupPlatformMethod
	{
		// Token: 0x0400137B RID: 4987
		Activate,
		// Token: 0x0400137C RID: 4988
		Deactivate
	}
}
