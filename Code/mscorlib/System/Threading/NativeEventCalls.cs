using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace System.Threading
{
	// Token: 0x020006A7 RID: 1703
	internal sealed class NativeEventCalls
	{
		// Token: 0x060040CD RID: 16589
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateEvent_internal(bool manual, bool initial, string name, out bool created);

		// Token: 0x060040CE RID: 16590
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x060040CF RID: 16591
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool ResetEvent_internal(IntPtr handle);

		// Token: 0x060040D0 RID: 16592
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseEvent_internal(IntPtr handle);

		// Token: 0x060040D1 RID: 16593
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OpenEvent_internal(string name, EventWaitHandleRights rights, out MonoIOError error);
	}
}
