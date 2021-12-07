using System;

namespace UnityEngine
{
	// Token: 0x02000065 RID: 101
	public struct Ray2D
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00009698 File Offset: 0x00007898
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x000096B0 File Offset: 0x000078B0
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x000096B8 File Offset: 0x000078B8
		public Vector2 origin
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000096C4 File Offset: 0x000078C4
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x000096CC File Offset: 0x000078CC
		public Vector2 direction
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

		// Token: 0x06000643 RID: 1603 RVA: 0x000096DC File Offset: 0x000078DC
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000096F8 File Offset: 0x000078F8
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin,
				this.m_Direction
			});
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00009734 File Offset: 0x00007934
		public string ToString(string format)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format),
				this.m_Direction.ToString(format)
			});
		}

		// Token: 0x04000101 RID: 257
		private Vector2 m_Origin;

		// Token: 0x04000102 RID: 258
		private Vector2 m_Direction;
	}
}
