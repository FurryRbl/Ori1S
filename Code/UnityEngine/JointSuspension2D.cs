using System;

namespace UnityEngine
{
	// Token: 0x02000156 RID: 342
	public struct JointSuspension2D
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00017D50 File Offset: 0x00015F50
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x00017D58 File Offset: 0x00015F58
		public float dampingRatio
		{
			get
			{
				return this.m_DampingRatio;
			}
			set
			{
				this.m_DampingRatio = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00017D64 File Offset: 0x00015F64
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x00017D6C File Offset: 0x00015F6C
		public float frequency
		{
			get
			{
				return this.m_Frequency;
			}
			set
			{
				this.m_Frequency = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00017D78 File Offset: 0x00015F78
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x00017D80 File Offset: 0x00015F80
		public float angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x040003EB RID: 1003
		private float m_DampingRatio;

		// Token: 0x040003EC RID: 1004
		private float m_Frequency;

		// Token: 0x040003ED RID: 1005
		private float m_Angle;
	}
}
