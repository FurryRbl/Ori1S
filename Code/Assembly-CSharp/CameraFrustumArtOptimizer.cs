using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C1 RID: 1729
public class CameraFrustumArtOptimizer : MonoBehaviour
{
	// Token: 0x06002976 RID: 10614 RVA: 0x000B3444 File Offset: 0x000B1644
	[ContextMenu("Update list")]
	public void UpdateList()
	{
		this.Sprites.Clear();
		foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			this.Sprites.Add(new MeshRendererFrustrumOptimiser(meshRenderer.gameObject));
		}
	}

	// Token: 0x04002507 RID: 9479
	public List<MeshRendererFrustrumOptimiser> Sprites = new List<MeshRendererFrustrumOptimiser>();
}
