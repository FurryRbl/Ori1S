using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200006D RID: 109
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardHandle : IEquatable<LeaderboardHandle>
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00006D28 File Offset: 0x00004F28
		public LeaderboardHandle(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00006D31 File Offset: 0x00004F31
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00006D39 File Offset: 0x00004F39
		public static bool operator ==(LeaderboardHandle x, LeaderboardHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00006D4B File Offset: 0x00004F4B
		public static bool operator !=(LeaderboardHandle x, LeaderboardHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00006D60 File Offset: 0x00004F60
		public bool Equals(LeaderboardHandle other)
		{
			return this == other;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00006D6E File Offset: 0x00004F6E
		public override bool Equals(object obj)
		{
			return obj is LeaderboardHandle && this == (LeaderboardHandle)obj;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00006D8B File Offset: 0x00004F8B
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00006D98 File Offset: 0x00004F98
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x040001EA RID: 490
		private ulong handle;
	}
}
