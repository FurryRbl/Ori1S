using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public static class PathHelper
{
	// Token: 0x060010A6 RID: 4262 RVA: 0x0004C334 File Offset: 0x0004A534
	public static Vector3 CalculateBeizer(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float r)
	{
		Vector3 a2 = Vector3.Lerp(a, b, r);
		Vector3 vector = Vector3.Lerp(b, c, r);
		Vector3 b2 = Vector3.Lerp(c, d, r);
		return Vector3.Lerp(Vector3.Lerp(a2, vector, r), Vector3.Lerp(vector, b2, r), r);
	}
}
