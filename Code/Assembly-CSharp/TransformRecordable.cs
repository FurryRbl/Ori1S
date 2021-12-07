using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class TransformRecordable : MonoBehaviour
{
	// Token: 0x06000FDF RID: 4063 RVA: 0x00048ACC File Offset: 0x00046CCC
	public void Awake()
	{
		TransformRecordable.All.Add(this);
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00048AD9 File Offset: 0x00046CD9
	public void OnDestroy()
	{
		TransformRecordable.All.Remove(this);
	}

	// Token: 0x04000D01 RID: 3329
	public static AllContainer<TransformRecordable> All = new AllContainer<TransformRecordable>();

	// Token: 0x04000D02 RID: 3330
	public string UniqueID;
}
