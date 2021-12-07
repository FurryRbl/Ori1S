using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000768 RID: 1896
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	[PooledSafe]
	[ExecuteInEditMode]
	public class TextMeshRenderer : TextRenderer
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x000BDEF5 File Offset: 0x000BC0F5
		protected void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.mesh);
			this.mesh = null;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000BDF0C File Offset: 0x000BC10C
		public override void Prepare()
		{
			if (this.mesh == null)
			{
				base.GetComponent<MeshFilter>().mesh = (this.mesh = new Mesh());
				this.mesh.hideFlags = HideFlags.HideAndDontSave;
				this.mesh.name = "Text Box Mesh";
			}
			this.currentVertexIndex = 0;
			if (this.lastRendererCharCount < this.renderedCharCount)
			{
				if (this.vertices == null || this.vertices.Length < this.renderedCharCount * 4)
				{
					int num;
					int i;
					int num2;
					if (this.vertices == null)
					{
						num = ((this.renderedCharCount - 1) / this.chunkSize + 1) * this.chunkSize;
						i = 0;
						num2 = 0;
					}
					else
					{
						num = this.vertices.Length / 4 + ((this.renderedCharCount - this.lastRendererCharCount - 1) / this.chunkSize + 1) * this.chunkSize;
						i = this.vertices.Length;
						num2 = this.triangles.Length;
					}
					Array.Resize<Vector3>(ref this.vertices, num * 4);
					Array.Resize<Color32>(ref this.colors, this.vertices.Length);
					Array.Resize<Vector2>(ref this.uv, this.vertices.Length);
					Array.Resize<int>(ref this.triangles, num * 6);
					while (i < this.vertices.Length)
					{
						this.triangles[num2] = i;
						this.triangles[num2 + 1] = i + 1;
						this.triangles[num2 + 2] = i + 2;
						this.triangles[num2 + 3] = i;
						this.triangles[num2 + 4] = i + 2;
						this.triangles[num2 + 5] = i + 3;
						i += 4;
						num2 += 6;
					}
					this.meshResized = true;
				}
			}
			else if (this.lastRendererCharCount > this.renderedCharCount)
			{
				int j = this.renderedCharCount * 4;
				int num3 = this.lastRendererCharCount * 4;
				while (j < num3)
				{
					this.vertices[j] = (this.vertices[j + 1] = (this.vertices[j + 2] = (this.vertices[j + 3] = TextMeshRenderer.hidden)));
					j += 4;
				}
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000BE148 File Offset: 0x000BC348
		public override void Add(CharMetaData meta, Vector2 offset)
		{
			BitmapFontChar bitmapFontChar = meta.font[meta.id];
			int num = this.currentVertexIndex;
			Vector2 vector;
			vector.x = bitmapFontChar.uMin;
			vector.y = bitmapFontChar.vMax;
			this.uv[num] = vector;
			vector.x = bitmapFontChar.uMax;
			this.uv[num + 1] = vector;
			vector.y = bitmapFontChar.vMin;
			this.uv[num + 2] = vector;
			vector.x = bitmapFontChar.uMin;
			this.uv[num + 3] = vector;
			this.colors[num] = (this.colors[num + 1] = (this.colors[num + 2] = (this.colors[num + 3] = meta.color)));
			Vector3 vector2;
			float x = vector2.x = offset.x + meta.scale * bitmapFontChar.xOffset + meta.positionInBox.x;
			vector2.y = offset.y + meta.scale * bitmapFontChar.yOffset + meta.positionInBox.y;
			vector2.z = 0f;
			this.vertices[num] = vector2;
			vector2.x += meta.scale * bitmapFontChar.width;
			this.vertices[num + 1] = vector2;
			vector2.y -= meta.scale * bitmapFontChar.height;
			this.vertices[num + 2] = vector2;
			vector2.x = x;
			this.vertices[num + 3] = vector2;
			this.currentVertexIndex += 4;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000BE364 File Offset: 0x000BC564
		public override void Apply()
		{
			if (this.renderedCharCount == 0)
			{
				base.gameObject.SetActive(false);
			}
			else
			{
				this.mesh.vertices = this.vertices;
				this.mesh.colors32 = this.colors;
				this.mesh.uv = this.uv;
				if (this.meshResized)
				{
					this.mesh.triangles = this.triangles;
				}
				this.mesh.RecalculateBounds();
				base.gameObject.SetActive(true);
			}
			this.lastRendererCharCount = this.renderedCharCount;
		}

		// Token: 0x040027FA RID: 10234
		protected static Vector3 hidden = Vector3.zero;

		// Token: 0x040027FB RID: 10235
		public int chunkSize = 1;

		// Token: 0x040027FC RID: 10236
		protected Mesh mesh;

		// Token: 0x040027FD RID: 10237
		protected Vector3[] vertices;

		// Token: 0x040027FE RID: 10238
		protected Color32[] colors;

		// Token: 0x040027FF RID: 10239
		protected Vector2[] uv;

		// Token: 0x04002800 RID: 10240
		protected int[] triangles;

		// Token: 0x04002801 RID: 10241
		protected bool meshResized;

		// Token: 0x04002802 RID: 10242
		protected int lastRendererCharCount;

		// Token: 0x04002803 RID: 10243
		protected int currentVertexIndex;
	}
}
