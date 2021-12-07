using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	public sealed class Profiler
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122
		public static extern bool supported { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007B RID: 123
		// (set) Token: 0x0600007C RID: 124
		public static extern string logFile { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007D RID: 125
		// (set) Token: 0x0600007E RID: 126
		public static extern bool enableBinaryLog { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		public static extern bool enabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000081 RID: 129
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddFramesFromFile(string file);

		// Token: 0x06000082 RID: 130 RVA: 0x00002560 File Offset: 0x00000760
		[Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name)
		{
			Profiler.BeginSampleOnly(name);
		}

		// Token: 0x06000083 RID: 131
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(string name, Object targetObject);

		// Token: 0x06000084 RID: 132
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSampleOnly(string name);

		// Token: 0x06000085 RID: 133
		[Conditional("ENABLE_PROFILER")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000086 RID: 134
		// (set) Token: 0x06000087 RID: 135
		public static extern int maxNumberOfSamplesPerFrame { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000088 RID: 136
		public static extern uint usedHeapSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000089 RID: 137
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRuntimeMemorySize(Object o);

		// Token: 0x0600008A RID: 138
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetMonoHeapSize();

		// Token: 0x0600008B RID: 139
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetMonoUsedSize();

		// Token: 0x0600008C RID: 140
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalAllocatedMemory();

		// Token: 0x0600008D RID: 141
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalUnusedReservedMemory();

		// Token: 0x0600008E RID: 142
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTotalReservedMemory();
	}
}
