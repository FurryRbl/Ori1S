using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018C RID: 396
	public sealed class AudioEchoFilter : Behaviour
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060018C2 RID: 6338
		// (set) Token: 0x060018C3 RID: 6339
		public extern float delay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060018C4 RID: 6340
		// (set) Token: 0x060018C5 RID: 6341
		public extern float decayRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060018C6 RID: 6342
		// (set) Token: 0x060018C7 RID: 6343
		public extern float dryMix { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060018C8 RID: 6344
		// (set) Token: 0x060018C9 RID: 6345
		public extern float wetMix { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
