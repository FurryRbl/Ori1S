using System;
using UnityEngine;

// Token: 0x020003A2 RID: 930
[Serializable]
public class BloomAndFlaresSettings
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x000704BD File Offset: 0x0006E6BD
	public BloomAndFlaresSettings Clone()
	{
		return (BloomAndFlaresSettings)base.MemberwiseClone();
	}

	// Token: 0x0400167C RID: 5756
	public float Intensity = 1f;

	// Token: 0x0400167D RID: 5757
	public float Threshhold = 0.34f;

	// Token: 0x0400167E RID: 5758
	public int BlurIterations = 3;

	// Token: 0x0400167F RID: 5759
	public float BlurSpread = 1f;

	// Token: 0x04001680 RID: 5760
	public float LocalIntensity = 1f;

	// Token: 0x04001681 RID: 5761
	public float LocalThreshhold = 0.3f;

	// Token: 0x04001682 RID: 5762
	public Color FlareColorA = new Color(0.4f, 0.4f, 0.8f, 0.7490196f);

	// Token: 0x04001683 RID: 5763
	public Color FlareColorB = new Color(0.4f, 0.8f, 0.8f, 0.7490196f);

	// Token: 0x04001684 RID: 5764
	public Color FlareColorC = new Color(0.8f, 0.4f, 0.8f, 0.7490196f);

	// Token: 0x04001685 RID: 5765
	public Color FlareColorD = new Color(0.8f, 0.4f, 0f, 0.7490196f);
}
