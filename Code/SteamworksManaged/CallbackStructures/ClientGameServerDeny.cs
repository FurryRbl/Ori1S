using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000028 RID: 40
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClientGameServerDeny
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00003A2B File Offset: 0x00001C2B
		internal static ClientGameServerDeny Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ClientGameServerDeny>(data, dataSize);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003A34 File Offset: 0x00001C34
		public uint AppID
		{
			get
			{
				return this.appID;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003A3C File Offset: 0x00001C3C
		public uint GameServerIP
		{
			get
			{
				return this.gameServerIP;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003A44 File Offset: 0x00001C44
		public ushort GameServerPort
		{
			get
			{
				return this.gameServerPort;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003A4C File Offset: 0x00001C4C
		public ushort Secure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003A54 File Offset: 0x00001C54
		public uint Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040000D1 RID: 209
		private uint appID;

		// Token: 0x040000D2 RID: 210
		private uint gameServerIP;

		// Token: 0x040000D3 RID: 211
		private ushort gameServerPort;

		// Token: 0x040000D4 RID: 212
		private ushort secure;

		// Token: 0x040000D5 RID: 213
		private uint reason;
	}
}
