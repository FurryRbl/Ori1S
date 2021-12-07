using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015A RID: 346
	public sealed class DistanceJoint2D : AnchoredJoint2D
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001671 RID: 5745
		// (set) Token: 0x06001672 RID: 5746
		public extern bool autoConfigureDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001673 RID: 5747
		// (set) Token: 0x06001674 RID: 5748
		public extern float distance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001675 RID: 5749
		// (set) Token: 0x06001676 RID: 5750
		public extern bool maxDistanceOnly { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
