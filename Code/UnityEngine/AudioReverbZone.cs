using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000188 RID: 392
	public sealed class AudioReverbZone : Behaviour
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001890 RID: 6288
		// (set) Token: 0x06001891 RID: 6289
		public extern float minDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001892 RID: 6290
		// (set) Token: 0x06001893 RID: 6291
		public extern float maxDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001894 RID: 6292
		// (set) Token: 0x06001895 RID: 6293
		public extern AudioReverbPreset reverbPreset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001896 RID: 6294
		// (set) Token: 0x06001897 RID: 6295
		public extern int room { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001898 RID: 6296
		// (set) Token: 0x06001899 RID: 6297
		public extern int roomHF { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600189A RID: 6298
		// (set) Token: 0x0600189B RID: 6299
		public extern int roomLF { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600189C RID: 6300
		// (set) Token: 0x0600189D RID: 6301
		public extern float decayTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600189E RID: 6302
		// (set) Token: 0x0600189F RID: 6303
		public extern float decayHFRatio { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060018A0 RID: 6304
		// (set) Token: 0x060018A1 RID: 6305
		public extern int reflections { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060018A2 RID: 6306
		// (set) Token: 0x060018A3 RID: 6307
		public extern float reflectionsDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060018A4 RID: 6308
		// (set) Token: 0x060018A5 RID: 6309
		public extern int reverb { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060018A6 RID: 6310
		// (set) Token: 0x060018A7 RID: 6311
		public extern float reverbDelay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060018A8 RID: 6312
		// (set) Token: 0x060018A9 RID: 6313
		public extern float HFReference { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060018AA RID: 6314
		// (set) Token: 0x060018AB RID: 6315
		public extern float LFReference { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060018AC RID: 6316
		// (set) Token: 0x060018AD RID: 6317
		public extern float roomRolloffFactor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060018AE RID: 6318
		// (set) Token: 0x060018AF RID: 6319
		public extern float diffusion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060018B0 RID: 6320
		// (set) Token: 0x060018B1 RID: 6321
		public extern float density { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
