using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200010F RID: 271
	[UsedByNativeCode]
	public struct Particle
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x00014108 File Offset: 0x00012308
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x00014110 File Offset: 0x00012310
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0001411C File Offset: 0x0001231C
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x00014124 File Offset: 0x00012324
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00014130 File Offset: 0x00012330
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x00014138 File Offset: 0x00012338
		public float energy
		{
			get
			{
				return this.m_Energy;
			}
			set
			{
				this.m_Energy = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00014144 File Offset: 0x00012344
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0001414C File Offset: 0x0001234C
		public float startEnergy
		{
			get
			{
				return this.m_StartEnergy;
			}
			set
			{
				this.m_StartEnergy = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00014158 File Offset: 0x00012358
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x00014160 File Offset: 0x00012360
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0001416C File Offset: 0x0001236C
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x00014174 File Offset: 0x00012374
		public float rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00014180 File Offset: 0x00012380
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00014188 File Offset: 0x00012388
		public float angularVelocity
		{
			get
			{
				return this.m_AngularVelocity;
			}
			set
			{
				this.m_AngularVelocity = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00014194 File Offset: 0x00012394
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0001419C File Offset: 0x0001239C
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x04000320 RID: 800
		private Vector3 m_Position;

		// Token: 0x04000321 RID: 801
		private Vector3 m_Velocity;

		// Token: 0x04000322 RID: 802
		private float m_Size;

		// Token: 0x04000323 RID: 803
		private float m_Rotation;

		// Token: 0x04000324 RID: 804
		private float m_AngularVelocity;

		// Token: 0x04000325 RID: 805
		private float m_Energy;

		// Token: 0x04000326 RID: 806
		private float m_StartEnergy;

		// Token: 0x04000327 RID: 807
		private Color m_Color;
	}
}
