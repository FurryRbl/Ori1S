using System;
using UnityEngine;

// Token: 0x020003CD RID: 973
public static class EaseFunction
{
	// Token: 0x06001AD4 RID: 6868 RVA: 0x0007355F File Offset: 0x0007175F
	public static float easeInOutSine(float value)
	{
		return -0.5f * (Mathf.Cos(3.1415927f * value / 1f) - 1f);
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0007357F File Offset: 0x0007177F
	public static float easeLinear(float value)
	{
		return value;
	}
}
