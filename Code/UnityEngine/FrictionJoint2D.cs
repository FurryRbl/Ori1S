using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015B RID: 347
	public sealed class FrictionJoint2D : AnchoredJoint2D
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001678 RID: 5752
		// (set) Token: 0x06001679 RID: 5753
		public extern float maxForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600167A RID: 5754
		// (set) Token: 0x0600167B RID: 5755
		public extern float maxTorque { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
