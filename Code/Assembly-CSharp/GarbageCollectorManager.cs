using System;
using UnityEngine;

// Token: 0x02000977 RID: 2423
public class GarbageCollectorManager : MonoBehaviour
{
	// Token: 0x0600351E RID: 13598 RVA: 0x000DEBAD File Offset: 0x000DCDAD
	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			GC.Collect();
		}
	}
}
