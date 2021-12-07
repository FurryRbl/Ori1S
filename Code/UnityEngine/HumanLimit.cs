using System;

namespace UnityEngine
{
	// Token: 0x020001B5 RID: 437
	public struct HumanLimit
	{
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00019B80 File Offset: 0x00017D80
		// (set) Token: 0x06001AFF RID: 6911 RVA: 0x00019B90 File Offset: 0x00017D90
		public bool useDefaultValues
		{
			get
			{
				return this.m_UseDefaultValues != 0;
			}
			set
			{
				this.m_UseDefaultValues = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x00019BA8 File Offset: 0x00017DA8
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public Vector3 min
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

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00019BBC File Offset: 0x00017DBC
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x00019BC4 File Offset: 0x00017DC4
		public Vector3 max
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

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x00019BD0 File Offset: 0x00017DD0
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x00019BD8 File Offset: 0x00017DD8
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x00019BE4 File Offset: 0x00017DE4
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x00019BEC File Offset: 0x00017DEC
		public float axisLength
		{
			get
			{
				return this.m_AxisLength;
			}
			set
			{
				this.m_AxisLength = value;
			}
		}

		// Token: 0x040004ED RID: 1261
		private Vector3 m_Min;

		// Token: 0x040004EE RID: 1262
		private Vector3 m_Max;

		// Token: 0x040004EF RID: 1263
		private Vector3 m_Center;

		// Token: 0x040004F0 RID: 1264
		private float m_AxisLength;

		// Token: 0x040004F1 RID: 1265
		private int m_UseDefaultValues;
	}
}
