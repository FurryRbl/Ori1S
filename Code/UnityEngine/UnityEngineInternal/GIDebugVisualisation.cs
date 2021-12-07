using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEngineInternal
{
	// Token: 0x020000E5 RID: 229
	public sealed class GIDebugVisualisation
	{
		// Token: 0x06000EA0 RID: 3744
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetRuntimeInputTextures();

		// Token: 0x06000EA1 RID: 3745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PlayCycleMode();

		// Token: 0x06000EA2 RID: 3746
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PauseCycleMode();

		// Token: 0x06000EA3 RID: 3747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCycleMode();

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000EA4 RID: 3748
		public static extern bool cycleMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000EA5 RID: 3749
		public static extern bool pauseCycleMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000EA6 RID: 3750
		// (set) Token: 0x06000EA7 RID: 3751
		public static extern GITextureType texType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EA8 RID: 3752
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipInstances(int skip);

		// Token: 0x06000EA9 RID: 3753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CycleSkipSystems(int skip);
	}
}
