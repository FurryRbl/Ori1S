using System;

namespace UnityEngine
{
	// Token: 0x02000155 RID: 341
	public struct JointMotor2D
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00017D28 File Offset: 0x00015F28
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00017D30 File Offset: 0x00015F30
		public float motorSpeed
		{
			get
			{
				return this.m_MotorSpeed;
			}
			set
			{
				this.m_MotorSpeed = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00017D3C File Offset: 0x00015F3C
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x00017D44 File Offset: 0x00015F44
		public float maxMotorTorque
		{
			get
			{
				return this.m_MaximumMotorTorque;
			}
			set
			{
				this.m_MaximumMotorTorque = value;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private float m_MotorSpeed;

		// Token: 0x040003EA RID: 1002
		private float m_MaximumMotorTorque;
	}
}
