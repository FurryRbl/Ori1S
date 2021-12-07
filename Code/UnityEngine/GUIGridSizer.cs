using System;

namespace UnityEngine
{
	// Token: 0x02000203 RID: 515
	internal sealed class GUIGridSizer : GUILayoutEntry
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x000246F4 File Offset: 0x000228F4
		private GUIGridSizer(GUIContent[] contents, int xCount, GUIStyle buttonStyle, GUILayoutOption[] options) : base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.m_Count = contents.Length;
			this.m_XCount = xCount;
			this.ApplyStyleSettings(buttonStyle);
			this.ApplyOptions(options);
			if (xCount == 0 || contents.Length == 0)
			{
				return;
			}
			float num = (float)(Mathf.Max(buttonStyle.margin.left, buttonStyle.margin.right) * (this.m_XCount - 1));
			float num2 = (float)(Mathf.Max(buttonStyle.margin.top, buttonStyle.margin.bottom) * (this.rows - 1));
			if (buttonStyle.fixedWidth != 0f)
			{
				this.m_MinButtonWidth = (this.m_MaxButtonWidth = buttonStyle.fixedWidth);
			}
			if (buttonStyle.fixedHeight != 0f)
			{
				this.m_MinButtonHeight = (this.m_MaxButtonHeight = buttonStyle.fixedHeight);
			}
			if (this.m_MinButtonWidth == -1f)
			{
				if (this.minWidth != 0f)
				{
					this.m_MinButtonWidth = (this.minWidth - num) / (float)this.m_XCount;
				}
				if (this.maxWidth != 0f)
				{
					this.m_MaxButtonWidth = (this.maxWidth - num) / (float)this.m_XCount;
				}
			}
			if (this.m_MinButtonHeight == -1f)
			{
				if (this.minHeight != 0f)
				{
					this.m_MinButtonHeight = (this.minHeight - num2) / (float)this.rows;
				}
				if (this.maxHeight != 0f)
				{
					this.m_MaxButtonHeight = (this.maxHeight - num2) / (float)this.rows;
				}
			}
			if (this.m_MinButtonHeight == -1f || this.m_MaxButtonHeight == -1f || this.m_MinButtonWidth == -1f || this.m_MaxButtonWidth == -1f)
			{
				float num3 = 0f;
				float num4 = 0f;
				foreach (GUIContent content in contents)
				{
					Vector2 vector = buttonStyle.CalcSize(content);
					num4 = Mathf.Max(num4, vector.x);
					num3 = Mathf.Max(num3, vector.y);
				}
				if (this.m_MinButtonWidth == -1f)
				{
					if (this.m_MaxButtonWidth != -1f)
					{
						this.m_MinButtonWidth = Mathf.Min(num4, this.m_MaxButtonWidth);
					}
					else
					{
						this.m_MinButtonWidth = num4;
					}
				}
				if (this.m_MaxButtonWidth == -1f)
				{
					if (this.m_MinButtonWidth != -1f)
					{
						this.m_MaxButtonWidth = Mathf.Max(num4, this.m_MinButtonWidth);
					}
					else
					{
						this.m_MaxButtonWidth = num4;
					}
				}
				if (this.m_MinButtonHeight == -1f)
				{
					if (this.m_MaxButtonHeight != -1f)
					{
						this.m_MinButtonHeight = Mathf.Min(num3, this.m_MaxButtonHeight);
					}
					else
					{
						this.m_MinButtonHeight = num3;
					}
				}
				if (this.m_MaxButtonHeight == -1f)
				{
					if (this.m_MinButtonHeight != -1f)
					{
						this.maxHeight = Mathf.Max(this.maxHeight, this.m_MinButtonHeight);
					}
					this.m_MaxButtonHeight = this.maxHeight;
				}
			}
			this.minWidth = this.m_MinButtonWidth * (float)this.m_XCount + num;
			this.maxWidth = this.m_MaxButtonWidth * (float)this.m_XCount + num;
			this.minHeight = this.m_MinButtonHeight * (float)this.rows + num2;
			this.maxHeight = this.m_MaxButtonHeight * (float)this.rows + num2;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00024AB8 File Offset: 0x00022CB8
		public static Rect GetRect(GUIContent[] contents, int xCount, GUIStyle style, GUILayoutOption[] options)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type == EventType.Used)
				{
					return GUILayoutEntry.kDummyRect;
				}
				rect = GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIGridSizer(contents, xCount, style, options));
			}
			return rect;
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00024B40 File Offset: 0x00022D40
		private int rows
		{
			get
			{
				int num = this.m_Count / this.m_XCount;
				if (this.m_Count % this.m_XCount != 0)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x040007D2 RID: 2002
		private readonly int m_Count;

		// Token: 0x040007D3 RID: 2003
		private readonly int m_XCount;

		// Token: 0x040007D4 RID: 2004
		private readonly float m_MinButtonWidth = -1f;

		// Token: 0x040007D5 RID: 2005
		private readonly float m_MaxButtonWidth = -1f;

		// Token: 0x040007D6 RID: 2006
		private readonly float m_MinButtonHeight = -1f;

		// Token: 0x040007D7 RID: 2007
		private readonly float m_MaxButtonHeight = -1f;
	}
}
