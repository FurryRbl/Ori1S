using System;
using System.Collections;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000086 RID: 134
	internal class ClientSessionCache
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001D788 File Offset: 0x0001B988
		public static void Add(string host, byte[] id)
		{
			object obj = ClientSessionCache.locker;
			lock (obj)
			{
				string key = BitConverter.ToString(id);
				ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[key];
				if (clientSessionInfo == null)
				{
					ClientSessionCache.cache.Add(key, new ClientSessionInfo(host, id));
				}
				else if (clientSessionInfo.HostName == host)
				{
					clientSessionInfo.KeepAlive();
				}
				else
				{
					clientSessionInfo.Dispose();
					ClientSessionCache.cache.Remove(key);
					ClientSessionCache.cache.Add(key, new ClientSessionInfo(host, id));
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001D840 File Offset: 0x0001BA40
		public static byte[] FromHost(string host)
		{
			object obj = ClientSessionCache.locker;
			byte[] result;
			lock (obj)
			{
				foreach (object obj2 in ClientSessionCache.cache.Values)
				{
					ClientSessionInfo clientSessionInfo = (ClientSessionInfo)obj2;
					if (clientSessionInfo.HostName == host && clientSessionInfo.Valid)
					{
						clientSessionInfo.KeepAlive();
						return clientSessionInfo.Id;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001D918 File Offset: 0x0001BB18
		private static ClientSessionInfo FromContext(Context context, bool checkValidity)
		{
			if (context == null)
			{
				return null;
			}
			byte[] sessionId = context.SessionId;
			if (sessionId == null || sessionId.Length == 0)
			{
				return null;
			}
			string key = BitConverter.ToString(sessionId);
			ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[key];
			if (clientSessionInfo == null)
			{
				return null;
			}
			if (context.ClientSettings.TargetHost != clientSessionInfo.HostName)
			{
				return null;
			}
			if (checkValidity && !clientSessionInfo.Valid)
			{
				clientSessionInfo.Dispose();
				ClientSessionCache.cache.Remove(key);
				return null;
			}
			return clientSessionInfo;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public static bool SetContextInCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool result;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, false);
				if (clientSessionInfo == null)
				{
					result = false;
				}
				else
				{
					clientSessionInfo.GetContext(context);
					clientSessionInfo.KeepAlive();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public static bool SetContextFromCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool result;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, true);
				if (clientSessionInfo == null)
				{
					result = false;
				}
				else
				{
					clientSessionInfo.SetContext(context);
					clientSessionInfo.KeepAlive();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0400025C RID: 604
		private static Hashtable cache = new Hashtable();

		// Token: 0x0400025D RID: 605
		private static object locker = new object();
	}
}
