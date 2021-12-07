using System;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000020 RID: 32
	public class UnixSignal : WaitHandle
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000073CC File Offset: 0x000055CC
		public UnixSignal(Signum signum)
		{
			this.signum = NativeConvert.FromSignum(signum);
			this.signal_info = UnixSignal.install(this.signum);
			if (this.signal_info == IntPtr.Zero)
			{
				throw new ArgumentException("Unable to handle signal", "signum");
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007424 File Offset: 0x00005624
		public UnixSignal(RealTimeSignum rtsig)
		{
			this.signum = NativeConvert.FromRealTimeSignum(rtsig);
			this.signal_info = UnixSignal.install(this.signum);
			Errno lastError = Stdlib.GetLastError();
			if (!(this.signal_info == IntPtr.Zero))
			{
				return;
			}
			if (lastError == Errno.EADDRINUSE)
			{
				throw new ArgumentException("Signal registered outside of Mono.Posix", "signum");
			}
			throw new ArgumentException("Unable to handle signal", "signum");
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007498 File Offset: 0x00005698
		public Signum Signum
		{
			get
			{
				if (this.IsRealTimeSignal)
				{
					throw new InvalidOperationException("This signal is a RealTimeSignum");
				}
				return NativeConvert.ToSignum(this.signum);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000074BC File Offset: 0x000056BC
		public RealTimeSignum RealTimeSignum
		{
			get
			{
				if (!this.IsRealTimeSignal)
				{
					throw new InvalidOperationException("This signal is not a RealTimeSignum");
				}
				return NativeConvert.ToRealTimeSignum(this.signum - UnixSignal.GetSIGRTMIN());
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000074E8 File Offset: 0x000056E8
		public bool IsRealTimeSignal
		{
			get
			{
				this.AssertValid();
				int sigrtmin = UnixSignal.GetSIGRTMIN();
				return sigrtmin != -1 && this.signum >= sigrtmin;
			}
		}

		// Token: 0x06000198 RID: 408
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_install", SetLastError = true)]
		private static extern IntPtr install(int signum);

		// Token: 0x06000199 RID: 409
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_uninstall")]
		private static extern int uninstall(IntPtr info);

		// Token: 0x0600019A RID: 410
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Unix_UnixSignal_WaitAny")]
		private static extern int WaitAny(IntPtr[] infos, int count, int timeout, UnixSignal.Mono_Posix_RuntimeIsShuttingDown shutting_down);

		// Token: 0x0600019B RID: 411
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_SIGRTMIN")]
		internal static extern int GetSIGRTMIN();

		// Token: 0x0600019C RID: 412
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_SIGRTMAX")]
		internal static extern int GetSIGRTMAX();

		// Token: 0x0600019D RID: 413 RVA: 0x00007518 File Offset: 0x00005718
		private void AssertValid()
		{
			if (this.signal_info == IntPtr.Zero)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000754C File Offset: 0x0000574C
		private unsafe UnixSignal.SignalInfo* Info
		{
			get
			{
				this.AssertValid();
				return (UnixSignal.SignalInfo*)((void*)this.signal_info);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007560 File Offset: 0x00005760
		public bool IsSet
		{
			get
			{
				return this.Count > 0;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000756C File Offset: 0x0000576C
		public unsafe bool Reset()
		{
			int num = Interlocked.Exchange(ref this.Info->count, 0);
			return num != 0;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007594 File Offset: 0x00005794
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000075A4 File Offset: 0x000057A4
		public unsafe int Count
		{
			get
			{
				return this.Info->count;
			}
			set
			{
				Interlocked.Exchange(ref this.Info->count, value);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000075B8 File Offset: 0x000057B8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.signal_info == IntPtr.Zero)
			{
				return;
			}
			UnixSignal.uninstall(this.signal_info);
			this.signal_info = IntPtr.Zero;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000075FC File Offset: 0x000057FC
		public override bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007608 File Offset: 0x00005808
		public override bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.WaitOne((int)num, exitContext);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007648 File Offset: 0x00005848
		public override bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			this.AssertValid();
			if (exitContext)
			{
				throw new InvalidOperationException("exitContext is not supported");
			}
			return UnixSignal.WaitAny(new UnixSignal[]
			{
				this
			}, millisecondsTimeout) == 0;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007680 File Offset: 0x00005880
		public static int WaitAny(UnixSignal[] signals)
		{
			return UnixSignal.WaitAny(signals, -1);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000768C File Offset: 0x0000588C
		public static int WaitAny(UnixSignal[] signals, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return UnixSignal.WaitAny(signals, (int)num);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000076CC File Offset: 0x000058CC
		public static int WaitAny(UnixSignal[] signals, int millisecondsTimeout)
		{
			if (signals == null)
			{
				throw new ArgumentNullException("signals");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			IntPtr[] array = new IntPtr[signals.Length];
			for (int i = 0; i < signals.Length; i++)
			{
				array[i] = signals[i].signal_info;
				if (array[i] == IntPtr.Zero)
				{
					throw new InvalidOperationException("Disposed UnixSignal");
				}
			}
			return UnixSignal.WaitAny(array, array.Length, millisecondsTimeout, () => (!Environment.HasShutdownStarted) ? 0 : 1);
		}

		// Token: 0x04000076 RID: 118
		private int signum;

		// Token: 0x04000077 RID: 119
		private IntPtr signal_info;

		// Token: 0x02000021 RID: 33
		[Map]
		private struct SignalInfo
		{
			// Token: 0x04000079 RID: 121
			public int signum;

			// Token: 0x0400007A RID: 122
			public int count;

			// Token: 0x0400007B RID: 123
			public int read_fd;

			// Token: 0x0400007C RID: 124
			public int write_fd;

			// Token: 0x0400007D RID: 125
			public int have_handler;

			// Token: 0x0400007E RID: 126
			public int pipecnt;

			// Token: 0x0400007F RID: 127
			public IntPtr handler;
		}

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x06000603 RID: 1539
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int Mono_Posix_RuntimeIsShuttingDown();
	}
}
