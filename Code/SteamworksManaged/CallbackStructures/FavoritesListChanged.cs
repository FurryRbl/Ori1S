using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListChanged
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00009984 File Offset: 0x00007B84
		internal static FavoritesListChanged Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<FavoritesListChanged>(data, dataSize);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000998D File Offset: 0x00007B8D
		public uint IP
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00009995 File Offset: 0x00007B95
		public uint QueryPort
		{
			get
			{
				return this.queryPort;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000999D File Offset: 0x00007B9D
		public uint ConnPort
		{
			get
			{
				return this.connPort;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000099A5 File Offset: 0x00007BA5
		public AppID AppID
		{
			get
			{
				return new AppID(this.appID);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000099B2 File Offset: 0x00007BB2
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000099BA File Offset: 0x00007BBA
		public bool Add
		{
			get
			{
				return this.add;
			}
		}

		// Token: 0x040003DC RID: 988
		private uint ip;

		// Token: 0x040003DD RID: 989
		private uint queryPort;

		// Token: 0x040003DE RID: 990
		private uint connPort;

		// Token: 0x040003DF RID: 991
		private uint appID;

		// Token: 0x040003E0 RID: 992
		private uint flags;

		// Token: 0x040003E1 RID: 993
		[MarshalAs(UnmanagedType.I1)]
		private bool add;
	}
}
