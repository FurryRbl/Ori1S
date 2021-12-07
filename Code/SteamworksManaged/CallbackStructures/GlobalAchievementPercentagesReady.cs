using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000062 RID: 98
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalAchievementPercentagesReady
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00006C83 File Offset: 0x00004E83
		internal static GlobalAchievementPercentagesReady Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GlobalAchievementPercentagesReady>(data, dataSize);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00006C8C File Offset: 0x00004E8C
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00006C94 File Offset: 0x00004E94
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040001C7 RID: 455
		private GameID gameID;

		// Token: 0x040001C8 RID: 456
		private Result result;
	}
}
