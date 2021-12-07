using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000169 RID: 361
	public sealed class BuoyancyEffector2D : Effector2D
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001708 RID: 5896
		// (set) Token: 0x06001709 RID: 5897
		public extern float surfaceLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600170A RID: 5898
		// (set) Token: 0x0600170B RID: 5899
		public extern float density { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600170C RID: 5900
		// (set) Token: 0x0600170D RID: 5901
		public extern float linearDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600170E RID: 5902
		// (set) Token: 0x0600170F RID: 5903
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001710 RID: 5904
		// (set) Token: 0x06001711 RID: 5905
		public extern float flowAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001712 RID: 5906
		// (set) Token: 0x06001713 RID: 5907
		public extern float flowMagnitude { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001714 RID: 5908
		// (set) Token: 0x06001715 RID: 5909
		public extern float flowVariation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
