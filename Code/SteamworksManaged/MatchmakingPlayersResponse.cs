using System;
using System.Collections.Generic;
using ManagedSteam.Implementations;

namespace ManagedSteam
{
	// Token: 0x0200001E RID: 30
	public abstract class MatchmakingPlayersResponse : IDisposable
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000366C File Offset: 0x0000186C
		protected MatchmakingPlayersResponse()
		{
			if (Steam.Instance == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.SteamInstanceIsNull, new object[0]));
			}
			if (Steam.Instance.MatchmakingServers == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.MatchmakingServersIsNull, new object[0]));
			}
			this.ObjectId = NativeMethods.MatchmakingPlayersResponse_CreateObject();
			this.addPlayerToList = new List<MatchmakingPlayersResponse.CachedResponses>();
			this.playersFailedToRespond = 0;
			this.playersRefreshComplete = 0;
			MatchmakingPlayersResponse.MatchmakingServers.PlayersResponse.Add(this.ObjectId, this);
			this.Disposed = false;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003700 File Offset: 0x00001900
		~MatchmakingPlayersResponse()
		{
			this.Dispose(false);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003738 File Offset: 0x00001938
		internal uint ObjectId { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003741 File Offset: 0x00001941
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003749 File Offset: 0x00001949
		public bool Disposed { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003752 File Offset: 0x00001952
		private static MatchmakingServers MatchmakingServers
		{
			get
			{
				return (MatchmakingServers)Steam.Instance.MatchmakingServers;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003763 File Offset: 0x00001963
		internal static void Initialize()
		{
			NativeMethods.MatchmakingPlayersResponse_RegisterCallbacks(new MatchmakingPlayersResponse_AddPlayerToList(MatchmakingPlayersResponse.AddPlayerToListCallback), new MatchmakingPlayersResponse_PlayersFailedToRespond(MatchmakingPlayersResponse.PlayersFailedToRespondCallback), new MatchmakingPlayersResponse_PlayersRefreshComplete(MatchmakingPlayersResponse.PlayersRefreshCompleteCallback));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003790 File Offset: 0x00001990
		private static void AddPlayerToListCallback(uint instanceId, IntPtr name, int score, float timePlayed)
		{
			try
			{
				MatchmakingPlayersResponse matchmakingPlayersResponse = MatchmakingPlayersResponse.MatchmakingServers.PlayersResponse[instanceId];
				matchmakingPlayersResponse.addPlayerToList.Add(new MatchmakingPlayersResponse.CachedResponses(NativeHelpers.ToStringUtf8(name), score, timePlayed));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingPlayersResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000037F8 File Offset: 0x000019F8
		private static void PlayersFailedToRespondCallback(uint instanceId)
		{
			try
			{
				MatchmakingPlayersResponse matchmakingPlayersResponse = MatchmakingPlayersResponse.MatchmakingServers.PlayersResponse[instanceId];
				matchmakingPlayersResponse.playersFailedToRespond++;
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingPlayersResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003858 File Offset: 0x00001A58
		private static void PlayersRefreshCompleteCallback(uint instanceId)
		{
			try
			{
				MatchmakingPlayersResponse matchmakingPlayersResponse = MatchmakingPlayersResponse.MatchmakingServers.PlayersResponse[instanceId];
				matchmakingPlayersResponse.playersRefreshComplete++;
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingPlayersResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal void InvokeEvents()
		{
			foreach (MatchmakingPlayersResponse.CachedResponses cachedResponses in this.addPlayerToList)
			{
				this.AddPlayerToList(cachedResponses.Name, cachedResponses.Score, cachedResponses.TimePlayed);
			}
			for (int i = 0; i < this.playersFailedToRespond; i++)
			{
				this.PlayersFailedToRespond();
			}
			for (int j = 0; j < this.playersRefreshComplete; j++)
			{
				this.PlayersRefreshComplete();
			}
			this.addPlayerToList.Clear();
			this.playersFailedToRespond = 0;
			this.playersRefreshComplete = 0;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003968 File Offset: 0x00001B68
		internal void LibraryShuttingDown()
		{
			NativeMethods.MatchmakingPlayersResponse_DestroyObject(this.ObjectId);
			this.ObjectId = 0U;
			this.Disposed = true;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003983 File Offset: 0x00001B83
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003994 File Offset: 0x00001B94
		private void Dispose(bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					this.addPlayerToList = null;
					if (Steam.Instance != null && Steam.Instance.MatchmakingServers != null)
					{
						MatchmakingPlayersResponse.MatchmakingServers.PlayersResponse.Remove(this.ObjectId);
					}
				}
				this.LibraryShuttingDown();
			}
			this.Disposed = true;
		}

		// Token: 0x060000E5 RID: 229
		protected abstract void AddPlayerToList(string name, int score, float timePlayed);

		// Token: 0x060000E6 RID: 230
		protected abstract void PlayersFailedToRespond();

		// Token: 0x060000E7 RID: 231
		protected abstract void PlayersRefreshComplete();

		// Token: 0x040000B9 RID: 185
		private List<MatchmakingPlayersResponse.CachedResponses> addPlayerToList;

		// Token: 0x040000BA RID: 186
		private int playersFailedToRespond;

		// Token: 0x040000BB RID: 187
		private int playersRefreshComplete;

		// Token: 0x0200001F RID: 31
		private struct CachedResponses
		{
			// Token: 0x060000E8 RID: 232 RVA: 0x000039E9 File Offset: 0x00001BE9
			public CachedResponses(string name, int score, float timePlayed)
			{
				this.Name = name;
				this.Score = score;
				this.TimePlayed = timePlayed;
			}

			// Token: 0x040000BE RID: 190
			public string Name;

			// Token: 0x040000BF RID: 191
			public int Score;

			// Token: 0x040000C0 RID: 192
			public float TimePlayed;
		}
	}
}
