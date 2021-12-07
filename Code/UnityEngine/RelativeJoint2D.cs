using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015D RID: 349
	public sealed class RelativeJoint2D : Joint2D
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001690 RID: 5776
		// (set) Token: 0x06001691 RID: 5777
		public extern float maxForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001692 RID: 5778
		// (set) Token: 0x06001693 RID: 5779
		public extern float maxTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001694 RID: 5780
		// (set) Token: 0x06001695 RID: 5781
		public extern float correctionScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001696 RID: 5782
		// (set) Token: 0x06001697 RID: 5783
		public extern bool autoConfigureOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00017EA4 File Offset: 0x000160A4
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00017EBC File Offset: 0x000160BC
		public Vector2 linearOffset
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_linearOffset(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_linearOffset(ref value);
			}
		}

		// Token: 0x0600169A RID: 5786
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_linearOffset(out Vector2 value);

		// Token: 0x0600169B RID: 5787
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_linearOffset(ref Vector2 value);

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600169C RID: 5788
		// (set) Token: 0x0600169D RID: 5789
		public extern float angularOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00017EC8 File Offset: 0x000160C8
		public Vector2 target
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_target(out result);
				return result;
			}
		}

		// Token: 0x0600169F RID: 5791
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_target(out Vector2 value);
	}
}
