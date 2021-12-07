using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class SplatterSpray : MonoBehaviour
{
	// Token: 0x0600087F RID: 2175 RVA: 0x00024973 File Offset: 0x00022B73
	private void Start()
	{
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00024978 File Offset: 0x00022B78
	private void PerformTheSpray()
	{
		for (int i = 0; i < this.NumberOfSplatterObjects; i++)
		{
		}
	}

	// Token: 0x040006B8 RID: 1720
	public GameObject SplatterObject;

	// Token: 0x040006B9 RID: 1721
	public int NumberOfSplatterObjects = 3;

	// Token: 0x040006BA RID: 1722
	public float SprayConeRadius = 10f;
}
