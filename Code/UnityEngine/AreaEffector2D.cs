using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000168 RID: 360
	public sealed class AreaEffector2D : Effector2D
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060016F9 RID: 5881
		// (set) Token: 0x060016FA RID: 5882
		public extern float forceAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060016FB RID: 5883
		// (set) Token: 0x060016FC RID: 5884
		public extern bool useGlobalAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060016FD RID: 5885
		// (set) Token: 0x060016FE RID: 5886
		public extern float forceMagnitude { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060016FF RID: 5887
		// (set) Token: 0x06001700 RID: 5888
		public extern float forceVariation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001701 RID: 5889
		// (set) Token: 0x06001702 RID: 5890
		public extern float drag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001703 RID: 5891
		// (set) Token: 0x06001704 RID: 5892
		public extern float angularDrag { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001705 RID: 5893
		// (set) Token: 0x06001706 RID: 5894
		public extern EffectorSelection2D forceTarget { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
