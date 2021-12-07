using System;
using System.Diagnostics;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000089 RID: 137
	internal class DebugHelper
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0001E168 File Offset: 0x0001C368
		[Conditional("DEBUG")]
		public static void Initialize()
		{
			if (!DebugHelper.isInitialized)
			{
				Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
				Debug.AutoFlush = true;
				DebugHelper.isInitialized = true;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001E198 File Offset: 0x0001C398
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001E19C File Offset: 0x0001C39C
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		[Conditional("DEBUG")]
		public static void WriteLine(string message, byte[] buffer)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		[Conditional("DEBUG")]
		public static void WriteBuffer(byte[] buffer)
		{
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		[Conditional("DEBUG")]
		public static void WriteBuffer(byte[] buffer, int index, int length)
		{
			for (int i = index; i < length; i += 16)
			{
				int num = (length - i < 16) ? (length - i) : 16;
				string str = string.Empty;
				for (int j = 0; j < num; j++)
				{
					str = str + buffer[i + j].ToString("x2") + " ";
				}
			}
		}

		// Token: 0x04000285 RID: 645
		private static bool isInitialized;
	}
}
