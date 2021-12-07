using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018D RID: 397
	public sealed class AudioChorusFilter : Behaviour
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060018CB RID: 6347
		// (set) Token: 0x060018CC RID: 6348
		public extern float dryMix { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060018CD RID: 6349
		// (set) Token: 0x060018CE RID: 6350
		public extern float wetMix1 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060018CF RID: 6351
		// (set) Token: 0x060018D0 RID: 6352
		public extern float wetMix2 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060018D1 RID: 6353
		// (set) Token: 0x060018D2 RID: 6354
		public extern float wetMix3 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060018D3 RID: 6355
		// (set) Token: 0x060018D4 RID: 6356
		public extern float delay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060018D5 RID: 6357
		// (set) Token: 0x060018D6 RID: 6358
		public extern float rate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060018D7 RID: 6359
		// (set) Token: 0x060018D8 RID: 6360
		public extern float depth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060018D9 RID: 6361
		// (set) Token: 0x060018DA RID: 6362
		[Obsolete("feedback is deprecated, this property does nothing.")]
		public extern float feedback { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
