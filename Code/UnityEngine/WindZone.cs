using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E1 RID: 225
	public sealed class WindZone : Component
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E83 RID: 3715
		// (set) Token: 0x06000E84 RID: 3716
		public extern WindZoneMode mode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E85 RID: 3717
		// (set) Token: 0x06000E86 RID: 3718
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E87 RID: 3719
		// (set) Token: 0x06000E88 RID: 3720
		public extern float windMain { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E89 RID: 3721
		// (set) Token: 0x06000E8A RID: 3722
		public extern float windTurbulence { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E8B RID: 3723
		// (set) Token: 0x06000E8C RID: 3724
		public extern float windPulseMagnitude { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E8D RID: 3725
		// (set) Token: 0x06000E8E RID: 3726
		public extern float windPulseFrequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
