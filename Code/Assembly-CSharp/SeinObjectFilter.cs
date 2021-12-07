using System;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public class SeinObjectFilter : GameObjectFilter
{
	// Token: 0x06001FA2 RID: 8098 RVA: 0x0008B26C File Offset: 0x0008946C
	public override bool Valid(GameObject gameObject)
	{
		return gameObject.CompareTag("Player");
	}
}
