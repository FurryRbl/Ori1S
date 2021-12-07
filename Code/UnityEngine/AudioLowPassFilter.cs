using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000189 RID: 393
	public sealed class AudioLowPassFilter : Behaviour
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060018B3 RID: 6323
		// (set) Token: 0x060018B4 RID: 6324
		public extern float cutoffFrequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060018B5 RID: 6325
		// (set) Token: 0x060018B6 RID: 6326
		public extern AnimationCurve customCutoffCurve { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060018B7 RID: 6327
		// (set) Token: 0x060018B8 RID: 6328
		public extern float lowpassResonanceQ { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
