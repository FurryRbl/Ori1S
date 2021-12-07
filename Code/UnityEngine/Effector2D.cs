using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000167 RID: 359
	public class Effector2D : Behaviour
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060016F1 RID: 5873
		// (set) Token: 0x060016F2 RID: 5874
		public extern bool useColliderMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060016F3 RID: 5875
		// (set) Token: 0x060016F4 RID: 5876
		public extern int colliderMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060016F5 RID: 5877
		internal extern bool requiresCollider { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060016F6 RID: 5878
		internal extern bool designedForTrigger { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060016F7 RID: 5879
		internal extern bool designedForNonTrigger { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
