using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	public sealed class OffMeshLink : Component
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060017F1 RID: 6129
		// (set) Token: 0x060017F2 RID: 6130
		public extern bool activated { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060017F3 RID: 6131
		public extern bool occupied { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060017F4 RID: 6132
		// (set) Token: 0x060017F5 RID: 6133
		public extern float costOverride { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060017F6 RID: 6134
		// (set) Token: 0x060017F7 RID: 6135
		public extern bool biDirectional { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060017F8 RID: 6136
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdatePositions();

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060017F9 RID: 6137
		// (set) Token: 0x060017FA RID: 6138
		[Obsolete("Use area instead.")]
		public extern int navMeshLayer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060017FB RID: 6139
		// (set) Token: 0x060017FC RID: 6140
		public extern int area { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060017FD RID: 6141
		// (set) Token: 0x060017FE RID: 6142
		public extern bool autoUpdatePositions { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060017FF RID: 6143
		// (set) Token: 0x06001800 RID: 6144
		public extern Transform startTransform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001801 RID: 6145
		// (set) Token: 0x06001802 RID: 6146
		public extern Transform endTransform { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
