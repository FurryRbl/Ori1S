using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200014E RID: 334
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x0600161E RID: 5662
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600161F RID: 5663
		public extern int edgeCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001620 RID: 5664
		public extern int pointCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001621 RID: 5665
		// (set) Token: 0x06001622 RID: 5666
		public extern Vector2[] points { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
