using System;
using UnityEngine;

// Token: 0x020007BE RID: 1982
[ExecuteInEditMode]
public class CageStructureToolCollider : MonoBehaviour, IStrippable
{
	// Token: 0x06002DAF RID: 11695 RVA: 0x000C2F95 File Offset: 0x000C1195
	public bool DoStrip()
	{
		return false;
	}

	// Token: 0x04002911 RID: 10513
	public Mesh GeneratedMesh;

	// Token: 0x04002912 RID: 10514
	public float EdgeWidth = 1f;

	// Token: 0x04002913 RID: 10515
	public bool SideFaces;
}
