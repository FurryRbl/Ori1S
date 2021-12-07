using System;

namespace UnityEngine
{
	// Token: 0x020001B0 RID: 432
	public struct MatchTargetWeightMask
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x0001917C File Offset: 0x0001737C
		public MatchTargetWeightMask(Vector3 positionXYZWeight, float rotationWeight)
		{
			this.m_PositionXYZWeight = positionXYZWeight;
			this.m_RotationWeight = rotationWeight;
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0001918C File Offset: 0x0001738C
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x00019194 File Offset: 0x00017394
		public Vector3 positionXYZWeight
		{
			get
			{
				return this.m_PositionXYZWeight;
			}
			set
			{
				this.m_PositionXYZWeight = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000191A0 File Offset: 0x000173A0
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x000191A8 File Offset: 0x000173A8
		public float rotationWeight
		{
			get
			{
				return this.m_RotationWeight;
			}
			set
			{
				this.m_RotationWeight = value;
			}
		}

		// Token: 0x040004E1 RID: 1249
		private Vector3 m_PositionXYZWeight;

		// Token: 0x040004E2 RID: 1250
		private float m_RotationWeight;
	}
}
