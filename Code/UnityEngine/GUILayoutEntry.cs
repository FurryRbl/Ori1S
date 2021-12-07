using System;

namespace UnityEngine
{
	// Token: 0x020001FF RID: 511
	internal class GUILayoutEntry
	{
		// Token: 0x06001FB4 RID: 8116 RVA: 0x0002268C File Offset: 0x0002088C
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			if (_style == null)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000226FC File Offset: 0x000208FC
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0002279C File Offset: 0x0002099C
		// (set) Token: 0x06001FB8 RID: 8120 RVA: 0x000227A4 File Offset: 0x000209A4
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x000227B4 File Offset: 0x000209B4
		public virtual RectOffset margin
		{
			get
			{
				return this.style.margin;
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000227C4 File Offset: 0x000209C4
		public virtual void CalcWidth()
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000227C8 File Offset: 0x000209C8
		public virtual void CalcHeight()
		{
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000227CC File Offset: 0x000209CC
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000227E8 File Offset: 0x000209E8
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00022804 File Offset: 0x00020A04
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth != 0f || !style.stretchWidth) ? 0 : 1);
			this.stretchHeight = ((style.fixedHeight != 0f || !style.stretchHeight) ? 0 : 1);
			this.m_Style = style;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00022868 File Offset: 0x00020A68
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
					this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.fixedHeight:
					this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.minWidth:
					this.minWidth = (float)guilayoutOption.value;
					if (this.maxWidth < this.minWidth)
					{
						this.maxWidth = this.minWidth;
					}
					break;
				case GUILayoutOption.Type.maxWidth:
					this.maxWidth = (float)guilayoutOption.value;
					if (this.minWidth > this.maxWidth)
					{
						this.minWidth = this.maxWidth;
					}
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.minHeight:
					this.minHeight = (float)guilayoutOption.value;
					if (this.maxHeight < this.minHeight)
					{
						this.maxHeight = this.minHeight;
					}
					break;
				case GUILayoutOption.Type.maxHeight:
					this.maxHeight = (float)guilayoutOption.value;
					if (this.minHeight > this.maxHeight)
					{
						this.minHeight = this.maxHeight;
					}
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.stretchWidth:
					this.stretchWidth = (int)guilayoutOption.value;
					break;
				case GUILayoutOption.Type.stretchHeight:
					this.stretchHeight = (int)guilayoutOption.value;
					break;
				}
			}
			if (this.maxWidth != 0f && this.maxWidth < this.minWidth)
			{
				this.maxWidth = this.minWidth;
			}
			if (this.maxHeight != 0f && this.maxHeight < this.minHeight)
			{
				this.maxHeight = this.minHeight;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00022A78 File Offset: 0x00020C78
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new object[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style == null) ? "NULL" : this.style.name,
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth,
				"-",
				this.maxWidth,
				(this.stretchWidth == 0) ? string.Empty : "+",
				", H: ",
				this.minHeight,
				"-",
				this.maxHeight,
				(this.stretchHeight == 0) ? string.Empty : "+"
			});
		}

		// Token: 0x040007AA RID: 1962
		public float minWidth;

		// Token: 0x040007AB RID: 1963
		public float maxWidth;

		// Token: 0x040007AC RID: 1964
		public float minHeight;

		// Token: 0x040007AD RID: 1965
		public float maxHeight;

		// Token: 0x040007AE RID: 1966
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040007AF RID: 1967
		public int stretchWidth;

		// Token: 0x040007B0 RID: 1968
		public int stretchHeight;

		// Token: 0x040007B1 RID: 1969
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x040007B2 RID: 1970
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040007B3 RID: 1971
		protected static int indent = 0;
	}
}
