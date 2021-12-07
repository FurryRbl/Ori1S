using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Connect
{
	// Token: 0x0200025A RID: 602
	internal class UnityPurchasingSettings
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600240E RID: 9230
		// (set) Token: 0x0600240F RID: 9231
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002410 RID: 9232
		// (set) Token: 0x06002411 RID: 9233
		public static extern bool testMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
