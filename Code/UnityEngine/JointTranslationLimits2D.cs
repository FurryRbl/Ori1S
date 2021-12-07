using System;

namespace UnityEngine
{
	// Token: 0x02000154 RID: 340
	public struct JointTranslationLimits2D
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x00017D00 File Offset: 0x00015F00
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x00017D08 File Offset: 0x00015F08
		public float min
		{
			get
			{
				return this.m_LowerTranslation;
			}
			set
			{
				this.m_LowerTranslation = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00017D14 File Offset: 0x00015F14
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x00017D1C File Offset: 0x00015F1C
		public float max
		{
			get
			{
				return this.m_UpperTranslation;
			}
			set
			{
				this.m_UpperTranslation = value;
			}
		}

		// Token: 0x040003E7 RID: 999
		private float m_LowerTranslation;

		// Token: 0x040003E8 RID: 1000
		private float m_UpperTranslation;
	}
}
