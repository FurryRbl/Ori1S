using System;
using UnityEngine.Serialization;
using UnityEngine.Sprites;

namespace UnityEngine.UI
{
	// Token: 0x0200004C RID: 76
	[AddComponentMenu("UI/Image", 11)]
	public class Image : MaskableGraphic, ICanvasRaycastFilter, ISerializationCallbackReceiver, ILayoutElement
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00009C78 File Offset: 0x00007E78
		protected Image()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009CFC File Offset: 0x00007EFC
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00009D04 File Offset: 0x00007F04
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009D20 File Offset: 0x00007F20
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00009D50 File Offset: 0x00007F50
		public Sprite overrideSprite
		{
			get
			{
				return (!(this.m_OverrideSprite == null)) ? this.m_OverrideSprite : this.sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009D6C File Offset: 0x00007F6C
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00009D74 File Offset: 0x00007F74
		public Image.Type type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.Type>(ref this.m_Type, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009D90 File Offset: 0x00007F90
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00009D98 File Offset: 0x00007F98
		public bool preserveAspect
		{
			get
			{
				return this.m_PreserveAspect;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_PreserveAspect, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00009DB4 File Offset: 0x00007FB4
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00009DBC File Offset: 0x00007FBC
		public bool fillCenter
		{
			get
			{
				return this.m_FillCenter;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillCenter, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00009DD8 File Offset: 0x00007FD8
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public Image.FillMethod fillMethod
		{
			get
			{
				return this.m_FillMethod;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.FillMethod>(ref this.m_FillMethod, value))
				{
					this.SetVerticesDirty();
					this.m_FillOrigin = 0;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009E00 File Offset: 0x00008000
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00009E08 File Offset: 0x00008008
		public float fillAmount
		{
			get
			{
				return this.m_FillAmount;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FillAmount, Mathf.Clamp01(value)))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009E28 File Offset: 0x00008028
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00009E30 File Offset: 0x00008030
		public bool fillClockwise
		{
			get
			{
				return this.m_FillClockwise;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillClockwise, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00009E4C File Offset: 0x0000804C
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00009E54 File Offset: 0x00008054
		public int fillOrigin
		{
			get
			{
				return this.m_FillOrigin;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_FillOrigin, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009E70 File Offset: 0x00008070
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00009E78 File Offset: 0x00008078
		public float eventAlphaThreshold
		{
			get
			{
				return this.m_EventAlphaThreshold;
			}
			set
			{
				this.m_EventAlphaThreshold = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009E84 File Offset: 0x00008084
		public override Texture mainTexture
		{
			get
			{
				if (!(this.overrideSprite == null))
				{
					return this.overrideSprite.texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009EE8 File Offset: 0x000080E8
		public bool hasBorder
		{
			get
			{
				return this.overrideSprite != null && this.overrideSprite.border.sqrMagnitude > 0f;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00009F24 File Offset: 0x00008124
		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if (this.sprite)
				{
					num = this.sprite.pixelsPerUnit;
				}
				float num2 = 100f;
				if (base.canvas)
				{
					num2 = base.canvas.referencePixelsPerUnit;
				}
				return num / num2;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009F78 File Offset: 0x00008178
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009F7C File Offset: 0x0000817C
		public virtual void OnAfterDeserialize()
		{
			if (this.m_FillOrigin < 0)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Horizontal && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Vertical && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillOrigin > 3)
			{
				this.m_FillOrigin = 0;
			}
			this.m_FillAmount = Mathf.Clamp(this.m_FillAmount, 0f, 1f);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A018 File Offset: 0x00008218
		private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
		{
			Vector4 vector = (!(this.overrideSprite == null)) ? DataUtility.GetPadding(this.overrideSprite) : Vector4.zero;
			Vector2 vector2 = (!(this.overrideSprite == null)) ? new Vector2(this.overrideSprite.rect.width, this.overrideSprite.rect.height) : Vector2.zero;
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			int num = Mathf.RoundToInt(vector2.x);
			int num2 = Mathf.RoundToInt(vector2.y);
			Vector4 result = new Vector4(vector.x / (float)num, vector.y / (float)num2, ((float)num - vector.z) / (float)num, ((float)num2 - vector.w) / (float)num2);
			if (shouldPreserveAspect && vector2.sqrMagnitude > 0f)
			{
				float num3 = vector2.x / vector2.y;
				float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
				if (num3 > num4)
				{
					float height = pixelAdjustedRect.height;
					pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num3);
					pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * base.rectTransform.pivot.y;
				}
				else
				{
					float width = pixelAdjustedRect.width;
					pixelAdjustedRect.width = pixelAdjustedRect.height * num3;
					pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * base.rectTransform.pivot.x;
				}
			}
			result = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * result.x, pixelAdjustedRect.y + pixelAdjustedRect.height * result.y, pixelAdjustedRect.x + pixelAdjustedRect.width * result.z, pixelAdjustedRect.y + pixelAdjustedRect.height * result.w);
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A230 File Offset: 0x00008430
		public override void SetNativeSize()
		{
			if (this.overrideSprite != null)
			{
				float x = this.overrideSprite.rect.width / this.pixelsPerUnit;
				float y = this.overrideSprite.rect.height / this.pixelsPerUnit;
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2(x, y);
				this.SetAllDirty();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A2B4 File Offset: 0x000084B4
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.overrideSprite == null)
			{
				base.OnPopulateMesh(toFill);
				return;
			}
			switch (this.type)
			{
			case Image.Type.Simple:
				this.GenerateSimpleSprite(toFill, this.m_PreserveAspect);
				break;
			case Image.Type.Sliced:
				this.GenerateSlicedSprite(toFill);
				break;
			case Image.Type.Tiled:
				this.GenerateTiledSprite(toFill);
				break;
			case Image.Type.Filled:
				this.GenerateFilledSprite(toFill, this.m_PreserveAspect);
				break;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A338 File Offset: 0x00008538
		private void GenerateSimpleSprite(VertexHelper vh, bool lPreserveAspect)
		{
			Vector4 drawingDimensions = this.GetDrawingDimensions(lPreserveAspect);
			Vector4 vector = (!(this.overrideSprite != null)) ? Vector4.zero : DataUtility.GetOuterUV(this.overrideSprite);
			Color color = base.color;
			vh.Clear();
			vh.AddVert(new Vector3(drawingDimensions.x, drawingDimensions.y), color, new Vector2(vector.x, vector.y));
			vh.AddVert(new Vector3(drawingDimensions.x, drawingDimensions.w), color, new Vector2(vector.x, vector.w));
			vh.AddVert(new Vector3(drawingDimensions.z, drawingDimensions.w), color, new Vector2(vector.z, vector.w));
			vh.AddVert(new Vector3(drawingDimensions.z, drawingDimensions.y), color, new Vector2(vector.z, vector.y));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A45C File Offset: 0x0000865C
		private void GenerateSlicedSprite(VertexHelper toFill)
		{
			if (!this.hasBorder)
			{
				this.GenerateSimpleSprite(toFill, false);
				return;
			}
			Vector4 vector;
			Vector4 vector2;
			Vector4 a;
			Vector4 a2;
			if (this.overrideSprite != null)
			{
				vector = DataUtility.GetOuterUV(this.overrideSprite);
				vector2 = DataUtility.GetInnerUV(this.overrideSprite);
				a = DataUtility.GetPadding(this.overrideSprite);
				a2 = this.overrideSprite.border;
			}
			else
			{
				vector = Vector4.zero;
				vector2 = Vector4.zero;
				a = Vector4.zero;
				a2 = Vector4.zero;
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			a2 = this.GetAdjustedBorders(a2 / this.pixelsPerUnit, pixelAdjustedRect);
			a /= this.pixelsPerUnit;
			Image.s_VertScratch[0] = new Vector2(a.x, a.y);
			Image.s_VertScratch[3] = new Vector2(pixelAdjustedRect.width - a.z, pixelAdjustedRect.height - a.w);
			Image.s_VertScratch[1].x = a2.x;
			Image.s_VertScratch[1].y = a2.y;
			Image.s_VertScratch[2].x = pixelAdjustedRect.width - a2.z;
			Image.s_VertScratch[2].y = pixelAdjustedRect.height - a2.w;
			for (int i = 0; i < 4; i++)
			{
				Vector2[] array = Image.s_VertScratch;
				int num = i;
				array[num].x = array[num].x + pixelAdjustedRect.x;
				Vector2[] array2 = Image.s_VertScratch;
				int num2 = i;
				array2[num2].y = array2[num2].y + pixelAdjustedRect.y;
			}
			Image.s_UVScratch[0] = new Vector2(vector.x, vector.y);
			Image.s_UVScratch[1] = new Vector2(vector2.x, vector2.y);
			Image.s_UVScratch[2] = new Vector2(vector2.z, vector2.w);
			Image.s_UVScratch[3] = new Vector2(vector.z, vector.w);
			toFill.Clear();
			for (int j = 0; j < 3; j++)
			{
				int num3 = j + 1;
				for (int k = 0; k < 3; k++)
				{
					if (this.m_FillCenter || j != 1 || k != 1)
					{
						int num4 = k + 1;
						Image.AddQuad(toFill, new Vector2(Image.s_VertScratch[j].x, Image.s_VertScratch[k].y), new Vector2(Image.s_VertScratch[num3].x, Image.s_VertScratch[num4].y), base.color, new Vector2(Image.s_UVScratch[j].x, Image.s_UVScratch[k].y), new Vector2(Image.s_UVScratch[num3].x, Image.s_UVScratch[num4].y));
					}
				}
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A7B4 File Offset: 0x000089B4
		private void GenerateTiledSprite(VertexHelper toFill)
		{
			Vector4 vector;
			Vector4 vector2;
			Vector4 a;
			Vector2 vector3;
			if (this.overrideSprite != null)
			{
				vector = DataUtility.GetOuterUV(this.overrideSprite);
				vector2 = DataUtility.GetInnerUV(this.overrideSprite);
				a = this.overrideSprite.border;
				vector3 = this.overrideSprite.rect.size;
			}
			else
			{
				vector = Vector4.zero;
				vector2 = Vector4.zero;
				a = Vector4.zero;
				vector3 = Vector2.one * 100f;
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			float num = (vector3.x - a.x - a.z) / this.pixelsPerUnit;
			float num2 = (vector3.y - a.y - a.w) / this.pixelsPerUnit;
			a = this.GetAdjustedBorders(a / this.pixelsPerUnit, pixelAdjustedRect);
			Vector2 uvMin = new Vector2(vector2.x, vector2.y);
			Vector2 vector4 = new Vector2(vector2.z, vector2.w);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			float x = a.x;
			float num3 = pixelAdjustedRect.width - a.z;
			float y = a.y;
			float num4 = pixelAdjustedRect.height - a.w;
			toFill.Clear();
			Vector2 uvMax = vector4;
			if (num == 0f)
			{
				num = num3 - x;
			}
			if (num2 == 0f)
			{
				num2 = num4 - y;
			}
			if (this.m_FillCenter)
			{
				for (float num5 = y; num5 < num4; num5 += num2)
				{
					float num6 = num5 + num2;
					if (num6 > num4)
					{
						uvMax.y = uvMin.y + (vector4.y - uvMin.y) * (num4 - num5) / (num6 - num5);
						num6 = num4;
					}
					uvMax.x = vector4.x;
					for (float num7 = x; num7 < num3; num7 += num)
					{
						float num8 = num7 + num;
						if (num8 > num3)
						{
							uvMax.x = uvMin.x + (vector4.x - uvMin.x) * (num3 - num7) / (num8 - num7);
							num8 = num3;
						}
						Image.AddQuad(toFill, new Vector2(num7, num5) + pixelAdjustedRect.position, new Vector2(num8, num6) + pixelAdjustedRect.position, base.color, uvMin, uvMax);
					}
				}
			}
			if (this.hasBorder)
			{
				uvMax = vector4;
				for (float num9 = y; num9 < num4; num9 += num2)
				{
					float num10 = num9 + num2;
					if (num10 > num4)
					{
						uvMax.y = uvMin.y + (vector4.y - uvMin.y) * (num4 - num9) / (num10 - num9);
						num10 = num4;
					}
					Image.AddQuad(toFill, new Vector2(0f, num9) + pixelAdjustedRect.position, new Vector2(x, num10) + pixelAdjustedRect.position, base.color, new Vector2(vector.x, uvMin.y), new Vector2(uvMin.x, uvMax.y));
					Image.AddQuad(toFill, new Vector2(num3, num9) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, num10) + pixelAdjustedRect.position, base.color, new Vector2(vector4.x, uvMin.y), new Vector2(vector.z, uvMax.y));
				}
				uvMax = vector4;
				for (float num11 = x; num11 < num3; num11 += num)
				{
					float num12 = num11 + num;
					if (num12 > num3)
					{
						uvMax.x = uvMin.x + (vector4.x - uvMin.x) * (num3 - num11) / (num12 - num11);
						num12 = num3;
					}
					Image.AddQuad(toFill, new Vector2(num11, 0f) + pixelAdjustedRect.position, new Vector2(num12, y) + pixelAdjustedRect.position, base.color, new Vector2(uvMin.x, vector.y), new Vector2(uvMax.x, uvMin.y));
					Image.AddQuad(toFill, new Vector2(num11, num4) + pixelAdjustedRect.position, new Vector2(num12, pixelAdjustedRect.height) + pixelAdjustedRect.position, base.color, new Vector2(uvMin.x, vector4.y), new Vector2(uvMax.x, vector.w));
				}
				Image.AddQuad(toFill, new Vector2(0f, 0f) + pixelAdjustedRect.position, new Vector2(x, y) + pixelAdjustedRect.position, base.color, new Vector2(vector.x, vector.y), new Vector2(uvMin.x, uvMin.y));
				Image.AddQuad(toFill, new Vector2(num3, 0f) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, y) + pixelAdjustedRect.position, base.color, new Vector2(vector4.x, vector.y), new Vector2(vector.z, uvMin.y));
				Image.AddQuad(toFill, new Vector2(0f, num4) + pixelAdjustedRect.position, new Vector2(x, pixelAdjustedRect.height) + pixelAdjustedRect.position, base.color, new Vector2(vector.x, vector4.y), new Vector2(uvMin.x, vector.w));
				Image.AddQuad(toFill, new Vector2(num3, num4) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, pixelAdjustedRect.height) + pixelAdjustedRect.position, base.color, new Vector2(vector4.x, vector4.y), new Vector2(vector.z, vector.w));
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AE38 File Offset: 0x00009038
		private static void AddQuad(VertexHelper vertexHelper, Vector3[] quadPositions, Color32 color, Vector3[] quadUVs)
		{
			int currentVertCount = vertexHelper.currentVertCount;
			for (int i = 0; i < 4; i++)
			{
				vertexHelper.AddVert(quadPositions[i], color, quadUVs[i]);
			}
			vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AE9C File Offset: 0x0000909C
		private static void AddQuad(VertexHelper vertexHelper, Vector2 posMin, Vector2 posMax, Color32 color, Vector2 uvMin, Vector2 uvMax)
		{
			int currentVertCount = vertexHelper.currentVertCount;
			vertexHelper.AddVert(new Vector3(posMin.x, posMin.y, 0f), color, new Vector2(uvMin.x, uvMin.y));
			vertexHelper.AddVert(new Vector3(posMin.x, posMax.y, 0f), color, new Vector2(uvMin.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMax.y, 0f), color, new Vector2(uvMax.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMin.y, 0f), color, new Vector2(uvMax.x, uvMin.y));
			vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000AF94 File Offset: 0x00009194
		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					ref Vector4 ptr = ref border;
					int index2;
					int index = index2 = i;
					float num3 = ptr[index2];
					border[index] = num3 * num2;
					ref Vector4 ptr2 = ref border;
					int index3 = index2 = i + 2;
					num3 = ptr2[index2];
					border[index3] = num3 * num2;
				}
			}
			return border;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B03C File Offset: 0x0000923C
		private void GenerateFilledSprite(VertexHelper toFill, bool preserveAspect)
		{
			toFill.Clear();
			if (this.m_FillAmount < 0.001f)
			{
				return;
			}
			Vector4 drawingDimensions = this.GetDrawingDimensions(preserveAspect);
			Vector4 vector = (!(this.overrideSprite != null)) ? Vector4.zero : DataUtility.GetOuterUV(this.overrideSprite);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			float num = vector.x;
			float num2 = vector.y;
			float num3 = vector.z;
			float num4 = vector.w;
			if (this.m_FillMethod == Image.FillMethod.Horizontal || this.m_FillMethod == Image.FillMethod.Vertical)
			{
				if (this.fillMethod == Image.FillMethod.Horizontal)
				{
					float num5 = (num3 - num) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * this.m_FillAmount;
						num = num3 - num5;
					}
					else
					{
						drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * this.m_FillAmount;
						num3 = num + num5;
					}
				}
				else if (this.fillMethod == Image.FillMethod.Vertical)
				{
					float num6 = (num4 - num2) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * this.m_FillAmount;
						num2 = num4 - num6;
					}
					else
					{
						drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * this.m_FillAmount;
						num4 = num2 + num6;
					}
				}
			}
			Image.s_Xy[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
			Image.s_Xy[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
			Image.s_Xy[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
			Image.s_Xy[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
			Image.s_Uv[0] = new Vector2(num, num2);
			Image.s_Uv[1] = new Vector2(num, num4);
			Image.s_Uv[2] = new Vector2(num3, num4);
			Image.s_Uv[3] = new Vector2(num3, num2);
			if (this.m_FillAmount < 1f && this.m_FillMethod != Image.FillMethod.Horizontal && this.m_FillMethod != Image.FillMethod.Vertical)
			{
				if (this.fillMethod == Image.FillMethod.Radial90)
				{
					if (Image.RadialCut(Image.s_Xy, Image.s_Uv, this.m_FillAmount, this.m_FillClockwise, this.m_FillOrigin))
					{
						Image.AddQuad(toFill, Image.s_Xy, base.color, Image.s_Uv);
					}
				}
				else if (this.fillMethod == Image.FillMethod.Radial180)
				{
					for (int i = 0; i < 2; i++)
					{
						int num7 = (this.m_FillOrigin <= 1) ? 0 : 1;
						float t;
						float t2;
						float t3;
						float t4;
						if (this.m_FillOrigin == 0 || this.m_FillOrigin == 2)
						{
							t = 0f;
							t2 = 1f;
							if (i == num7)
							{
								t3 = 0f;
								t4 = 0.5f;
							}
							else
							{
								t3 = 0.5f;
								t4 = 1f;
							}
						}
						else
						{
							t3 = 0f;
							t4 = 1f;
							if (i == num7)
							{
								t = 0.5f;
								t2 = 1f;
							}
							else
							{
								t = 0f;
								t2 = 0.5f;
							}
						}
						Image.s_Xy[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t3);
						Image.s_Xy[1].x = Image.s_Xy[0].x;
						Image.s_Xy[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t4);
						Image.s_Xy[3].x = Image.s_Xy[2].x;
						Image.s_Xy[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t);
						Image.s_Xy[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t2);
						Image.s_Xy[2].y = Image.s_Xy[1].y;
						Image.s_Xy[3].y = Image.s_Xy[0].y;
						Image.s_Uv[0].x = Mathf.Lerp(num, num3, t3);
						Image.s_Uv[1].x = Image.s_Uv[0].x;
						Image.s_Uv[2].x = Mathf.Lerp(num, num3, t4);
						Image.s_Uv[3].x = Image.s_Uv[2].x;
						Image.s_Uv[0].y = Mathf.Lerp(num2, num4, t);
						Image.s_Uv[1].y = Mathf.Lerp(num2, num4, t2);
						Image.s_Uv[2].y = Image.s_Uv[1].y;
						Image.s_Uv[3].y = Image.s_Uv[0].y;
						float value = (!this.m_FillClockwise) ? (this.m_FillAmount * 2f - (float)(1 - i)) : (this.fillAmount * 2f - (float)i);
						if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(value), this.m_FillClockwise, (i + this.m_FillOrigin + 3) % 4))
						{
							Image.AddQuad(toFill, Image.s_Xy, base.color, Image.s_Uv);
						}
					}
				}
				else if (this.fillMethod == Image.FillMethod.Radial360)
				{
					for (int j = 0; j < 4; j++)
					{
						float t5;
						float t6;
						if (j < 2)
						{
							t5 = 0f;
							t6 = 0.5f;
						}
						else
						{
							t5 = 0.5f;
							t6 = 1f;
						}
						float t7;
						float t8;
						if (j == 0 || j == 3)
						{
							t7 = 0f;
							t8 = 0.5f;
						}
						else
						{
							t7 = 0.5f;
							t8 = 1f;
						}
						Image.s_Xy[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t5);
						Image.s_Xy[1].x = Image.s_Xy[0].x;
						Image.s_Xy[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t6);
						Image.s_Xy[3].x = Image.s_Xy[2].x;
						Image.s_Xy[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t7);
						Image.s_Xy[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t8);
						Image.s_Xy[2].y = Image.s_Xy[1].y;
						Image.s_Xy[3].y = Image.s_Xy[0].y;
						Image.s_Uv[0].x = Mathf.Lerp(num, num3, t5);
						Image.s_Uv[1].x = Image.s_Uv[0].x;
						Image.s_Uv[2].x = Mathf.Lerp(num, num3, t6);
						Image.s_Uv[3].x = Image.s_Uv[2].x;
						Image.s_Uv[0].y = Mathf.Lerp(num2, num4, t7);
						Image.s_Uv[1].y = Mathf.Lerp(num2, num4, t8);
						Image.s_Uv[2].y = Image.s_Uv[1].y;
						Image.s_Uv[3].y = Image.s_Uv[0].y;
						float value2 = (!this.m_FillClockwise) ? (this.m_FillAmount * 4f - (float)(3 - (j + this.m_FillOrigin) % 4)) : (this.m_FillAmount * 4f - (float)((j + this.m_FillOrigin) % 4));
						if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(value2), this.m_FillClockwise, (j + 2) % 4))
						{
							Image.AddQuad(toFill, Image.s_Xy, base.color, Image.s_Uv);
						}
					}
				}
			}
			else
			{
				Image.AddQuad(toFill, Image.s_Xy, base.color, Image.s_Uv);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private static bool RadialCut(Vector3[] xy, Vector3[] uv, float fill, bool invert, int corner)
		{
			if (fill < 0.001f)
			{
				return false;
			}
			if ((corner & 1) == 1)
			{
				invert = !invert;
			}
			if (!invert && fill > 0.999f)
			{
				return true;
			}
			float num = Mathf.Clamp01(fill);
			if (invert)
			{
				num = 1f - num;
			}
			num *= 1.5707964f;
			float cos = Mathf.Cos(num);
			float sin = Mathf.Sin(num);
			Image.RadialCut(xy, cos, sin, invert, corner);
			Image.RadialCut(uv, cos, sin, invert, corner);
			return true;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BA5C File Offset: 0x00009C5C
		private static void RadialCut(Vector3[] xy, float cos, float sin, bool invert, int corner)
		{
			int num = (corner + 1) % 4;
			int num2 = (corner + 2) % 4;
			int num3 = (corner + 3) % 4;
			if ((corner & 1) == 1)
			{
				if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num2].x = xy[num].x;
					}
				}
				else if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num3].y = xy[num2].y;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (!invert)
				{
					xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
				else
				{
					xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
			}
			else
			{
				if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num2].y = xy[num].y;
					}
				}
				else if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num3].x = xy[num2].x;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (invert)
				{
					xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
				else
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public virtual float preferredWidth
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.overrideSprite).x / this.pixelsPerUnit;
				}
				return this.overrideSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BD78 File Offset: 0x00009F78
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BD80 File Offset: 0x00009F80
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000BD88 File Offset: 0x00009F88
		public virtual float preferredHeight
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.overrideSprite).y / this.pixelsPerUnit;
				}
				return this.overrideSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000BE04 File Offset: 0x0000A004
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000BE10 File Offset: 0x0000A010
		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.m_EventAlphaThreshold >= 1f)
			{
				return true;
			}
			Sprite overrideSprite = this.overrideSprite;
			if (overrideSprite == null)
			{
				return true;
			}
			Vector2 local;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out local);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			local.x += base.rectTransform.pivot.x * pixelAdjustedRect.width;
			local.y += base.rectTransform.pivot.y * pixelAdjustedRect.height;
			local = this.MapCoordinate(local, pixelAdjustedRect);
			Rect textureRect = overrideSprite.textureRect;
			Vector2 vector = new Vector2(local.x / textureRect.width, local.y / textureRect.height);
			float u = Mathf.Lerp(textureRect.x, textureRect.xMax, vector.x) / (float)overrideSprite.texture.width;
			float v = Mathf.Lerp(textureRect.y, textureRect.yMax, vector.y) / (float)overrideSprite.texture.height;
			bool result;
			try
			{
				result = (overrideSprite.texture.GetPixelBilinear(u, v).a >= this.m_EventAlphaThreshold);
			}
			catch (UnityException ex)
			{
				Debug.LogError("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				result = true;
			}
			return result;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect rect2 = this.sprite.rect;
			if (this.type == Image.Type.Simple || this.type == Image.Type.Filled)
			{
				return new Vector2(local.x * rect2.width / rect.width, local.y * rect2.height / rect.height);
			}
			Vector4 border = this.sprite.border;
			Vector4 adjustedBorders = this.GetAdjustedBorders(border / this.pixelsPerUnit, rect);
			for (int i = 0; i < 2; i++)
			{
				if (local[i] > adjustedBorders[i])
				{
					if (rect.size[i] - local[i] <= adjustedBorders[i + 2])
					{
						ref Vector2 ptr = ref local;
						int index2;
						int index = index2 = i;
						float num = ptr[index2];
						local[index] = num - (rect.size[i] - rect2.size[i]);
					}
					else if (this.type == Image.Type.Sliced)
					{
						float t = Mathf.InverseLerp(adjustedBorders[i], rect.size[i] - adjustedBorders[i + 2], local[i]);
						local[i] = Mathf.Lerp(border[i], rect2.size[i] - border[i + 2], t);
					}
					else
					{
						ref Vector2 ptr2 = ref local;
						int index2;
						int index3 = index2 = i;
						float num = ptr2[index2];
						local[index3] = num - adjustedBorders[i];
						local[i] = Mathf.Repeat(local[i], rect2.size[i] - border[i] - border[i + 2]);
						ref Vector2 ptr3 = ref local;
						int index4 = index2 = i;
						num = ptr3[index2];
						local[index4] = num + border[i];
					}
				}
			}
			return local;
		}

		// Token: 0x04000110 RID: 272
		[SerializeField]
		[FormerlySerializedAs("m_Frame")]
		private Sprite m_Sprite;

		// Token: 0x04000111 RID: 273
		[NonSerialized]
		private Sprite m_OverrideSprite;

		// Token: 0x04000112 RID: 274
		[SerializeField]
		private Image.Type m_Type;

		// Token: 0x04000113 RID: 275
		[SerializeField]
		private bool m_PreserveAspect;

		// Token: 0x04000114 RID: 276
		[SerializeField]
		private bool m_FillCenter = true;

		// Token: 0x04000115 RID: 277
		[SerializeField]
		private Image.FillMethod m_FillMethod = Image.FillMethod.Radial360;

		// Token: 0x04000116 RID: 278
		[Range(0f, 1f)]
		[SerializeField]
		private float m_FillAmount = 1f;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		private bool m_FillClockwise = true;

		// Token: 0x04000118 RID: 280
		[SerializeField]
		private int m_FillOrigin;

		// Token: 0x04000119 RID: 281
		private float m_EventAlphaThreshold = 1f;

		// Token: 0x0400011A RID: 282
		private static readonly Vector2[] s_VertScratch = new Vector2[4];

		// Token: 0x0400011B RID: 283
		private static readonly Vector2[] s_UVScratch = new Vector2[4];

		// Token: 0x0400011C RID: 284
		private static readonly Vector3[] s_Xy = new Vector3[4];

		// Token: 0x0400011D RID: 285
		private static readonly Vector3[] s_Uv = new Vector3[4];

		// Token: 0x0200004D RID: 77
		public enum Type
		{
			// Token: 0x0400011F RID: 287
			Simple,
			// Token: 0x04000120 RID: 288
			Sliced,
			// Token: 0x04000121 RID: 289
			Tiled,
			// Token: 0x04000122 RID: 290
			Filled
		}

		// Token: 0x0200004E RID: 78
		public enum FillMethod
		{
			// Token: 0x04000124 RID: 292
			Horizontal,
			// Token: 0x04000125 RID: 293
			Vertical,
			// Token: 0x04000126 RID: 294
			Radial90,
			// Token: 0x04000127 RID: 295
			Radial180,
			// Token: 0x04000128 RID: 296
			Radial360
		}

		// Token: 0x0200004F RID: 79
		public enum OriginHorizontal
		{
			// Token: 0x0400012A RID: 298
			Left,
			// Token: 0x0400012B RID: 299
			Right
		}

		// Token: 0x02000050 RID: 80
		public enum OriginVertical
		{
			// Token: 0x0400012D RID: 301
			Bottom,
			// Token: 0x0400012E RID: 302
			Top
		}

		// Token: 0x02000051 RID: 81
		public enum Origin90
		{
			// Token: 0x04000130 RID: 304
			BottomLeft,
			// Token: 0x04000131 RID: 305
			TopLeft,
			// Token: 0x04000132 RID: 306
			TopRight,
			// Token: 0x04000133 RID: 307
			BottomRight
		}

		// Token: 0x02000052 RID: 82
		public enum Origin180
		{
			// Token: 0x04000135 RID: 309
			Bottom,
			// Token: 0x04000136 RID: 310
			Left,
			// Token: 0x04000137 RID: 311
			Top,
			// Token: 0x04000138 RID: 312
			Right
		}

		// Token: 0x02000053 RID: 83
		public enum Origin360
		{
			// Token: 0x0400013A RID: 314
			Bottom,
			// Token: 0x0400013B RID: 315
			Right,
			// Token: 0x0400013C RID: 316
			Top,
			// Token: 0x0400013D RID: 317
			Left
		}
	}
}
