using System;

namespace UnityEngine
{
	// Token: 0x0200011B RID: 283
	public struct WheelFrictionCurve
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000143F8 File Offset: 0x000125F8
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00014400 File Offset: 0x00012600
		public float extremumSlip
		{
			get
			{
				return this.m_ExtremumSlip;
			}
			set
			{
				this.m_ExtremumSlip = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0001440C File Offset: 0x0001260C
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00014414 File Offset: 0x00012614
		public float extremumValue
		{
			get
			{
				return this.m_ExtremumValue;
			}
			set
			{
				this.m_ExtremumValue = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00014420 File Offset: 0x00012620
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00014428 File Offset: 0x00012628
		public float asymptoteSlip
		{
			get
			{
				return this.m_AsymptoteSlip;
			}
			set
			{
				this.m_AsymptoteSlip = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00014434 File Offset: 0x00012634
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0001443C File Offset: 0x0001263C
		public float asymptoteValue
		{
			get
			{
				return this.m_AsymptoteValue;
			}
			set
			{
				this.m_AsymptoteValue = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00014448 File Offset: 0x00012648
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00014450 File Offset: 0x00012650
		public float stiffness
		{
			get
			{
				return this.m_Stiffness;
			}
			set
			{
				this.m_Stiffness = value;
			}
		}

		// Token: 0x0400034E RID: 846
		private float m_ExtremumSlip;

		// Token: 0x0400034F RID: 847
		private float m_ExtremumValue;

		// Token: 0x04000350 RID: 848
		private float m_AsymptoteSlip;

		// Token: 0x04000351 RID: 849
		private float m_AsymptoteValue;

		// Token: 0x04000352 RID: 850
		private float m_Stiffness;
	}
}
