using System;
using UnityEngine;

// Token: 0x02000658 RID: 1624
[Serializable]
public class SpiritLightTurbulence
{
	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x060027AD RID: 10157 RVA: 0x000AC864 File Offset: 0x000AAA64
	public float TurbulenceValueInThisFrame
	{
		get
		{
			return TurbulenceManager.Instance.SampleTurbulenceValueAtTime(this.TurbulenceMagnitude, this.TurbulenceSpeed, this.TurbulenceTimeOffset, Time.realtimeSinceStartup);
		}
	}

	// Token: 0x04002248 RID: 8776
	public float TurbulenceSpeed = 1f;

	// Token: 0x04002249 RID: 8777
	public float TurbulenceMagnitude;

	// Token: 0x0400224A RID: 8778
	public float TurbulenceTimeOffset;
}
