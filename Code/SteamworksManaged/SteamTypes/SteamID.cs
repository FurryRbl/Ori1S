using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000FC RID: 252
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamID : IEquatable<SteamID>
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0000B332 File Offset: 0x00009532
		public SteamID(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000B33B File Offset: 0x0000953B
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000B343 File Offset: 0x00009543
		public static bool operator ==(SteamID x, SteamID y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000B355 File Offset: 0x00009555
		public static bool operator !=(SteamID x, SteamID y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000B36A File Offset: 0x0000956A
		public bool Equals(SteamID other)
		{
			return this == other;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000B378 File Offset: 0x00009578
		public override bool Equals(object obj)
		{
			return obj is SteamID && this == (SteamID)obj;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000B395 File Offset: 0x00009595
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000B3A2 File Offset: 0x000095A2
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000461 RID: 1121
		public static readonly SteamID Invalid = new SteamID(0UL);

		// Token: 0x04000462 RID: 1122
		private ulong handle;
	}
}
