using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000065 RID: 101
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0001027C File Offset: 0x0000E47C
		protected RawImage()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public override Texture mainTexture
		{
			get
			{
				if (!(this.m_Texture == null))
				{
					return this.m_Texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010318 File Offset: 0x0000E518
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00010320 File Offset: 0x0000E520
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00010348 File Offset: 0x0000E548
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00010350 File Offset: 0x0000E550
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010374 File Offset: 0x0000E574
		public override void SetNativeSize()
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)num, (float)num2);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000103F8 File Offset: 0x0000E5F8
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			Texture mainTexture = this.mainTexture;
			vh.Clear();
			if (mainTexture != null)
			{
				Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
				Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
				Color color = base.color;
				vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMin));
				vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMax));
				vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMax));
				vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMin));
				vh.AddTriangle(0, 1, 2);
				vh.AddTriangle(2, 3, 0);
			}
		}

		// Token: 0x040001A4 RID: 420
		[SerializeField]
		[FormerlySerializedAs("m_Tex")]
		private Texture m_Texture;

		// Token: 0x040001A5 RID: 421
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
