using System;

namespace UnityEngine
{
	// Token: 0x02000153 RID: 339
	public struct JointAngleLimits2D
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00017CD8 File Offset: 0x00015ED8
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x00017CE0 File Offset: 0x00015EE0
		public float min
		{
			get
			{
				return this.m_LowerAngle;
			}
			set
			{
				this.m_LowerAngle = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00017CEC File Offset: 0x00015EEC
		// (set) Token: 0x0600163E RID: 5694 RVA: 0x00017CF4 File Offset: 0x00015EF4
		public float max
		{
			get
			{
				return this.m_UpperAngle;
			}
			set
			{
				this.m_UpperAngle = value;
			}
		}

		// Token: 0x040003E5 RID: 997
		private float m_LowerAngle;

		// Token: 0x040003E6 RID: 998
		private float m_UpperAngle;
	}
}
