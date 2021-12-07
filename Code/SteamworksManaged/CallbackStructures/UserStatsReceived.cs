using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200005B RID: 91
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsReceived
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00006BAC File Offset: 0x00004DAC
		internal static UserStatsReceived Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UserStatsReceived>(data, dataSize);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00006BBD File Offset: 0x00004DBD
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public SteamID UserID
		{
			get
			{
				return this.userID;
			}
		}

		// Token: 0x040001B4 RID: 436
		private GameID gameID;

		// Token: 0x040001B5 RID: 437
		private Result result;

		// Token: 0x040001B6 RID: 438
		private SteamID userID;
	}
}
