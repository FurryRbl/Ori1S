using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016C RID: 364
	public sealed class SurfaceEffector2D : Effector2D
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001735 RID: 5941
		// (set) Token: 0x06001736 RID: 5942
		public extern float speed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001737 RID: 5943
		// (set) Token: 0x06001738 RID: 5944
		public extern float speedVariation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001739 RID: 5945
		// (set) Token: 0x0600173A RID: 5946
		public extern float forceScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600173B RID: 5947
		// (set) Token: 0x0600173C RID: 5948
		public extern bool useContactForce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600173D RID: 5949
		// (set) Token: 0x0600173E RID: 5950
		public extern bool useFriction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600173F RID: 5951
		// (set) Token: 0x06001740 RID: 5952
		public extern bool useBounce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
