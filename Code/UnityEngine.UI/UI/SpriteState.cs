using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public struct SpriteState
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00014674 File Offset: 0x00012874
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001467C File Offset: 0x0001287C
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00014688 File Offset: 0x00012888
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00014690 File Offset: 0x00012890
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001469C File Offset: 0x0001289C
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000146A4 File Offset: 0x000128A4
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x0400021B RID: 539
		[FormerlySerializedAs("m_SelectedSprite")]
		[SerializeField]
		[FormerlySerializedAs("highlightedSprite")]
		private Sprite m_HighlightedSprite;

		// Token: 0x0400021C RID: 540
		[FormerlySerializedAs("pressedSprite")]
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400021D RID: 541
		[FormerlySerializedAs("disabledSprite")]
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}
