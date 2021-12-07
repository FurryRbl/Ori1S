using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.VR
{
	// Token: 0x02000266 RID: 614
	public sealed class VRDevice
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600249A RID: 9370
		public static extern bool isPresent { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600249B RID: 9371
		public static extern string family { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600249C RID: 9372
		public static extern string model { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600249D RID: 9373
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetNativePtr();
	}
}
