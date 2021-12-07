using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.Implementations
{
	// Token: 0x020000CF RID: 207
	internal class GameServerStats : SteamServiceGameServer, IGameServerStats
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x000095C0 File Offset: 0x000077C0
		public GameServerStats()
		{
			this.gsStatsReceived = new List<SteamService.Result<GSStatsReceived>>();
			this.gsStatsStored = new List<SteamService.Result<GSStatsStored>>();
			this.gsStatsUnloaded = new List<GSStatsUnloaded>();
			SteamServiceGameServer.Results[ResultID.GSStatsReceived] = delegate(IntPtr data, int size, bool flag)
			{
				this.gsStatsReceived.Add(new SteamService.Result<GSStatsReceived>(ManagedSteam.CallbackStructures.GSStatsReceived.Create(data, size), flag));
			};
			SteamServiceGameServer.Results[ResultID.GSStatsStored] = delegate(IntPtr data, int size, bool flag)
			{
				this.gsStatsStored.Add(new SteamService.Result<GSStatsStored>(ManagedSteam.CallbackStructures.GSStatsStored.Create(data, size), flag));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSStatsUnloaded] = delegate(IntPtr data, int size)
			{
				this.gsStatsUnloaded.Add(ManagedSteam.CallbackStructures.GSStatsUnloaded.Create(data, size));
			};
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060005D4 RID: 1492 RVA: 0x00009654 File Offset: 0x00007854
		// (remove) Token: 0x060005D5 RID: 1493 RVA: 0x0000968C File Offset: 0x0000788C
		public event ResultEvent<GSStatsReceived> GSStatsReceived;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060005D6 RID: 1494 RVA: 0x000096C4 File Offset: 0x000078C4
		// (remove) Token: 0x060005D7 RID: 1495 RVA: 0x000096FC File Offset: 0x000078FC
		public event ResultEvent<GSStatsStored> GSStatsStored;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060005D8 RID: 1496 RVA: 0x00009734 File Offset: 0x00007934
		// (remove) Token: 0x060005D9 RID: 1497 RVA: 0x0000976C File Offset: 0x0000796C
		public event CallbackEvent<GSStatsUnloaded> GSStatsUnloaded;

		// Token: 0x060005DA RID: 1498 RVA: 0x000097A1 File Offset: 0x000079A1
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000097A3 File Offset: 0x000079A3
		internal override void ReleaseManagedResources()
		{
			this.gsStatsReceived = null;
			this.GSStatsReceived = null;
			this.gsStatsStored = null;
			this.GSStatsStored = null;
			this.gsStatsUnloaded = null;
			this.GSStatsUnloaded = null;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000097CF File Offset: 0x000079CF
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<GSStatsReceived>(this.gsStatsReceived, this.GSStatsReceived);
			SteamService.InvokeEvents<GSStatsStored>(this.gsStatsStored, this.GSStatsStored);
			SteamService.InvokeEvents<GSStatsUnloaded>(this.gsStatsUnloaded, this.GSStatsUnloaded);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00009804 File Offset: 0x00007A04
		public void RequestUserStats(SteamID steamIDUser)
		{
			base.CheckIfUsable();
			NativeMethods.GameServerStats_RequestUserStats(steamIDUser.AsUInt64);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00009818 File Offset: 0x00007A18
		public bool GetUserStat(SteamID steamIDUser, string name, out int data)
		{
			base.CheckIfUsable();
			data = 0;
			return NativeMethods.GameServerStats_GetUserStatInt(steamIDUser.AsUInt64, name, ref data);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00009834 File Offset: 0x00007A34
		public GameServerStatsGetUserStatIntResult GetUserStatInt(SteamID steamIDUser, string name)
		{
			GameServerStatsGetUserStatIntResult result = default(GameServerStatsGetUserStatIntResult);
			result.Result = this.GetUserStat(steamIDUser, name, out result.IntValue);
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00009860 File Offset: 0x00007A60
		public bool GetUserStat(SteamID steamIDUser, string name, out float data)
		{
			base.CheckIfUsable();
			data = 0f;
			return NativeMethods.GameServerStats_GetUserStatFloat(steamIDUser.AsUInt64, name, ref data);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00009880 File Offset: 0x00007A80
		public GameServerStatsGetUserStatFloatResult GetUserStatFloat(SteamID steamIDUser, string name)
		{
			GameServerStatsGetUserStatFloatResult result = default(GameServerStatsGetUserStatFloatResult);
			result.Result = this.GetUserStat(steamIDUser, name, out result.FloatValue);
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000098AC File Offset: 0x00007AAC
		public bool GetUserAchievement(SteamID steamIDUser, string name, out bool achieved)
		{
			base.CheckIfUsable();
			achieved = false;
			return NativeMethods.GameServerStats_GetUserAchievement(steamIDUser.AsUInt64, name, ref achieved);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000098C8 File Offset: 0x00007AC8
		public GameServerGetUserAchievementResult GetUserAchievement(SteamID steamIDUser, string name)
		{
			GameServerGetUserAchievementResult result = default(GameServerGetUserAchievementResult);
			result.Result = this.GetUserAchievement(steamIDUser, name, out result.Achieved);
			return result;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000098F4 File Offset: 0x00007AF4
		public bool SetUserStat(SteamID steamIDUser, string name, int data)
		{
			base.CheckIfUsable();
			return NativeMethods.GameServerStats_SetUserStatInt(steamIDUser.AsUInt64, name, data);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000990A File Offset: 0x00007B0A
		public bool SetUserStat(SteamID steamIDUser, string name, float data)
		{
			base.CheckIfUsable();
			return NativeMethods.GameServerStats_SetUserStatFloat(steamIDUser.AsUInt64, name, data);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00009920 File Offset: 0x00007B20
		public bool UpdateUserAvgRateStat(SteamID steamIDUser, string name, float countThisSession, double sessionLength)
		{
			base.CheckIfUsable();
			return NativeMethods.GameServerStats_UpdateUserAvgRateStat(steamIDUser.AsUInt64, name, countThisSession, sessionLength);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00009938 File Offset: 0x00007B38
		public bool SetUserAchievement(SteamID steamIDUser, string name)
		{
			base.CheckIfUsable();
			return NativeMethods.GameServerStats_SetUserAchievement(steamIDUser.AsUInt64, name);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000994D File Offset: 0x00007B4D
		public bool ClearUserAchievement(SteamID steamIDUser, string name)
		{
			base.CheckIfUsable();
			return NativeMethods.GameServerStats_ClearUserAchievement(steamIDUser.AsUInt64, name);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00009962 File Offset: 0x00007B62
		public void StoreUserStats(SteamID steamIDUser)
		{
			base.CheckIfUsable();
			NativeMethods.GameServerStats_StoreUserStats(steamIDUser.AsUInt64);
		}

		// Token: 0x0400038A RID: 906
		private List<SteamService.Result<GSStatsReceived>> gsStatsReceived;

		// Token: 0x0400038B RID: 907
		private List<SteamService.Result<GSStatsStored>> gsStatsStored;

		// Token: 0x0400038C RID: 908
		private List<GSStatsUnloaded> gsStatsUnloaded;
	}
}
