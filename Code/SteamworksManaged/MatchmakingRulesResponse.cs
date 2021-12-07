using System;
using System.Collections.Generic;
using ManagedSteam.Implementations;

namespace ManagedSteam
{
	// Token: 0x0200008B RID: 139
	public abstract class MatchmakingRulesResponse : IDisposable
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00007E24 File Offset: 0x00006024
		protected MatchmakingRulesResponse()
		{
			if (Steam.Instance == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.SteamInstanceIsNull, new object[0]));
			}
			if (Steam.Instance.MatchmakingServers == null)
			{
				throw new InvalidOperationException(StringMap.GetString(ErrorCodes.MatchmakingServersIsNull, new object[0]));
			}
			this.ObjectId = NativeMethods.MatchmakingRulesResponse_CreateObject();
			this.rulesResponded = new List<MatchmakingRulesResponse.CachedResponses>();
			this.rulesFailedToRespond = 0;
			this.rulesRefreshComplete = 0;
			MatchmakingRulesResponse.MatchmakingServers.RulesResponse.Add(this.ObjectId, this);
			this.Disposed = false;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00007EB8 File Offset: 0x000060B8
		~MatchmakingRulesResponse()
		{
			this.Dispose(false);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00007EE8 File Offset: 0x000060E8
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00007EF0 File Offset: 0x000060F0
		internal uint ObjectId { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00007EF9 File Offset: 0x000060F9
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00007F01 File Offset: 0x00006101
		public bool Disposed { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00007F0A File Offset: 0x0000610A
		private static MatchmakingServers MatchmakingServers
		{
			get
			{
				return (MatchmakingServers)Steam.Instance.MatchmakingServers;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00007F1B File Offset: 0x0000611B
		internal static void Initialize()
		{
			NativeMethods.MatchmakingRulesResponse_RegisterCallbacks(new MatchmakingRulesResponse_RulesResponded(MatchmakingRulesResponse.RulesRespondedCallback), new MatchmakingRulesResponse_RulesFailedToRespond(MatchmakingRulesResponse.RulesFailedToRespondCallback), new MatchmakingRulesResponse_RulesRefreshComplete(MatchmakingRulesResponse.RulesRefreshCompleteCallback));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00007F48 File Offset: 0x00006148
		private static void RulesRespondedCallback(uint instanceId, IntPtr key, IntPtr value)
		{
			try
			{
				MatchmakingRulesResponse matchmakingRulesResponse = MatchmakingRulesResponse.MatchmakingServers.RulesResponse[instanceId];
				matchmakingRulesResponse.rulesResponded.Add(new MatchmakingRulesResponse.CachedResponses(NativeHelpers.ToStringUtf8(key), NativeHelpers.ToStringUtf8(value)));
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingRulesResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00007FB4 File Offset: 0x000061B4
		private static void RulesFailedToRespondCallback(uint instanceId)
		{
			try
			{
				MatchmakingRulesResponse matchmakingRulesResponse = MatchmakingRulesResponse.MatchmakingServers.RulesResponse[instanceId];
				matchmakingRulesResponse.rulesFailedToRespond++;
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingRulesResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00008014 File Offset: 0x00006214
		private static void RulesRefreshCompleteCallback(uint instanceId)
		{
			try
			{
				MatchmakingRulesResponse matchmakingRulesResponse = MatchmakingRulesResponse.MatchmakingServers.RulesResponse[instanceId];
				matchmakingRulesResponse.rulesRefreshComplete++;
			}
			catch (Exception exception)
			{
				try
				{
					MatchmakingRulesResponse.MatchmakingServers.SaveException(exception);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00008074 File Offset: 0x00006274
		internal void InvokeEvents()
		{
			foreach (MatchmakingRulesResponse.CachedResponses cachedResponses in this.rulesResponded)
			{
				this.RulesResponded(cachedResponses.Key, cachedResponses.Value);
			}
			for (int i = 0; i < this.rulesFailedToRespond; i++)
			{
				this.RulesFailedToRespond();
			}
			for (int j = 0; j < this.rulesRefreshComplete; j++)
			{
				this.RulesRefreshComplete();
			}
			this.rulesResponded.Clear();
			this.rulesFailedToRespond = 0;
			this.rulesRefreshComplete = 0;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000811C File Offset: 0x0000631C
		internal void LibraryShuttingDown()
		{
			NativeMethods.MatchmakingRulesResponse_DestroyObject(this.ObjectId);
			this.ObjectId = 0U;
			this.Disposed = true;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00008137 File Offset: 0x00006337
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00008148 File Offset: 0x00006348
		private void Dispose(bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					this.rulesResponded = null;
					if (Steam.Instance != null && Steam.Instance.MatchmakingServers != null)
					{
						MatchmakingRulesResponse.MatchmakingServers.RulesResponse.Remove(this.ObjectId);
					}
				}
				this.LibraryShuttingDown();
			}
			this.Disposed = true;
		}

		// Token: 0x0600045B RID: 1115
		protected abstract void RulesResponded(string rule, string value);

		// Token: 0x0600045C RID: 1116
		protected abstract void RulesFailedToRespond();

		// Token: 0x0600045D RID: 1117
		protected abstract void RulesRefreshComplete();

		// Token: 0x04000285 RID: 645
		private List<MatchmakingRulesResponse.CachedResponses> rulesResponded;

		// Token: 0x04000286 RID: 646
		private int rulesFailedToRespond;

		// Token: 0x04000287 RID: 647
		private int rulesRefreshComplete;

		// Token: 0x0200008C RID: 140
		private struct CachedResponses
		{
			// Token: 0x0600045E RID: 1118 RVA: 0x0000819D File Offset: 0x0000639D
			public CachedResponses(string key, string value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x0400028A RID: 650
			public string Key;

			// Token: 0x0400028B RID: 651
			public string Value;
		}
	}
}
