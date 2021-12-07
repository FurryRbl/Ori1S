using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016A RID: 362
	public sealed class PointEffector2D : Effector2D
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001717 RID: 5911
		// (set) Token: 0x06001718 RID: 5912
		public extern float forceMagnitude { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001719 RID: 5913
		// (set) Token: 0x0600171A RID: 5914
		public extern float forceVariation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600171B RID: 5915
		// (set) Token: 0x0600171C RID: 5916
		public extern float distanceScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600171D RID: 5917
		// (set) Token: 0x0600171E RID: 5918
		public extern float drag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600171F RID: 5919
		// (set) Token: 0x06001720 RID: 5920
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001721 RID: 5921
		// (set) Token: 0x06001722 RID: 5922
		public extern EffectorSelection2D forceSource { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001723 RID: 5923
		// (set) Token: 0x06001724 RID: 5924
		public extern EffectorSelection2D forceTarget { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001725 RID: 5925
		// (set) Token: 0x06001726 RID: 5926
		public extern EffectorForceMode2D forceMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
