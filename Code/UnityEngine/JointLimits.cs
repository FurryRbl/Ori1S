using System;

namespace UnityEngine
{
	// Token: 0x02000122 RID: 290
	public struct JointLimits
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0001457C File Offset: 0x0001277C
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00014584 File Offset: 0x00012784
		public float min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00014590 File Offset: 0x00012790
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x00014598 File Offset: 0x00012798
		public float max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000145A4 File Offset: 0x000127A4
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x000145AC File Offset: 0x000127AC
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x000145B8 File Offset: 0x000127B8
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x000145C0 File Offset: 0x000127C0
		public float bounceMinVelocity
		{
			get
			{
				return this.m_BounceMinVelocity;
			}
			set
			{
				this.m_BounceMinVelocity = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x000145CC File Offset: 0x000127CC
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x000145D4 File Offset: 0x000127D4
		public float contactDistance
		{
			get
			{
				return this.m_ContactDistance;
			}
			set
			{
				this.m_ContactDistance = value;
			}
		}

		// Token: 0x04000365 RID: 869
		private float m_Min;

		// Token: 0x04000366 RID: 870
		private float m_Max;

		// Token: 0x04000367 RID: 871
		private float m_Bounciness;

		// Token: 0x04000368 RID: 872
		private float m_BounceMinVelocity;

		// Token: 0x04000369 RID: 873
		private float m_ContactDistance;

		// Token: 0x0400036A RID: 874
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		public float minBounce;

		// Token: 0x0400036B RID: 875
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		public float maxBounce;
	}
}
