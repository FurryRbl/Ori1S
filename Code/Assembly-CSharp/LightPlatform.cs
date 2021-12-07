using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200064B RID: 1611
public class LightPlatform : MonoBehaviour
{
	// Token: 0x06002771 RID: 10097 RVA: 0x000ABD2F File Offset: 0x000A9F2F
	public void OnEnable()
	{
		LightPlatform.All.Add(this);
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x000ABD3C File Offset: 0x000A9F3C
	public void OnDisable()
	{
		LightPlatform.All.Remove(this);
	}

	// Token: 0x04002211 RID: 8721
	public static List<LightPlatform> All = new List<LightPlatform>();

	// Token: 0x04002212 RID: 8722
	public bool InsideLight;
}
