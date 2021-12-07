using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	[UsedByNativeCode]
	public struct ContactPoint
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000158A0 File Offset: 0x00013AA0
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000158A8 File Offset: 0x00013AA8
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x000158B0 File Offset: 0x00013AB0
		public Collider thisCollider
		{
			get
			{
				return ContactPoint.ColliderFromInstanceId(this.m_ThisColliderInstanceID);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000158C0 File Offset: 0x00013AC0
		public Collider otherCollider
		{
			get
			{
				return ContactPoint.ColliderFromInstanceId(this.m_OtherColliderInstanceID);
			}
		}

		// Token: 0x060012BF RID: 4799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider ColliderFromInstanceId(int instanceID);

		// Token: 0x04000399 RID: 921
		internal Vector3 m_Point;

		// Token: 0x0400039A RID: 922
		internal Vector3 m_Normal;

		// Token: 0x0400039B RID: 923
		internal int m_ThisColliderInstanceID;

		// Token: 0x0400039C RID: 924
		internal int m_OtherColliderInstanceID;
	}
}
