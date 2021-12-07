using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200012F RID: 303
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsReceived
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0000C545 File Offset: 0x0000A745
		internal static GSStatsReceived Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSStatsReceived>(data, dataSize);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0000C54E File Offset: 0x0000A74E
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0000C556 File Offset: 0x0000A756
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x0400052A RID: 1322
		private Result result;

		// Token: 0x0400052B RID: 1323
		private SteamID steamIDUser;
	}
}
