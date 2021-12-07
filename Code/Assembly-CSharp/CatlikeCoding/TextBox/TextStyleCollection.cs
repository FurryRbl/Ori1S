using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000683 RID: 1667
	public sealed class TextStyleCollection : ScriptableObject
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x000AFFDC File Offset: 0x000AE1DC
		public void ComputeRendererCount()
		{
			this.rendererCount = 0;
			for (int i = 0; i < this.styles.Length; i++)
			{
				TextStyle textStyle = this.styles[i];
				textStyle.rendererId = -1;
				if (textStyle.renderer != null)
				{
					for (int j = 0; j < i; j++)
					{
						if (textStyle.renderer == this.styles[j].renderer)
						{
							textStyle.rendererId = this.styles[j].rendererId;
							break;
						}
					}
					if (textStyle.rendererId == -1)
					{
						textStyle.rendererId = this.rendererCount++;
					}
				}
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000B0094 File Offset: 0x000AE294
		public TextRenderer[] CreateRenderers(TextBox box)
		{
			if (this.rendererCount == 0)
			{
				this.ComputeRendererCount();
			}
			Transform transform = box.transform;
			TextRenderer[] array = new TextRenderer[this.rendererCount];
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				if (Application.isPlaying)
				{
					InstantiateUtility.Destroy(transform.GetChild(i).gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
				}
			}
			int j = 0;
			int k = 0;
			while (j < this.rendererCount)
			{
				while (k < this.styles.Length)
				{
					if (this.styles[k].rendererId == j)
					{
						break;
					}
					k++;
				}
				TextRenderer textRenderer = array[j] = UnityEngine.Object.Instantiate<TextRenderer>(this.styles[k].renderer);
				Transform transform2 = textRenderer.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
				j++;
			}
			return array;
		}

		// Token: 0x04002402 RID: 9218
		public TextStyle[] styles;

		// Token: 0x04002403 RID: 9219
		[NonSerialized]
		private int rendererCount;
	}
}
