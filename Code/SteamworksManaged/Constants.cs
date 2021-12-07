using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x020000D3 RID: 211
	public static class Constants
	{
		// Token: 0x020000D4 RID: 212
		public static class Apps
		{
			// Token: 0x04000396 RID: 918
			public const int AppProofOfPurchaseKeyMax = 64;

			// Token: 0x04000397 RID: 919
			public const int MaxAchievementNameLength = 128;

			// Token: 0x04000398 RID: 920
			public const int MaxBetaNameLength = 128;

			// Token: 0x04000399 RID: 921
			public const int MaxGamepadTextInputLength = 256;
		}

		// Token: 0x020000D5 RID: 213
		public static class VersionInfo
		{
			// Token: 0x0400039A RID: 922
			public const uint InterfaceID = 786U;
		}

		// Token: 0x020000D6 RID: 214
		public static class Cloud
		{
			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060005ED RID: 1517 RVA: 0x00009976 File Offset: 0x00007B76
			public static UGCHandle InvalidUGCHandle
			{
				get
				{
					return UGCHandle.Invalid;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000997D File Offset: 0x00007B7D
			public static PublishedFileUpdateHandle InvalidPublishFileUpdateHandle
			{
				get
				{
					return PublishedFileUpdateHandle.Invalid;
				}
			}

			// Token: 0x0400039B RID: 923
			public const int MaxFileSize = 104857600;

			// Token: 0x0400039C RID: 924
			public const int PublishedDocumentTitleMax = 129;

			// Token: 0x0400039D RID: 925
			public const int PublishedDocumentDescriptionMax = 8000;

			// Token: 0x0400039E RID: 926
			public const int PublishedDocumentChangeDescriptionMax = 256;

			// Token: 0x0400039F RID: 927
			public const int EnumeratePublishedFilesMaxResults = 50;

			// Token: 0x040003A0 RID: 928
			public const int TagListMax = 1025;

			// Token: 0x040003A1 RID: 929
			public const int FileNameMax = 260;

			// Token: 0x040003A2 RID: 930
			public const int PublishedFileURLMax = 256;
		}

		// Token: 0x020000D7 RID: 215
		public static class User
		{
			// Token: 0x040003A3 RID: 931
			public const int URLNameMax = 256;
		}

		// Token: 0x020000D8 RID: 216
		public static class Stats
		{
			// Token: 0x040003A4 RID: 932
			public const int StatNameMax = 128;

			// Token: 0x040003A5 RID: 933
			public const int LeaderboardNameMax = 128;

			// Token: 0x040003A6 RID: 934
			public const int LeaderboardDetailsMax = 64;
		}

		// Token: 0x020000D9 RID: 217
		public static class Friends
		{
			// Token: 0x040003A7 RID: 935
			public const int MaxFriendsGroupName = 64;

			// Token: 0x040003A8 RID: 936
			public const int FriendsGroupLimit = 100;

			// Token: 0x040003A9 RID: 937
			public const int EnumerateFollowersMax = 50;

			// Token: 0x040003AA RID: 938
			public const int PersonaNameMaxUTF8 = 128;

			// Token: 0x040003AB RID: 939
			public const int PersonaNameMaxUTF16 = 32;

			// Token: 0x040003AC RID: 940
			public const int ChatMetadataMax = 8192;

			// Token: 0x040003AD RID: 941
			public const int MaxRichPresenceKeys = 20;

			// Token: 0x040003AE RID: 942
			public const int MaxRichPresenceKeyLength = 64;

			// Token: 0x040003AF RID: 943
			public const int MaxRichPresenceValueLength = 256;

			// Token: 0x040003B0 RID: 944
			public const int ChatMsgLength = 256;

			// Token: 0x040003B1 RID: 945
			public const int ServerAddressMaxLength = 64;

			// Token: 0x040003B2 RID: 946
			public const int ServerPasswordMaxLength = 64;
		}

		// Token: 0x020000DA RID: 218
		public static class Matchmaking
		{
			// Token: 0x040003B3 RID: 947
			public const int MaxLobbyKeyLength = 255;

			// Token: 0x040003B4 RID: 948
			public const int MaxLobbyValueLength = 255;

			// Token: 0x040003B5 RID: 949
			public const uint FavoriteFlagNone = 0U;

			// Token: 0x040003B6 RID: 950
			public const uint FavoriteFlagFavorite = 1U;

			// Token: 0x040003B7 RID: 951
			public const uint FavoriteFlagHistory = 2U;

			// Token: 0x040003B8 RID: 952
			public const int KeyValuePairMaxKeySize = 256;

			// Token: 0x040003B9 RID: 953
			public const int KeyValuePairMaxValueSize = 256;

			// Token: 0x040003BA RID: 954
			public const int MaxGameServerGameDir = 32;

			// Token: 0x040003BB RID: 955
			public const int MaxGameServerMapName = 32;

			// Token: 0x040003BC RID: 956
			public const int MaxGameServerGameDescription = 64;

			// Token: 0x040003BD RID: 957
			public const int MaxGameServerName = 64;

			// Token: 0x040003BE RID: 958
			public const int MaxGameServerTags = 128;

			// Token: 0x040003BF RID: 959
			public const int MaxGameServerGameData = 2048;
		}

		// Token: 0x020000DB RID: 219
		public static class GameServer
		{
			// Token: 0x040003C0 RID: 960
			internal const int GSClientDenyText = 128;

			// Token: 0x040003C1 RID: 961
			internal const int GSClientAchievementStatusText = 128;
		}

		// Token: 0x020000DC RID: 220
		public static class Networking
		{
			// Token: 0x040003C2 RID: 962
			public const int PacketSize = 1200;
		}

		// Token: 0x020000DD RID: 221
		public static class Screenshots
		{
			// Token: 0x040003C3 RID: 963
			public const int ScreenshotThumbWidth = 200;

			// Token: 0x040003C4 RID: 964
			public const uint ScreenshotMaxTaggedUsers = 32U;

			// Token: 0x040003C5 RID: 965
			public const uint ScreenshotMaxTaggedPublishedFiles = 32U;

			// Token: 0x040003C6 RID: 966
			public const int UFSTagTypeMax = 255;

			// Token: 0x040003C7 RID: 967
			public const int UFSTagValueMax = 255;
		}

		// Token: 0x020000DE RID: 222
		public static class UGC
		{
			// Token: 0x040003C8 RID: 968
			public const int PublishedDocumentTitleMax = 129;

			// Token: 0x040003C9 RID: 969
			public const int PublishedDocumentDescriptionMax = 8000;

			// Token: 0x040003CA RID: 970
			public const int TagListMax = 1025;

			// Token: 0x040003CB RID: 971
			public const int FilenameMax = 260;

			// Token: 0x040003CC RID: 972
			public const int PublishedFileURLMax = 256;
		}

		// Token: 0x020000DF RID: 223
		public static class SteamController
		{
			// Token: 0x040003CD RID: 973
			public const int MaxSteamControllers = 8;
		}

		// Token: 0x020000E0 RID: 224
		public static class Hmd
		{
			// Token: 0x040003CE RID: 974
			public const int MaxIDBufferSize = 128;
		}
	}
}
