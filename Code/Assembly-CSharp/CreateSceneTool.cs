using System;
using UnityEngine;

// Token: 0x0200070E RID: 1806
[ExecuteInEditMode]
public class CreateSceneTool : MonoBehaviour
{
	// Token: 0x06002AF9 RID: 11001 RVA: 0x000B802A File Offset: 0x000B622A
	public void Update()
	{
		if (this.DoDestroy)
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x0400264C RID: 9804
	[HideInInspector]
	public bool DoDestroy;
}
