using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000006 RID: 6
	internal class ConnectionArray
	{
		// Token: 0x06000057 RID: 87 RVA: 0x000040F4 File Offset: 0x000022F4
		public ConnectionArray()
		{
			this.m_Connections = new List<NetworkConnection>();
			this.m_LocalConnections = new List<NetworkConnection>();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004114 File Offset: 0x00002314
		internal List<NetworkConnection> localConnections
		{
			get
			{
				return this.m_LocalConnections;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000411C File Offset: 0x0000231C
		internal List<NetworkConnection> connections
		{
			get
			{
				return this.m_Connections;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004124 File Offset: 0x00002324
		public int Count
		{
			get
			{
				return this.m_Connections.Count;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004134 File Offset: 0x00002334
		public int LocalIndex
		{
			get
			{
				return -this.m_LocalConnections.Count;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004144 File Offset: 0x00002344
		public int Add(int connId, NetworkConnection conn)
		{
			if (connId < 0)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ConnectionArray Add bad id " + connId);
				}
				return -1;
			}
			if (connId < this.m_Connections.Count && this.m_Connections[connId] != null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ConnectionArray Add dupe at " + connId);
				}
				return -1;
			}
			while (connId > this.m_Connections.Count - 1)
			{
				this.m_Connections.Add(null);
			}
			this.m_Connections[connId] = conn;
			return connId;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000041F0 File Offset: 0x000023F0
		public NetworkConnection Get(int connId)
		{
			if (connId < 0)
			{
				return this.m_LocalConnections[Mathf.Abs(connId) - 1];
			}
			if (connId < 0 || connId > this.m_Connections.Count)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ConnectionArray Get invalid index " + connId);
				}
				return null;
			}
			return this.m_Connections[connId];
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004260 File Offset: 0x00002460
		public NetworkConnection GetUnsafe(int connId)
		{
			if (connId < 0 || connId > this.m_Connections.Count)
			{
				return null;
			}
			return this.m_Connections[connId];
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004294 File Offset: 0x00002494
		public void Remove(int connId)
		{
			if (connId < 0)
			{
				this.m_LocalConnections[Mathf.Abs(connId) - 1] = null;
				return;
			}
			if (connId < 0 || connId > this.m_Connections.Count)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ConnectionArray Remove invalid index " + connId);
				}
				return;
			}
			this.m_Connections[connId] = null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004304 File Offset: 0x00002504
		public int AddLocal(NetworkConnection conn)
		{
			this.m_LocalConnections.Add(conn);
			int num = -this.m_LocalConnections.Count;
			conn.connectionId = num;
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004334 File Offset: 0x00002534
		public bool ContainsPlayer(GameObject player, out NetworkConnection conn)
		{
			conn = null;
			if (player == null)
			{
				return false;
			}
			for (int i = this.LocalIndex; i < this.m_Connections.Count; i++)
			{
				conn = this.Get(i);
				if (conn != null)
				{
					for (int j = 0; j < conn.playerControllers.Count; j++)
					{
						if (conn.playerControllers[j].IsValid && conn.playerControllers[j].gameObject == player)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000030 RID: 48
		private List<NetworkConnection> m_LocalConnections;

		// Token: 0x04000031 RID: 49
		private List<NetworkConnection> m_Connections;
	}
}
