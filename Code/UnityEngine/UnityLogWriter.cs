using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	// Token: 0x020000A5 RID: 165
	internal sealed class UnityLogWriter : TextWriter
	{
		// Token: 0x0600098B RID: 2443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteStringToUnityLog(string s);

		// Token: 0x0600098C RID: 2444 RVA: 0x0000DE9C File Offset: 0x0000C09C
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}
	}
}
