using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x02000291 RID: 657
	internal sealed class MonoIO
	{
		// Token: 0x060016E4 RID: 5860
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Close(IntPtr handle, out MonoIOError error);

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060016E5 RID: 5861
		public static extern IntPtr ConsoleOutput { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060016E6 RID: 5862
		public static extern IntPtr ConsoleInput { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060016E7 RID: 5863
		public static extern IntPtr ConsoleError { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060016E8 RID: 5864
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CreatePipe(out IntPtr read_handle, out IntPtr write_handle);

		// Token: 0x060016E9 RID: 5865
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DuplicateHandle(IntPtr source_process_handle, IntPtr source_handle, IntPtr target_process_handle, out IntPtr target_handle, int access, int inherit, int options);

		// Token: 0x060016EA RID: 5866
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetTempPath(out string path);
	}
}
