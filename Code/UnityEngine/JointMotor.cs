using System;

namespace UnityEngine
{
	// Token: 0x02000120 RID: 288
	public struct JointMotor
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00014530 File Offset: 0x00012730
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x00014538 File Offset: 0x00012738
		public float targetVelocity
		{
			get
			{
				return this.m_TargetVelocity;
			}
			set
			{
				this.m_TargetVelocity = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00014544 File Offset: 0x00012744
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x0001454C File Offset: 0x0001274C
		public float force
		{
			get
			{
				return this.m_Force;
			}
			set
			{
				this.m_Force = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00014558 File Offset: 0x00012758
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x00014564 File Offset: 0x00012764
		public bool freeSpin
		{
			get
			{
				return this.m_FreeSpin == 1;
			}
			set
			{
				this.m_FreeSpin = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x0400035F RID: 863
		private float m_TargetVelocity;

		// Token: 0x04000360 RID: 864
		private float m_Force;

		// Token: 0x04000361 RID: 865
		private int m_FreeSpin;
	}
}
