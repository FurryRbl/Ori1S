using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015F RID: 351
	public sealed class TargetJoint2D : Joint2D
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00017F44 File Offset: 0x00016144
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00017F5C File Offset: 0x0001615C
		public Vector2 anchor
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchor(ref value);
			}
		}

		// Token: 0x060016BA RID: 5818
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchor(out Vector2 value);

		// Token: 0x060016BB RID: 5819
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchor(ref Vector2 value);

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00017F68 File Offset: 0x00016168
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x00017F80 File Offset: 0x00016180
		public Vector2 target
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_target(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_target(ref value);
			}
		}

		// Token: 0x060016BE RID: 5822
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_target(out Vector2 value);

		// Token: 0x060016BF RID: 5823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_target(ref Vector2 value);

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060016C0 RID: 5824
		// (set) Token: 0x060016C1 RID: 5825
		public extern bool autoConfigureTarget { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060016C2 RID: 5826
		// (set) Token: 0x060016C3 RID: 5827
		public extern float maxForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060016C4 RID: 5828
		// (set) Token: 0x060016C5 RID: 5829
		public extern float dampingRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060016C6 RID: 5830
		// (set) Token: 0x060016C7 RID: 5831
		public extern float frequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
