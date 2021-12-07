using System;
using System.Collections.Generic;
using ManagedSteam.Implementations;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000050 RID: 80
	public abstract class MatchmakingPingResponse : IDisposable
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00003EE4 File Offset: 0x000020E4
		protected MatchmakingPingResponse()
		{
			if (Steam.Instance == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.SteamInstanceIsNull, new object[0]));
			}
			if (Steam.Instance.MatchmakingServers == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.MatchmakingServersIsNull, new object[0]));
			}
			this.ObjectId = NativeMethods.MatchmakingPingResponse_CreateObject();
			this.serverResponded = new List<GameServerItem>();
			this.serverFailedToRespond = 0;
			MatchmakingPingResponse.MatchmakingServers.PingResponse.Add(this.ObjectId, this);
			this.Disposed = false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00003F70 File Offset: 0x00002170
		~MatchmakingPingResponse()
		{
			this.Dispose(false);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00003FA0 File Offset: 0x000021A0
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003FA8 File Offset: 0x000021A8
		internal uint ObjectId { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003FB1 File Offset: 0x000021B1
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00003FB9 File Offset: 0x000021B9
		public bool Disposed { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003FC2 File Offset: 0x000021C2
		private static MatchmakingServers MatchmakingServers
		{
			get
			{
				return (MatchmakingServers)Steam.Instance.MatchmakingServers;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00003FD3 File Offset: 0x000021D3
		internal static void Initialize()
		{
			NativeMethods.MatchmakingPingResponse_RegisterCallbacks(new MatchmakingPingResponse_ServerRespondedCallback(MatchmakingPingResponse.ServerRespondedCallback), new MatchmakingPingResponse_ServerFailedToRespond(MatchmakingPingResponse.ServerFailedToRespondCallback));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00003FF4 File Offset: 0x000021F4
		private static void ServerRespondedCallback(uint instanceId, IntPtr server, int serverSize)
		{
			try
			{
				MatchmakingPingResponse matchmakingPingResponse = MatchmakingPingResponse.MatchmakingServers.PingResponse[instanceId];
				matchmakingPingResponse.serverResponded.Add(GameServerItem.Create(server, serverSize));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingPingResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00004058 File Offset: 0x00002258
		private static void ServerFailedToRespondCallback(uint instanceId)
		{
			try
			{
				MatchmakingPingResponse matchmakingPingResponse = MatchmakingPingResponse.MatchmakingServers.PingResponse[instanceId];
				matchmakingPingResponse.serverFailedToRespond++;
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingPingResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000040B8 File Offset: 0x000022B8
		internal void InvokeEvents()
		{
			foreach (GameServerItem server in this.serverResponded)
			{
				this.ServerResponded(server);
			}
			for (int i = 0; i < this.serverFailedToRespond; i++)
			{
				this.ServerFailedToRespond();
			}
			this.serverResponded.Clear();
			this.serverFailedToRespond = 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00004134 File Offset: 0x00002334
		internal void LibraryShuttingDown()
		{
			NativeMethods.MatchmakingPingResponse_DestroyObject(this.ObjectId);
			this.ObjectId = 0U;
			this.Disposed = true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000414F File Offset: 0x0000234F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00004160 File Offset: 0x00002360
		private void Dispose(bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					this.serverResponded = null;
					if (Steam.Instance != null && Steam.Instance.MatchmakingServers != null)
					{
						MatchmakingPingResponse.MatchmakingServers.PingResponse.Remove(this.ObjectId);
					}
				}
				this.LibraryShuttingDown();
			}
			this.Disposed = true;
		}

		// Token: 0x060001FE RID: 510
		protected abstract void ServerResponded(GameServerItem server);

		// Token: 0x060001FF RID: 511
		protected abstract void ServerFailedToRespond();

		// Token: 0x04000179 RID: 377
		private List<GameServerItem> serverResponded;

		// Token: 0x0400017A RID: 378
		private int serverFailedToRespond;
	}
}
