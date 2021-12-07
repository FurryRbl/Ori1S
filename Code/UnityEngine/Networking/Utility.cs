using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x02000247 RID: 583
	public class Utility
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		private Utility()
		{
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0002C13C File Offset: 0x0002A33C
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x0002C144 File Offset: 0x0002A344
		public static bool useRandomSourceID
		{
			get
			{
				return Utility.s_useRandomSourceID;
			}
			set
			{
				Utility.SetUseRandomSourceID(value);
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0002C14C File Offset: 0x0002A34C
		public static SourceID GetSourceID()
		{
			return (SourceID)((long)(SystemInfo.deviceUniqueIdentifier + Utility.s_randomSourceComponent).GetHashCode());
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0002C168 File Offset: 0x0002A368
		private static void SetUseRandomSourceID(bool useRandomSourceID)
		{
			if (useRandomSourceID && !Utility.s_useRandomSourceID)
			{
				Utility.s_randomSourceComponent = Utility.s_randomGenerator.Next(int.MaxValue);
			}
			else if (!useRandomSourceID && Utility.s_useRandomSourceID)
			{
				Utility.s_randomSourceComponent = 0;
			}
			Utility.s_useRandomSourceID = useRandomSourceID;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		public static void SetAppID(AppID newAppID)
		{
			Utility.s_programAppID = newAppID;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public static AppID GetAppID()
		{
			return Utility.s_programAppID;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		public static void SetAccessTokenForNetwork(NetworkID netId, NetworkAccessToken accessToken)
		{
			Utility.s_dictTokens.Add(netId, accessToken);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public static NetworkAccessToken GetAccessTokenForNetwork(NetworkID netId)
		{
			NetworkAccessToken result;
			if (!Utility.s_dictTokens.TryGetValue(netId, out result))
			{
				result = new NetworkAccessToken();
			}
			return result;
		}

		// Token: 0x0400093C RID: 2364
		private static Random s_randomGenerator = new Random(Environment.TickCount);

		// Token: 0x0400093D RID: 2365
		private static bool s_useRandomSourceID = false;

		// Token: 0x0400093E RID: 2366
		private static int s_randomSourceComponent = 0;

		// Token: 0x0400093F RID: 2367
		private static AppID s_programAppID = AppID.Invalid;

		// Token: 0x04000940 RID: 2368
		private static Dictionary<NetworkID, NetworkAccessToken> s_dictTokens = new Dictionary<NetworkID, NetworkAccessToken>();
	}
}
