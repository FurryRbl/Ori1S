using System;
using UnityEngine;

// Token: 0x02000981 RID: 2433
public class Vector2Helper
{
	// Token: 0x06003546 RID: 13638 RVA: 0x000DF396 File Offset: 0x000DD596
	public static float Cross(Vector2 v, Vector2 w)
	{
		return v.x * w.y - v.y * w.x;
	}
}
