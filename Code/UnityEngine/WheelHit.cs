using System;

namespace UnityEngine
{
	// Token: 0x0200012A RID: 298
	public struct WheelHit
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x000157FC File Offset: 0x000139FC
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00015804 File Offset: 0x00013A04
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
			set
			{
				this.m_Collider = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00015810 File Offset: 0x00013A10
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00015818 File Offset: 0x00013A18
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00015824 File Offset: 0x00013A24
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0001582C File Offset: 0x00013A2C
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00015838 File Offset: 0x00013A38
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00015840 File Offset: 0x00013A40
		public Vector3 forwardDir
		{
			get
			{
				return this.m_ForwardDir;
			}
			set
			{
				this.m_ForwardDir = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0001584C File Offset: 0x00013A4C
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00015854 File Offset: 0x00013A54
		public Vector3 sidewaysDir
		{
			get
			{
				return this.m_SidewaysDir;
			}
			set
			{
				this.m_SidewaysDir = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00015860 File Offset: 0x00013A60
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00015868 File Offset: 0x00013A68
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00015874 File Offset: 0x00013A74
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0001587C File Offset: 0x00013A7C
		public float forwardSlip
		{
			get
			{
				return this.m_ForwardSlip;
			}
			set
			{
				this.m_Force = this.m_ForwardSlip;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0001588C File Offset: 0x00013A8C
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x00015894 File Offset: 0x00013A94
		public float sidewaysSlip
		{
			get
			{
				return this.m_SidewaysSlip;
			}
			set
			{
				this.m_SidewaysSlip = value;
			}
		}

		// Token: 0x04000391 RID: 913
		private Vector3 m_Point;

		// Token: 0x04000392 RID: 914
		private Vector3 m_Normal;

		// Token: 0x04000393 RID: 915
		private Vector3 m_ForwardDir;

		// Token: 0x04000394 RID: 916
		private Vector3 m_SidewaysDir;

		// Token: 0x04000395 RID: 917
		private float m_Force;

		// Token: 0x04000396 RID: 918
		private float m_ForwardSlip;

		// Token: 0x04000397 RID: 919
		private float m_SidewaysSlip;

		// Token: 0x04000398 RID: 920
		private Collider m_Collider;
	}
}
