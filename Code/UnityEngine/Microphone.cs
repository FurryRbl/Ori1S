using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200018F RID: 399
	public sealed class Microphone
	{
		// Token: 0x060018FD RID: 6397
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency);

		// Token: 0x060018FE RID: 6398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void End(string deviceName);

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060018FF RID: 6399
		public static extern string[] devices { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001900 RID: 6400
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsRecording(string deviceName);

		// Token: 0x06001901 RID: 6401
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPosition(string deviceName);

		// Token: 0x06001902 RID: 6402
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq);
	}
}
