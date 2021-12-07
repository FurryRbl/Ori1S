using System;

namespace UnityEngine
{
	// Token: 0x020000BB RID: 187
	public struct Touch
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public Vector2 rawPosition
		{
			get
			{
				return this.m_RawPosition;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		public Vector2 deltaPosition
		{
			get
			{
				return this.m_PositionDelta;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public int tapCount
		{
			get
			{
				return this.m_TapCount;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public float maximumPossiblePressure
		{
			get
			{
				return this.m_maximumPossiblePressure;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public TouchType type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0000F104 File Offset: 0x0000D304
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0000F10C File Offset: 0x0000D30C
		public float radiusVariance
		{
			get
			{
				return this.m_RadiusVariance;
			}
		}

		// Token: 0x04000230 RID: 560
		private int m_FingerId;

		// Token: 0x04000231 RID: 561
		private Vector2 m_Position;

		// Token: 0x04000232 RID: 562
		private Vector2 m_RawPosition;

		// Token: 0x04000233 RID: 563
		private Vector2 m_PositionDelta;

		// Token: 0x04000234 RID: 564
		private float m_TimeDelta;

		// Token: 0x04000235 RID: 565
		private int m_TapCount;

		// Token: 0x04000236 RID: 566
		private TouchPhase m_Phase;

		// Token: 0x04000237 RID: 567
		private TouchType m_Type;

		// Token: 0x04000238 RID: 568
		private float m_Pressure;

		// Token: 0x04000239 RID: 569
		private float m_maximumPossiblePressure;

		// Token: 0x0400023A RID: 570
		private float m_Radius;

		// Token: 0x0400023B RID: 571
		private float m_RadiusVariance;

		// Token: 0x0400023C RID: 572
		private float m_AltitudeAngle;

		// Token: 0x0400023D RID: 573
		private float m_AzimuthAngle;
	}
}
