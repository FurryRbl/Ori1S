using System;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001B RID: 27
	internal class ErrorMarshal
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000061F4 File Offset: 0x000043F4
		static ErrorMarshal()
		{
			try
			{
				ErrorMarshal.Translate = new ErrorMarshal.ErrorTranslator(ErrorMarshal.strerror_r);
				ErrorMarshal.Translate(Errno.ERANGE);
			}
			catch (EntryPointNotFoundException)
			{
				ErrorMarshal.Translate = new ErrorMarshal.ErrorTranslator(ErrorMarshal.strerror);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006258 File Offset: 0x00004458
		private static string strerror(Errno errno)
		{
			return Stdlib.strerror(errno);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006260 File Offset: 0x00004460
		private static string strerror_r(Errno errno)
		{
			StringBuilder stringBuilder = new StringBuilder(16);
			int num;
			do
			{
				stringBuilder.Capacity *= 2;
				num = Syscall.strerror_r(errno, stringBuilder);
			}
			while (num == -1 && Stdlib.GetLastError() == Errno.ERANGE);
			if (num == -1)
			{
				return "** Unknown error code: " + (int)errno + "**";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400006D RID: 109
		internal static readonly ErrorMarshal.ErrorTranslator Translate;

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x060005FF RID: 1535
		internal delegate string ErrorTranslator(Errno errno);
	}
}
