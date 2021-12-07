using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200005E RID: 94
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00006BF7 File Offset: 0x00004DF7
		internal static GlobalStatsReceived Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GlobalStatsReceived>(data, dataSize);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00006C00 File Offset: 0x00004E00
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00006C08 File Offset: 0x00004E08
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040001BA RID: 442
		private GameID gameID;

		// Token: 0x040001BB RID: 443
		private Result result;
	}
}
