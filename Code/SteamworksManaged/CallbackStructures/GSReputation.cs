using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSReputation
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0000B75B File Offset: 0x0000995B
		internal static GSReputation Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSReputation>(data, dataSize);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000B764 File Offset: 0x00009964
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0000B76C File Offset: 0x0000996C
		public uint ReputationScore
		{
			get
			{
				return this.reputationScore;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000B774 File Offset: 0x00009974
		public bool Banned
		{
			get
			{
				return this.banned;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0000B77C File Offset: 0x0000997C
		public uint BannedIP
		{
			get
			{
				return this.bannedIP;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000B784 File Offset: 0x00009984
		public ushort BannedPort
		{
			get
			{
				return this.bannedPort;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0000B78C File Offset: 0x0000998C
		public GameID BannedGameID
		{
			get
			{
				return this.bannedGameID;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0000B794 File Offset: 0x00009994
		public uint BanExpires
		{
			get
			{
				return this.banExpires;
			}
		}

		// Token: 0x040004A7 RID: 1191
		private Result result;

		// Token: 0x040004A8 RID: 1192
		private uint reputationScore;

		// Token: 0x040004A9 RID: 1193
		[MarshalAs(UnmanagedType.I1)]
		private bool banned;

		// Token: 0x040004AA RID: 1194
		private uint bannedIP;

		// Token: 0x040004AB RID: 1195
		private ushort bannedPort;

		// Token: 0x040004AC RID: 1196
		private GameID bannedGameID;

		// Token: 0x040004AD RID: 1197
		private uint banExpires;
	}
}
