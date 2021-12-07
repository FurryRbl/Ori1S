using System;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	public struct SoftJointLimitSpring
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000144C4 File Offset: 0x000126C4
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x000144CC File Offset: 0x000126CC
		public float spring
		{
			get
			{
				return this.m_Spring;
			}
			set
			{
				this.m_Spring = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000144D8 File Offset: 0x000126D8
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x000144E0 File Offset: 0x000126E0
		public float damper
		{
			get
			{
				return this.m_Damper;
			}
			set
			{
				this.m_Damper = value;
			}
		}

		// Token: 0x04000356 RID: 854
		private float m_Spring;

		// Token: 0x04000357 RID: 855
		private float m_Damper;
	}
}
