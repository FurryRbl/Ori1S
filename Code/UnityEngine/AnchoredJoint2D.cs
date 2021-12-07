using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000158 RID: 344
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x00017DE0 File Offset: 0x00015FE0
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x00017DF8 File Offset: 0x00015FF8
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

		// Token: 0x0600165F RID: 5727
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchor(out Vector2 value);

		// Token: 0x06001660 RID: 5728
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchor(ref Vector2 value);

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00017E04 File Offset: 0x00016004
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x00017E1C File Offset: 0x0001601C
		public Vector2 connectedAnchor
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_connectedAnchor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_connectedAnchor(ref value);
			}
		}

		// Token: 0x06001663 RID: 5731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_connectedAnchor(out Vector2 value);

		// Token: 0x06001664 RID: 5732
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_connectedAnchor(ref Vector2 value);

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001665 RID: 5733
		// (set) Token: 0x06001666 RID: 5734
		public extern bool autoConfigureConnectedAnchor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
