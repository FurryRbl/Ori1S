using System;

namespace UnityEngine
{
	// Token: 0x0200011E RID: 286
	public struct JointDrive
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000144EC File Offset: 0x000126EC
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000144F0 File Offset: 0x000126F0
		[Obsolete("JointDriveMode is obsolete")]
		public JointDriveMode mode
		{
			get
			{
				return JointDriveMode.None;
			}
			set
			{
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000144F4 File Offset: 0x000126F4
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x000144FC File Offset: 0x000126FC
		public float positionSpring
		{
			get
			{
				return this.m_PositionSpring;
			}
			set
			{
				this.m_PositionSpring = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00014508 File Offset: 0x00012708
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00014510 File Offset: 0x00012710
		public float positionDamper
		{
			get
			{
				return this.m_PositionDamper;
			}
			set
			{
				this.m_PositionDamper = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0001451C File Offset: 0x0001271C
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x00014524 File Offset: 0x00012724
		public float maximumForce
		{
			get
			{
				return this.m_MaximumForce;
			}
			set
			{
				this.m_MaximumForce = value;
			}
		}

		// Token: 0x04000358 RID: 856
		private float m_PositionSpring;

		// Token: 0x04000359 RID: 857
		private float m_PositionDamper;

		// Token: 0x0400035A RID: 858
		private float m_MaximumForce;
	}
}
