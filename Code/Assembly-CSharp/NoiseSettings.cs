using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
[Serializable]
public class NoiseSettings
{
	// Token: 0x06001A11 RID: 6673 RVA: 0x000703E3 File Offset: 0x0006E5E3
	public NoiseSettings Clone()
	{
		return (NoiseSettings)base.MemberwiseClone();
	}

	// Token: 0x04001678 RID: 5752
	public float GrainIntensityMin = 0.1f;

	// Token: 0x04001679 RID: 5753
	public float GrainIntensityMax = 0.2f;

	// Token: 0x0400167A RID: 5754
	public float GrainSize = 2f;

	// Token: 0x0400167B RID: 5755
	public Texture GrainTexture;
}
