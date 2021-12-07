using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public sealed class CCTextCylinderWrapper : CCTextModifier
{
	// Token: 0x060012DF RID: 4831 RVA: 0x00056E09 File Offset: 0x00055009
	public override void Modify(CCText text)
	{
		if (this.wrapMode == CCTextCylinderWrapper.WrapMode.X)
		{
			this.WrapX(text);
		}
		else
		{
			this.WrapY(text);
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x00056E2C File Offset: 0x0005502C
	private void WrapX(CCText text)
	{
		Vector3[] vertices = text.vertices;
		Vector3 minBounds = CCTextCylinderWrapper.notMinimum;
		Vector3 maxBounds = CCTextCylinderWrapper.notMaximum;
		float num = text.Offset.z - this.radius;
		float num2 = (num != 0f) ? (1f / num) : 0f;
		int i = 0;
		int num3 = 0;
		int length = text.Length;
		while (i < length)
		{
			if (text[i] > ' ')
			{
				Vector3 vector = vertices[num3];
				float f = vector.y * num2;
				vector.y = Mathf.Sin(f) * num;
				vector.z = Mathf.Cos(f) * num;
				vertices[num3] = vector;
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
				vertices[num3 + 1].y = vector.y;
				vertices[num3 + 1].z = vector.z;
				vector = vertices[num3 + 2];
				f = vector.y * num2;
				vector.y = Mathf.Sin(f) * num;
				vector.z = Mathf.Cos(f) * num;
				vertices[num3 + 2] = vector;
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
				vertices[num3 + 3].y = vector.y;
				vertices[num3 + 3].z = vector.z;
				num3 += 4;
			}
			i++;
		}
		text.minBounds = minBounds;
		text.maxBounds = maxBounds;
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x00057148 File Offset: 0x00055348
	private void WrapY(CCText text)
	{
		Vector3[] vertices = text.vertices;
		Vector3 minBounds = CCTextCylinderWrapper.notMinimum;
		Vector3 maxBounds = CCTextCylinderWrapper.notMaximum;
		float num = text.Offset.z - this.radius;
		float num2 = (num != 0f) ? (1f / num) : 0f;
		int i = 0;
		int num3 = 0;
		int length = text.Length;
		while (i < length)
		{
			if (text[i] > ' ')
			{
				Vector3 vector = vertices[num3];
				float f = vector.x * num2;
				vector.x = Mathf.Sin(f) * num;
				vector.z = Mathf.Cos(f) * num;
				vertices[num3] = vector;
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
				vertices[num3 + 3].x = vector.x;
				vertices[num3 + 3].z = vector.z;
				vector = vertices[num3 + 1];
				f = vector.x * num2;
				vector.x = Mathf.Sin(f) * num;
				vector.z = Mathf.Cos(f) * num;
				vertices[num3 + 1] = vector;
				vector.y = vertices[num3 + 2].y;
				vertices[num3 + 2] = vector;
				num3 += 4;
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
			}
			i++;
		}
		text.minBounds = minBounds;
		text.maxBounds = maxBounds;
	}

	// Token: 0x04001092 RID: 4242
	private static Vector3 notMinimum = Vector3.one * float.MaxValue;

	// Token: 0x04001093 RID: 4243
	private static Vector3 notMaximum = Vector3.one * float.MinValue;

	// Token: 0x04001094 RID: 4244
	public CCTextCylinderWrapper.WrapMode wrapMode;

	// Token: 0x04001095 RID: 4245
	public float radius;

	// Token: 0x02000235 RID: 565
	public enum WrapMode
	{
		// Token: 0x04001097 RID: 4247
		X,
		// Token: 0x04001098 RID: 4248
		Y
	}
}
