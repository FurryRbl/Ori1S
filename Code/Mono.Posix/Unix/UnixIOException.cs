using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class UnixIOException : IOException
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005E7C File Offset: 0x0000407C
		public UnixIOException() : this(Marshal.GetLastWin32Error())
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005E8C File Offset: 0x0000408C
		public UnixIOException(int errno) : base(UnixIOException.GetMessage(NativeConvert.ToErrno(errno)))
		{
			this.errno = errno;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005EA8 File Offset: 0x000040A8
		public UnixIOException(int errno, Exception inner) : base(UnixIOException.GetMessage(NativeConvert.ToErrno(errno)), inner)
		{
			this.errno = errno;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005EC4 File Offset: 0x000040C4
		public UnixIOException(Errno errno) : base(UnixIOException.GetMessage(errno))
		{
			this.errno = NativeConvert.FromErrno(errno);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005EE0 File Offset: 0x000040E0
		public UnixIOException(Errno errno, Exception inner) : base(UnixIOException.GetMessage(errno), inner)
		{
			this.errno = NativeConvert.FromErrno(errno);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005EFC File Offset: 0x000040FC
		public UnixIOException(string message) : base(message)
		{
			this.errno = 0;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005F0C File Offset: 0x0000410C
		public UnixIOException(string message, Exception inner) : base(message, inner)
		{
			this.errno = 0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005F20 File Offset: 0x00004120
		protected UnixIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005F2C File Offset: 0x0000412C
		public int NativeErrorCode
		{
			get
			{
				return this.errno;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005F34 File Offset: 0x00004134
		public Errno ErrorCode
		{
			get
			{
				return NativeConvert.ToErrno(this.errno);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005F44 File Offset: 0x00004144
		private static string GetMessage(Errno errno)
		{
			return string.Format("{0} [{1}].", UnixMarshal.GetErrorDescription(errno), errno);
		}

		// Token: 0x04000068 RID: 104
		private int errno;
	}
}
