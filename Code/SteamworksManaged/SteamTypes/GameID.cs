using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000FE RID: 254
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameID : IEquatable<GameID>
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x0000B3C7 File Offset: 0x000095C7
		public GameID(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static bool operator ==(GameID x, GameID y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000B3EA File Offset: 0x000095EA
		public static bool operator !=(GameID x, GameID y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000B3FF File Offset: 0x000095FF
		public bool Equals(GameID other)
		{
			return this == other;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000B40D File Offset: 0x0000960D
		public override bool Equals(object obj)
		{
			return obj is GameID && this == (GameID)obj;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000B42A File Offset: 0x0000962A
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000B437 File Offset: 0x00009637
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000B44E File Offset: 0x0000964E
		public bool IsMod()
		{
			return this.Type() == GameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000B459 File Offset: 0x00009659
		public bool IsShortcut()
		{
			return this.Type() == GameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000B464 File Offset: 0x00009664
		public bool IsP2PFile()
		{
			return this.Type() == GameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000B46F File Offset: 0x0000966F
		public bool IsSteamApp()
		{
			return this.Type() == GameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000B47A File Offset: 0x0000967A
		public GameID.EGameIDType Type()
		{
			return (GameID.EGameIDType)(this.handle >> 24);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000B486 File Offset: 0x00009686
		public uint ModID()
		{
			return (uint)(this.handle >> 32);
		}

		// Token: 0x0400048D RID: 1165
		private ulong handle;

		// Token: 0x020000FF RID: 255
		public enum EGameIDType : byte
		{
			// Token: 0x0400048F RID: 1167
			k_EGameIDTypeApp,
			// Token: 0x04000490 RID: 1168
			k_EGameIDTypeGameMod,
			// Token: 0x04000491 RID: 1169
			k_EGameIDTypeShortcut,
			// Token: 0x04000492 RID: 1170
			k_EGameIDTypeP2P
		}
	}
}
