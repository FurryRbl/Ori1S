using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
[Serializable]
public class WideScreenAdjustmentSettings
{
	// Token: 0x060011D0 RID: 4560 RVA: 0x000520CD File Offset: 0x000502CD
	public void ApplyToPuppet(CameraPuppetController puppet)
	{
		if (this.Enabled)
		{
			puppet.SetWideScreenZoomStrength(this.ZoomStrength);
			puppet.SetWideScreenHorizontalPanStrength(this.HorizontalPanStrength);
			puppet.SetWideScreenVerticalPanStrength(this.VerticalPanStrength);
		}
	}

	// Token: 0x04000F57 RID: 3927
	public bool Enabled;

	// Token: 0x04000F58 RID: 3928
	[Range(0f, 1f)]
	public float ZoomStrength;

	// Token: 0x04000F59 RID: 3929
	[Range(-2f, 2f)]
	public float HorizontalPanStrength;

	// Token: 0x04000F5A RID: 3930
	[Range(-2f, 2f)]
	public float VerticalPanStrength;
}
