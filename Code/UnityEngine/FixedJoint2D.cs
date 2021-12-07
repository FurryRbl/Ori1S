using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000160 RID: 352
	public sealed class FixedJoint2D : AnchoredJoint2D
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060016C9 RID: 5833
		// (set) Token: 0x060016CA RID: 5834
		public extern float dampingRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016CB RID: 5835
		// (set) Token: 0x060016CC RID: 5836
		public extern float frequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060016CD RID: 5837
		public extern float referenceAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
