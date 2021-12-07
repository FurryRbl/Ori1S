using System;
using UnityEngine;

// Token: 0x020007AF RID: 1967
public static class Vector2Extensions
{
	// Token: 0x06002D87 RID: 11655 RVA: 0x000C25FC File Offset: 0x000C07FC
	public static Vector2 Rotate(this Vector2 v, float degrees)
	{
		float num = Mathf.Sin(degrees * 0.017453292f);
		float num2 = Mathf.Cos(degrees * 0.017453292f);
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num * x + num2 * y;
		return v;
	}
}
