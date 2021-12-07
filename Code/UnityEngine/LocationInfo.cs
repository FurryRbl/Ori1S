using System;

namespace UnityEngine
{
	// Token: 0x020000BF RID: 191
	public struct LocationInfo
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000F250 File Offset: 0x0000D450
		public float latitude
		{
			get
			{
				return this.m_Latitude;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0000F258 File Offset: 0x0000D458
		public float longitude
		{
			get
			{
				return this.m_Longitude;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0000F260 File Offset: 0x0000D460
		public float altitude
		{
			get
			{
				return this.m_Altitude;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0000F268 File Offset: 0x0000D468
		public float horizontalAccuracy
		{
			get
			{
				return this.m_HorizontalAccuracy;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0000F270 File Offset: 0x0000D470
		public float verticalAccuracy
		{
			get
			{
				return this.m_VerticalAccuracy;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0000F278 File Offset: 0x0000D478
		public double timestamp
		{
			get
			{
				return this.m_Timestamp;
			}
		}

		// Token: 0x0400024B RID: 587
		private double m_Timestamp;

		// Token: 0x0400024C RID: 588
		private float m_Latitude;

		// Token: 0x0400024D RID: 589
		private float m_Longitude;

		// Token: 0x0400024E RID: 590
		private float m_Altitude;

		// Token: 0x0400024F RID: 591
		private float m_HorizontalAccuracy;

		// Token: 0x04000250 RID: 592
		private float m_VerticalAccuracy;
	}
}
