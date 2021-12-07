using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.VR
{
	// Token: 0x02000265 RID: 613
	public sealed class VRSettings
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002491 RID: 9361
		// (set) Token: 0x06002492 RID: 9362
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002493 RID: 9363
		// (set) Token: 0x06002494 RID: 9364
		public static extern bool showDeviceView { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002495 RID: 9365
		// (set) Token: 0x06002496 RID: 9366
		public static extern float renderScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002497 RID: 9367
		// (set) Token: 0x06002498 RID: 9368
		public static extern VRDeviceType loadedDevice { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
