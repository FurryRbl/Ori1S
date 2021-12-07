using System;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class BlendFloat : Blend<float>
{
	// Token: 0x06001AD0 RID: 6864 RVA: 0x000734C3 File Offset: 0x000716C3
	public BlendFloat(Func<float, float> ease) : base(ease, new Func<float, float, float, float>(Mathf.Lerp))
	{
	}
}
