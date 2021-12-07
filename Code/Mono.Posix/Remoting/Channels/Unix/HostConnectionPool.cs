using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008A RID: 138
	internal class HostConnectionPool
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x0000F8CC File Offset: 0x0000DACC
		public HostConnectionPool(string path)
		{
			this._path = path;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public UnixConnection GetConnection()
		{
			UnixConnection unixConnection = null;
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (;;)
				{
					if (this._pool.Count <= 0)
					{
						goto IL_6F;
					}
					unixConnection = (UnixConnection)this._pool[this._pool.Count - 1];
					this._pool.RemoveAt(this._pool.Count - 1);
					if (unixConnection.IsAlive)
					{
						goto IL_6F;
					}
					this.CancelConnection(unixConnection);
					unixConnection = null;
					IL_9C:
					if (unixConnection != null)
					{
						break;
					}
					continue;
					IL_6F:
					if (unixConnection == null && this._activeConnections < UnixConnectionPool.MaxOpenConnections)
					{
						break;
					}
					if (unixConnection == null)
					{
						Monitor.Wait(this._pool);
						goto IL_9C;
					}
					goto IL_9C;
				}
			}
			if (unixConnection == null)
			{
				return this.CreateConnection();
			}
			return unixConnection;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		private UnixConnection CreateConnection()
		{
			UnixConnection result;
			try
			{
				ReusableUnixClient client = new ReusableUnixClient(this._path);
				UnixConnection unixConnection = new UnixConnection(this, client);
				this._activeConnections++;
				result = unixConnection;
			}
			catch (Exception ex)
			{
				throw new RemotingException(ex.Message);
			}
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public void ReleaseConnection(UnixConnection entry)
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				entry.ControlTime = DateTime.Now;
				this._pool.Add(entry);
				Monitor.Pulse(this._pool);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		private void CancelConnection(UnixConnection entry)
		{
			try
			{
				entry.Stream.Close();
				this._activeConnections--;
			}
			catch
			{
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		public void PurgeConnections()
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (int i = 0; i < this._pool.Count; i++)
				{
					UnixConnection unixConnection = (UnixConnection)this._pool[i];
					if ((DateTime.Now - unixConnection.ControlTime).TotalSeconds > (double)UnixConnectionPool.KeepAliveSeconds)
					{
						this.CancelConnection(unixConnection);
						this._pool.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x0400045C RID: 1116
		private ArrayList _pool = new ArrayList();

		// Token: 0x0400045D RID: 1117
		private int _activeConnections;

		// Token: 0x0400045E RID: 1118
		private string _path;
	}
}
