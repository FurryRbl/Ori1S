using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018E RID: 398
	public sealed class AudioReverbFilter : Behaviour
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060018DC RID: 6364
		// (set) Token: 0x060018DD RID: 6365
		public extern AudioReverbPreset reverbPreset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060018DE RID: 6366
		// (set) Token: 0x060018DF RID: 6367
		public extern float dryLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060018E0 RID: 6368
		// (set) Token: 0x060018E1 RID: 6369
		public extern float room { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060018E2 RID: 6370
		// (set) Token: 0x060018E3 RID: 6371
		public extern float roomHF { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060018E4 RID: 6372
		// (set) Token: 0x060018E5 RID: 6373
		public extern float roomRolloff { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060018E6 RID: 6374
		// (set) Token: 0x060018E7 RID: 6375
		public extern float decayTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060018E8 RID: 6376
		// (set) Token: 0x060018E9 RID: 6377
		public extern float decayHFRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060018EA RID: 6378
		// (set) Token: 0x060018EB RID: 6379
		public extern float reflectionsLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060018EC RID: 6380
		// (set) Token: 0x060018ED RID: 6381
		public extern float reflectionsDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060018EE RID: 6382
		// (set) Token: 0x060018EF RID: 6383
		public extern float reverbLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060018F0 RID: 6384
		// (set) Token: 0x060018F1 RID: 6385
		public extern float reverbDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060018F2 RID: 6386
		// (set) Token: 0x060018F3 RID: 6387
		public extern float diffusion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060018F4 RID: 6388
		// (set) Token: 0x060018F5 RID: 6389
		public extern float density { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060018F6 RID: 6390
		// (set) Token: 0x060018F7 RID: 6391
		public extern float hfReference { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060018F8 RID: 6392
		// (set) Token: 0x060018F9 RID: 6393
		public extern float roomLF { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060018FA RID: 6394
		// (set) Token: 0x060018FB RID: 6395
		public extern float lfReference { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
