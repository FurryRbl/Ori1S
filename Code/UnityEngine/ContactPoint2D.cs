using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000150 RID: 336
	[UsedByNativeCode]
	public struct ContactPoint2D
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00017C10 File Offset: 0x00015E10
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x00017C18 File Offset: 0x00015E18
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00017C20 File Offset: 0x00015E20
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00017C28 File Offset: 0x00015E28
		public Collider2D otherCollider
		{
			get
			{
				return this.m_OtherCollider;
			}
		}

		// Token: 0x040003D7 RID: 983
		internal Vector2 m_Point;

		// Token: 0x040003D8 RID: 984
		internal Vector2 m_Normal;

		// Token: 0x040003D9 RID: 985
		internal Collider2D m_Collider;

		// Token: 0x040003DA RID: 986
		internal Collider2D m_OtherCollider;
	}
}
