using System;
using UnityEngine;

// Token: 0x02000984 RID: 2436
[ExecuteInEditMode]
public class LineMeshTextureTiler : MonoBehaviour
{
	// Token: 0x06003550 RID: 13648 RVA: 0x000DFAD9 File Offset: 0x000DDCD9
	public void Awake()
	{
		this.m_lineMesh = base.GetComponent<LineMesh>();
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x000DFAE8 File Offset: 0x000DDCE8
	public void Start()
	{
		Material material = base.GetComponent<Renderer>().material;
		this.m_scale = material.GetTextureScale(this.TextureName);
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x000DFB14 File Offset: 0x000DDD14
	public void LateUpdate()
	{
		float worldSpaceLength = this.m_lineMesh.WorldSpaceLength;
		Material material = base.GetComponent<Renderer>().material;
		material.SetTextureScale(this.TextureName, Vector2.Scale(this.m_scale, new Vector2(worldSpaceLength, 1f)));
	}

	// Token: 0x04002FEF RID: 12271
	private LineMesh m_lineMesh;

	// Token: 0x04002FF0 RID: 12272
	private Vector2 m_scale;

	// Token: 0x04002FF1 RID: 12273
	public string TextureName;
}
