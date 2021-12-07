using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000142 RID: 322
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x0000FA36 File Offset: 0x0000DC36
		internal static GameServerChangeRequested Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameServerChangeRequested>(data, dataSize);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0000FA3F File Offset: 0x0000DC3F
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0000FA47 File Offset: 0x0000DC47
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x040005C7 RID: 1479
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		private string server;

		// Token: 0x040005C8 RID: 1480
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		private string password;
	}
}
