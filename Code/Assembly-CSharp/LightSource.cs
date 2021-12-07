using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class LightSource : MonoBehaviour
{
	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0008A4B6 File Offset: 0x000886B6
	public static bool AnyExist
	{
		get
		{
			return LightSource.All.Count > 0;
		}
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x0008A4C5 File Offset: 0x000886C5
	public void OnEnable()
	{
		LightSource.All.Add(this);
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x0008A4D2 File Offset: 0x000886D2
	public void OnDisable()
	{
		LightSource.All.Remove(this);
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0008A4E0 File Offset: 0x000886E0
	public static bool TestPosition(Vector2 position, float padding = 0f)
	{
		for (int i = 0; i < LightSource.All.Count; i++)
		{
			LightSource lightSource = LightSource.All[i];
			Vector2 a = lightSource.transform.position;
			if (Vector2.Distance(a, position) < lightSource.Radius + padding)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001B07 RID: 6919
	public static List<LightSource> All = new List<LightSource>();

	// Token: 0x04001B08 RID: 6920
	public float Radius = 10f;
}
