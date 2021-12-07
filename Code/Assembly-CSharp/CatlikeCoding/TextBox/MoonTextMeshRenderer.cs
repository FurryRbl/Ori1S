using System;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x02000681 RID: 1665
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	[ExecuteInEditMode]
	public class MoonTextMeshRenderer : TextRenderer
	{
		// Token: 0x06002878 RID: 10360 RVA: 0x000AF2CB File Offset: 0x000AD4CB
		protected void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.mesh);
			this.mesh = null;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000AF2E0 File Offset: 0x000AD4E0
		public void Start()
		{
			if (Application.isPlaying)
			{
				base.GetComponent<Renderer>().material.SetFloat("_TxtTime", 999999f);
				TransparencyAnimator.Register(base.transform);
			}
			else
			{
				base.GetComponent<Renderer>().sharedMaterial.SetFloat("_TxtTime", 999999f);
			}
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000AF33C File Offset: 0x000AD53C
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
					Array.Resize<Vector2>(ref this.uv2, this.vertices.Length);
					Array.Resize<int>(ref this.triangles, num * 6);
					Array.Resize<Vector3>(ref this.normals, num * 4);
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
					this.vertices[j] = (this.vertices[j + 1] = (this.vertices[j + 2] = (this.vertices[j + 3] = MoonTextMeshRenderer.hidden)));
					j += 4;
				}
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000AF59C File Offset: 0x000AD79C
		public override void Add(CharMetaData meta, Vector2 offset)
		{
			BitmapFontChar bitmapFontChar = meta.font[meta.id];
			int num = this.currentVertexIndex;
			Vector2 vector;
			vector.x = bitmapFontChar.uMin;
			vector.y = bitmapFontChar.vMax;
			this.uv2[num] = vector;
			vector.x = bitmapFontChar.uMax;
			this.uv2[num + 1] = vector;
			vector.y = bitmapFontChar.vMin;
			this.uv2[num + 2] = vector;
			vector.x = bitmapFontChar.uMin;
			this.uv2[num + 3] = vector;
			this.uv[num] = new Vector2(0f, 1f);
			this.uv[num + 1] = new Vector2(1f, 1f);
			this.uv[num + 2] = new Vector2(1f, 0f);
			this.uv[num + 3] = new Vector2(0f, 0f);
			float d = Mathf.Max(0f, (float)meta.unstyledIndex / this.FadeSpread);
			this.normals[num] = (this.normals[num + 1] = (this.normals[num + 2] = (this.normals[num + 3] = Vector3.right * d)));
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

		// Token: 0x0600287C RID: 10364 RVA: 0x000AF8C0 File Offset: 0x000ADAC0
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
				this.mesh.uv2 = this.uv2;
				this.mesh.normals = this.normals;
				if (this.meshResized)
				{
					this.mesh.triangles = this.triangles;
				}
				this.mesh.RecalculateBounds();
				base.gameObject.SetActive(true);
			}
			this.lastRendererCharCount = this.renderedCharCount;
		}

		// Token: 0x040023F3 RID: 9203
		protected static Vector3 hidden = Vector3.zero;

		// Token: 0x040023F4 RID: 9204
		public int chunkSize = 1;

		// Token: 0x040023F5 RID: 9205
		protected Mesh mesh;

		// Token: 0x040023F6 RID: 9206
		protected Vector3[] vertices;

		// Token: 0x040023F7 RID: 9207
		protected Color32[] colors;

		// Token: 0x040023F8 RID: 9208
		protected Vector2[] uv;

		// Token: 0x040023F9 RID: 9209
		protected Vector2[] uv2;

		// Token: 0x040023FA RID: 9210
		protected Vector3[] normals;

		// Token: 0x040023FB RID: 9211
		protected int[] triangles;

		// Token: 0x040023FC RID: 9212
		protected bool meshResized;

		// Token: 0x040023FD RID: 9213
		protected int lastRendererCharCount;

		// Token: 0x040023FE RID: 9214
		protected int currentVertexIndex;

		// Token: 0x040023FF RID: 9215
		public float FadeSpread = 5f;
	}
}
