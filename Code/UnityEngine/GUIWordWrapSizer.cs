using System;

namespace UnityEngine
{
	// Token: 0x02000204 RID: 516
	internal sealed class GUIWordWrapSizer : GUILayoutEntry
	{
		// Token: 0x06001FDA RID: 8154 RVA: 0x00024B74 File Offset: 0x00022D74
		public GUIWordWrapSizer(GUIStyle style, GUIContent content, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, style)
		{
			this.m_Content = new GUIContent(content);
			this.ApplyOptions(options);
			this.m_ForcedMinHeight = this.minHeight;
			this.m_ForcedMaxHeight = this.maxHeight;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00024BC8 File Offset: 0x00022DC8
		public override void CalcWidth()
		{
			if (this.minWidth == 0f || this.maxWidth == 0f)
			{
				float minWidth;
				float maxWidth;
				base.style.CalcMinMaxWidth(this.m_Content, out minWidth, out maxWidth);
				if (this.minWidth == 0f)
				{
					this.minWidth = minWidth;
				}
				if (this.maxWidth == 0f)
				{
					this.maxWidth = maxWidth;
				}
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00024C38 File Offset: 0x00022E38
		public override void CalcHeight()
		{
			if (this.m_ForcedMinHeight == 0f || this.m_ForcedMaxHeight == 0f)
			{
				float num = base.style.CalcHeight(this.m_Content, this.rect.width);
				if (this.m_ForcedMinHeight == 0f)
				{
					this.minHeight = num;
				}
				else
				{
					this.minHeight = this.m_ForcedMinHeight;
				}
				if (this.m_ForcedMaxHeight == 0f)
				{
					this.maxHeight = num;
				}
				else
				{
					this.maxHeight = this.m_ForcedMaxHeight;
				}
			}
		}

		// Token: 0x040007D8 RID: 2008
		private readonly GUIContent m_Content;

		// Token: 0x040007D9 RID: 2009
		private readonly float m_ForcedMinHeight;

		// Token: 0x040007DA RID: 2010
		private readonly float m_ForcedMaxHeight;
	}
}
