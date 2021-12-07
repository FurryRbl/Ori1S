using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class OptionsScreenLegendController : MonoBehaviour
{
	// Token: 0x06000BFE RID: 3070 RVA: 0x000351D4 File Offset: 0x000333D4
	private void Update()
	{
		if (LeaderboardsB.Instance.IsVisible)
		{
			this.GeneralLegend.Initialize();
			this.GeneralLegend.AnimatorDriver.ContinueBackwards();
		}
		else
		{
			this.GeneralLegend.Initialize();
			this.GeneralLegend.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x040009AF RID: 2479
	public TransparencyAnimator GeneralLegend;
}
