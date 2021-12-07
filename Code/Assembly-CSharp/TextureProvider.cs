using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009E2 RID: 2530
public class TextureProvider : ScriptableObject
{
	// Token: 0x06003708 RID: 14088 RVA: 0x000E7124 File Offset: 0x000E5324
	public Texture2D GetTexture()
	{
		for (int i = 0; i < this.TextureConditionPairs.Count; i++)
		{
			if (this.TextureConditionPairs[i].Condition.Validate(null))
			{
				return this.TextureConditionPairs[i].Texture2D;
			}
		}
		return this.DefaultTexture2D;
	}

	// Token: 0x04003202 RID: 12802
	public Texture2D DefaultTexture2D;

	// Token: 0x04003203 RID: 12803
	public List<TextureProvider.TetxureContidionPair> TextureConditionPairs;

	// Token: 0x020009E3 RID: 2531
	[Serializable]
	public class TetxureContidionPair
	{
		// Token: 0x04003204 RID: 12804
		public Texture2D Texture2D;

		// Token: 0x04003205 RID: 12805
		public Condition Condition;
	}
}
