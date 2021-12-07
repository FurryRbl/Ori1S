using System;
using System.Collections.Generic;
using ManagedSteam.Implementations;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x0200000A RID: 10
	public abstract class MatchmakingServerListResponse : IDisposable
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002118 File Offset: 0x00000318
		protected MatchmakingServerListResponse()
		{
			if (Steam.Instance == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.SteamInstanceIsNull, new object[0]));
			}
			if (Steam.Instance.MatchmakingServers == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.MatchmakingServersIsNull, new object[0]));
			}
			this.ObjectId = NativeMethods.MatchmakingServerListResponse_CreateObject();
			this.serverResponded = new List<MatchmakingServerListResponse.CachedResponses>();
			this.serverFailedToRespond = new List<MatchmakingServerListResponse.CachedResponses>();
			this.refreshComplete = new List<MatchmakingServerListResponse.CachedResponses>();
			MatchmakingServerListResponse.MatchmakingServers.ServerListResponse.Add(this.ObjectId, this);
			this.Disposed = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		~MatchmakingServerListResponse()
		{
			this.Dispose(false);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000003EC
		internal uint ObjectId { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021FD File Offset: 0x000003FD
		public bool Disposed { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002206 File Offset: 0x00000406
		private static MatchmakingServers MatchmakingServers
		{
			get
			{
				return (MatchmakingServers)Steam.Instance.MatchmakingServers;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002217 File Offset: 0x00000417
		internal static void Initialize()
		{
			NativeMethods.MatchmakingServerListResponse_RegisterCallbacks(new MatchmakingServerListResponse_ServerRespondedCallback(MatchmakingServerListResponse.ServerRespondedCallback), new MatchmakingServerListResponse_ServerFailedToRespond(MatchmakingServerListResponse.ServerFailedToRespondCallback), new MatchmakingServerListResponse_RefreshComplete(MatchmakingServerListResponse.RefreshCompleteCallback));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002244 File Offset: 0x00000444
		private static void ServerRespondedCallback(uint instanceId, uint request, int server)
		{
			try
			{
				MatchmakingServerListResponse matchmakingServerListResponse = MatchmakingServerListResponse.MatchmakingServers.ServerListResponse[instanceId];
				matchmakingServerListResponse.serverResponded.Add(new MatchmakingServerListResponse.CachedResponses(request, server));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingServerListResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A8 File Offset: 0x000004A8
		private static void ServerFailedToRespondCallback(uint instanceId, uint request, int server)
		{
			try
			{
				MatchmakingServerListResponse matchmakingServerListResponse = MatchmakingServerListResponse.MatchmakingServers.ServerListResponse[instanceId];
				matchmakingServerListResponse.serverFailedToRespond.Add(new MatchmakingServerListResponse.CachedResponses(request, server));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingServerListResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230C File Offset: 0x0000050C
		private static void RefreshCompleteCallback(uint instanceId, uint request, int response)
		{
			try
			{
				MatchmakingServerListResponse matchmakingServerListResponse = MatchmakingServerListResponse.MatchmakingServers.ServerListResponse[instanceId];
				matchmakingServerListResponse.refreshComplete.Add(new MatchmakingServerListResponse.CachedResponses(request, response));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingServerListResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002370 File Offset: 0x00000570
		internal void InvokeEvents()
		{
			foreach (MatchmakingServerListResponse.CachedResponses cachedResponses in this.serverResponded)
			{
				this.ServerResponded(new ServerListRequestHandle(cachedResponses.Request), cachedResponses.Data);
			}
			foreach (MatchmakingServerListResponse.CachedResponses cachedResponses2 in this.serverFailedToRespond)
			{
				this.ServerFailedToRespond(new ServerListRequestHandle(cachedResponses2.Request), cachedResponses2.Data);
			}
			foreach (MatchmakingServerListResponse.CachedResponses cachedResponses3 in this.refreshComplete)
			{
				this.RefreshComplete(new ServerListRequestHandle(cachedResponses3.Request), (MatchMakingServerResponse)cachedResponses3.Data);
			}
			this.serverResponded.Clear();
			this.serverFailedToRespond.Clear();
			this.refreshComplete.Clear();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024A0 File Offset: 0x000006A0
		internal void LibraryShuttingDown()
		{
			NativeMethods.MatchmakingServerListResponse_DestroyObject(this.ObjectId);
			this.ObjectId = 0U;
			this.Disposed = true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024BB File Offset: 0x000006BB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024CC File Offset: 0x000006CC
		private void Dispose(bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					this.serverResponded = null;
					this.serverFailedToRespond = null;
					this.refreshComplete = null;
					if (Steam.Instance != null && Steam.Instance.MatchmakingServers != null)
					{
						MatchmakingServerListResponse.MatchmakingServers.ServerListResponse.Remove(this.ObjectId);
					}
				}
				this.LibraryShuttingDown();
			}
			this.Disposed = true;
		}

		// Token: 0x0600001A RID: 26
		protected abstract void ServerResponded(ServerListRequestHandle request, int server);

		// Token: 0x0600001B RID: 27
		protected abstract void ServerFailedToRespond(ServerListRequestHandle request, int server);

		// Token: 0x0600001C RID: 28
		protected abstract void RefreshComplete(ServerListRequestHandle request, MatchMakingServerResponse response);

		// Token: 0x0400002C RID: 44
		private List<MatchmakingServerListResponse.CachedResponses> serverResponded;

		// Token: 0x0400002D RID: 45
		private List<MatchmakingServerListResponse.CachedResponses> serverFailedToRespond;

		// Token: 0x0400002E RID: 46
		private List<MatchmakingServerListResponse.CachedResponses> refreshComplete;

		// Token: 0x0200000B RID: 11
		private struct CachedResponses
		{
			// Token: 0x0600001D RID: 29 RVA: 0x0000252F File Offset: 0x0000072F
			public CachedResponses(uint request, int data)
			{
				this.Request = request;
				this.Data = data;
			}

			// Token: 0x04000031 RID: 49
			public uint Request;

			// Token: 0x04000032 RID: 50
			public int Data;
		}
	}
}
