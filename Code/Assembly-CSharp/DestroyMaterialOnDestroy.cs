using System;
using UnityEngine;

// Token: 0x0200095D RID: 2397
public class DestroyMaterialOnDestroy : MonoBehaviour
{
	// Token: 0x060034C4 RID: 13508 RVA: 0x000DD72D File Offset: 0x000DB92D
	private void OnDestroy()
	{
		UnityEngine.Object.DestroyObject(base.GetComponent<Renderer>().material);
	}
}
