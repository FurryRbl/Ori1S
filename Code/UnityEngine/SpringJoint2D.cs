using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000159 RID: 345
	public sealed class SpringJoint2D : AnchoredJoint2D
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001668 RID: 5736
		// (set) Token: 0x06001669 RID: 5737
		public extern bool autoConfigureDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600166A RID: 5738
		// (set) Token: 0x0600166B RID: 5739
		public extern float distance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600166C RID: 5740
		// (set) Token: 0x0600166D RID: 5741
		public extern float dampingRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600166E RID: 5742
		// (set) Token: 0x0600166F RID: 5743
		public extern float frequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
