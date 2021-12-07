using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600021D RID: 541
		// (set) Token: 0x0600021E RID: 542
		public extern float time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600021F RID: 543
		// (set) Token: 0x06000220 RID: 544
		public extern float startWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000221 RID: 545
		// (set) Token: 0x06000222 RID: 546
		public extern float endWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000223 RID: 547
		// (set) Token: 0x06000224 RID: 548
		public extern bool autodestruct { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000225 RID: 549
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();
	}
}
