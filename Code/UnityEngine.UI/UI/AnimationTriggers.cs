using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class AnimationTriggers
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005B68 File Offset: 0x00003D68
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005B70 File Offset: 0x00003D70
		public string normalTrigger
		{
			get
			{
				return this.m_NormalTrigger;
			}
			set
			{
				this.m_NormalTrigger = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005B7C File Offset: 0x00003D7C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005B84 File Offset: 0x00003D84
		public string highlightedTrigger
		{
			get
			{
				return this.m_HighlightedTrigger;
			}
			set
			{
				this.m_HighlightedTrigger = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005B90 File Offset: 0x00003D90
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005B98 File Offset: 0x00003D98
		public string pressedTrigger
		{
			get
			{
				return this.m_PressedTrigger;
			}
			set
			{
				this.m_PressedTrigger = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005BA4 File Offset: 0x00003DA4
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005BAC File Offset: 0x00003DAC
		public string disabledTrigger
		{
			get
			{
				return this.m_DisabledTrigger;
			}
			set
			{
				this.m_DisabledTrigger = value;
			}
		}

		// Token: 0x040000A1 RID: 161
		private const string kDefaultNormalAnimName = "Normal";

		// Token: 0x040000A2 RID: 162
		private const string kDefaultSelectedAnimName = "Highlighted";

		// Token: 0x040000A3 RID: 163
		private const string kDefaultPressedAnimName = "Pressed";

		// Token: 0x040000A4 RID: 164
		private const string kDefaultDisabledAnimName = "Disabled";

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		[FormerlySerializedAs("normalTrigger")]
		private string m_NormalTrigger = "Normal";

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		[FormerlySerializedAs("m_SelectedTrigger")]
		[FormerlySerializedAs("highlightedTrigger")]
		private string m_HighlightedTrigger = "Highlighted";

		// Token: 0x040000A7 RID: 167
		[FormerlySerializedAs("pressedTrigger")]
		[SerializeField]
		private string m_PressedTrigger = "Pressed";

		// Token: 0x040000A8 RID: 168
		[FormerlySerializedAs("disabledTrigger")]
		[SerializeField]
		private string m_DisabledTrigger = "Disabled";
	}
}
