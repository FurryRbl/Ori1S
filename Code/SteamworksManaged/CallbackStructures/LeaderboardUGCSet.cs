using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000066 RID: 102
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardUGCSet
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00006D0F File Offset: 0x00004F0F
		internal static LeaderboardUGCSet Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LeaderboardUGCSet>(data, dataSize);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00006D18 File Offset: 0x00004F18
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00006D20 File Offset: 0x00004F20
		public LeaderboardHandle Leaderboard
		{
			get
			{
				return this.leaderboard;
			}
		}

		// Token: 0x040001D4 RID: 468
		private Result result;

		// Token: 0x040001D5 RID: 469
		private LeaderboardHandle leaderboard;
	}
}
