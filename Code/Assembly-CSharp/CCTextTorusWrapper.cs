using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public sealed class CCTextTorusWrapper : CCTextModifier
{
	// Token: 0x060012E8 RID: 4840 RVA: 0x00057765 File Offset: 0x00055965
	public override void Modify(CCText text)
	{
		if (this.revolveMode == CCTextTorusWrapper.RevolveMode.X)
		{
			this.RevolveX(text);
		}
		else
		{
			this.RevolveY(text);
		}
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00057788 File Offset: 0x00055988
	private void RevolveX(CCText text)
	{
		Vector3[] vertices = text.vertices;
		Vector3 minBounds = CCTextTorusWrapper.notMinimum;
		Vector3 maxBounds = CCTextTorusWrapper.notMaximum;
		float num = text.Offset.z - this.minorRadius;
		float num2 = (this.majorRadius != 0f) ? (-1f / this.majorRadius) : 0f;
		float num3 = (num != 0f) ? (1f / num) : 0f;
		int i = 0;
		int j = 0;
		int length = text.Length;
		while (i < length)
		{
			if (text[i] > ' ')
			{
				int num4 = j + 4;
				while (j < num4)
				{
					Vector3 vector = vertices[j];
					float f = vector.y * num2;
					float f2 = vector.x * num3;
					float num5 = num * Mathf.Cos(f2) - this.majorRadius;
					vector.z = num5 * Mathf.Cos(f);
					vector.y = num5 * Mathf.Sin(f);
					vector.x = num * Mathf.Sin(f2);
					vertices[j] = vector;
					if (vector.x > maxBounds.x)
					{
						maxBounds.x = vector.x;
					}
					if (vector.x < minBounds.x)
					{
						minBounds.x = vector.x;
					}
					if (vector.y > maxBounds.y)
					{
						maxBounds.y = vector.y;
					}
					if (vector.y < minBounds.y)
					{
						minBounds.y = vector.y;
					}
					if (vector.z > maxBounds.z)
					{
						maxBounds.z = vector.z;
					}
					if (vector.z < minBounds.z)
					{
						minBounds.z = vector.z;
					}
					j++;
				}
			}
			i++;
		}
		text.minBounds = minBounds;
		text.maxBounds = maxBounds;
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000579A4 File Offset: 0x00055BA4
	private void RevolveY(CCText text)
	{
		Vector3[] vertices = text.vertices;
		Vector3 minBounds = CCTextTorusWrapper.notMinimum;
		Vector3 maxBounds = CCTextTorusWrapper.notMaximum;
		float num = text.Offset.z - this.minorRadius;
		float num2 = (this.majorRadius != 0f) ? (-1f / this.majorRadius) : 0f;
		float num3 = (num != 0f) ? (1f / num) : 0f;
		int i = 0;
		int j = 0;
		int length = text.Length;
		while (i < length)
		{
			if (text[i] > ' ')
			{
				int num4 = j + 4;
				while (j < num4)
				{
					Vector3 vector = vertices[j];
					float f = vector.x * num2;
					float f2 = vector.y * num3;
					float num5 = num * Mathf.Cos(f2) - this.majorRadius;
					vector.z = num5 * Mathf.Cos(f);
					vector.x = num5 * Mathf.Sin(f);
					vector.y = num * Mathf.Sin(f2);
					vertices[j] = vector;
					if (vector.x > maxBounds.x)
					{
						maxBounds.x = vector.x;
					}
					if (vector.x < minBounds.x)
					{
						minBounds.x = vector.x;
					}
					if (vector.y > maxBounds.y)
					{
						maxBounds.y = vector.y;
					}
					if (vector.y < minBounds.y)
					{
						minBounds.y = vector.y;
					}
					if (vector.z > maxBounds.z)
					{
						maxBounds.z = vector.z;
					}
					if (vector.z < minBounds.z)
					{
						minBounds.z = vector.z;
					}
					j++;
				}
			}
			i++;
		}
		text.minBounds = minBounds;
		text.maxBounds = maxBounds;
	}

	// Token: 0x0400109F RID: 4255
	private static Vector3 notMinimum = Vector3.one * float.MaxValue;

	// Token: 0x040010A0 RID: 4256
	private static Vector3 notMaximum = Vector3.one * float.MinValue;

	// Token: 0x040010A1 RID: 4257
	public CCTextTorusWrapper.RevolveMode revolveMode;

	// Token: 0x040010A2 RID: 4258
	public float minorRadius;

	// Token: 0x040010A3 RID: 4259
	public float majorRadius;

	// Token: 0x02000239 RID: 569
	public enum RevolveMode
	{
		// Token: 0x040010A5 RID: 4261
		X,
		// Token: 0x040010A6 RID: 4262
		Y
	}
}
