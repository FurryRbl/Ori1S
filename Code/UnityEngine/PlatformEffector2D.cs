using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016B RID: 363
	public sealed class PlatformEffector2D : Effector2D
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001728 RID: 5928
		// (set) Token: 0x06001729 RID: 5929
		public extern bool useOneWay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600172A RID: 5930
		// (set) Token: 0x0600172B RID: 5931
		public extern bool useOneWayGrouping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600172C RID: 5932
		// (set) Token: 0x0600172D RID: 5933
		public extern bool useSideFriction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600172E RID: 5934
		// (set) Token: 0x0600172F RID: 5935
		public extern bool useSideBounce { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001730 RID: 5936
		// (set) Token: 0x06001731 RID: 5937
		public extern float surfaceArc { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001732 RID: 5938
		// (set) Token: 0x06001733 RID: 5939
		public extern float sideArc { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
