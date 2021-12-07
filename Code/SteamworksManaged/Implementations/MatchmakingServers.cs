using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000054 RID: 84
	internal class MatchmakingServers : SteamService, IMatchmakingServers
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00004528 File Offset: 0x00002728
		internal MatchmakingServers()
		{
			this.bufferedExceptions = new List<Exception>();
			this.ServerListResponse = new Dictionary<uint, MatchmakingServerListResponse>();
			this.PingResponse = new Dictionary<uint, MatchmakingPingResponse>();
			this.PlayersResponse = new Dictionary<uint, MatchmakingPlayersResponse>();
			this.RulesResponse = new Dictionary<uint, MatchmakingRulesResponse>();
			MatchmakingServerListResponse.Initialize();
			MatchmakingPingResponse.Initialize();
			MatchmakingPlayersResponse.Initialize();
			MatchmakingRulesResponse.Initialize();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004586 File Offset: 0x00002786
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000458E File Offset: 0x0000278E
		internal Dictionary<uint, MatchmakingServerListResponse> ServerListResponse { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004597 File Offset: 0x00002797
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000459F File Offset: 0x0000279F
		internal Dictionary<uint, MatchmakingPingResponse> PingResponse { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000045A8 File Offset: 0x000027A8
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000045B0 File Offset: 0x000027B0
		internal Dictionary<uint, MatchmakingPlayersResponse> PlayersResponse { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000045B9 File Offset: 0x000027B9
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000045C1 File Offset: 0x000027C1
		internal Dictionary<uint, MatchmakingRulesResponse> RulesResponse { get; private set; }

		// Token: 0x06000236 RID: 566 RVA: 0x000045CA File Offset: 0x000027CA
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000045CC File Offset: 0x000027CC
		internal override void ReleaseManagedResources()
		{
			foreach (KeyValuePair<uint, MatchmakingServerListResponse> keyValuePair in this.ServerListResponse)
			{
				keyValuePair.Value.LibraryShuttingDown();
			}
			foreach (KeyValuePair<uint, MatchmakingPingResponse> keyValuePair2 in this.PingResponse)
			{
				keyValuePair2.Value.LibraryShuttingDown();
			}
			foreach (KeyValuePair<uint, MatchmakingPlayersResponse> keyValuePair3 in this.PlayersResponse)
			{
				keyValuePair3.Value.LibraryShuttingDown();
			}
			foreach (KeyValuePair<uint, MatchmakingRulesResponse> keyValuePair4 in this.RulesResponse)
			{
				keyValuePair4.Value.LibraryShuttingDown();
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00004700 File Offset: 0x00002900
		internal override void InvokeEvents()
		{
			foreach (KeyValuePair<uint, MatchmakingServerListResponse> keyValuePair in this.ServerListResponse)
			{
				keyValuePair.Value.InvokeEvents();
			}
			foreach (KeyValuePair<uint, MatchmakingPingResponse> keyValuePair2 in this.PingResponse)
			{
				keyValuePair2.Value.InvokeEvents();
			}
			foreach (KeyValuePair<uint, MatchmakingPlayersResponse> keyValuePair3 in this.PlayersResponse)
			{
				keyValuePair3.Value.InvokeEvents();
			}
			foreach (KeyValuePair<uint, MatchmakingRulesResponse> keyValuePair4 in this.RulesResponse)
			{
				keyValuePair4.Value.InvokeEvents();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00004834 File Offset: 0x00002A34
		internal void ReportExceptions()
		{
			foreach (Exception e in this.bufferedExceptions)
			{
				Steam.Instance.ReportException(e);
			}
			this.bufferedExceptions.Clear();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004898 File Offset: 0x00002A98
		internal void SaveException(Exception exception)
		{
			this.bufferedExceptions.Add(exception);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000048A8 File Offset: 0x00002AA8
		private ServerListRequestHandle ServerRequest(MatchmakingServers.ServerRequestType requestType, AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestResponse)
		{
			ServerListRequestHandle result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(IntPtr)) * filters.Length))
			{
				NativeBuffer[] array = new NativeBuffer[filters.Length];
				try
				{
					for (int i = 0; i < filters.Length; i++)
					{
						NativeBuffer nativeBuffer2 = NativeBuffer.CopyToNative<MatchMakingKeyValuePair>(filters[i]);
						array[i] = nativeBuffer2;
						Marshal.WriteInt32(nativeBuffer.UnmanagedMemory, i * Marshal.SizeOf(typeof(int)), nativeBuffer2.UnmanagedMemory.ToInt32());
					}
					switch (requestType)
					{
					case MatchmakingServers.ServerRequestType.Internet:
						result = new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestInternetServerList(appID.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)filters.Length, requestResponse.ObjectId));
						break;
					case MatchmakingServers.ServerRequestType.Friends:
						result = new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestFriendsServerList(appID.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)filters.Length, requestResponse.ObjectId));
						break;
					case MatchmakingServers.ServerRequestType.Favorites:
						result = new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestFavoritesServerList(appID.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)filters.Length, requestResponse.ObjectId));
						break;
					case MatchmakingServers.ServerRequestType.History:
						result = new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestHistoryServerList(appID.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)filters.Length, requestResponse.ObjectId));
						break;
					case MatchmakingServers.ServerRequestType.Spectator:
						result = new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestSpectatorServerList(appID.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)filters.Length, requestResponse.ObjectId));
						break;
					default:
						throw new ArgumentException();
					}
				}
				finally
				{
					foreach (NativeBuffer nativeBuffer3 in array)
					{
						if (nativeBuffer3 != null)
						{
							nativeBuffer3.Dispose();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00004A7C File Offset: 0x00002C7C
		public ServerListRequestHandle RequestInternetServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse)
		{
			return this.ServerRequest(MatchmakingServers.ServerRequestType.Internet, appID, filters, requestServersResponse);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00004A88 File Offset: 0x00002C88
		public ServerListRequestHandle RequestLANServerList(AppID appID, MatchmakingServerListResponse requestServersResponse)
		{
			return new ServerListRequestHandle(NativeMethods.MatchmakingServers_RequestLANServerList(appID.AsUInt32, requestServersResponse.ObjectId));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00004AA1 File Offset: 0x00002CA1
		public ServerListRequestHandle RequestFriendsServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse)
		{
			return this.ServerRequest(MatchmakingServers.ServerRequestType.Friends, appID, filters, requestServersResponse);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00004AAD File Offset: 0x00002CAD
		public ServerListRequestHandle RequestFavoritesServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse)
		{
			return this.ServerRequest(MatchmakingServers.ServerRequestType.Favorites, appID, filters, requestServersResponse);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00004AB9 File Offset: 0x00002CB9
		public ServerListRequestHandle RequestHistoryServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse)
		{
			return this.ServerRequest(MatchmakingServers.ServerRequestType.History, appID, filters, requestServersResponse);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00004AC5 File Offset: 0x00002CC5
		public ServerListRequestHandle RequestSpectatorServerList(AppID appID, MatchMakingKeyValuePair[] filters, MatchmakingServerListResponse requestServersResponse)
		{
			return this.ServerRequest(MatchmakingServers.ServerRequestType.Spectator, appID, filters, requestServersResponse);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00004AD1 File Offset: 0x00002CD1
		public void ReleaseRequest(ServerListRequestHandle serverListRequest)
		{
			NativeMethods.MatchmakingServers_ReleaseRequest(serverListRequest.AsUInt32);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public GameServerItem GetServerDetails(ServerListRequestHandle request, int server)
		{
			IntPtr data = NativeMethods.MatchmakingServers_GetServerDetails(request.AsUInt32, server);
			return GameServerItem.Create(data, NativeMethods.MatchmakingServers_GetGameServerItemSize());
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00004B06 File Offset: 0x00002D06
		public void CancelQuery(ServerListRequestHandle request)
		{
			NativeMethods.MatchmakingServers_CancelQuery(request.AsUInt32);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00004B14 File Offset: 0x00002D14
		public void RefreshQuery(ServerListRequestHandle request)
		{
			NativeMethods.MatchmakingServers_RefreshQuery(request.AsUInt32);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00004B22 File Offset: 0x00002D22
		public bool IsRefreshing(ServerListRequestHandle request)
		{
			return NativeMethods.MatchmakingServers_IsRefreshing(request.AsUInt32);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00004B30 File Offset: 0x00002D30
		public int GetServerCount(ServerListRequestHandle request)
		{
			return NativeMethods.MatchmakingServers_GetServerCount(request.AsUInt32);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00004B3E File Offset: 0x00002D3E
		public void RefreshServer(ServerListRequestHandle request, int server)
		{
			NativeMethods.MatchmakingServers_RefreshServer(request.AsUInt32, server);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00004B4D File Offset: 0x00002D4D
		public ServerQueryHandle PingServer(uint ip, ushort port, MatchmakingPingResponse requestServersResponse)
		{
			return new ServerQueryHandle(NativeMethods.MatchmakingServers_PingServer(ip, port, requestServersResponse.ObjectId));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00004B61 File Offset: 0x00002D61
		public ServerQueryHandle PlayerDetails(uint ip, ushort port, MatchmakingPlayersResponse requestServersResponse)
		{
			return new ServerQueryHandle(NativeMethods.MatchmakingServers_PlayerDetails(ip, port, requestServersResponse.ObjectId));
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00004B75 File Offset: 0x00002D75
		public ServerQueryHandle ServerRules(uint ip, ushort port, MatchmakingRulesResponse requestServersResponse)
		{
			return new ServerQueryHandle(NativeMethods.MatchmakingServers_ServerRules(ip, port, requestServersResponse.ObjectId));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004B89 File Offset: 0x00002D89
		public void CancelServerQuery(ServerQueryHandle serverQuery)
		{
			NativeMethods.MatchmakingServers_CancelServerQuery(serverQuery.AsInt32);
		}

		// Token: 0x04000181 RID: 385
		private List<Exception> bufferedExceptions;

		// Token: 0x02000055 RID: 85
		private enum ServerRequestType
		{
			// Token: 0x04000187 RID: 391
			Internet,
			// Token: 0x04000188 RID: 392
			Friends,
			// Token: 0x04000189 RID: 393
			Favorites,
			// Token: 0x0400018A RID: 394
			History,
			// Token: 0x0400018B RID: 395
			Spectator
		}
	}
}
