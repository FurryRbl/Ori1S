using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200022B RID: 555
	public sealed class ClusterNetwork
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600222D RID: 8749
		public static extern bool isMasterOfCluster { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600222E RID: 8750
		public static extern bool isDisconnected { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600222F RID: 8751
		// (set) Token: 0x06002230 RID: 8752
		public static extern int nodeIndex { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
