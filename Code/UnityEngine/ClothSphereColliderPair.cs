using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000140 RID: 320
	[UsedByNativeCode]
	public struct ClothSphereColliderPair
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x000166F8 File Offset: 0x000148F8
		public ClothSphereColliderPair(SphereCollider a)
		{
			this.m_First = null;
			this.m_Second = null;
			this.first = a;
			this.second = null;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00016718 File Offset: 0x00014918
		public ClothSphereColliderPair(SphereCollider a, SphereCollider b)
		{
			this.m_First = null;
			this.m_Second = null;
			this.first = a;
			this.second = b;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00016738 File Offset: 0x00014938
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00016740 File Offset: 0x00014940
		public SphereCollider first
		{
			get
			{
				return this.m_First;
			}
			set
			{
				this.m_First = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0001674C File Offset: 0x0001494C
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x00016754 File Offset: 0x00014954
		public SphereCollider second
		{
			get
			{
				return this.m_Second;
			}
			set
			{
				this.m_Second = value;
			}
		}

		// Token: 0x040003B1 RID: 945
		private SphereCollider m_First;

		// Token: 0x040003B2 RID: 946
		private SphereCollider m_Second;
	}
}
