using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200007A RID: 122
	[AddComponentMenu("UI/Text", 10)]
	public class Text : MaskableGraphic, ILayoutElement
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00014B50 File Offset: 0x00012D50
		protected Text()
		{
			base.useLegacyMeshGeneration = false;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00014B88 File Offset: 0x00012D88
		public TextGenerator cachedTextGenerator
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCache) == null)
				{
					result = (this.m_TextCache = ((this.m_Text.Length == 0) ? new TextGenerator() : new TextGenerator(this.m_Text.Length)));
				}
				return result;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00014BD8 File Offset: 0x00012DD8
		public TextGenerator cachedTextGeneratorForLayout
		{
			get
			{
				TextGenerator result;
				if ((result = this.m_TextCacheForLayout) == null)
				{
					result = (this.m_TextCacheForLayout = new TextGenerator());
				}
				return result;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00014C00 File Offset: 0x00012E00
		public override Texture mainTexture
		{
			get
			{
				if (this.font != null && this.font.material != null && this.font.material.mainTexture != null)
				{
					return this.font.material.mainTexture;
				}
				if (this.m_Material != null)
				{
					return this.m_Material.mainTexture;
				}
				return base.mainTexture;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014C84 File Offset: 0x00012E84
		public void FontTextureChanged()
		{
			if (!this)
			{
				FontUpdateTracker.UntrackText(this);
				return;
			}
			if (this.m_DisableFontTextureRebuiltCallback)
			{
				return;
			}
			this.cachedTextGenerator.Invalidate();
			if (!this.IsActive())
			{
				return;
			}
			if (CanvasUpdateRegistry.IsRebuildingGraphics() || CanvasUpdateRegistry.IsRebuildingLayout())
			{
				this.UpdateGeometry();
			}
			else
			{
				this.SetAllDirty();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00014CEC File Offset: 0x00012EEC
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00014CFC File Offset: 0x00012EFC
		public Font font
		{
			get
			{
				return this.m_FontData.font;
			}
			set
			{
				if (this.m_FontData.font == value)
				{
					return;
				}
				FontUpdateTracker.UntrackText(this);
				this.m_FontData.font = value;
				FontUpdateTracker.TrackText(this);
				this.SetAllDirty();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00014D40 File Offset: 0x00012F40
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00014D48 File Offset: 0x00012F48
		public virtual string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					if (string.IsNullOrEmpty(this.m_Text))
					{
						return;
					}
					this.m_Text = string.Empty;
					this.SetVerticesDirty();
				}
				else if (this.m_Text != value)
				{
					this.m_Text = value;
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00014DAC File Offset: 0x00012FAC
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00014DBC File Offset: 0x00012FBC
		public bool supportRichText
		{
			get
			{
				return this.m_FontData.richText;
			}
			set
			{
				if (this.m_FontData.richText == value)
				{
					return;
				}
				this.m_FontData.richText = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00014DF4 File Offset: 0x00012FF4
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00014E04 File Offset: 0x00013004
		public bool resizeTextForBestFit
		{
			get
			{
				return this.m_FontData.bestFit;
			}
			set
			{
				if (this.m_FontData.bestFit == value)
				{
					return;
				}
				this.m_FontData.bestFit = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00014E3C File Offset: 0x0001303C
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00014E4C File Offset: 0x0001304C
		public int resizeTextMinSize
		{
			get
			{
				return this.m_FontData.minSize;
			}
			set
			{
				if (this.m_FontData.minSize == value)
				{
					return;
				}
				this.m_FontData.minSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00014E84 File Offset: 0x00013084
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00014E94 File Offset: 0x00013094
		public int resizeTextMaxSize
		{
			get
			{
				return this.m_FontData.maxSize;
			}
			set
			{
				if (this.m_FontData.maxSize == value)
				{
					return;
				}
				this.m_FontData.maxSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00014ECC File Offset: 0x000130CC
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00014EDC File Offset: 0x000130DC
		public TextAnchor alignment
		{
			get
			{
				return this.m_FontData.alignment;
			}
			set
			{
				if (this.m_FontData.alignment == value)
				{
					return;
				}
				this.m_FontData.alignment = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00014F14 File Offset: 0x00013114
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00014F24 File Offset: 0x00013124
		public bool alignByGeometry
		{
			get
			{
				return this.m_FontData.alignByGeometry;
			}
			set
			{
				if (this.m_FontData.alignByGeometry == value)
				{
					return;
				}
				this.m_FontData.alignByGeometry = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00014F58 File Offset: 0x00013158
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00014F68 File Offset: 0x00013168
		public int fontSize
		{
			get
			{
				return this.m_FontData.fontSize;
			}
			set
			{
				if (this.m_FontData.fontSize == value)
				{
					return;
				}
				this.m_FontData.fontSize = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00014FA0 File Offset: 0x000131A0
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00014FB0 File Offset: 0x000131B0
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_FontData.horizontalOverflow;
			}
			set
			{
				if (this.m_FontData.horizontalOverflow == value)
				{
					return;
				}
				this.m_FontData.horizontalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00014FE8 File Offset: 0x000131E8
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00014FF8 File Offset: 0x000131F8
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_FontData.verticalOverflow;
			}
			set
			{
				if (this.m_FontData.verticalOverflow == value)
				{
					return;
				}
				this.m_FontData.verticalOverflow = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00015030 File Offset: 0x00013230
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00015040 File Offset: 0x00013240
		public float lineSpacing
		{
			get
			{
				return this.m_FontData.lineSpacing;
			}
			set
			{
				if (this.m_FontData.lineSpacing == value)
				{
					return;
				}
				this.m_FontData.lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00015078 File Offset: 0x00013278
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00015088 File Offset: 0x00013288
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontData.fontStyle;
			}
			set
			{
				if (this.m_FontData.fontStyle == value)
				{
					return;
				}
				this.m_FontData.fontStyle = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000150C0 File Offset: 0x000132C0
		public float pixelsPerUnit
		{
			get
			{
				Canvas canvas = base.canvas;
				if (!canvas)
				{
					return 1f;
				}
				if (!this.font || this.font.dynamic)
				{
					return canvas.scaleFactor;
				}
				if (this.m_FontData.fontSize <= 0 || this.font.fontSize <= 0)
				{
					return 1f;
				}
				return (float)this.font.fontSize / (float)this.m_FontData.fontSize;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00015150 File Offset: 0x00013350
		protected override void OnEnable()
		{
			base.OnEnable();
			this.cachedTextGenerator.Invalidate();
			FontUpdateTracker.TrackText(this);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001516C File Offset: 0x0001336C
		protected override void OnDisable()
		{
			FontUpdateTracker.UntrackText(this);
			base.OnDisable();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001517C File Offset: 0x0001337C
		protected override void UpdateGeometry()
		{
			if (this.font != null)
			{
				base.UpdateGeometry();
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015198 File Offset: 0x00013398
		public TextGenerationSettings GetGenerationSettings(Vector2 extents)
		{
			TextGenerationSettings result = default(TextGenerationSettings);
			result.generationExtents = extents;
			if (this.font != null && this.font.dynamic)
			{
				result.fontSize = this.m_FontData.fontSize;
				result.resizeTextMinSize = this.m_FontData.minSize;
				result.resizeTextMaxSize = this.m_FontData.maxSize;
			}
			result.textAnchor = this.m_FontData.alignment;
			result.alignByGeometry = this.m_FontData.alignByGeometry;
			result.scaleFactor = this.pixelsPerUnit;
			result.color = base.color;
			result.font = this.font;
			result.pivot = base.rectTransform.pivot;
			result.richText = this.m_FontData.richText;
			result.lineSpacing = this.m_FontData.lineSpacing;
			result.fontStyle = this.m_FontData.fontStyle;
			result.resizeTextForBestFit = this.m_FontData.bestFit;
			result.updateBounds = false;
			result.horizontalOverflow = this.m_FontData.horizontalOverflow;
			result.verticalOverflow = this.m_FontData.verticalOverflow;
			return result;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000152E0 File Offset: 0x000134E0
		public static Vector2 GetTextAnchorPivot(TextAnchor anchor)
		{
			switch (anchor)
			{
			case TextAnchor.UpperLeft:
				return new Vector2(0f, 1f);
			case TextAnchor.UpperCenter:
				return new Vector2(0.5f, 1f);
			case TextAnchor.UpperRight:
				return new Vector2(1f, 1f);
			case TextAnchor.MiddleLeft:
				return new Vector2(0f, 0.5f);
			case TextAnchor.MiddleCenter:
				return new Vector2(0.5f, 0.5f);
			case TextAnchor.MiddleRight:
				return new Vector2(1f, 0.5f);
			case TextAnchor.LowerLeft:
				return new Vector2(0f, 0f);
			case TextAnchor.LowerCenter:
				return new Vector2(0.5f, 0f);
			case TextAnchor.LowerRight:
				return new Vector2(1f, 0f);
			default:
				return Vector2.zero;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000153B4 File Offset: 0x000135B4
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (this.font == null)
			{
				return;
			}
			this.m_DisableFontTextureRebuiltCallback = true;
			Vector2 size = base.rectTransform.rect.size;
			TextGenerationSettings generationSettings = this.GetGenerationSettings(size);
			this.cachedTextGenerator.Populate(this.text, generationSettings);
			Rect rect = base.rectTransform.rect;
			Vector2 textAnchorPivot = Text.GetTextAnchorPivot(this.m_FontData.alignment);
			Vector2 zero = Vector2.zero;
			zero.x = Mathf.Lerp(rect.xMin, rect.xMax, textAnchorPivot.x);
			zero.y = Mathf.Lerp(rect.yMin, rect.yMax, textAnchorPivot.y);
			Vector2 lhs = base.PixelAdjustPoint(zero) - zero;
			IList<UIVertex> verts = this.cachedTextGenerator.verts;
			float d = 1f / this.pixelsPerUnit;
			int num = verts.Count - 4;
			toFill.Clear();
			if (lhs != Vector2.zero)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = i & 3;
					this.m_TempVerts[num2] = verts[i];
					UIVertex[] tempVerts = this.m_TempVerts;
					int num3 = num2;
					tempVerts[num3].position = tempVerts[num3].position * d;
					UIVertex[] tempVerts2 = this.m_TempVerts;
					int num4 = num2;
					tempVerts2[num4].position.x = tempVerts2[num4].position.x + lhs.x;
					UIVertex[] tempVerts3 = this.m_TempVerts;
					int num5 = num2;
					tempVerts3[num5].position.y = tempVerts3[num5].position.y + lhs.y;
					if (num2 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					int num6 = j & 3;
					this.m_TempVerts[num6] = verts[j];
					UIVertex[] tempVerts4 = this.m_TempVerts;
					int num7 = num6;
					tempVerts4[num7].position = tempVerts4[num7].position * d;
					if (num6 == 3)
					{
						toFill.AddUIVertexQuad(this.m_TempVerts);
					}
				}
			}
			this.m_DisableFontTextureRebuiltCallback = false;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000155EC File Offset: 0x000137EC
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000155F0 File Offset: 0x000137F0
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000155F4 File Offset: 0x000137F4
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000155FC File Offset: 0x000137FC
		public virtual float preferredWidth
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(Vector2.zero);
				return this.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00015630 File Offset: 0x00013830
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00015638 File Offset: 0x00013838
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00015640 File Offset: 0x00013840
		public virtual float preferredHeight
		{
			get
			{
				TextGenerationSettings generationSettings = this.GetGenerationSettings(new Vector2(base.rectTransform.rect.size.x, 0f));
				return this.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00015694 File Offset: 0x00013894
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001569C File Offset: 0x0001389C
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000229 RID: 553
		[SerializeField]
		private FontData m_FontData = FontData.defaultFontData;

		// Token: 0x0400022A RID: 554
		[SerializeField]
		[TextArea(3, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x0400022B RID: 555
		private TextGenerator m_TextCache;

		// Token: 0x0400022C RID: 556
		private TextGenerator m_TextCacheForLayout;

		// Token: 0x0400022D RID: 557
		protected static Material s_DefaultText;

		// Token: 0x0400022E RID: 558
		[NonSerialized]
		protected bool m_DisableFontTextureRebuiltCallback;

		// Token: 0x0400022F RID: 559
		private readonly UIVertex[] m_TempVerts = new UIVertex[4];
	}
}
