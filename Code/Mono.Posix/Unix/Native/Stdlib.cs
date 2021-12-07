using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x0200002E RID: 46
	public class Stdlib
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000A068 File Offset: 0x00008268
		internal Stdlib()
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000A070 File Offset: 0x00008270
		static Stdlib()
		{
			Array values = Enum.GetValues(typeof(Signum));
			Stdlib.registered_signals = new SignalHandler[(int)values.GetValue(values.Length - 1)];
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public static Errno GetLastError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			return NativeConvert.ToErrno(lastWin32Error);
		}

		// Token: 0x06000300 RID: 768
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SetLastError")]
		private static extern void SetLastError(int error);

		// Token: 0x06000301 RID: 769 RVA: 0x0000A1CC File Offset: 0x000083CC
		protected static void SetLastError(Errno error)
		{
			int lastError = NativeConvert.FromErrno(error);
			Stdlib.SetLastError(lastError);
		}

		// Token: 0x06000302 RID: 770
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_InvokeSignalHandler")]
		internal static extern void InvokeSignalHandler(int signum, IntPtr handler);

		// Token: 0x06000303 RID: 771
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_DFL")]
		private static extern IntPtr GetDefaultSignal();

		// Token: 0x06000304 RID: 772
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_ERR")]
		private static extern IntPtr GetErrorSignal();

		// Token: 0x06000305 RID: 773
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_SIG_IGN")]
		private static extern IntPtr GetIgnoreSignal();

		// Token: 0x06000306 RID: 774 RVA: 0x0000A1E8 File Offset: 0x000083E8
		private static void _ErrorHandler(int signum)
		{
			Console.Error.WriteLine("Error handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A20C File Offset: 0x0000840C
		private static void _DefaultHandler(int signum)
		{
			Console.Error.WriteLine("Default handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A230 File Offset: 0x00008430
		private static void _IgnoreHandler(int signum)
		{
			Console.Error.WriteLine("Ignore handler invoked for signum " + signum + ".  Don't do that.");
		}

		// Token: 0x06000309 RID: 777
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "signal", SetLastError = true)]
		private static extern IntPtr sys_signal(int signum, SignalHandler handler);

		// Token: 0x0600030A RID: 778
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "signal", SetLastError = true)]
		private static extern IntPtr sys_signal(int signum, IntPtr handler);

		// Token: 0x0600030B RID: 779 RVA: 0x0000A254 File Offset: 0x00008454
		[CLSCompliant(false)]
		[Obsolete("This is not safe; use Mono.Unix.UnixSignal for signal delivery or SetSignalAction()")]
		public static SignalHandler signal(Signum signum, SignalHandler handler)
		{
			int signum2 = NativeConvert.FromSignum(signum);
			Delegate[] invocationList = handler.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				Marshal.Prelink(invocationList[i].Method);
			}
			SignalHandler[] obj = Stdlib.registered_signals;
			lock (obj)
			{
				Stdlib.registered_signals[(int)signum] = handler;
			}
			IntPtr handler2;
			if (handler == Stdlib.SIG_DFL)
			{
				handler2 = Stdlib.sys_signal(signum2, Stdlib._SIG_DFL);
			}
			else if (handler == Stdlib.SIG_ERR)
			{
				handler2 = Stdlib.sys_signal(signum2, Stdlib._SIG_ERR);
			}
			else if (handler == Stdlib.SIG_IGN)
			{
				handler2 = Stdlib.sys_signal(signum2, Stdlib._SIG_IGN);
			}
			else
			{
				handler2 = Stdlib.sys_signal(signum2, handler);
			}
			return Stdlib.TranslateHandler(handler2);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A344 File Offset: 0x00008544
		private static SignalHandler TranslateHandler(IntPtr handler)
		{
			if (handler == Stdlib._SIG_DFL)
			{
				return Stdlib.SIG_DFL;
			}
			if (handler == Stdlib._SIG_ERR)
			{
				return Stdlib.SIG_ERR;
			}
			if (handler == Stdlib._SIG_IGN)
			{
				return Stdlib.SIG_IGN;
			}
			return (SignalHandler)Marshal.GetDelegateForFunctionPointer(handler, typeof(SignalHandler));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public static int SetSignalAction(Signum signal, SignalAction action)
		{
			return Stdlib.SetSignalAction(NativeConvert.FromSignum(signal), action);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public static int SetSignalAction(RealTimeSignum rts, SignalAction action)
		{
			return Stdlib.SetSignalAction(NativeConvert.FromRealTimeSignum(rts), action);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A3C8 File Offset: 0x000085C8
		private static int SetSignalAction(int signum, SignalAction action)
		{
			IntPtr handler = IntPtr.Zero;
			switch (action)
			{
			case SignalAction.Default:
				handler = Stdlib._SIG_DFL;
				break;
			case SignalAction.Ignore:
				handler = Stdlib._SIG_IGN;
				break;
			case SignalAction.Error:
				handler = Stdlib._SIG_ERR;
				break;
			default:
				throw new ArgumentException("Invalid action value.", "action");
			}
			IntPtr value = Stdlib.sys_signal(signum, handler);
			if (value == Stdlib._SIG_ERR)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000310 RID: 784
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "raise")]
		private static extern int sys_raise(int sig);

		// Token: 0x06000311 RID: 785 RVA: 0x0000A440 File Offset: 0x00008640
		[CLSCompliant(false)]
		public static int raise(Signum sig)
		{
			return Stdlib.sys_raise(NativeConvert.FromSignum(sig));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A450 File Offset: 0x00008650
		public static int raise(RealTimeSignum rts)
		{
			return Stdlib.sys_raise(NativeConvert.FromRealTimeSignum(rts));
		}

		// Token: 0x06000313 RID: 787
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOFBF")]
		private static extern int GetFullyBuffered();

		// Token: 0x06000314 RID: 788
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IOLBF")]
		private static extern int GetLineBuffered();

		// Token: 0x06000315 RID: 789
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib__IONBF")]
		private static extern int GetNonBuffered();

		// Token: 0x06000316 RID: 790
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_BUFSIZ")]
		private static extern int GetBufferSize();

		// Token: 0x06000317 RID: 791
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_CreateFilePosition")]
		internal static extern IntPtr CreateFilePosition();

		// Token: 0x06000318 RID: 792
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_DumpFilePosition")]
		internal static extern int DumpFilePosition(StringBuilder buf, HandleRef handle, int len);

		// Token: 0x06000319 RID: 793
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EOF")]
		private static extern int GetEOF();

		// Token: 0x0600031A RID: 794
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FILENAME_MAX")]
		private static extern int GetFilenameMax();

		// Token: 0x0600031B RID: 795
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_FOPEN_MAX")]
		private static extern int GetFopenMax();

		// Token: 0x0600031C RID: 796
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_L_tmpnam")]
		private static extern int GetTmpnamLength();

		// Token: 0x0600031D RID: 797
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdin")]
		private static extern IntPtr GetStandardInput();

		// Token: 0x0600031E RID: 798
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stdout")]
		private static extern IntPtr GetStandardOutput();

		// Token: 0x0600031F RID: 799
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_stderr")]
		private static extern IntPtr GetStandardError();

		// Token: 0x06000320 RID: 800
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_TMP_MAX")]
		private static extern int GetTmpMax();

		// Token: 0x06000321 RID: 801
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int remove([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string filename);

		// Token: 0x06000322 RID: 802
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int rename([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string oldpath, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string newpath);

		// Token: 0x06000323 RID: 803
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern IntPtr tmpfile();

		// Token: 0x06000324 RID: 804
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tmpnam", SetLastError = true)]
		private static extern IntPtr sys_tmpnam(StringBuilder s);

		// Token: 0x06000325 RID: 805 RVA: 0x0000A460 File Offset: 0x00008660
		[Obsolete("Syscall.mkstemp() should be preferred.")]
		public static string tmpnam(StringBuilder s)
		{
			if (s != null && s.Capacity < Stdlib.L_tmpnam)
			{
				throw new ArgumentOutOfRangeException("s", "s.Capacity < L_tmpnam");
			}
			object obj = Stdlib.tmpnam_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Stdlib.sys_tmpnam(s);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000A4DC File Offset: 0x000086DC
		[Obsolete("Syscall.mkstemp() should be preferred.")]
		public static string tmpnam()
		{
			object obj = Stdlib.tmpnam_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Stdlib.sys_tmpnam(null);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x06000327 RID: 807
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int fclose(IntPtr stream);

		// Token: 0x06000328 RID: 808
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int fflush(IntPtr stream);

		// Token: 0x06000329 RID: 809
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern IntPtr fopen([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string mode);

		// Token: 0x0600032A RID: 810
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern IntPtr freopen([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Mono.Unix.Native.FileNameMarshaler)] string path, string mode, IntPtr stream);

		// Token: 0x0600032B RID: 811
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_setbuf", SetLastError = true)]
		public static extern int setbuf(IntPtr stream, IntPtr buf);

		// Token: 0x0600032C RID: 812 RVA: 0x0000A534 File Offset: 0x00008734
		[CLSCompliant(false)]
		public unsafe static int setbuf(IntPtr stream, byte* buf)
		{
			return Stdlib.setbuf(stream, (IntPtr)((void*)buf));
		}

		// Token: 0x0600032D RID: 813
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_setvbuf", SetLastError = true)]
		public static extern int setvbuf(IntPtr stream, IntPtr buf, int mode, ulong size);

		// Token: 0x0600032E RID: 814 RVA: 0x0000A544 File Offset: 0x00008744
		[CLSCompliant(false)]
		public unsafe static int setvbuf(IntPtr stream, byte* buf, int mode, ulong size)
		{
			return Stdlib.setvbuf(stream, (IntPtr)((void*)buf), mode, size);
		}

		// Token: 0x0600032F RID: 815
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fprintf")]
		private static extern int sys_fprintf(IntPtr stream, string format, string message);

		// Token: 0x06000330 RID: 816 RVA: 0x0000A554 File Offset: 0x00008754
		public static int fprintf(IntPtr stream, string message)
		{
			return Stdlib.sys_fprintf(stream, "%s", message);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A564 File Offset: 0x00008764
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse fprintf (IntPtr, string) instead.")]
		public static int fprintf(IntPtr stream, string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 2)];
			array[0] = stream;
			array[1] = format;
			Array.Copy(parameters, 0, array, 2, parameters.Length);
			return (int)XPrintfFunctions.fprintf(array);
		}

		// Token: 0x06000332 RID: 818
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "printf")]
		private static extern int sys_printf(string format, string message);

		// Token: 0x06000333 RID: 819 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public static int printf(string message)
		{
			return Stdlib.sys_printf("%s", message);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse printf (string) instead.")]
		public static int printf(string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 1)];
			array[0] = format;
			Array.Copy(parameters, 0, array, 1, parameters.Length);
			return (int)XPrintfFunctions.printf(array);
		}

		// Token: 0x06000335 RID: 821
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_snprintf")]
		private static extern int sys_snprintf(StringBuilder s, ulong n, string format, string message);

		// Token: 0x06000336 RID: 822 RVA: 0x0000A5F0 File Offset: 0x000087F0
		[CLSCompliant(false)]
		public static int snprintf(StringBuilder s, ulong n, string message)
		{
			if (n > (ulong)((long)s.Capacity))
			{
				throw new ArgumentOutOfRangeException("n", "n must be <= s.Capacity");
			}
			return Stdlib.sys_snprintf(s, n, "%s", message);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000A628 File Offset: 0x00008828
		public static int snprintf(StringBuilder s, string message)
		{
			return Stdlib.sys_snprintf(s, (ulong)((long)s.Capacity), "%s", message);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A640 File Offset: 0x00008840
		[CLSCompliant(false)]
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse snprintf (StringBuilder, string) instead.")]
		public static int snprintf(StringBuilder s, ulong n, string format, params object[] parameters)
		{
			if (n > (ulong)((long)s.Capacity))
			{
				throw new ArgumentOutOfRangeException("n", "n must be <= s.Capacity");
			}
			object[] array = new object[checked(parameters.Length + 3)];
			array[0] = s;
			array[1] = n;
			array[2] = format;
			Array.Copy(parameters, 0, array, 3, parameters.Length);
			return (int)XPrintfFunctions.snprintf(array);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A6A4 File Offset: 0x000088A4
		[CLSCompliant(false)]
		[Obsolete("Not necessarily portable due to cdecl restrictions.\nUse snprintf (StringBuilder, string) instead.")]
		public static int snprintf(StringBuilder s, string format, params object[] parameters)
		{
			object[] array = new object[checked(parameters.Length + 3)];
			array[0] = s;
			array[1] = (ulong)((long)s.Capacity);
			array[2] = format;
			Array.Copy(parameters, 0, array, 3, parameters.Length);
			return (int)XPrintfFunctions.snprintf(array);
		}

		// Token: 0x0600033A RID: 826
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int fgetc(IntPtr stream);

		// Token: 0x0600033B RID: 827
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "fgets", SetLastError = true)]
		private static extern IntPtr sys_fgets(StringBuilder sb, int size, IntPtr stream);

		// Token: 0x0600033C RID: 828 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public static StringBuilder fgets(StringBuilder sb, int size, IntPtr stream)
		{
			IntPtr value = Stdlib.sys_fgets(sb, size, stream);
			if (value == IntPtr.Zero)
			{
				return null;
			}
			return sb;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000A71C File Offset: 0x0000891C
		public static StringBuilder fgets(StringBuilder sb, IntPtr stream)
		{
			return Stdlib.fgets(sb, sb.Capacity, stream);
		}

		// Token: 0x0600033E RID: 830
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int fputc(int c, IntPtr stream);

		// Token: 0x0600033F RID: 831
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int fputs(string s, IntPtr stream);

		// Token: 0x06000340 RID: 832
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int getc(IntPtr stream);

		// Token: 0x06000341 RID: 833
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int getchar();

		// Token: 0x06000342 RID: 834
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int putc(int c, IntPtr stream);

		// Token: 0x06000343 RID: 835
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int putchar(int c);

		// Token: 0x06000344 RID: 836
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int puts(string s);

		// Token: 0x06000345 RID: 837
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int ungetc(int c, IntPtr stream);

		// Token: 0x06000346 RID: 838
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fread", SetLastError = true)]
		public static extern ulong fread(IntPtr ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x06000347 RID: 839 RVA: 0x0000A72C File Offset: 0x0000892C
		[CLSCompliant(false)]
		public unsafe static ulong fread(void* ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			return Stdlib.fread((IntPtr)ptr, size, nmemb, stream);
		}

		// Token: 0x06000348 RID: 840
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fread", SetLastError = true)]
		private static extern ulong sys_fread([Out] byte[] ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x06000349 RID: 841 RVA: 0x0000A73C File Offset: 0x0000893C
		[CLSCompliant(false)]
		public static ulong fread(byte[] ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			if (size * nmemb > (ulong)((long)ptr.Length))
			{
				throw new ArgumentOutOfRangeException("nmemb");
			}
			return Stdlib.sys_fread(ptr, size, nmemb, stream);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000A76C File Offset: 0x0000896C
		[CLSCompliant(false)]
		public static ulong fread(byte[] ptr, IntPtr stream)
		{
			return Stdlib.fread(ptr, 1UL, (ulong)((long)ptr.Length), stream);
		}

		// Token: 0x0600034B RID: 843
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fwrite", SetLastError = true)]
		public static extern ulong fwrite(IntPtr ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x0600034C RID: 844 RVA: 0x0000A77C File Offset: 0x0000897C
		[CLSCompliant(false)]
		public unsafe static ulong fwrite(void* ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			return Stdlib.fwrite((IntPtr)ptr, size, nmemb, stream);
		}

		// Token: 0x0600034D RID: 845
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fwrite", SetLastError = true)]
		private static extern ulong sys_fwrite(byte[] ptr, ulong size, ulong nmemb, IntPtr stream);

		// Token: 0x0600034E RID: 846 RVA: 0x0000A78C File Offset: 0x0000898C
		[CLSCompliant(false)]
		public static ulong fwrite(byte[] ptr, ulong size, ulong nmemb, IntPtr stream)
		{
			if (size * nmemb > (ulong)((long)ptr.Length))
			{
				throw new ArgumentOutOfRangeException("nmemb");
			}
			return Stdlib.sys_fwrite(ptr, size, nmemb, stream);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A7BC File Offset: 0x000089BC
		[CLSCompliant(false)]
		public static ulong fwrite(byte[] ptr, IntPtr stream)
		{
			return Stdlib.fwrite(ptr, 1UL, (ulong)((long)ptr.Length), stream);
		}

		// Token: 0x06000350 RID: 848
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fgetpos", SetLastError = true)]
		private static extern int sys_fgetpos(IntPtr stream, HandleRef pos);

		// Token: 0x06000351 RID: 849 RVA: 0x0000A7CC File Offset: 0x000089CC
		public static int fgetpos(IntPtr stream, FilePosition pos)
		{
			return Stdlib.sys_fgetpos(stream, pos.Handle);
		}

		// Token: 0x06000352 RID: 850
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fseek", SetLastError = true)]
		private static extern int sys_fseek(IntPtr stream, long offset, int origin);

		// Token: 0x06000353 RID: 851 RVA: 0x0000A7DC File Offset: 0x000089DC
		[CLSCompliant(false)]
		public static int fseek(IntPtr stream, long offset, SeekFlags origin)
		{
			int origin2 = (int)NativeConvert.FromSeekFlags(origin);
			return Stdlib.sys_fseek(stream, offset, origin2);
		}

		// Token: 0x06000354 RID: 852
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_fsetpos", SetLastError = true)]
		private static extern int sys_fsetpos(IntPtr stream, HandleRef pos);

		// Token: 0x06000355 RID: 853 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public static int fsetpos(IntPtr stream, FilePosition pos)
		{
			return Stdlib.sys_fsetpos(stream, pos.Handle);
		}

		// Token: 0x06000356 RID: 854
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_ftell", SetLastError = true)]
		public static extern long ftell(IntPtr stream);

		// Token: 0x06000357 RID: 855
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_rewind", SetLastError = true)]
		public static extern int rewind(IntPtr stream);

		// Token: 0x06000358 RID: 856
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_clearerr", SetLastError = true)]
		public static extern int clearerr(IntPtr stream);

		// Token: 0x06000359 RID: 857
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern int feof(IntPtr stream);

		// Token: 0x0600035A RID: 858
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ferror(IntPtr stream);

		// Token: 0x0600035B RID: 859
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_perror", SetLastError = true)]
		private static extern int perror(string s, int err);

		// Token: 0x0600035C RID: 860 RVA: 0x0000A808 File Offset: 0x00008A08
		public static int perror(string s)
		{
			return Stdlib.perror(s, Marshal.GetLastWin32Error());
		}

		// Token: 0x0600035D RID: 861
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_FAILURE")]
		private static extern int GetExitFailure();

		// Token: 0x0600035E RID: 862
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_EXIT_SUCCESS")]
		private static extern int GetExitSuccess();

		// Token: 0x0600035F RID: 863
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_MB_CUR_MAX")]
		private static extern int GetMbCurMax();

		// Token: 0x06000360 RID: 864
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_RAND_MAX")]
		private static extern int GetRandMax();

		// Token: 0x06000361 RID: 865
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern int rand();

		// Token: 0x06000362 RID: 866
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void srand(uint seed);

		// Token: 0x06000363 RID: 867
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_calloc", SetLastError = true)]
		public static extern IntPtr calloc(ulong nmemb, ulong size);

		// Token: 0x06000364 RID: 868
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void free(IntPtr ptr);

		// Token: 0x06000365 RID: 869
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_malloc", SetLastError = true)]
		public static extern IntPtr malloc(ulong size);

		// Token: 0x06000366 RID: 870
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_realloc", SetLastError = true)]
		public static extern IntPtr realloc(IntPtr ptr, ulong size);

		// Token: 0x06000367 RID: 871
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void abort();

		// Token: 0x06000368 RID: 872
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void exit(int status);

		// Token: 0x06000369 RID: 873
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
		public static extern void _Exit(int status);

		// Token: 0x0600036A RID: 874
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "getenv")]
		private static extern IntPtr sys_getenv(string name);

		// Token: 0x0600036B RID: 875 RVA: 0x0000A818 File Offset: 0x00008A18
		public static string getenv(string name)
		{
			IntPtr p = Stdlib.sys_getenv(name);
			return UnixMarshal.PtrToString(p);
		}

		// Token: 0x0600036C RID: 876
		[CLSCompliant(false)]
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		public static extern int system(string @string);

		// Token: 0x0600036D RID: 877
		[DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl, EntryPoint = "strerror", SetLastError = true)]
		private static extern IntPtr sys_strerror(int errnum);

		// Token: 0x0600036E RID: 878 RVA: 0x0000A834 File Offset: 0x00008A34
		[CLSCompliant(false)]
		public static string strerror(Errno errnum)
		{
			int errnum2 = NativeConvert.FromErrno(errnum);
			object obj = Stdlib.strerror_lock;
			string result;
			lock (obj)
			{
				IntPtr p = Stdlib.sys_strerror(errnum2);
				result = UnixMarshal.PtrToString(p);
			}
			return result;
		}

		// Token: 0x0600036F RID: 879
		[CLSCompliant(false)]
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Mono_Posix_Stdlib_strlen", SetLastError = true)]
		public static extern ulong strlen(IntPtr s);

		// Token: 0x04000124 RID: 292
		internal const string LIBC = "msvcrt";

		// Token: 0x04000125 RID: 293
		internal const string MPH = "MonoPosixHelper";

		// Token: 0x04000126 RID: 294
		private static readonly IntPtr _SIG_DFL = Stdlib.GetDefaultSignal();

		// Token: 0x04000127 RID: 295
		private static readonly IntPtr _SIG_ERR = Stdlib.GetErrorSignal();

		// Token: 0x04000128 RID: 296
		private static readonly IntPtr _SIG_IGN = Stdlib.GetIgnoreSignal();

		// Token: 0x04000129 RID: 297
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_DFL = new SignalHandler(Stdlib._DefaultHandler);

		// Token: 0x0400012A RID: 298
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_ERR = new SignalHandler(Stdlib._ErrorHandler);

		// Token: 0x0400012B RID: 299
		[CLSCompliant(false)]
		public static readonly SignalHandler SIG_IGN = new SignalHandler(Stdlib._IgnoreHandler);

		// Token: 0x0400012C RID: 300
		private static readonly SignalHandler[] registered_signals;

		// Token: 0x0400012D RID: 301
		[CLSCompliant(false)]
		public static readonly int _IOFBF = Stdlib.GetFullyBuffered();

		// Token: 0x0400012E RID: 302
		[CLSCompliant(false)]
		public static readonly int _IOLBF = Stdlib.GetLineBuffered();

		// Token: 0x0400012F RID: 303
		[CLSCompliant(false)]
		public static readonly int _IONBF = Stdlib.GetNonBuffered();

		// Token: 0x04000130 RID: 304
		[CLSCompliant(false)]
		public static readonly int BUFSIZ = Stdlib.GetBufferSize();

		// Token: 0x04000131 RID: 305
		[CLSCompliant(false)]
		public static readonly int EOF = Stdlib.GetEOF();

		// Token: 0x04000132 RID: 306
		[CLSCompliant(false)]
		public static readonly int FOPEN_MAX = Stdlib.GetFopenMax();

		// Token: 0x04000133 RID: 307
		[CLSCompliant(false)]
		public static readonly int FILENAME_MAX = Stdlib.GetFilenameMax();

		// Token: 0x04000134 RID: 308
		[CLSCompliant(false)]
		public static readonly int L_tmpnam = Stdlib.GetTmpnamLength();

		// Token: 0x04000135 RID: 309
		public static readonly IntPtr stderr = Stdlib.GetStandardError();

		// Token: 0x04000136 RID: 310
		public static readonly IntPtr stdin = Stdlib.GetStandardInput();

		// Token: 0x04000137 RID: 311
		public static readonly IntPtr stdout = Stdlib.GetStandardOutput();

		// Token: 0x04000138 RID: 312
		[CLSCompliant(false)]
		public static readonly int TMP_MAX = Stdlib.GetTmpMax();

		// Token: 0x04000139 RID: 313
		private static object tmpnam_lock = new object();

		// Token: 0x0400013A RID: 314
		[CLSCompliant(false)]
		public static readonly int EXIT_FAILURE = Stdlib.GetExitFailure();

		// Token: 0x0400013B RID: 315
		[CLSCompliant(false)]
		public static readonly int EXIT_SUCCESS = Stdlib.GetExitSuccess();

		// Token: 0x0400013C RID: 316
		[CLSCompliant(false)]
		public static readonly int MB_CUR_MAX = Stdlib.GetMbCurMax();

		// Token: 0x0400013D RID: 317
		[CLSCompliant(false)]
		public static readonly int RAND_MAX = Stdlib.GetRandMax();

		// Token: 0x0400013E RID: 318
		private static object strerror_lock = new object();
	}
}
