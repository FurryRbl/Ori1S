using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000066 RID: 102
	public class MsgType
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0001A9F4 File Offset: 0x00018BF4
		public static string MsgTypeToString(short value)
		{
			if (value < 0 || value > 47)
			{
				return string.Empty;
			}
			string text = MsgType.msgLabels[(int)value];
			if (string.IsNullOrEmpty(text))
			{
				text = "[" + value + "]";
			}
			return text;
		}

		// Token: 0x0400020F RID: 527
		public const short ObjectDestroy = 1;

		// Token: 0x04000210 RID: 528
		public const short Rpc = 2;

		// Token: 0x04000211 RID: 529
		public const short ObjectSpawn = 3;

		// Token: 0x04000212 RID: 530
		public const short Owner = 4;

		// Token: 0x04000213 RID: 531
		public const short Command = 5;

		// Token: 0x04000214 RID: 532
		public const short LocalPlayerTransform = 6;

		// Token: 0x04000215 RID: 533
		public const short SyncEvent = 7;

		// Token: 0x04000216 RID: 534
		public const short UpdateVars = 8;

		// Token: 0x04000217 RID: 535
		public const short SyncList = 9;

		// Token: 0x04000218 RID: 536
		public const short ObjectSpawnScene = 10;

		// Token: 0x04000219 RID: 537
		public const short NetworkInfo = 11;

		// Token: 0x0400021A RID: 538
		public const short SpawnFinished = 12;

		// Token: 0x0400021B RID: 539
		public const short ObjectHide = 13;

		// Token: 0x0400021C RID: 540
		public const short CRC = 14;

		// Token: 0x0400021D RID: 541
		public const short LocalClientAuthority = 15;

		// Token: 0x0400021E RID: 542
		public const short LocalChildTransform = 16;

		// Token: 0x0400021F RID: 543
		public const short PeerClientAuthority = 17;

		// Token: 0x04000220 RID: 544
		internal const short UserMessage = 0;

		// Token: 0x04000221 RID: 545
		internal const short HLAPIMsg = 28;

		// Token: 0x04000222 RID: 546
		internal const short LLAPIMsg = 29;

		// Token: 0x04000223 RID: 547
		internal const short HLAPIResend = 30;

		// Token: 0x04000224 RID: 548
		internal const short HLAPIPending = 31;

		// Token: 0x04000225 RID: 549
		public const short InternalHighest = 31;

		// Token: 0x04000226 RID: 550
		public const short Connect = 32;

		// Token: 0x04000227 RID: 551
		public const short Disconnect = 33;

		// Token: 0x04000228 RID: 552
		public const short Error = 34;

		// Token: 0x04000229 RID: 553
		public const short Ready = 35;

		// Token: 0x0400022A RID: 554
		public const short NotReady = 36;

		// Token: 0x0400022B RID: 555
		public const short AddPlayer = 37;

		// Token: 0x0400022C RID: 556
		public const short RemovePlayer = 38;

		// Token: 0x0400022D RID: 557
		public const short Scene = 39;

		// Token: 0x0400022E RID: 558
		public const short Animation = 40;

		// Token: 0x0400022F RID: 559
		public const short AnimationParameters = 41;

		// Token: 0x04000230 RID: 560
		public const short AnimationTrigger = 42;

		// Token: 0x04000231 RID: 561
		public const short LobbyReadyToBegin = 43;

		// Token: 0x04000232 RID: 562
		public const short LobbySceneLoaded = 44;

		// Token: 0x04000233 RID: 563
		public const short LobbyAddPlayerFailed = 45;

		// Token: 0x04000234 RID: 564
		public const short LobbyReturnToLobby = 46;

		// Token: 0x04000235 RID: 565
		public const short ReconnectPlayer = 47;

		// Token: 0x04000236 RID: 566
		public const short Highest = 47;

		// Token: 0x04000237 RID: 567
		internal static string[] msgLabels = new string[]
		{
			"none",
			"ObjectDestroy",
			"Rpc",
			"ObjectSpawn",
			"Owner",
			"Command",
			"LocalPlayerTransform",
			"SyncEvent",
			"UpdateVars",
			"SyncList",
			"ObjectSpawnScene",
			"NetworkInfo",
			"SpawnFinished",
			"ObjectHide",
			"CRC",
			"LocalClientAuthority",
			"LocalChildTransform",
			"PeerClientAuthority",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			"Connect",
			"Disconnect",
			"Error",
			"Ready",
			"NotReady",
			"AddPlayer",
			"RemovePlayer",
			"Scene",
			"Animation",
			"AnimationParams",
			"AnimationTrigger",
			"LobbyReadyToBegin",
			"LobbySceneLoaded",
			"LobbyAddPlayerFailed",
			"LobbyReturnToLobby",
			"ReconnectPlayer"
		};
	}
}
