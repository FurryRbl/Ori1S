using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000087 RID: 135
	internal class UnixConnectionPool
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0000F63C File Offset: 0x0000D83C
		static UnixConnectionPool()
		{
			UnixConnectionPool._poolThread.Start();
			UnixConnectionPool._poolThread.IsBackground = true;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000F690 File Offset: 0x0000D890
		public static void Shutdown()
		{
			if (UnixConnectionPool._poolThread != null)
			{
				UnixConnectionPool._poolThread.Abort();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		public static int MaxOpenConnections
		{
			get
			{
				return UnixConnectionPool._maxOpenConnections;
			}
			set
			{
				if (value < 1)
				{
					throw new RemotingException("MaxOpenConnections must be greater than zero");
				}
				UnixConnectionPool._maxOpenConnections = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		public static int KeepAliveSeconds
		{
			get
			{
				return UnixConnectionPool._keepAliveSeconds;
			}
			set
			{
				UnixConnectionPool._keepAliveSeconds = value;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		public static UnixConnection GetConnection(string path)
		{
			Hashtable pools = UnixConnectionPool._pools;
			HostConnectionPool hostConnectionPool;
			lock (pools)
			{
				hostConnectionPool = (HostConnectionPool)UnixConnectionPool._pools[path];
				if (hostConnectionPool == null)
				{
					hostConnectionPool = new HostConnectionPool(path);
					UnixConnectionPool._pools[path] = hostConnectionPool;
				}
			}
			return hostConnectionPool.GetConnection();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000F750 File Offset: 0x0000D950
		private static void ConnectionCollector()
		{
			for (;;)
			{
				Thread.Sleep(3000);
				Hashtable pools = UnixConnectionPool._pools;
				lock (pools)
				{
					ICollection values = UnixConnectionPool._pools.Values;
					foreach (object obj in values)
					{
						HostConnectionPool hostConnectionPool = (HostConnectionPool)obj;
						hostConnectionPool.PurgeConnections();
					}
				}
			}
		}

		// Token: 0x04000453 RID: 1107
		private static Hashtable _pools = new Hashtable();

		// Token: 0x04000454 RID: 1108
		private static int _maxOpenConnections = int.MaxValue;

		// Token: 0x04000455 RID: 1109
		private static int _keepAliveSeconds = 15;

		// Token: 0x04000456 RID: 1110
		private static Thread _poolThread = new Thread(new ThreadStart(UnixConnectionPool.ConnectionCollector));
	}
}
