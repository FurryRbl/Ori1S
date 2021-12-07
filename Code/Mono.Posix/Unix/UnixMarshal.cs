using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001C RID: 28
	public sealed class UnixMarshal
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000062C4 File Offset: 0x000044C4
		private UnixMarshal()
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000062CC File Offset: 0x000044CC
		[CLSCompliant(false)]
		public static string GetErrorDescription(Errno errno)
		{
			return ErrorMarshal.Translate(errno);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000062DC File Offset: 0x000044DC
		public static IntPtr AllocHeap(long size)
		{
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", "< 0");
			}
			return Stdlib.malloc((ulong)size);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000062FC File Offset: 0x000044FC
		public static IntPtr ReAllocHeap(IntPtr ptr, long size)
		{
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", "< 0");
			}
			return Stdlib.realloc(ptr, (ulong)size);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006320 File Offset: 0x00004520
		public static void FreeHeap(IntPtr ptr)
		{
			Stdlib.free(ptr);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006328 File Offset: 0x00004528
		public unsafe static string PtrToStringUnix(IntPtr p)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			int length = checked((int)Stdlib.strlen(p));
			return new string((sbyte*)((void*)p), 0, length, UnixEncoding.Instance);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006364 File Offset: 0x00004564
		public static string PtrToString(IntPtr p)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			return UnixMarshal.PtrToString(p, UnixEncoding.Instance);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006384 File Offset: 0x00004584
		public unsafe static string PtrToString(IntPtr p, Encoding encoding)
		{
			if (p == IntPtr.Zero)
			{
				return null;
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int num = UnixMarshal.GetStringByteLength(p, encoding);
			string text = new string((sbyte*)((void*)p), 0, num, encoding);
			num = text.Length;
			while (num > 0 && text[num - 1] == '\0')
			{
				num--;
			}
			if (num == text.Length)
			{
				return text;
			}
			return text.Substring(0, num);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006408 File Offset: 0x00004608
		private static int GetStringByteLength(IntPtr p, Encoding encoding)
		{
			Type type = encoding.GetType();
			int num;
			if (typeof(UTF8Encoding).IsAssignableFrom(type) || typeof(UTF7Encoding).IsAssignableFrom(type) || typeof(UnixEncoding).IsAssignableFrom(type) || typeof(ASCIIEncoding).IsAssignableFrom(type))
			{
				num = checked((int)Stdlib.strlen(p));
			}
			else if (typeof(UnicodeEncoding).IsAssignableFrom(type))
			{
				num = UnixMarshal.GetInt16BufferLength(p);
			}
			else
			{
				num = UnixMarshal.GetRandomBufferLength(p, encoding.GetMaxByteCount(1));
			}
			if (num == -1)
			{
				throw new NotSupportedException("Unable to determine native string buffer length");
			}
			return num;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000064C4 File Offset: 0x000046C4
		private static int GetInt16BufferLength(IntPtr p)
		{
			int num = 0;
			checked
			{
				while (Marshal.ReadInt16(p, unchecked(num * 2)) != 0)
				{
					num++;
				}
				return num * 2;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000064F0 File Offset: 0x000046F0
		private static int GetInt32BufferLength(IntPtr p)
		{
			int num = 0;
			checked
			{
				while (Marshal.ReadInt32(p, unchecked(num * 4)) != 0)
				{
					num++;
				}
				return num * 4;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000651C File Offset: 0x0000471C
		private static int GetRandomBufferLength(IntPtr p, int nullLength)
		{
			switch (nullLength)
			{
			case 1:
				return checked((int)Stdlib.strlen(p));
			case 2:
				return UnixMarshal.GetInt16BufferLength(p);
			case 4:
				return UnixMarshal.GetInt32BufferLength(p);
			}
			int result = 0;
			int num = 0;
			do
			{
				if (Marshal.ReadByte(p, result++) == 0)
				{
					num++;
				}
				else
				{
					num = 0;
				}
			}
			while (num != nullLength);
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006588 File Offset: 0x00004788
		public static string[] PtrToStringArray(IntPtr stringArray)
		{
			return UnixMarshal.PtrToStringArray(stringArray, UnixEncoding.Instance);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006598 File Offset: 0x00004798
		public static string[] PtrToStringArray(IntPtr stringArray, Encoding encoding)
		{
			if (stringArray == IntPtr.Zero)
			{
				return new string[0];
			}
			int count = UnixMarshal.CountStrings(stringArray);
			return UnixMarshal.PtrToStringArray(count, stringArray, encoding);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000065CC File Offset: 0x000047CC
		private static int CountStrings(IntPtr stringArray)
		{
			int num = 0;
			while (Marshal.ReadIntPtr(stringArray, num * IntPtr.Size) != IntPtr.Zero)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006604 File Offset: 0x00004804
		public static string[] PtrToStringArray(int count, IntPtr stringArray)
		{
			return UnixMarshal.PtrToStringArray(count, stringArray, UnixEncoding.Instance);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006614 File Offset: 0x00004814
		public static string[] PtrToStringArray(int count, IntPtr stringArray, Encoding encoding)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (stringArray == IntPtr.Zero)
			{
				return new string[count];
			}
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				IntPtr p = Marshal.ReadIntPtr(stringArray, i * IntPtr.Size);
				array[i] = UnixMarshal.PtrToString(p, encoding);
			}
			return array;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006694 File Offset: 0x00004894
		public static IntPtr StringToHeap(string s)
		{
			return UnixMarshal.StringToHeap(s, UnixEncoding.Instance);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000066A4 File Offset: 0x000048A4
		public static IntPtr StringToHeap(string s, Encoding encoding)
		{
			return UnixMarshal.StringToHeap(s, 0, s.Length, encoding);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000066B4 File Offset: 0x000048B4
		public static IntPtr StringToHeap(string s, int index, int count)
		{
			return UnixMarshal.StringToHeap(s, index, count, UnixEncoding.Instance);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000066C4 File Offset: 0x000048C4
		public static IntPtr StringToHeap(string s, int index, int count, Encoding encoding)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int maxByteCount = encoding.GetMaxByteCount(1);
			char[] array = s.ToCharArray(index, count);
			byte[] array2 = new byte[encoding.GetByteCount(array) + maxByteCount];
			int bytes = encoding.GetBytes(array, 0, array.Length, array2, 0);
			if (bytes != array2.Length - maxByteCount)
			{
				throw new NotSupportedException("encoding.GetBytes() doesn't equal encoding.GetByteCount()!");
			}
			IntPtr intPtr = UnixMarshal.AllocHeap((long)array2.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new UnixIOException(Errno.ENOMEM);
			}
			bool flag = false;
			try
			{
				Marshal.Copy(array2, 0, intPtr, array2.Length);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					UnixMarshal.FreeHeap(intPtr);
				}
			}
			return intPtr;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000679C File Offset: 0x0000499C
		public static bool ShouldRetrySyscall(int r)
		{
			return r == -1 && Stdlib.GetLastError() == Errno.EINTR;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000067B4 File Offset: 0x000049B4
		[CLSCompliant(false)]
		public static bool ShouldRetrySyscall(int r, out Errno errno)
		{
			errno = (Errno)0;
			return r == -1 && (errno = Stdlib.GetLastError()) == Errno.EINTR;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000067E0 File Offset: 0x000049E0
		internal static string EscapeFormatString(string message, char[] permitted)
		{
			if (message == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(message.Length);
			for (int i = 0; i < message.Length; i++)
			{
				char c = message[i];
				stringBuilder.Append(c);
				if (c == '%' && i + 1 < message.Length)
				{
					char c2 = message[i + 1];
					if (c2 == '%' || UnixMarshal.IsCharPresent(permitted, c2))
					{
						stringBuilder.Append(c2);
					}
					else
					{
						stringBuilder.Append('%').Append(c2);
					}
					i++;
				}
				else if (c == '%')
				{
					stringBuilder.Append('%');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000689C File Offset: 0x00004A9C
		private static bool IsCharPresent(char[] array, char c)
		{
			if (array == null)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000068D4 File Offset: 0x00004AD4
		internal static Exception CreateExceptionForError(Errno errno)
		{
			string errorDescription = UnixMarshal.GetErrorDescription(errno);
			UnixIOException ex = new UnixIOException(errno);
			switch (errno)
			{
			case Errno.EPERM:
				break;
			case Errno.ENOENT:
				return new FileNotFoundException(errorDescription, ex);
			default:
				switch (errno)
				{
				case Errno.ENOSPC:
				case Errno.ESPIPE:
				case Errno.EROFS:
				case Errno.ENOTEMPTY:
					goto IL_ED;
				default:
					if (errno == Errno.EOVERFLOW)
					{
						return new OverflowException(errorDescription, ex);
					}
					if (errno != Errno.EOPNOTSUPP)
					{
						return ex;
					}
					break;
				case Errno.ERANGE:
					return new ArgumentOutOfRangeException(errorDescription);
				case Errno.ENAMETOOLONG:
					return new PathTooLongException(errorDescription, ex);
				}
				break;
			case Errno.EIO:
			case Errno.ENXIO:
				goto IL_ED;
			case Errno.ENOEXEC:
				return new InvalidProgramException(errorDescription, ex);
			case Errno.EBADF:
			case Errno.EINVAL:
				return new ArgumentException(errorDescription, ex);
			case Errno.EACCES:
			case Errno.EISDIR:
				return new UnauthorizedAccessException(errorDescription, ex);
			case Errno.EFAULT:
				return new NullReferenceException(errorDescription, ex);
			case Errno.ENOTDIR:
				return new DirectoryNotFoundException(errorDescription, ex);
			}
			return new InvalidOperationException(errorDescription, ex);
			IL_ED:
			return new IOException(errorDescription, ex);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000069FC File Offset: 0x00004BFC
		internal static Exception CreateExceptionForLastError()
		{
			return UnixMarshal.CreateExceptionForError(Stdlib.GetLastError());
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006A08 File Offset: 0x00004C08
		[CLSCompliant(false)]
		public static void ThrowExceptionForError(Errno errno)
		{
			throw UnixMarshal.CreateExceptionForError(errno);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006A10 File Offset: 0x00004C10
		public static void ThrowExceptionForLastError()
		{
			throw UnixMarshal.CreateExceptionForLastError();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006A18 File Offset: 0x00004C18
		[CLSCompliant(false)]
		public static void ThrowExceptionForErrorIf(int retval, Errno errno)
		{
			if (retval == -1)
			{
				UnixMarshal.ThrowExceptionForError(errno);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006A28 File Offset: 0x00004C28
		public static void ThrowExceptionForLastErrorIf(int retval)
		{
			if (retval == -1)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
		}
	}
}
