using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000089 RID: 137
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DepotID : IEquatable<DepotID>
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00007D0A File Offset: 0x00005F0A
		public DepotID(uint value)
		{
			this.handle = value;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00007D13 File Offset: 0x00005F13
		public ulong AsUInt64
		{
			get
			{
				return (ulong)this.handle;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00007D1C File Offset: 0x00005F1C
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00007D24 File Offset: 0x00005F24
		public static bool operator ==(DepotID x, DepotID y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00007D36 File Offset: 0x00005F36
		public static bool operator !=(DepotID x, DepotID y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00007D4B File Offset: 0x00005F4B
		public bool Equals(DepotID other)
		{
			return this == other;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00007D59 File Offset: 0x00005F59
		public override bool Equals(object obj)
		{
			return obj is DepotID && this == (DepotID)obj;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00007D76 File Offset: 0x00005F76
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00007D83 File Offset: 0x00005F83
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000283 RID: 643
		private uint handle;
	}
}
