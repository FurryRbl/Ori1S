using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000201 RID: 513
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x00024278 File Offset: 0x00022478
		[RequiredByNativeCode]
		public GUIScrollGroup()
		{
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00024290 File Offset: 0x00022490
		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				if (this.minWidth > 32f)
				{
					this.minWidth = 32f;
				}
				if (minWidth != 0f)
				{
					this.minWidth = minWidth;
				}
				if (maxWidth != 0f)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0002433C File Offset: 0x0002253C
		public override void SetHorizontal(float x, float width)
		{
			float num = (!this.needsVerticalScrollbar) ? width : (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left);
			if (this.allowHorizontalScroll && num < this.calcMinWidth)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				if (this.allowHorizontalScroll)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x00024420 File Offset: 0x00022620
		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			if (this.allowVerticalScroll)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			if (this.needsHorizontalScrollbar)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			if (this.allowVerticalScroll)
			{
				if (this.minHeight > 32f)
				{
					this.minHeight = 32f;
				}
				if (minHeight != 0f)
				{
					this.minHeight = minHeight;
				}
				if (maxHeight != 0f)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x00024510 File Offset: 0x00022710
		public override void SetVertical(float y, float height)
		{
			float num = height;
			if (this.needsHorizontalScrollbar)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			if (this.allowVerticalScroll && num < this.calcMinHeight)
			{
				if (!this.needsHorizontalScrollbar && !this.needsVerticalScrollbar)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					if (this.clientWidth < this.calcMinWidth)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				if (this.allowVerticalScroll)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}

		// Token: 0x040007C5 RID: 1989
		public float calcMinWidth;

		// Token: 0x040007C6 RID: 1990
		public float calcMaxWidth;

		// Token: 0x040007C7 RID: 1991
		public float calcMinHeight;

		// Token: 0x040007C8 RID: 1992
		public float calcMaxHeight;

		// Token: 0x040007C9 RID: 1993
		public float clientWidth;

		// Token: 0x040007CA RID: 1994
		public float clientHeight;

		// Token: 0x040007CB RID: 1995
		public bool allowHorizontalScroll = true;

		// Token: 0x040007CC RID: 1996
		public bool allowVerticalScroll = true;

		// Token: 0x040007CD RID: 1997
		public bool needsHorizontalScrollbar;

		// Token: 0x040007CE RID: 1998
		public bool needsVerticalScrollbar;

		// Token: 0x040007CF RID: 1999
		public GUIStyle horizontalScrollbar;

		// Token: 0x040007D0 RID: 2000
		public GUIStyle verticalScrollbar;
	}
}
