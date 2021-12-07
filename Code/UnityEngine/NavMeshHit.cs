using System;

namespace UnityEngine
{
	// Token: 0x0200016F RID: 367
	public struct NavMeshHit
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x000181F4 File Offset: 0x000163F4
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x000181FC File Offset: 0x000163FC
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00018208 File Offset: 0x00016408
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x00018210 File Offset: 0x00016410
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

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0001821C File Offset: 0x0001641C
		// (set) Token: 0x060017A0 RID: 6048 RVA: 0x00018224 File Offset: 0x00016424
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00018230 File Offset: 0x00016430
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x00018238 File Offset: 0x00016438
		public int mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00018244 File Offset: 0x00016444
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x00018254 File Offset: 0x00016454
		public bool hit
		{
			get
			{
				return this.m_Hit != 0;
			}
			set
			{
				this.m_Hit = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x040003FB RID: 1019
		private Vector3 m_Position;

		// Token: 0x040003FC RID: 1020
		private Vector3 m_Normal;

		// Token: 0x040003FD RID: 1021
		private float m_Distance;

		// Token: 0x040003FE RID: 1022
		private int m_Mask;

		// Token: 0x040003FF RID: 1023
		private int m_Hit;
	}
}
