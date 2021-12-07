using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010C RID: 268
	public struct ParticleCollisionEvent
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x000140B4 File Offset: 0x000122B4
		public Vector3 intersection
		{
			get
			{
				return this.m_Intersection;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x000140BC File Offset: 0x000122BC
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000140C4 File Offset: 0x000122C4
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000140CC File Offset: 0x000122CC
		[Obsolete("collider property is deprecated. Use colliderComponent instead, which supports Collider and Collider2D components.")]
		public Collider collider
		{
			get
			{
				return ParticleCollisionEvent.InstanceIDToCollider(this.m_ColliderInstanceID);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000140DC File Offset: 0x000122DC
		public Component colliderComponent
		{
			get
			{
				return ParticleCollisionEvent.InstanceIDToColliderComponent(this.m_ColliderInstanceID);
			}
		}

		// Token: 0x0600112A RID: 4394
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider InstanceIDToCollider(int instanceID);

		// Token: 0x0600112B RID: 4395
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Component InstanceIDToColliderComponent(int instanceID);

		// Token: 0x0400031C RID: 796
		private Vector3 m_Intersection;

		// Token: 0x0400031D RID: 797
		private Vector3 m_Normal;

		// Token: 0x0400031E RID: 798
		private Vector3 m_Velocity;

		// Token: 0x0400031F RID: 799
		private int m_ColliderInstanceID;
	}
}
