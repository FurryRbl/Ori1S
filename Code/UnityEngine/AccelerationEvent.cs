using System;

namespace UnityEngine
{
	// Token: 0x020000BD RID: 189
	public struct AccelerationEvent
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0000F114 File Offset: 0x0000D314
		public Vector3 acceleration
		{
			get
			{
				return new Vector3(this.x, this.y, this.z);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0000F130 File Offset: 0x0000D330
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x04000246 RID: 582
		private float x;

		// Token: 0x04000247 RID: 583
		private float y;

		// Token: 0x04000248 RID: 584
		private float z;

		// Token: 0x04000249 RID: 585
		private float m_TimeDelta;
	}
}
