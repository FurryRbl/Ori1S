using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x020000FA RID: 250
	internal class GameServer : SteamServiceGameServer, IGameServer
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public GameServer()
		{
			this.steamServersConnected = new List<SteamServersConnected>();
			this.steamServerConnectFailure = new List<SteamServerConnectFailure>();
			this.steamServersDisconnected = new List<SteamServersDisconnected>();
			this.validateAuthTicketResponse = new List<ValidateAuthTicketResponse>();
			this.gsClientApprove = new List<GSClientApprove>();
			this.gsClientDeny = new List<GSClientDeny>();
			this.gsClientKick = new List<GSClientKick>();
			this.gsClientAchievementStatus = new List<GSClientAchievementStatus>();
			this.gsPolicyResponse = new List<GSPolicyResponse>();
			this.gsGameplayStats = new List<GSGameplayStats>();
			this.gsClientGroupStatus = new List<GSClientGroupStatus>();
			this.gsReputation = new List<SteamService.Result<GSReputation>>();
			this.associateWithClanResult = new List<SteamService.Result<AssociateWithClanResult>>();
			this.computeNewPlayerCompatibilityResult = new List<SteamService.Result<ComputeNewPlayerCompatibilityResult>>();
			SteamServiceGameServer.Callbacks[CallbackID.SteamServersConnected] = delegate(IntPtr data, int size)
			{
				this.steamServersConnected.Add(ManagedSteam.CallbackStructures.SteamServersConnected.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.SteamServerConnectFailure] = delegate(IntPtr data, int size)
			{
				this.steamServerConnectFailure.Add(ManagedSteam.CallbackStructures.SteamServerConnectFailure.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.SteamServersDisconnected] = delegate(IntPtr data, int size)
			{
				this.steamServersDisconnected.Add(ManagedSteam.CallbackStructures.SteamServersDisconnected.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.ValidateAuthTicketResponse] = delegate(IntPtr data, int size)
			{
				this.validateAuthTicketResponse.Add(ManagedSteam.CallbackStructures.ValidateAuthTicketResponse.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSClientApprove] = delegate(IntPtr data, int size)
			{
				this.gsClientApprove.Add(ManagedSteam.CallbackStructures.GSClientApprove.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSClientDeny] = delegate(IntPtr data, int size)
			{
				this.gsClientDeny.Add(ManagedSteam.CallbackStructures.GSClientDeny.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSClientKick] = delegate(IntPtr data, int size)
			{
				this.gsClientKick.Add(ManagedSteam.CallbackStructures.GSClientKick.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSClientAchievementStatus] = delegate(IntPtr data, int size)
			{
				this.gsClientAchievementStatus.Add(ManagedSteam.CallbackStructures.GSClientAchievementStatus.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSPolicyResponse] = delegate(IntPtr data, int size)
			{
				this.gsPolicyResponse.Add(ManagedSteam.CallbackStructures.GSPolicyResponse.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSGameplayStats] = delegate(IntPtr data, int size)
			{
				this.gsGameplayStats.Add(ManagedSteam.CallbackStructures.GSGameplayStats.Create(data, size));
			};
			SteamServiceGameServer.Callbacks[CallbackID.GSClientGroupStatus] = delegate(IntPtr data, int size)
			{
				this.gsClientGroupStatus.Add(ManagedSteam.CallbackStructures.GSClientGroupStatus.Create(data, size));
			};
			SteamServiceGameServer.Results[ResultID.GSReputation] = delegate(IntPtr data, int size, bool flag)
			{
				this.gsReputation.Add(new SteamService.Result<GSReputation>(ManagedSteam.CallbackStructures.GSReputation.Create(data, size), flag));
			};
			SteamServiceGameServer.Results[ResultID.AssociateWithClanResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.associateWithClanResult.Add(new SteamService.Result<AssociateWithClanResult>(ManagedSteam.CallbackStructures.AssociateWithClanResult.Create(data, size), flag));
			};
			SteamServiceGameServer.Results[ResultID.ComputeNewPlayerCompatibilityResult] = delegate(IntPtr data, int size, bool flag)
			{
				this.computeNewPlayerCompatibilityResult.Add(new SteamService.Result<ComputeNewPlayerCompatibilityResult>(ManagedSteam.CallbackStructures.ComputeNewPlayerCompatibilityResult.Create(data, size), flag));
			};
		}

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06000703 RID: 1795 RVA: 0x0000A730 File Offset: 0x00008930
		// (remove) Token: 0x06000704 RID: 1796 RVA: 0x0000A768 File Offset: 0x00008968
		public event CallbackEvent<SteamServersConnected> SteamServersConnected;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06000705 RID: 1797 RVA: 0x0000A7A0 File Offset: 0x000089A0
		// (remove) Token: 0x06000706 RID: 1798 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public event CallbackEvent<SteamServerConnectFailure> SteamServerConnectFailure;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06000707 RID: 1799 RVA: 0x0000A810 File Offset: 0x00008A10
		// (remove) Token: 0x06000708 RID: 1800 RVA: 0x0000A848 File Offset: 0x00008A48
		public event CallbackEvent<SteamServersDisconnected> SteamServersDisconnected;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06000709 RID: 1801 RVA: 0x0000A880 File Offset: 0x00008A80
		// (remove) Token: 0x0600070A RID: 1802 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public event CallbackEvent<ValidateAuthTicketResponse> ValidateAuthTicketResponse;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600070B RID: 1803 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		// (remove) Token: 0x0600070C RID: 1804 RVA: 0x0000A928 File Offset: 0x00008B28
		public event CallbackEvent<GSClientApprove> GSClientApprove;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600070D RID: 1805 RVA: 0x0000A960 File Offset: 0x00008B60
		// (remove) Token: 0x0600070E RID: 1806 RVA: 0x0000A998 File Offset: 0x00008B98
		public event CallbackEvent<GSClientDeny> GSClientDeny;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600070F RID: 1807 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		// (remove) Token: 0x06000710 RID: 1808 RVA: 0x0000AA08 File Offset: 0x00008C08
		public event CallbackEvent<GSClientKick> GSClientKick;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06000711 RID: 1809 RVA: 0x0000AA40 File Offset: 0x00008C40
		// (remove) Token: 0x06000712 RID: 1810 RVA: 0x0000AA78 File Offset: 0x00008C78
		public event CallbackEvent<GSClientAchievementStatus> GSClientAchievementStatus;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06000713 RID: 1811 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		// (remove) Token: 0x06000714 RID: 1812 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public event CallbackEvent<GSPolicyResponse> GSPolicyResponse;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06000715 RID: 1813 RVA: 0x0000AB20 File Offset: 0x00008D20
		// (remove) Token: 0x06000716 RID: 1814 RVA: 0x0000AB58 File Offset: 0x00008D58
		public event CallbackEvent<GSGameplayStats> GSGameplayStats;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06000717 RID: 1815 RVA: 0x0000AB90 File Offset: 0x00008D90
		// (remove) Token: 0x06000718 RID: 1816 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public event CallbackEvent<GSClientGroupStatus> GSClientGroupStatus;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06000719 RID: 1817 RVA: 0x0000AC00 File Offset: 0x00008E00
		// (remove) Token: 0x0600071A RID: 1818 RVA: 0x0000AC38 File Offset: 0x00008E38
		public event ResultEvent<GSReputation> GSReputation;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600071B RID: 1819 RVA: 0x0000AC70 File Offset: 0x00008E70
		// (remove) Token: 0x0600071C RID: 1820 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public event ResultEvent<AssociateWithClanResult> AssociateWithClanResult;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x0600071D RID: 1821 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		// (remove) Token: 0x0600071E RID: 1822 RVA: 0x0000AD18 File Offset: 0x00008F18
		public event ResultEvent<ComputeNewPlayerCompatibilityResult> ComputeNewPlayerCompatibilityResult;

		// Token: 0x0600071F RID: 1823 RVA: 0x0000AD4D File Offset: 0x00008F4D
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000AD50 File Offset: 0x00008F50
		internal override void ReleaseManagedResources()
		{
			this.steamServersConnected = null;
			this.SteamServersConnected = null;
			this.steamServerConnectFailure = null;
			this.SteamServerConnectFailure = null;
			this.steamServersDisconnected = null;
			this.SteamServersDisconnected = null;
			this.validateAuthTicketResponse = null;
			this.ValidateAuthTicketResponse = null;
			this.gsClientApprove = null;
			this.GSClientApprove = null;
			this.gsClientDeny = null;
			this.GSClientDeny = null;
			this.gsClientKick = null;
			this.GSClientKick = null;
			this.gsClientAchievementStatus = null;
			this.GSClientAchievementStatus = null;
			this.gsPolicyResponse = null;
			this.GSPolicyResponse = null;
			this.gsGameplayStats = null;
			this.GSGameplayStats = null;
			this.gsClientGroupStatus = null;
			this.GSClientGroupStatus = null;
			this.gsReputation = null;
			this.GSReputation = null;
			this.associateWithClanResult = null;
			this.AssociateWithClanResult = null;
			this.computeNewPlayerCompatibilityResult = null;
			this.ComputeNewPlayerCompatibilityResult = null;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000AE24 File Offset: 0x00009024
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<SteamServersConnected>(this.steamServersConnected, this.SteamServersConnected);
			SteamService.InvokeEvents<SteamServerConnectFailure>(this.steamServerConnectFailure, this.SteamServerConnectFailure);
			SteamService.InvokeEvents<SteamServersDisconnected>(this.steamServersDisconnected, this.SteamServersDisconnected);
			SteamService.InvokeEvents<ValidateAuthTicketResponse>(this.validateAuthTicketResponse, this.ValidateAuthTicketResponse);
			SteamService.InvokeEvents<GSClientApprove>(this.gsClientApprove, this.GSClientApprove);
			SteamService.InvokeEvents<GSClientDeny>(this.gsClientDeny, this.GSClientDeny);
			SteamService.InvokeEvents<GSClientKick>(this.gsClientKick, this.GSClientKick);
			SteamService.InvokeEvents<GSClientAchievementStatus>(this.gsClientAchievementStatus, this.GSClientAchievementStatus);
			SteamService.InvokeEvents<GSPolicyResponse>(this.gsPolicyResponse, this.GSPolicyResponse);
			SteamService.InvokeEvents<GSGameplayStats>(this.gsGameplayStats, this.GSGameplayStats);
			SteamService.InvokeEvents<GSClientGroupStatus>(this.gsClientGroupStatus, this.GSClientGroupStatus);
			SteamService.InvokeEvents<GSReputation>(this.gsReputation, this.GSReputation);
			SteamService.InvokeEvents<AssociateWithClanResult>(this.associateWithClanResult, this.AssociateWithClanResult);
			SteamService.InvokeEvents<ComputeNewPlayerCompatibilityResult>(this.computeNewPlayerCompatibilityResult, this.ComputeNewPlayerCompatibilityResult);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000AF1F File Offset: 0x0000911F
		public bool InitGameServer(uint ip, ushort gamePort, ushort queryPort, uint flags, AppID gameAppId, string versionString)
		{
			return NativeMethods.GameServer_InitGameServer(ip, gamePort, queryPort, flags, gameAppId.AsUInt32, versionString);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000AF34 File Offset: 0x00009134
		public void SetProduct(string product)
		{
			NativeMethods.GameServer_SetProduct(product);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000AF3C File Offset: 0x0000913C
		public void SetGameDescription(string gameDescription)
		{
			NativeMethods.GameServer_SetGameDescription(gameDescription);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000AF44 File Offset: 0x00009144
		public void SetModDir(string modDir)
		{
			NativeMethods.GameServer_SetModDir(modDir);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000AF4C File Offset: 0x0000914C
		public void SetDedicatedServer(bool dedicated)
		{
			NativeMethods.GameServer_SetDedicatedServer(dedicated);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000AF54 File Offset: 0x00009154
		public void LogOn(string accountName, string password)
		{
			NativeMethods.GameServer_LogOn(accountName, password);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000AF5D File Offset: 0x0000915D
		public void LogOnAnonymous()
		{
			NativeMethods.GameServer_LogOnAnonymous();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000AF64 File Offset: 0x00009164
		public void LogOff()
		{
			NativeMethods.GameServer_LogOff();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000AF6B File Offset: 0x0000916B
		public bool LoggedOn()
		{
			return NativeMethods.GameServer_LoggedOn();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000AF72 File Offset: 0x00009172
		public bool Secure()
		{
			return NativeMethods.GameServer_Secure();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000AF79 File Offset: 0x00009179
		public SteamID GetSteamID()
		{
			return new SteamID(NativeMethods.GameServer_GetSteamID());
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000AF85 File Offset: 0x00009185
		public bool WasRestartRequested()
		{
			return NativeMethods.GameServer_WasRestartRequested();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000AF8C File Offset: 0x0000918C
		public void SetMaxPlayerCount(int playersMax)
		{
			NativeMethods.GameServer_SetMaxPlayerCount(playersMax);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000AF94 File Offset: 0x00009194
		public void SetBotPlayerCount(int botPlayers)
		{
			NativeMethods.GameServer_SetBotPlayerCount(botPlayers);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000AF9C File Offset: 0x0000919C
		public void SetServerName(string serverName)
		{
			NativeMethods.GameServer_SetServerName(serverName);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public void SetMapName(string mapName)
		{
			NativeMethods.GameServer_SetMapName(mapName);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000AFAC File Offset: 0x000091AC
		public void SetPasswordProtected(bool passwordProtected)
		{
			NativeMethods.GameServer_SetPasswordProtected(passwordProtected);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000AFB4 File Offset: 0x000091B4
		public void SetSpectatorPort(ushort spectatorPort)
		{
			NativeMethods.GameServer_SetSpectatorPort(spectatorPort);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000AFBC File Offset: 0x000091BC
		public void SetSpectatorServerName(string spectatorServerName)
		{
			NativeMethods.GameServer_SetSpectatorServerName(spectatorServerName);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000AFC4 File Offset: 0x000091C4
		public void ClearAllKeyValues()
		{
			NativeMethods.GameServer_ClearAllKeyValues();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000AFCB File Offset: 0x000091CB
		public void SetKeyValue(string key, string value)
		{
			NativeMethods.GameServer_SetKeyValue(key, value);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public void SetGameTags(string gameTags)
		{
			NativeMethods.GameServer_SetGameTags(gameTags);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000AFDC File Offset: 0x000091DC
		public void SetGameData(string gameData)
		{
			NativeMethods.GameServer_SetGameData(gameData);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public void SetRegion(string region)
		{
			NativeMethods.GameServer_SetRegion(region);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000AFEC File Offset: 0x000091EC
		public bool SendUserConnectAndAuthenticate(uint ipClient, byte[] authenticationBlob, out SteamID steamIDUser)
		{
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(authenticationBlob))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				ulong value = 0UL;
				bool flag = NativeMethods.GameServer_SendUserConnectAndAuthenticate(ipClient, nativeBuffer.UnmanagedMemory, (uint)((int)nativeBuffer.UnmanagedMemory), ref value);
				steamIDUser = new SteamID(value);
				result = flag;
			}
			return result;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000B050 File Offset: 0x00009250
		public GameServerSendUserConnectAndAuthenticateResult SendUserConnectAndAuthenticate(uint ipClient, byte[] authenticationBlob)
		{
			GameServerSendUserConnectAndAuthenticateResult result = default(GameServerSendUserConnectAndAuthenticateResult);
			result.Result = this.SendUserConnectAndAuthenticate(ipClient, authenticationBlob, out result.SteamIDUser);
			return result;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000B07C File Offset: 0x0000927C
		public SteamID CreateUnauthenticatedUserConnection()
		{
			return new SteamID(NativeMethods.GameServer_CreateUnauthenticatedUserConnection());
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000B088 File Offset: 0x00009288
		public void SendUserDisconnect(SteamID steamIDUser)
		{
			NativeMethods.GameServer_SendUserDisconnect(steamIDUser.AsUInt64);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000B096 File Offset: 0x00009296
		public bool UpdateUserData(SteamID steamIDUser, string playerName, uint score)
		{
			return NativeMethods.GameServer_UpdateUserData(steamIDUser.AsUInt64, playerName, score);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000B0A6 File Offset: 0x000092A6
		public AuthTicketHandle GetAuthSessionTicket(IntPtr ticket, int maxTicket, out uint ticketLength)
		{
			ticketLength = 0U;
			return new AuthTicketHandle(NativeMethods.GameServer_GetAuthSessionTicket(ticket, maxTicket, ref ticketLength));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public GameServerGetAuthSessionTicketResult GetAuthSessionTicket(IntPtr ticket, int maxTicket)
		{
			GameServerGetAuthSessionTicketResult result = default(GameServerGetAuthSessionTicketResult);
			result.Result = this.GetAuthSessionTicket(ticket, maxTicket, out result.TicketSize);
			return result;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public BeginAuthSessionResult BeginAuthSession(IntPtr authTicket, int cbAuthTicket, SteamID steamID)
		{
			return (BeginAuthSessionResult)NativeMethods.GameServer_BeginAuthSession(authTicket, cbAuthTicket, steamID.AsUInt64);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public void EndAuthSession(SteamID steamID)
		{
			NativeMethods.GameServer_EndAuthSession(steamID.AsUInt64);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000B102 File Offset: 0x00009302
		public void CancelAuthTicket(AuthTicketHandle authTicket)
		{
			NativeMethods.GameServer_CancelAuthTicket(authTicket.AsUInt32);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000B110 File Offset: 0x00009310
		public UserHasLicenseForAppResult UserHasLicenseForApp(SteamID steamID, AppID appID)
		{
			return (UserHasLicenseForAppResult)NativeMethods.GameServer_UserHasLicenseForApp(steamID.AsUInt64, appID.AsUInt32);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000B132 File Offset: 0x00009332
		public bool RequestUserGroupStatus(SteamID steamIDUser, SteamID steamIDGroup)
		{
			return NativeMethods.GameServer_RequestUserGroupStatus(steamIDUser.AsUInt64, steamIDGroup.AsUInt64);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000B147 File Offset: 0x00009347
		[Obsolete("As of Steamworks SDK 1.28 this function has been deprecated. It currently does not return anything, and will be removed in the future.")]
		public void GetGameplayStats()
		{
			NativeMethods.GameServer_GetGameplayStats();
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000B14E File Offset: 0x0000934E
		[Obsolete("As of Steamworks SDK 1.28 this function has been deprecated. It currently does not return anything, and will be removed in the future.")]
		public void GetServerReputation()
		{
			NativeMethods.GameServer_GetServerReputation();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000B155 File Offset: 0x00009355
		public uint GetPublicIP()
		{
			return NativeMethods.GameServer_GetPublicIP();
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000B15C File Offset: 0x0000935C
		public bool HandleIncomingPacket(byte[] data, uint ip, ushort port)
		{
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(data))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				result = NativeMethods.GameServer_HandleIncomingPacket(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, ip, port);
			}
			return result;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public int GetNextOutgoingPacket(byte[] data, out uint netAdr, out ushort port)
		{
			netAdr = 0U;
			port = 0;
			int result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(data))
			{
				int num = NativeMethods.GameServer_GetNextOutgoingPacket(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize, ref netAdr, ref port);
				nativeBuffer.ReadFromUnmanagedMemory(num);
				result = num;
			}
			return result;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000B1FC File Offset: 0x000093FC
		public GameServerGetNextOutgoingPacketResult GetNextOutgoingPacket(byte[] data)
		{
			GameServerGetNextOutgoingPacketResult result = default(GameServerGetNextOutgoingPacketResult);
			result.Result = this.GetNextOutgoingPacket(data, out result.NetAdr, out result.Port);
			return result;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000B22E File Offset: 0x0000942E
		public void EnableHeartbeats(bool active)
		{
			NativeMethods.GameServer_EnableHeartbeats(active);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000B236 File Offset: 0x00009436
		public void SetHeartbeatInterval(int heartbeatInterval)
		{
			NativeMethods.GameServer_SetHeartbeatInterval(heartbeatInterval);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000B23E File Offset: 0x0000943E
		public void ForceHeartbeat()
		{
			NativeMethods.GameServer_ForceHeartbeat();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000B245 File Offset: 0x00009445
		public void AssociateWithClan(SteamID steamIDClan)
		{
			NativeMethods.GameServer_AssociateWithClan(steamIDClan.AsUInt64);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000B253 File Offset: 0x00009453
		public void ComputeNewPlayerCompatibility(SteamID steamIDNewPlayer)
		{
			NativeMethods.GameServer_ComputeNewPlayerCompatibility(steamIDNewPlayer.AsUInt64);
		}

		// Token: 0x0400042C RID: 1068
		private List<SteamServersConnected> steamServersConnected;

		// Token: 0x0400042D RID: 1069
		private List<SteamServerConnectFailure> steamServerConnectFailure;

		// Token: 0x0400042E RID: 1070
		private List<SteamServersDisconnected> steamServersDisconnected;

		// Token: 0x0400042F RID: 1071
		private List<ValidateAuthTicketResponse> validateAuthTicketResponse;

		// Token: 0x04000430 RID: 1072
		private List<GSClientApprove> gsClientApprove;

		// Token: 0x04000431 RID: 1073
		private List<GSClientDeny> gsClientDeny;

		// Token: 0x04000432 RID: 1074
		private List<GSClientKick> gsClientKick;

		// Token: 0x04000433 RID: 1075
		private List<GSClientAchievementStatus> gsClientAchievementStatus;

		// Token: 0x04000434 RID: 1076
		private List<GSPolicyResponse> gsPolicyResponse;

		// Token: 0x04000435 RID: 1077
		private List<GSGameplayStats> gsGameplayStats;

		// Token: 0x04000436 RID: 1078
		private List<GSClientGroupStatus> gsClientGroupStatus;

		// Token: 0x04000437 RID: 1079
		private List<SteamService.Result<GSReputation>> gsReputation;

		// Token: 0x04000438 RID: 1080
		private List<SteamService.Result<AssociateWithClanResult>> associateWithClanResult;

		// Token: 0x04000439 RID: 1081
		private List<SteamService.Result<ComputeNewPlayerCompatibilityResult>> computeNewPlayerCompatibilityResult;
	}
}
