using System;
using UnityEngine;

// Token: 0x02000229 RID: 553
public sealed class CCGradient : ScriptableObject
{
	// Token: 0x060012C2 RID: 4802 RVA: 0x000561C7 File Offset: 0x000543C7
	private CCGradient()
	{
	}

	// Token: 0x17000352 RID: 850
	public Color this[float p]
	{
		get
		{
			switch (this.wrap)
			{
			case CCGradient.WrapMode.Clamp:
				if (p < 0f)
				{
					p = 0f;
				}
				if (p > 1f)
				{
					p = 1f;
				}
				break;
			case CCGradient.WrapMode.Repeat:
				while (p < 0f)
				{
					p += 1f;
				}
				while (p > 1f)
				{
					p -= 1f;
				}
				break;
			case CCGradient.WrapMode.Flip:
			{
				bool flag = false;
				while (p < 0f)
				{
					p += 1f;
					flag = !flag;
				}
				while (p > 1f)
				{
					p -= 1f;
					flag = !flag;
				}
				if (flag)
				{
					p = 1f - p;
				}
				break;
			}
			}
			if (p <= this.positions[0])
			{
				return this.colors[0];
			}
			if (p >= this.positions[this.positions.Length - 1])
			{
				return this.colors[this.positions.Length - 1];
			}
			int num = 0;
			while (this.positions[num] < p)
			{
				num++;
			}
			num--;
			float t = (p - this.positions[num]) / (this.positions[num + 1] - this.positions[num]);
			if (this.interpolation == CCGradient.InterpolationMode.Smooth)
			{
				t = Mathf.SmoothStep(0f, 1f, t);
			}
			return Color.Lerp(this.colors[num], this.colors[num + 1], t);
		}
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x00056384 File Offset: 0x00054584
	public void WriteToTexture(float minimum, float maximum, Texture2D texture)
	{
		try
		{
			texture.GetPixel(0, 0);
		}
		catch
		{
			return;
		}
		int i = 0;
		int width = texture.width;
		while (i < width)
		{
			texture.SetPixel(i, 0, this[Mathf.Lerp(minimum, maximum, ((float)i + 0.5f) / (float)width)]);
			i++;
		}
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000563F4 File Offset: 0x000545F4
	private void OnEnable()
	{
		if (this.positions == null)
		{
			this.colors = new Color[]
			{
				Color.black,
				Color.white
			};
			this.positions = new float[]
			{
				0f,
				1f
			};
		}
	}

	// Token: 0x04001041 RID: 4161
	public CCGradient.InterpolationMode interpolation;

	// Token: 0x04001042 RID: 4162
	public CCGradient.WrapMode wrap;

	// Token: 0x04001043 RID: 4163
	[SerializeField]
	private Color[] colors;

	// Token: 0x04001044 RID: 4164
	[SerializeField]
	private float[] positions;

	// Token: 0x0200022A RID: 554
	public enum InterpolationMode
	{
		// Token: 0x04001046 RID: 4166
		Linear,
		// Token: 0x04001047 RID: 4167
		Smooth
	}

	// Token: 0x0200022B RID: 555
	public enum WrapMode
	{
		// Token: 0x04001049 RID: 4169
		Clamp,
		// Token: 0x0400104A RID: 4170
		Repeat,
		// Token: 0x0400104B RID: 4171
		Flip
	}
}
