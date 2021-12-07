using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x02000054 RID: 84
	public class NetworkServerSimple
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000156FC File Offset: 0x000138FC
		public NetworkServerSimple()
		{
			this.m_ConnectionsReadOnly = new ReadOnlyCollection<NetworkConnection>(this.m_Connections);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00015754 File Offset: 0x00013954
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001575C File Offset: 0x0001395C
		public int listenPort
		{
			get
			{
				return this.m_ListenPort;
			}
			set
			{
				this.m_ListenPort = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00015768 File Offset: 0x00013968
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00015770 File Offset: 0x00013970
		public int serverHostId
		{
			get
			{
				return this.m_ServerHostId;
			}
			set
			{
				this.m_ServerHostId = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001577C File Offset: 0x0001397C
		public HostTopology hostTopology
		{
			get
			{
				return this.m_HostTopology;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0001578C File Offset: 0x0001398C
		public bool useWebSockets
		{
			get
			{
				return this.m_UseWebSockets;
			}
			set
			{
				this.m_UseWebSockets = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00015798 File Offset: 0x00013998
		public ReadOnlyCollection<NetworkConnection> connections
		{
			get
			{
				return this.m_ConnectionsReadOnly;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000157A0 File Offset: 0x000139A0
		public Dictionary<short, NetworkMessageDelegate> handlers
		{
			get
			{
				return this.m_MessageHandlers.GetHandlers();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000157B0 File Offset: 0x000139B0
		public byte[] messageBuffer
		{
			get
			{
				return this.m_MsgBuffer;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000157B8 File Offset: 0x000139B8
		public NetworkReader messageReader
		{
			get
			{
				return this.m_MsgReader;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000157C0 File Offset: 0x000139C0
		public Type networkConnectionClass
		{
			get
			{
				return this.m_NetworkConnectionClass;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000157C8 File Offset: 0x000139C8
		public void SetNetworkConnectionClass<T>() where T : NetworkConnection
		{
			this.m_NetworkConnectionClass = typeof(T);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000157DC File Offset: 0x000139DC
		public virtual void Initialize()
		{
			if (this.m_Initialized)
			{
				return;
			}
			this.m_Initialized = true;
			NetworkTransport.Init();
			this.m_MsgBuffer = new byte[65535];
			this.m_MsgReader = new NetworkReader(this.m_MsgBuffer);
			if (this.m_HostTopology == null)
			{
				ConnectionConfig connectionConfig = new ConnectionConfig();
				connectionConfig.AddChannel(QosType.Reliable);
				connectionConfig.AddChannel(QosType.Unreliable);
				this.m_HostTopology = new HostTopology(connectionConfig, 8);
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkServerSimple initialize.");
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015864 File Offset: 0x00013A64
		public bool Configure(ConnectionConfig config, int maxConnections)
		{
			HostTopology topology = new HostTopology(config, maxConnections);
			return this.Configure(topology);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00015880 File Offset: 0x00013A80
		public bool Configure(HostTopology topology)
		{
			this.m_HostTopology = topology;
			return true;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001588C File Offset: 0x00013A8C
		public bool Listen(string ipAddress, int serverListenPort)
		{
			this.Initialize();
			this.m_ListenPort = serverListenPort;
			if (this.m_UseWebSockets)
			{
				this.m_ServerHostId = NetworkTransport.AddWebsocketHost(this.m_HostTopology, serverListenPort, ipAddress);
			}
			else
			{
				this.m_ServerHostId = NetworkTransport.AddHost(this.m_HostTopology, serverListenPort, ipAddress);
			}
			if (this.m_ServerHostId == -1)
			{
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"NetworkServerSimple listen: ",
					ipAddress,
					":",
					this.m_ListenPort
				}));
			}
			return true;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00015928 File Offset: 0x00013B28
		public bool Listen(int serverListenPort)
		{
			return this.Listen(serverListenPort, this.m_HostTopology);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015938 File Offset: 0x00013B38
		public bool Listen(int serverListenPort, HostTopology topology)
		{
			this.m_HostTopology = topology;
			this.Initialize();
			this.m_ListenPort = serverListenPort;
			if (this.m_UseWebSockets)
			{
				this.m_ServerHostId = NetworkTransport.AddWebsocketHost(this.m_HostTopology, serverListenPort);
			}
			else
			{
				this.m_ServerHostId = NetworkTransport.AddHost(this.m_HostTopology, serverListenPort);
			}
			if (this.m_ServerHostId == -1)
			{
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkServerSimple listen " + this.m_ListenPort);
			}
			return true;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000159C0 File Offset: 0x00013BC0
		public void ListenRelay(string relayIp, int relayPort, NetworkID netGuid, SourceID sourceId, NodeID nodeId)
		{
			this.Initialize();
			this.m_ServerHostId = NetworkTransport.AddHost(this.m_HostTopology, this.listenPort);
			if (LogFilter.logDebug)
			{
				Debug.Log("Server Host Slot Id: " + this.m_ServerHostId);
			}
			this.Update();
			byte b;
			NetworkTransport.ConnectAsNetworkHost(this.m_ServerHostId, relayIp, relayPort, netGuid, sourceId, nodeId, out b);
			this.m_RelaySlotId = 0;
			if (LogFilter.logDebug)
			{
				Debug.Log("Relay Slot Id: " + this.m_RelaySlotId);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00015A54 File Offset: 0x00013C54
		public void Stop()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkServerSimple stop ");
			}
			NetworkTransport.RemoveHost(this.m_ServerHostId);
			this.m_ServerHostId = -1;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00015A80 File Offset: 0x00013C80
		internal void RegisterHandlerSafe(short msgType, NetworkMessageDelegate handler)
		{
			this.m_MessageHandlers.RegisterHandlerSafe(msgType, handler);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00015A90 File Offset: 0x00013C90
		public void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			this.m_MessageHandlers.RegisterHandler(msgType, handler);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public void UnregisterHandler(short msgType)
		{
			this.m_MessageHandlers.UnregisterHandler(msgType);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00015AB0 File Offset: 0x00013CB0
		public void ClearHandlers()
		{
			this.m_MessageHandlers.ClearMessageHandlers();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public void UpdateConnections()
		{
			for (int i = 0; i < this.m_Connections.Count; i++)
			{
				NetworkConnection networkConnection = this.m_Connections[i];
				if (networkConnection != null)
				{
					networkConnection.FlushChannels();
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015B04 File Offset: 0x00013D04
		public void Update()
		{
			if (this.m_ServerHostId == -1)
			{
				return;
			}
			NetworkEventType networkEventType;
			if (this.m_RelaySlotId != -1)
			{
				byte error;
				networkEventType = NetworkTransport.ReceiveRelayEventFromHost(this.m_ServerHostId, out error);
				if (networkEventType != NetworkEventType.Nothing && LogFilter.logDebug)
				{
					Debug.Log("NetGroup event:" + networkEventType);
				}
				if (networkEventType == NetworkEventType.ConnectEvent && LogFilter.logDebug)
				{
					Debug.Log("NetGroup server connected");
				}
				if (networkEventType == NetworkEventType.DisconnectEvent && LogFilter.logDebug)
				{
					Debug.Log("NetGroup server disconnected");
				}
			}
			do
			{
				byte error;
				int connectionId;
				int channelId;
				int receivedSize;
				networkEventType = NetworkTransport.ReceiveFromHost(this.m_ServerHostId, out connectionId, out channelId, this.m_MsgBuffer, this.m_MsgBuffer.Length, out receivedSize, out error);
				if (networkEventType != NetworkEventType.Nothing)
				{
				}
				switch (networkEventType)
				{
				case NetworkEventType.DataEvent:
					this.HandleData(connectionId, channelId, receivedSize, error);
					break;
				case NetworkEventType.ConnectEvent:
					this.HandleConnect(connectionId, error);
					break;
				case NetworkEventType.DisconnectEvent:
					this.HandleDisconnect(connectionId, error);
					break;
				case NetworkEventType.Nothing:
					break;
				default:
					if (LogFilter.logError)
					{
						Debug.LogError("Unknown network message type received: " + networkEventType);
					}
					break;
				}
			}
			while (networkEventType != NetworkEventType.Nothing);
			this.UpdateConnections();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00015C48 File Offset: 0x00013E48
		public NetworkConnection FindConnection(int connectionId)
		{
			if (connectionId < 0 || connectionId >= this.m_Connections.Count)
			{
				return null;
			}
			return this.m_Connections[connectionId];
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00015C7C File Offset: 0x00013E7C
		public bool SetConnectionAtIndex(NetworkConnection conn)
		{
			while (this.m_Connections.Count <= conn.connectionId)
			{
				this.m_Connections.Add(null);
			}
			if (this.m_Connections[conn.connectionId] != null)
			{
				return false;
			}
			this.m_Connections[conn.connectionId] = conn;
			conn.SetHandlers(this.m_MessageHandlers);
			return true;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public bool RemoveConnectionAtIndex(int connectionId)
		{
			if (connectionId < 0 || connectionId >= this.m_Connections.Count)
			{
				return false;
			}
			this.m_Connections[connectionId] = null;
			return true;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00015D20 File Offset: 0x00013F20
		private void HandleConnect(int connectionId, byte error)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkServerSimple accepted client:" + connectionId);
			}
			if (error != 0)
			{
				this.OnConnectError(connectionId, error);
				return;
			}
			string networkAddress;
			int num;
			NetworkID networkID;
			NodeID nodeID;
			byte b;
			NetworkTransport.GetConnectionInfo(this.m_ServerHostId, connectionId, out networkAddress, out num, out networkID, out nodeID, out b);
			NetworkConnection networkConnection = (NetworkConnection)Activator.CreateInstance(this.m_NetworkConnectionClass);
			networkConnection.SetHandlers(this.m_MessageHandlers);
			networkConnection.Initialize(networkAddress, this.m_ServerHostId, connectionId, this.m_HostTopology);
			while (this.m_Connections.Count <= connectionId)
			{
				this.m_Connections.Add(null);
			}
			this.m_Connections[connectionId] = networkConnection;
			this.OnConnected(networkConnection);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00015DE0 File Offset: 0x00013FE0
		private void HandleDisconnect(int connectionId, byte error)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkServerSimple disconnect client:" + connectionId);
			}
			NetworkConnection networkConnection = this.FindConnection(connectionId);
			if (networkConnection == null)
			{
				return;
			}
			if (error != 0 && error != 6)
			{
				this.m_Connections[connectionId] = null;
				if (LogFilter.logError)
				{
					Debug.LogError("Server client disconnect error:" + connectionId);
				}
				this.OnDisconnectError(networkConnection, error);
				return;
			}
			networkConnection.Disconnect();
			this.m_Connections[connectionId] = null;
			if (LogFilter.logDebug)
			{
				Debug.Log("Server lost client:" + connectionId);
			}
			this.OnDisconnected(networkConnection);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015E98 File Offset: 0x00014098
		private void HandleData(int connectionId, int channelId, int receivedSize, byte error)
		{
			NetworkConnection networkConnection = this.FindConnection(connectionId);
			if (networkConnection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleData Unknown connectionId:" + connectionId);
				}
				return;
			}
			if (error != 0)
			{
				this.OnDataError(networkConnection, error);
				return;
			}
			this.m_MsgReader.SeekZero();
			this.OnData(networkConnection, receivedSize, channelId);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00015EF8 File Offset: 0x000140F8
		public void SendBytesTo(int connectionId, byte[] bytes, int numBytes, int channelId)
		{
			NetworkConnection networkConnection = this.FindConnection(connectionId);
			if (networkConnection == null)
			{
				return;
			}
			networkConnection.SendBytes(bytes, numBytes, channelId);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00015F20 File Offset: 0x00014120
		public void SendWriterTo(int connectionId, NetworkWriter writer, int channelId)
		{
			NetworkConnection networkConnection = this.FindConnection(connectionId);
			if (networkConnection == null)
			{
				return;
			}
			networkConnection.SendWriter(writer, channelId);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00015F48 File Offset: 0x00014148
		public void Disconnect(int connectionId)
		{
			NetworkConnection networkConnection = this.FindConnection(connectionId);
			if (networkConnection == null)
			{
				return;
			}
			networkConnection.Disconnect();
			this.m_Connections[connectionId] = null;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015F78 File Offset: 0x00014178
		public void DisconnectAllConnections()
		{
			for (int i = 0; i < this.m_Connections.Count; i++)
			{
				NetworkConnection networkConnection = this.m_Connections[i];
				if (networkConnection != null)
				{
					networkConnection.Disconnect();
					networkConnection.Dispose();
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00015FC0 File Offset: 0x000141C0
		public virtual void OnConnectError(int connectionId, byte error)
		{
			Debug.LogError("OnConnectError error:" + error);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00015FD8 File Offset: 0x000141D8
		public virtual void OnDataError(NetworkConnection conn, byte error)
		{
			Debug.LogError("OnDataError error:" + error);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00015FF0 File Offset: 0x000141F0
		public virtual void OnDisconnectError(NetworkConnection conn, byte error)
		{
			Debug.LogError("OnDisconnectError error:" + error);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00016008 File Offset: 0x00014208
		public virtual void OnConnected(NetworkConnection conn)
		{
			conn.InvokeHandlerNoData(32);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00016014 File Offset: 0x00014214
		public virtual void OnDisconnected(NetworkConnection conn)
		{
			conn.InvokeHandlerNoData(33);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00016020 File Offset: 0x00014220
		public virtual void OnData(NetworkConnection conn, int receivedSize, int channelId)
		{
			conn.TransportRecieve(this.m_MsgBuffer, receivedSize, channelId);
		}

		// Token: 0x040001A2 RID: 418
		private bool m_Initialized;

		// Token: 0x040001A3 RID: 419
		private int m_ListenPort;

		// Token: 0x040001A4 RID: 420
		private int m_ServerHostId = -1;

		// Token: 0x040001A5 RID: 421
		private int m_RelaySlotId = -1;

		// Token: 0x040001A6 RID: 422
		private bool m_UseWebSockets;

		// Token: 0x040001A7 RID: 423
		private byte[] m_MsgBuffer;

		// Token: 0x040001A8 RID: 424
		private NetworkReader m_MsgReader;

		// Token: 0x040001A9 RID: 425
		private Type m_NetworkConnectionClass = typeof(NetworkConnection);

		// Token: 0x040001AA RID: 426
		private HostTopology m_HostTopology;

		// Token: 0x040001AB RID: 427
		private List<NetworkConnection> m_Connections = new List<NetworkConnection>();

		// Token: 0x040001AC RID: 428
		private ReadOnlyCollection<NetworkConnection> m_ConnectionsReadOnly;

		// Token: 0x040001AD RID: 429
		private NetworkMessageHandlers m_MessageHandlers = new NetworkMessageHandlers();
	}
}
