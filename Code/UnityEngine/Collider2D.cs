using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200014B RID: 331
	public class Collider2D : Behaviour
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060015FF RID: 5631
		// (set) Token: 0x06001600 RID: 5632
		public extern float density { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001601 RID: 5633
		// (set) Token: 0x06001602 RID: 5634
		public extern bool isTrigger { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001603 RID: 5635
		// (set) Token: 0x06001604 RID: 5636
		public extern bool usedByEffector { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00017B18 File Offset: 0x00015D18
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00017B30 File Offset: 0x00015D30
		public Vector2 offset
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_offset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_offset(ref value);
			}
		}

		// Token: 0x06001607 RID: 5639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_offset(out Vector2 value);

		// Token: 0x06001608 RID: 5640
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_offset(ref Vector2 value);

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001609 RID: 5641
		public extern Rigidbody2D attachedRigidbody { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600160A RID: 5642
		public extern int shapeCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00017B3C File Offset: 0x00015D3C
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x0600160C RID: 5644
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600160D RID: 5645
		internal extern ColliderErrorState2D errorState { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600160E RID: 5646 RVA: 0x00017B54 File Offset: 0x00015D54
		public bool OverlapPoint(Vector2 point)
		{
			return Collider2D.INTERNAL_CALL_OverlapPoint(this, ref point);
		}

		// Token: 0x0600160F RID: 5647
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_OverlapPoint(Collider2D self, ref Vector2 point);

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001610 RID: 5648
		// (set) Token: 0x06001611 RID: 5649
		public extern PhysicsMaterial2D sharedMaterial { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001612 RID: 5650
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouching(Collider2D collider);

		// Token: 0x06001613 RID: 5651
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x06001614 RID: 5652 RVA: 0x00017B60 File Offset: 0x00015D60
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			int layerMask = -1;
			return this.IsTouchingLayers(layerMask);
		}
	}
}
