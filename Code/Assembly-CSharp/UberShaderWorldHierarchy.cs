using System;
using UnityEngine;

// Token: 0x02000844 RID: 2116
[ExecuteInEditMode]
public class UberShaderWorldHierarchy : MonoBehaviour
{
	// Token: 0x06003031 RID: 12337 RVA: 0x000CBEA4 File Offset: 0x000CA0A4
	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector2 scale = new Vector2(1f / this.TextureSize.x, 1f / this.TextureSize.y);
		foreach (Renderer renderer in this.Properties.Keys)
		{
			UberShaderWorldProperty uberShaderWorldProperty = this.Properties[renderer];
			if (uberShaderWorldProperty.Enabled)
			{
				UberShaderAPI.SetTextureSettings(renderer, uberShaderWorldProperty.Name, scale, this.Offset - new Vector2(position.x * scale.x, position.y * scale.y), Vector2.zero, 0f, 0f, false);
			}
		}
	}

	// Token: 0x04002B5F RID: 11103
	public UberShaderWorldHierarchyDictionary Properties = new UberShaderWorldHierarchyDictionary();

	// Token: 0x04002B60 RID: 11104
	public Vector2 Offset;

	// Token: 0x04002B61 RID: 11105
	public Vector2 TextureSize = new Vector2(1f, 1f);
}
