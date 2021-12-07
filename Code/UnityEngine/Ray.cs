using System;

namespace UnityEngine
{
	// Token: 0x02000064 RID: 100
	public struct Ray
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x000095C0 File Offset: 0x000077C0
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000095D8 File Offset: 0x000077D8
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x000095E0 File Offset: 0x000077E0
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000095EC File Offset: 0x000077EC
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x000095F4 File Offset: 0x000077F4
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00009604 File Offset: 0x00007804
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00009620 File Offset: 0x00007820
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin,
				this.m_Direction
			});
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000965C File Offset: 0x0000785C
		public string ToString(string format)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format),
				this.m_Direction.ToString(format)
			});
		}

		// Token: 0x040000FF RID: 255
		private Vector3 m_Origin;

		// Token: 0x04000100 RID: 256
		private Vector3 m_Direction;
	}
}
