using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200088C RID: 2188
public class MistyWoodsAreaMapCanvas : MonoBehaviour
{
	// Token: 0x0600313A RID: 12602 RVA: 0x000D1C18 File Offset: 0x000CFE18
	public void OnEnable()
	{
		foreach (MistyWoodsAreaMapCanvas.CanvasItem canvasItem in this.Items)
		{
			if (canvasItem.Condition.Validate(null))
			{
				base.GetComponent<Renderer>().material.SetTexture(ShaderProperties.MainTexture, canvasItem.Texture);
			}
		}
	}

	// Token: 0x04002C91 RID: 11409
	public List<MistyWoodsAreaMapCanvas.CanvasItem> Items;

	// Token: 0x0200088D RID: 2189
	[Serializable]
	public class CanvasItem
	{
		// Token: 0x04002C92 RID: 11410
		public Texture Texture;

		// Token: 0x04002C93 RID: 11411
		public Condition Condition;
	}
}
