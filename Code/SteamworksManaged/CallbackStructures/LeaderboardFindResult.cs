using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000063 RID: 99
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardFindResult
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00006C9C File Offset: 0x00004E9C
		internal static LeaderboardFindResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LeaderboardFindResult>(data, dataSize);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00006CA5 File Offset: 0x00004EA5
		public LeaderboardHandle Leaderboard
		{
			get
			{
				return this.leaderboard;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00006CAD File Offset: 0x00004EAD
		public byte LeaderboardFound
		{
			get
			{
				return this.leaderboardFound;
			}
		}

		// Token: 0x040001C9 RID: 457
		private LeaderboardHandle leaderboard;

		// Token: 0x040001CA RID: 458
		private byte leaderboardFound;
	}
}
