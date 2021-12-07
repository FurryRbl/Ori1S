using System;
using System.Runtime.InteropServices;
using ManagedSteam.Utility;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000111 RID: 273
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameServerItem
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x0000B872 File Offset: 0x00009A72
		internal static GameServerItem Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameServerItem>(data, dataSize);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0000B87B File Offset: 0x00009A7B
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0000B883 File Offset: 0x00009A83
		public ServerNetworkAddress ServerNetworkAddress
		{
			get
			{
				return this.serverAddress;
			}
			set
			{
				this.serverAddress = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000B88C File Offset: 0x00009A8C
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0000B894 File Offset: 0x00009A94
		public int Ping
		{
			get
			{
				return this.ping;
			}
			set
			{
				this.ping = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000B89D File Offset: 0x00009A9D
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		public bool HadSuccessfulResponse
		{
			get
			{
				return this.hadSuccessfulResponse;
			}
			set
			{
				this.hadSuccessfulResponse = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000B8AE File Offset: 0x00009AAE
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		public bool DoNotRefresh
		{
			get
			{
				return this.doNotRefresh;
			}
			set
			{
				this.doNotRefresh = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000B8BF File Offset: 0x00009ABF
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public string GameDir
		{
			get
			{
				return this.gameDir;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 32)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						32
					}));
				}
				this.gameDir = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0000B90D File Offset: 0x00009B0D
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0000B918 File Offset: 0x00009B18
		public string Map
		{
			get
			{
				return this.map;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 32)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						32
					}));
				}
				this.map = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0000B95D File Offset: 0x00009B5D
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0000B968 File Offset: 0x00009B68
		public string GameDescription
		{
			get
			{
				return this.gameDescription;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 64)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						64
					}));
				}
				this.gameDescription = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0000B9AD File Offset: 0x00009BAD
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0000B9BA File Offset: 0x00009BBA
		public AppID AppID
		{
			get
			{
				return new AppID(this.appID);
			}
			set
			{
				this.appID = value.AsUInt32;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0000B9C9 File Offset: 0x00009BC9
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		public int Players
		{
			get
			{
				return this.players;
			}
			set
			{
				this.players = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0000B9DA File Offset: 0x00009BDA
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		public int MaxPlayers
		{
			get
			{
				return this.maxPlayers;
			}
			set
			{
				this.maxPlayers = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0000B9EB File Offset: 0x00009BEB
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		public int BotPlayers
		{
			get
			{
				return this.botPlayers;
			}
			set
			{
				this.botPlayers = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0000B9FC File Offset: 0x00009BFC
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0000BA04 File Offset: 0x00009C04
		public bool Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0000BA0D File Offset: 0x00009C0D
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0000BA15 File Offset: 0x00009C15
		public bool Secure
		{
			get
			{
				return this.secure;
			}
			set
			{
				this.secure = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0000BA1E File Offset: 0x00009C1E
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0000BA26 File Offset: 0x00009C26
		public uint TimeLastPlayed
		{
			get
			{
				return this.timeLastPlayed;
			}
			set
			{
				this.timeLastPlayed = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0000BA2F File Offset: 0x00009C2F
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0000BA37 File Offset: 0x00009C37
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
			set
			{
				this.serverVersion = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0000BA40 File Offset: 0x00009C40
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0000BA64 File Offset: 0x00009C64
		public string ServerName
		{
			get
			{
				if (string.IsNullOrEmpty(this.serverName))
				{
					return this.serverAddress.GetConnectionAddressString();
				}
				return this.serverName;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 64)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						64
					}));
				}
				this.serverName = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public string GameTags
		{
			get
			{
				return this.gameTags;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 128)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						128
					}));
				}
				this.gameTags = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0000BAFF File Offset: 0x00009CFF
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0000BB07 File Offset: 0x00009D07
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
			set
			{
				this.steamID = value;
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0000BB10 File Offset: 0x00009D10
		public string GetName()
		{
			return this.ServerName;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0000BB18 File Offset: 0x00009D18
		public void SetName(string name)
		{
			this.ServerName = name;
		}

		// Token: 0x040004CC RID: 1228
		private ServerNetworkAddress serverAddress;

		// Token: 0x040004CD RID: 1229
		private int ping;

		// Token: 0x040004CE RID: 1230
		[MarshalAs(UnmanagedType.I1)]
		private bool hadSuccessfulResponse;

		// Token: 0x040004CF RID: 1231
		[MarshalAs(UnmanagedType.I1)]
		private bool doNotRefresh;

		// Token: 0x040004D0 RID: 1232
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string gameDir;

		// Token: 0x040004D1 RID: 1233
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		private string map;

		// Token: 0x040004D2 RID: 1234
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		private string gameDescription;

		// Token: 0x040004D3 RID: 1235
		private uint appID;

		// Token: 0x040004D4 RID: 1236
		private int players;

		// Token: 0x040004D5 RID: 1237
		private int maxPlayers;

		// Token: 0x040004D6 RID: 1238
		private int botPlayers;

		// Token: 0x040004D7 RID: 1239
		[MarshalAs(UnmanagedType.I1)]
		private bool password;

		// Token: 0x040004D8 RID: 1240
		[MarshalAs(UnmanagedType.I1)]
		private bool secure;

		// Token: 0x040004D9 RID: 1241
		private uint timeLastPlayed;

		// Token: 0x040004DA RID: 1242
		private int serverVersion;

		// Token: 0x040004DB RID: 1243
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		private string serverName;

		// Token: 0x040004DC RID: 1244
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string gameTags;

		// Token: 0x040004DD RID: 1245
		private SteamID steamID;
	}
}
