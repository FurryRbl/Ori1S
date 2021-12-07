using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class FontData : ISerializationCallbackReceiver
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x000085F4 File Offset: 0x000067F4
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000085F8 File Offset: 0x000067F8
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, this.m_FontSize);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, this.m_FontSize, 300);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008650 File Offset: 0x00006850
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_FontStyle = FontStyle.Normal,
					m_BestFit = false,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_Alignment = TextAnchor.UpperLeft,
					m_HorizontalOverflow = HorizontalWrapMode.Wrap,
					m_VerticalOverflow = VerticalWrapMode.Truncate,
					m_RichText = true,
					m_AlignByGeometry = false
				};
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000086BC File Offset: 0x000068BC
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000086C4 File Offset: 0x000068C4
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000086D0 File Offset: 0x000068D0
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000086D8 File Offset: 0x000068D8
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000086E4 File Offset: 0x000068E4
		// (set) Token: 0x060001EE RID: 494 RVA: 0x000086EC File Offset: 0x000068EC
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000086F8 File Offset: 0x000068F8
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008700 File Offset: 0x00006900
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000870C File Offset: 0x0000690C
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008714 File Offset: 0x00006914
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008720 File Offset: 0x00006920
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00008728 File Offset: 0x00006928
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008734 File Offset: 0x00006934
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000873C File Offset: 0x0000693C
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008748 File Offset: 0x00006948
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00008750 File Offset: 0x00006950
		public bool alignByGeometry
		{
			get
			{
				return this.m_AlignByGeometry;
			}
			set
			{
				this.m_AlignByGeometry = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000875C File Offset: 0x0000695C
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00008764 File Offset: 0x00006964
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008770 File Offset: 0x00006970
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00008778 File Offset: 0x00006978
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00008784 File Offset: 0x00006984
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000878C File Offset: 0x0000698C
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008798 File Offset: 0x00006998
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000087A0 File Offset: 0x000069A0
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		[FormerlySerializedAs("font")]
		private Font m_Font;

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x040000E4 RID: 228
		[FormerlySerializedAs("fontStyle")]
		[SerializeField]
		private FontStyle m_FontStyle;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		private int m_MinSize;

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x040000E8 RID: 232
		[FormerlySerializedAs("alignment")]
		[SerializeField]
		private TextAnchor m_Alignment;

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private bool m_AlignByGeometry;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		[FormerlySerializedAs("richText")]
		private bool m_RichText;

		// Token: 0x040000EB RID: 235
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x040000ED RID: 237
		[SerializeField]
		private float m_LineSpacing;
	}
}
