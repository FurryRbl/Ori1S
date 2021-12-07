using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000767 RID: 1895
	public class TextMeshCylinderRenderer : TextMeshRenderer
	{
		// Token: 0x06002C38 RID: 11320 RVA: 0x000BDAC8 File Offset: 0x000BBCC8
		public override void Prepare()
		{
			base.Prepare();
			if (!this.fetchedSettings)
			{
				this.fetchedSettings = true;
				this.settings = base.transform.parent.GetComponent<TextMeshCylinderSettings>();
			}
			if (this.settings)
			{
				this.radius = this.settings.radius;
				this.revolveAxis = this.settings.revolveAxis;
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000BDB38 File Offset: 0x000BBD38
		public override void Add(CharMetaData meta, Vector2 offset)
		{
			BitmapFontChar bitmapFontChar = meta.font[meta.id];
			int currentVertexIndex = this.currentVertexIndex;
			Vector2 vector;
			vector.x = bitmapFontChar.uMin;
			vector.y = bitmapFontChar.vMax;
			this.uv[currentVertexIndex] = vector;
			vector.x = bitmapFontChar.uMax;
			this.uv[currentVertexIndex + 1] = vector;
			vector.y = bitmapFontChar.vMin;
			this.uv[currentVertexIndex + 2] = vector;
			vector.x = bitmapFontChar.uMin;
			this.uv[currentVertexIndex + 3] = vector;
			this.colors[currentVertexIndex] = (this.colors[currentVertexIndex + 1] = (this.colors[currentVertexIndex + 2] = (this.colors[currentVertexIndex + 3] = meta.color)));
			float num = -this.radius;
			float num2 = (num != 0f) ? (1f / num) : 0f;
			if (this.revolveAxis == CylinderRevolveAxis.X)
			{
				float num3 = (offset.y + meta.scale * bitmapFontChar.yOffset + meta.positionInBox.y) * num2;
				Vector3 vector2;
				float num4 = vector2.x = offset.x + meta.scale * bitmapFontChar.xOffset + meta.positionInBox.x;
				vector2.y = Mathf.Sin(num3) * num;
				vector2.z = Mathf.Cos(num3) * num;
				this.vertices[currentVertexIndex] = vector2;
				vector2.x += meta.scale * bitmapFontChar.width;
				this.vertices[currentVertexIndex + 1] = vector2;
				num3 -= bitmapFontChar.height * meta.scale * num2;
				vector2.y = Mathf.Sin(num3) * num;
				vector2.z = Mathf.Cos(num3) * num;
				this.vertices[currentVertexIndex + 2] = vector2;
				vector2.x = num4;
				this.vertices[currentVertexIndex + 3] = vector2;
			}
			else
			{
				float num3 = (offset.x + meta.scale * bitmapFontChar.xOffset + meta.positionInBox.x) * num2;
				Vector3 vector2;
				vector2.x = Mathf.Sin(num3) * num;
				float num4 = vector2.y = offset.y + meta.scale * bitmapFontChar.yOffset + meta.positionInBox.y;
				vector2.z = Mathf.Cos(num3) * num;
				this.vertices[currentVertexIndex] = vector2;
				vector2.y -= meta.scale * bitmapFontChar.height;
				this.vertices[currentVertexIndex + 3] = vector2;
				num3 += bitmapFontChar.width * meta.scale * num2;
				vector2.x = Mathf.Sin(num3) * num;
				vector2.z = Mathf.Cos(num3) * num;
				this.vertices[currentVertexIndex + 2] = vector2;
				vector2.y = num4;
				this.vertices[currentVertexIndex + 1] = vector2;
			}
			this.currentVertexIndex += 4;
		}

		// Token: 0x040027F6 RID: 10230
		public CylinderRevolveAxis revolveAxis;

		// Token: 0x040027F7 RID: 10231
		public float radius;

		// Token: 0x040027F8 RID: 10232
		private TextMeshCylinderSettings settings;

		// Token: 0x040027F9 RID: 10233
		private bool fetchedSettings;
	}
}
