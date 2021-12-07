using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018A RID: 394
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060018BA RID: 6330
		// (set) Token: 0x060018BB RID: 6331
		public extern float cutoffFrequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060018BC RID: 6332
		// (set) Token: 0x060018BD RID: 6333
		public extern float highpassResonanceQ { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
