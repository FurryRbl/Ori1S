using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000006 RID: 6
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntriesHandle : IEquatable<LeaderboardEntriesHandle>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public LeaderboardEntriesHandle(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002059 File Offset: 0x00000259
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public static bool operator ==(LeaderboardEntriesHandle x, LeaderboardEntriesHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002073 File Offset: 0x00000273
		public static bool operator !=(LeaderboardEntriesHandle x, LeaderboardEntriesHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002088 File Offset: 0x00000288
		public bool Equals(LeaderboardEntriesHandle other)
		{
			return this == other;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002096 File Offset: 0x00000296
		public override bool Equals(object obj)
		{
			return obj is LeaderboardEntriesHandle && this == (LeaderboardEntriesHandle)obj;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000021 RID: 33
		private ulong handle;
	}
}
