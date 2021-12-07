using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200005C RID: 92
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsStored
	{
		// Token: 0x06000325 RID: 805 RVA: 0x00006BCD File Offset: 0x00004DCD
		internal static UserStatsStored Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UserStatsStored>(data, dataSize);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00006BD6 File Offset: 0x00004DD6
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00006BDE File Offset: 0x00004DDE
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040001B7 RID: 439
		private GameID gameID;

		// Token: 0x040001B8 RID: 440
		private Result result;
	}
}
