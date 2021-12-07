using System;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x02000327 RID: 807
	public struct FrameData
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00039208 File Offset: 0x00037408
		public int updateId
		{
			get
			{
				return this.m_UpdateId;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x00039210 File Offset: 0x00037410
		public float time
		{
			get
			{
				return (float)this.m_Time;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x0003921C File Offset: 0x0003741C
		public float lastTime
		{
			get
			{
				return (float)this.m_LastTime;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x00039228 File Offset: 0x00037428
		public float deltaTime
		{
			get
			{
				return (float)this.m_Time - (float)this.m_LastTime;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x0003923C File Offset: 0x0003743C
		public float timeScale
		{
			get
			{
				return (float)this.m_TimeScale;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x00039248 File Offset: 0x00037448
		public double dTime
		{
			get
			{
				return this.m_Time;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00039250 File Offset: 0x00037450
		public double dLastTime
		{
			get
			{
				return this.m_LastTime;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00039258 File Offset: 0x00037458
		public double dDeltaTime
		{
			get
			{
				return this.m_Time - this.m_LastTime;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00039268 File Offset: 0x00037468
		public double dtimeScale
		{
			get
			{
				return this.m_TimeScale;
			}
		}

		// Token: 0x04000C4D RID: 3149
		internal int m_UpdateId;

		// Token: 0x04000C4E RID: 3150
		internal double m_Time;

		// Token: 0x04000C4F RID: 3151
		internal double m_LastTime;

		// Token: 0x04000C50 RID: 3152
		internal double m_TimeScale;
	}
}
