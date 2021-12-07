using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x02000052 RID: 82
	public sealed class NetworkServer
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0001295C File Offset: 0x00010B5C
		private NetworkServer()
		{
			NetworkTransport.Init();
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkServer Created version " + Version.Current);
			}
			this.m_RemoveList = new HashSet<NetworkInstanceId>();
			this.m_ExternalConnections = new HashSet<int>();
			this.m_NetworkScene = new NetworkScene();
			this.m_SimpleServerSimple = new NetworkServer.ServerSimpleWrapper(this);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000129F0 File Offset: 0x00010BF0
		public static List<NetworkConnection> localConnections
		{
			get
			{
				return NetworkServer.instance.m_LocalConnectionsFakeList;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000129FC File Offset: 0x00010BFC
		public static int listenPort
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.listenPort;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00012A10 File Offset: 0x00010C10
		public static int serverHostId
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.serverHostId;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00012A24 File Offset: 0x00010C24
		public static ReadOnlyCollection<NetworkConnection> connections
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.connections;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00012A38 File Offset: 0x00010C38
		public static Dictionary<short, NetworkMessageDelegate> handlers
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.handlers;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00012A4C File Offset: 0x00010C4C
		public static HostTopology hostTopology
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.hostTopology;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00012A60 File Offset: 0x00010C60
		public static Dictionary<NetworkInstanceId, NetworkIdentity> objects
		{
			get
			{
				return NetworkServer.instance.m_NetworkScene.localObjects;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00012A74 File Offset: 0x00010C74
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00012A78 File Offset: 0x00010C78
		[Obsolete("Moved to NetworkMigrationManager")]
		public static bool sendPeerInfo
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00012A7C File Offset: 0x00010C7C
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00012A84 File Offset: 0x00010C84
		public static bool dontListen
		{
			get
			{
				return NetworkServer.m_DontListen;
			}
			set
			{
				NetworkServer.m_DontListen = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012A8C File Offset: 0x00010C8C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00012AA0 File Offset: 0x00010CA0
		public static bool useWebSockets
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.useWebSockets;
			}
			set
			{
				NetworkServer.instance.m_SimpleServerSimple.useWebSockets = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012AB4 File Offset: 0x00010CB4
		internal static NetworkServer instance
		{
			get
			{
				if (NetworkServer.s_Instance == null)
				{
					object obj = NetworkServer.s_Sync;
					lock (obj)
					{
						if (NetworkServer.s_Instance == null)
						{
							NetworkServer.s_Instance = new NetworkServer();
						}
					}
				}
				return NetworkServer.s_Instance;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00012B20 File Offset: 0x00010D20
		public static bool active
		{
			get
			{
				return NetworkServer.s_Active;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00012B28 File Offset: 0x00010D28
		public static bool localClientActive
		{
			get
			{
				return NetworkServer.instance.m_LocalClientActive;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00012B34 File Offset: 0x00010D34
		public static int numChannels
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.hostTopology.DefaultConfig.ChannelCount;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00012B50 File Offset: 0x00010D50
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00012B5C File Offset: 0x00010D5C
		public static float maxDelay
		{
			get
			{
				return NetworkServer.instance.m_MaxDelay;
			}
			set
			{
				NetworkServer.instance.InternalSetMaxDelay(value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00012B6C File Offset: 0x00010D6C
		public static Type networkConnectionClass
		{
			get
			{
				return NetworkServer.instance.m_SimpleServerSimple.networkConnectionClass;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012B80 File Offset: 0x00010D80
		public static void SetNetworkConnectionClass<T>() where T : NetworkConnection
		{
			NetworkServer.instance.m_SimpleServerSimple.SetNetworkConnectionClass<T>();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012B94 File Offset: 0x00010D94
		public static bool Configure(ConnectionConfig config, int maxConnections)
		{
			return NetworkServer.instance.m_SimpleServerSimple.Configure(config, maxConnections);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012BA8 File Offset: 0x00010DA8
		public static bool Configure(HostTopology topology)
		{
			return NetworkServer.instance.m_SimpleServerSimple.Configure(topology);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012BBC File Offset: 0x00010DBC
		public static void Reset()
		{
			NetworkTransport.Shutdown();
			NetworkTransport.Init();
			NetworkServer.s_Instance = null;
			NetworkServer.s_Active = false;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public static void Shutdown()
		{
			if (NetworkServer.s_Instance != null)
			{
				NetworkServer.s_Instance.InternalDisconnectAll();
				if (!NetworkServer.m_DontListen)
				{
					NetworkServer.s_Instance.m_SimpleServerSimple.Stop();
				}
				NetworkServer.s_Instance = null;
			}
			NetworkServer.m_DontListen = false;
			NetworkServer.s_Active = false;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00012C34 File Offset: 0x00010E34
		public static bool Listen(MatchInfo matchInfo, int listenPort)
		{
			if (!matchInfo.usingRelay)
			{
				return NetworkServer.instance.InternalListen(null, listenPort);
			}
			NetworkServer.instance.InternalListenRelay(matchInfo.address, matchInfo.port, matchInfo.networkId, Utility.GetSourceID(), matchInfo.nodeId);
			return true;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012C84 File Offset: 0x00010E84
		internal void RegisterMessageHandlers()
		{
			this.m_SimpleServerSimple.RegisterHandlerSafe(35, new NetworkMessageDelegate(NetworkServer.OnClientReadyMessage));
			this.m_SimpleServerSimple.RegisterHandlerSafe(5, new NetworkMessageDelegate(NetworkServer.OnCommandMessage));
			this.m_SimpleServerSimple.RegisterHandlerSafe(6, new NetworkMessageDelegate(NetworkTransform.HandleTransform));
			this.m_SimpleServerSimple.RegisterHandlerSafe(16, new NetworkMessageDelegate(NetworkTransformChild.HandleChildTransform));
			this.m_SimpleServerSimple.RegisterHandlerSafe(38, new NetworkMessageDelegate(NetworkServer.OnRemovePlayerMessage));
			this.m_SimpleServerSimple.RegisterHandlerSafe(40, new NetworkMessageDelegate(NetworkAnimator.OnAnimationServerMessage));
			this.m_SimpleServerSimple.RegisterHandlerSafe(41, new NetworkMessageDelegate(NetworkAnimator.OnAnimationParametersServerMessage));
			this.m_SimpleServerSimple.RegisterHandlerSafe(42, new NetworkMessageDelegate(NetworkAnimator.OnAnimationTriggerServerMessage));
			NetworkServer.maxPacketSize = NetworkServer.hostTopology.DefaultConfig.PacketSize;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012D6C File Offset: 0x00010F6C
		public static void ListenRelay(string relayIp, int relayPort, NetworkID netGuid, SourceID sourceId, NodeID nodeId)
		{
			NetworkServer.instance.InternalListenRelay(relayIp, relayPort, netGuid, sourceId, nodeId);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012D80 File Offset: 0x00010F80
		private void InternalListenRelay(string relayIp, int relayPort, NetworkID netGuid, SourceID sourceId, NodeID nodeId)
		{
			this.m_SimpleServerSimple.ListenRelay(relayIp, relayPort, netGuid, sourceId, nodeId);
			NetworkServer.s_Active = true;
			this.RegisterMessageHandlers();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012DA0 File Offset: 0x00010FA0
		public static bool Listen(int serverPort)
		{
			return NetworkServer.instance.InternalListen(null, serverPort);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public static bool Listen(string ipAddress, int serverPort)
		{
			return NetworkServer.instance.InternalListen(ipAddress, serverPort);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012DC0 File Offset: 0x00010FC0
		internal bool InternalListen(string ipAddress, int serverPort)
		{
			if (NetworkServer.m_DontListen)
			{
				this.m_SimpleServerSimple.Initialize();
			}
			else if (!this.m_SimpleServerSimple.Listen(ipAddress, serverPort))
			{
				return false;
			}
			NetworkServer.maxPacketSize = NetworkServer.hostTopology.DefaultConfig.PacketSize;
			NetworkServer.s_Active = true;
			this.RegisterMessageHandlers();
			return true;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012E1C File Offset: 0x0001101C
		public static NetworkClient BecomeHost(NetworkClient oldClient, int port, MatchInfo matchInfo, int oldConnectionId, PeerInfoMessage[] peers)
		{
			return NetworkServer.instance.BecomeHostInternal(oldClient, port, matchInfo, oldConnectionId, peers);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012E30 File Offset: 0x00011030
		internal NetworkClient BecomeHostInternal(NetworkClient oldClient, int port, MatchInfo matchInfo, int oldConnectionId, PeerInfoMessage[] peers)
		{
			if (NetworkServer.s_Active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("BecomeHost already a server.");
				}
				return null;
			}
			if (!NetworkClient.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("BecomeHost NetworkClient not active.");
				}
				return null;
			}
			NetworkServer.Configure(NetworkServer.hostTopology);
			if (matchInfo == null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("BecomeHost Listen on " + port);
				}
				if (!NetworkServer.Listen(port))
				{
					if (LogFilter.logError)
					{
						Debug.LogError("BecomeHost bind failed.");
					}
					return null;
				}
			}
			else
			{
				if (LogFilter.logDev)
				{
					Debug.Log("BecomeHost match:" + matchInfo.networkId);
				}
				NetworkServer.ListenRelay(matchInfo.address, matchInfo.port, matchInfo.networkId, Utility.GetSourceID(), matchInfo.nodeId);
			}
			foreach (NetworkIdentity networkIdentity in ClientScene.objects.Values)
			{
				if (!(networkIdentity == null) && !(networkIdentity.gameObject == null))
				{
					NetworkIdentity.AddNetworkId(networkIdentity.netId.Value);
					this.m_NetworkScene.SetLocalObject(networkIdentity.netId, networkIdentity.gameObject, false, false);
					networkIdentity.OnStartServer(true);
				}
			}
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkServer BecomeHost done. oldConnectionId:" + oldConnectionId);
			}
			this.RegisterMessageHandlers();
			if (!NetworkClient.RemoveClient(oldClient) && LogFilter.logError)
			{
				Debug.LogError("BecomeHost failed to remove client");
			}
			if (LogFilter.logDev)
			{
				Debug.Log("BecomeHost localClient ready");
			}
			NetworkClient networkClient = ClientScene.ReconnectLocalServer();
			ClientScene.Ready(networkClient.connection);
			ClientScene.SetReconnectId(oldConnectionId, peers);
			ClientScene.AddPlayer(ClientScene.readyConnection, 0);
			return networkClient;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00013048 File Offset: 0x00011248
		private void InternalSetMaxDelay(float seconds)
		{
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					networkConnection.SetMaxDelay(seconds);
				}
			}
			this.m_MaxDelay = seconds;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013090 File Offset: 0x00011290
		internal int AddLocalClient(LocalClient localClient)
		{
			if (this.m_LocalConnectionsFakeList.Count != 0)
			{
				Debug.LogError("Local Connection already exists");
				return -1;
			}
			this.m_LocalConnection = new ULocalConnectionToClient(localClient);
			this.m_LocalConnection.connectionId = 0;
			this.m_SimpleServerSimple.SetConnectionAtIndex(this.m_LocalConnection);
			this.m_LocalConnectionsFakeList.Add(this.m_LocalConnection);
			this.m_LocalConnection.InvokeHandlerNoData(32);
			return 0;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013104 File Offset: 0x00011304
		internal void RemoveLocalClient(NetworkConnection localClientConnection)
		{
			for (int i = 0; i < this.m_LocalConnectionsFakeList.Count; i++)
			{
				if (this.m_LocalConnectionsFakeList[i].connectionId == localClientConnection.connectionId)
				{
					this.m_LocalConnectionsFakeList.RemoveAt(i);
					break;
				}
			}
			if (this.m_LocalConnection != null)
			{
				this.m_LocalConnection.Disconnect();
				this.m_LocalConnection.Dispose();
				this.m_LocalConnection = null;
			}
			this.m_LocalClientActive = false;
			this.m_SimpleServerSimple.RemoveConnectionAtIndex(0);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013198 File Offset: 0x00011398
		internal void SetLocalObjectOnServer(NetworkInstanceId netId, GameObject obj)
		{
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"SetLocalObjectOnServer ",
					netId,
					" ",
					obj
				}));
			}
			this.m_NetworkScene.SetLocalObject(netId, obj, false, true);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000131EC File Offset: 0x000113EC
		internal void ActivateLocalClientScene()
		{
			if (this.m_LocalClientActive)
			{
				return;
			}
			this.m_LocalClientActive = true;
			foreach (NetworkIdentity networkIdentity in NetworkServer.objects.Values)
			{
				if (!networkIdentity.isClient)
				{
					if (LogFilter.logDev)
					{
						Debug.Log(string.Concat(new object[]
						{
							"ActivateClientScene ",
							networkIdentity.netId,
							" ",
							networkIdentity.gameObject
						}));
					}
					ClientScene.SetLocalObject(networkIdentity.netId, networkIdentity.gameObject);
					networkIdentity.OnStartClient();
				}
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000132C8 File Offset: 0x000114C8
		public static bool SendToAll(short msgType, MessageBase msg)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendToAll msgType:" + msgType);
			}
			bool flag = true;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					flag &= networkConnection.Send(msgType, msg);
				}
			}
			return flag;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00013330 File Offset: 0x00011530
		private static bool SendToObservers(GameObject contextObj, short msgType, MessageBase msg)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendToObservers id:" + msgType);
			}
			bool flag = true;
			NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
			if (component == null || component.observers == null)
			{
				return false;
			}
			int count = component.observers.Count;
			for (int i = 0; i < count; i++)
			{
				NetworkConnection networkConnection = component.observers[i];
				flag &= networkConnection.Send(msgType, msg);
			}
			return flag;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000133B8 File Offset: 0x000115B8
		public static bool SendToReady(GameObject contextObj, short msgType, MessageBase msg)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendToReady id:" + msgType);
			}
			if (contextObj == null)
			{
				for (int i = 0; i < NetworkServer.connections.Count; i++)
				{
					NetworkConnection networkConnection = NetworkServer.connections[i];
					if (networkConnection != null && networkConnection.isReady)
					{
						networkConnection.Send(msgType, msg);
					}
				}
				return true;
			}
			bool flag = true;
			NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
			if (component == null || component.observers == null)
			{
				return false;
			}
			int count = component.observers.Count;
			for (int j = 0; j < count; j++)
			{
				NetworkConnection networkConnection2 = component.observers[j];
				if (networkConnection2.isReady)
				{
					flag &= networkConnection2.Send(msgType, msg);
				}
			}
			return flag;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000134A8 File Offset: 0x000116A8
		public static void SendWriterToReady(GameObject contextObj, NetworkWriter writer, int channelId)
		{
			if (writer.AsArraySegment().Count > 32767)
			{
				throw new UnityException("NetworkWriter used buffer is too big!");
			}
			NetworkServer.SendBytesToReady(contextObj, writer.AsArraySegment().Array, writer.AsArraySegment().Count, channelId);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000134FC File Offset: 0x000116FC
		public static void SendBytesToReady(GameObject contextObj, byte[] buffer, int numBytes, int channelId)
		{
			if (contextObj == null)
			{
				bool flag = true;
				for (int i = 0; i < NetworkServer.connections.Count; i++)
				{
					NetworkConnection networkConnection = NetworkServer.connections[i];
					if (networkConnection != null && networkConnection.isReady && !networkConnection.SendBytes(buffer, numBytes, channelId))
					{
						flag = false;
					}
				}
				if (!flag && LogFilter.logWarn)
				{
					Debug.LogWarning("SendBytesToReady failed");
				}
				return;
			}
			NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
			try
			{
				bool flag2 = true;
				int count = component.observers.Count;
				for (int j = 0; j < count; j++)
				{
					NetworkConnection networkConnection2 = component.observers[j];
					if (networkConnection2.isReady)
					{
						if (!networkConnection2.SendBytes(buffer, numBytes, channelId))
						{
							flag2 = false;
						}
					}
				}
				if (!flag2 && LogFilter.logWarn)
				{
					Debug.LogWarning("SendBytesToReady failed for " + contextObj);
				}
			}
			catch (NullReferenceException)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("SendBytesToReady object " + contextObj + " has not been spawned");
				}
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013644 File Offset: 0x00011844
		public static void SendBytesToPlayer(GameObject player, byte[] buffer, int numBytes, int channelId)
		{
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					for (int j = 0; j < networkConnection.playerControllers.Count; j++)
					{
						if (networkConnection.playerControllers[j].IsValid && networkConnection.playerControllers[j].gameObject == player)
						{
							networkConnection.SendBytes(buffer, numBytes, channelId);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000136E0 File Offset: 0x000118E0
		public static bool SendUnreliableToAll(short msgType, MessageBase msg)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendUnreliableToAll msgType:" + msgType);
			}
			bool flag = true;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					flag &= networkConnection.SendUnreliable(msgType, msg);
				}
			}
			return flag;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013748 File Offset: 0x00011948
		public static bool SendUnreliableToReady(GameObject contextObj, short msgType, MessageBase msg)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendUnreliableToReady id:" + msgType);
			}
			if (contextObj == null)
			{
				for (int i = 0; i < NetworkServer.connections.Count; i++)
				{
					NetworkConnection networkConnection = NetworkServer.connections[i];
					if (networkConnection != null && networkConnection.isReady)
					{
						networkConnection.SendUnreliable(msgType, msg);
					}
				}
				return true;
			}
			bool flag = true;
			NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
			int count = component.observers.Count;
			for (int j = 0; j < count; j++)
			{
				NetworkConnection networkConnection2 = component.observers[j];
				if (networkConnection2.isReady)
				{
					flag &= networkConnection2.SendUnreliable(msgType, msg);
				}
			}
			return flag;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00013820 File Offset: 0x00011A20
		public static bool SendByChannelToAll(short msgType, MessageBase msg, int channelId)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendByChannelToAll id:" + msgType);
			}
			bool flag = true;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					flag &= networkConnection.SendByChannel(msgType, msg, channelId);
				}
			}
			return flag;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013888 File Offset: 0x00011A88
		public static bool SendByChannelToReady(GameObject contextObj, short msgType, MessageBase msg, int channelId)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Server.SendByChannelToReady msgType:" + msgType);
			}
			if (contextObj == null)
			{
				for (int i = 0; i < NetworkServer.connections.Count; i++)
				{
					NetworkConnection networkConnection = NetworkServer.connections[i];
					if (networkConnection != null && networkConnection.isReady)
					{
						networkConnection.SendByChannel(msgType, msg, channelId);
					}
				}
				return true;
			}
			bool flag = true;
			NetworkIdentity component = contextObj.GetComponent<NetworkIdentity>();
			int count = component.observers.Count;
			for (int j = 0; j < count; j++)
			{
				NetworkConnection networkConnection2 = component.observers[j];
				if (networkConnection2.isReady)
				{
					flag &= networkConnection2.SendByChannel(msgType, msg, channelId);
				}
			}
			return flag;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013960 File Offset: 0x00011B60
		public static void DisconnectAll()
		{
			NetworkServer.instance.InternalDisconnectAll();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001396C File Offset: 0x00011B6C
		internal void InternalDisconnectAll()
		{
			this.m_SimpleServerSimple.DisconnectAllConnections();
			if (this.m_LocalConnection != null)
			{
				this.m_LocalConnection.Disconnect();
				this.m_LocalConnection.Dispose();
				this.m_LocalConnection = null;
			}
			NetworkServer.s_Active = false;
			this.m_LocalClientActive = false;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000139BC File Offset: 0x00011BBC
		internal static void Update()
		{
			if (NetworkServer.s_Instance != null)
			{
				NetworkServer.s_Instance.InternalUpdate();
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000139D8 File Offset: 0x00011BD8
		private void UpdateServerObjects()
		{
			foreach (NetworkIdentity networkIdentity in NetworkServer.objects.Values)
			{
				try
				{
					networkIdentity.UNetUpdate();
				}
				catch (NullReferenceException)
				{
				}
				catch (MissingReferenceException)
				{
				}
			}
			if (this.m_RemoveListCount++ % 100 == 0)
			{
				this.CheckForNullObjects();
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013AA4 File Offset: 0x00011CA4
		private void CheckForNullObjects()
		{
			foreach (NetworkInstanceId networkInstanceId in NetworkServer.objects.Keys)
			{
				NetworkIdentity networkIdentity = NetworkServer.objects[networkInstanceId];
				if (networkIdentity == null || networkIdentity.gameObject == null)
				{
					this.m_RemoveList.Add(networkInstanceId);
				}
			}
			if (this.m_RemoveList.Count > 0)
			{
				foreach (NetworkInstanceId key in this.m_RemoveList)
				{
					NetworkServer.objects.Remove(key);
				}
				this.m_RemoveList.Clear();
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00013BB8 File Offset: 0x00011DB8
		internal void InternalUpdate()
		{
			this.m_SimpleServerSimple.Update();
			if (NetworkServer.m_DontListen)
			{
				this.m_SimpleServerSimple.UpdateConnections();
			}
			this.UpdateServerObjects();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013BEC File Offset: 0x00011DEC
		private void OnConnected(NetworkConnection conn)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("Server accepted client:" + conn.connectionId);
			}
			conn.SetMaxDelay(this.m_MaxDelay);
			conn.InvokeHandlerNoData(32);
			NetworkServer.SendCrc(conn);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00013C38 File Offset: 0x00011E38
		private void OnDisconnected(NetworkConnection conn)
		{
			conn.InvokeHandlerNoData(33);
			for (int i = 0; i < conn.playerControllers.Count; i++)
			{
				if (conn.playerControllers[i].gameObject != null && LogFilter.logWarn)
				{
					Debug.LogWarning("Player not destroyed when connection disconnected.");
				}
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("Server lost client:" + conn.connectionId);
			}
			conn.RemoveObservers();
			conn.Dispose();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013CCC File Offset: 0x00011ECC
		private void OnData(NetworkConnection conn, int receivedSize, int channelId)
		{
			conn.TransportRecieve(this.m_SimpleServerSimple.messageBuffer, receivedSize, channelId);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00013CE4 File Offset: 0x00011EE4
		private void GenerateConnectError(int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError("UNet Server Connect Error: " + error);
			}
			this.GenerateError(null, error);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013D10 File Offset: 0x00011F10
		private void GenerateDataError(NetworkConnection conn, int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError("UNet Server Data Error: " + (NetworkError)error);
			}
			this.GenerateError(conn, error);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00013D48 File Offset: 0x00011F48
		private void GenerateDisconnectError(NetworkConnection conn, int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"UNet Server Disconnect Error: ",
					(NetworkError)error,
					" conn:[",
					conn,
					"]:",
					conn.connectionId
				}));
			}
			this.GenerateError(conn, error);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00013DAC File Offset: 0x00011FAC
		private void GenerateError(NetworkConnection conn, int error)
		{
			if (NetworkServer.handlers.ContainsKey(34))
			{
				ErrorMessage errorMessage = new ErrorMessage();
				errorMessage.errorCode = error;
				NetworkWriter writer = new NetworkWriter();
				errorMessage.Serialize(writer);
				NetworkReader reader = new NetworkReader(writer);
				conn.InvokeHandler(34, reader, 0);
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public static void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			NetworkServer.instance.m_SimpleServerSimple.RegisterHandler(msgType, handler);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013E0C File Offset: 0x0001200C
		public static void UnregisterHandler(short msgType)
		{
			NetworkServer.instance.m_SimpleServerSimple.UnregisterHandler(msgType);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013E20 File Offset: 0x00012020
		public static void ClearHandlers()
		{
			NetworkServer.instance.m_SimpleServerSimple.ClearHandlers();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00013E34 File Offset: 0x00012034
		public static void ClearSpawners()
		{
			NetworkScene.ClearSpawners();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00013E3C File Offset: 0x0001203C
		public static void GetStatsOut(out int numMsgs, out int numBufferedMsgs, out int numBytes, out int lastBufferedPerSecond)
		{
			numMsgs = 0;
			numBufferedMsgs = 0;
			numBytes = 0;
			lastBufferedPerSecond = 0;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					int num;
					int num2;
					int num3;
					int num4;
					networkConnection.GetStatsOut(out num, out num2, out num3, out num4);
					numMsgs += num;
					numBufferedMsgs += num2;
					numBytes += num3;
					lastBufferedPerSecond += num4;
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013EAC File Offset: 0x000120AC
		public static void GetStatsIn(out int numMsgs, out int numBytes)
		{
			numMsgs = 0;
			numBytes = 0;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					int num;
					int num2;
					networkConnection.GetStatsIn(out num, out num2);
					numMsgs += num;
					numBytes += num2;
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013F04 File Offset: 0x00012104
		public static void SendToClientOfPlayer(GameObject player, short msgType, MessageBase msg)
		{
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					for (int j = 0; j < networkConnection.playerControllers.Count; j++)
					{
						if (networkConnection.playerControllers[j].IsValid && networkConnection.playerControllers[j].gameObject == player)
						{
							networkConnection.Send(msgType, msg);
							return;
						}
					}
				}
			}
			if (LogFilter.logError)
			{
				Debug.LogError("Failed to send message to player object '" + player.name + ", not found in connection list");
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013FBC File Offset: 0x000121BC
		public static void SendToClient(int connectionId, short msgType, MessageBase msg)
		{
			if (connectionId < NetworkServer.connections.Count)
			{
				NetworkConnection networkConnection = NetworkServer.connections[connectionId];
				if (networkConnection != null)
				{
					networkConnection.Send(msgType, msg);
					return;
				}
			}
			if (LogFilter.logError)
			{
				Debug.LogError("Failed to send message to connection ID '" + connectionId + ", not found in connection list");
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001401C File Offset: 0x0001221C
		public static bool ReplacePlayerForConnection(NetworkConnection conn, GameObject player, short playerControllerId, NetworkHash128 assetId)
		{
			NetworkIdentity networkIdentity;
			if (NetworkServer.GetNetworkIdentity(player, out networkIdentity))
			{
				networkIdentity.SetDynamicAssetId(assetId);
			}
			return NetworkServer.instance.InternalReplacePlayerForConnection(conn, player, playerControllerId);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001404C File Offset: 0x0001224C
		public static bool ReplacePlayerForConnection(NetworkConnection conn, GameObject player, short playerControllerId)
		{
			return NetworkServer.instance.InternalReplacePlayerForConnection(conn, player, playerControllerId);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001405C File Offset: 0x0001225C
		public static bool AddPlayerForConnection(NetworkConnection conn, GameObject player, short playerControllerId, NetworkHash128 assetId)
		{
			NetworkIdentity networkIdentity;
			if (NetworkServer.GetNetworkIdentity(player, out networkIdentity))
			{
				networkIdentity.SetDynamicAssetId(assetId);
			}
			return NetworkServer.instance.InternalAddPlayerForConnection(conn, player, playerControllerId);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001408C File Offset: 0x0001228C
		public static bool AddPlayerForConnection(NetworkConnection conn, GameObject player, short playerControllerId)
		{
			return NetworkServer.instance.InternalAddPlayerForConnection(conn, player, playerControllerId);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001409C File Offset: 0x0001229C
		internal bool InternalAddPlayerForConnection(NetworkConnection conn, GameObject playerGameObject, short playerControllerId)
		{
			NetworkIdentity networkIdentity;
			if (!NetworkServer.GetNetworkIdentity(playerGameObject, out networkIdentity))
			{
				if (LogFilter.logError)
				{
					Debug.Log("AddPlayer: playerGameObject has no NetworkIdentity. Please add a NetworkIdentity to " + playerGameObject);
				}
				return false;
			}
			if (!NetworkServer.CheckPlayerControllerIdForConnection(conn, playerControllerId))
			{
				return false;
			}
			PlayerController playerController = null;
			GameObject x = null;
			if (conn.GetPlayerController(playerControllerId, out playerController))
			{
				x = playerController.gameObject;
			}
			if (x != null)
			{
				if (LogFilter.logError)
				{
					Debug.Log("AddPlayer: player object already exists for playerControllerId of " + playerControllerId);
				}
				return false;
			}
			PlayerController playerController2 = new PlayerController(playerGameObject, playerControllerId);
			conn.SetPlayerController(playerController2);
			networkIdentity.SetConnectionToClient(conn, playerController2.playerControllerId);
			NetworkServer.SetClientReady(conn);
			if (this.SetupLocalPlayerForConnection(conn, networkIdentity, playerController2))
			{
				return true;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Adding new playerGameObject object netId: ",
					playerGameObject.GetComponent<NetworkIdentity>().netId,
					" asset ID ",
					playerGameObject.GetComponent<NetworkIdentity>().assetId
				}));
			}
			NetworkServer.FinishPlayerForConnection(conn, networkIdentity, playerGameObject);
			if (networkIdentity.localPlayerAuthority)
			{
				networkIdentity.SetClientOwner(conn);
			}
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000141C4 File Offset: 0x000123C4
		private static bool CheckPlayerControllerIdForConnection(NetworkConnection conn, short playerControllerId)
		{
			if (playerControllerId < 0)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AddPlayer: playerControllerId of " + playerControllerId + " is negative");
				}
				return false;
			}
			if (playerControllerId > 32)
			{
				if (LogFilter.logError)
				{
					Debug.Log(string.Concat(new object[]
					{
						"AddPlayer: playerControllerId of ",
						playerControllerId,
						" is too high. max is ",
						32
					}));
				}
				return false;
			}
			if (playerControllerId > 16 && LogFilter.logWarn)
			{
				Debug.LogWarning("AddPlayer: playerControllerId of " + playerControllerId + " is unusually high");
			}
			return true;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014274 File Offset: 0x00012474
		private bool SetupLocalPlayerForConnection(NetworkConnection conn, NetworkIdentity uv, PlayerController newPlayerController)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkServer SetupLocalPlayerForConnection netID:" + uv.netId);
			}
			ULocalConnectionToClient ulocalConnectionToClient = conn as ULocalConnectionToClient;
			if (ulocalConnectionToClient != null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkServer AddPlayer handling ULocalConnectionToClient");
				}
				if (uv.netId.IsEmpty())
				{
					uv.OnStartServer(true);
				}
				uv.RebuildObservers(true);
				this.SendSpawnMessage(uv, null);
				ulocalConnectionToClient.localClient.AddLocalPlayer(newPlayerController);
				uv.SetClientOwner(conn);
				uv.ForceAuthority(true);
				uv.SetLocalPlayer(newPlayerController.playerControllerId);
				return true;
			}
			return false;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00014318 File Offset: 0x00012518
		private static void FinishPlayerForConnection(NetworkConnection conn, NetworkIdentity uv, GameObject playerGameObject)
		{
			if (uv.netId.IsEmpty())
			{
				NetworkServer.Spawn(playerGameObject);
			}
			conn.Send(4, new OwnerMessage
			{
				netId = uv.netId,
				playerControllerId = uv.playerControllerId
			});
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00014368 File Offset: 0x00012568
		internal bool InternalReplacePlayerForConnection(NetworkConnection conn, GameObject playerGameObject, short playerControllerId)
		{
			NetworkIdentity networkIdentity;
			if (!NetworkServer.GetNetworkIdentity(playerGameObject, out networkIdentity))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReplacePlayer: playerGameObject has no NetworkIdentity. Please add a NetworkIdentity to " + playerGameObject);
				}
				return false;
			}
			if (!NetworkServer.CheckPlayerControllerIdForConnection(conn, playerControllerId))
			{
				return false;
			}
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkServer ReplacePlayer");
			}
			PlayerController playerController;
			if (conn.GetPlayerController(playerControllerId, out playerController))
			{
				playerController.unetView.SetNotLocalPlayer();
			}
			PlayerController playerController2 = new PlayerController(playerGameObject, playerControllerId);
			conn.SetPlayerController(playerController2);
			networkIdentity.SetConnectionToClient(conn, playerController2.playerControllerId);
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkServer ReplacePlayer setup local");
			}
			if (this.SetupLocalPlayerForConnection(conn, networkIdentity, playerController2))
			{
				return true;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Replacing playerGameObject object netId: ",
					playerGameObject.GetComponent<NetworkIdentity>().netId,
					" asset ID ",
					playerGameObject.GetComponent<NetworkIdentity>().assetId
				}));
			}
			NetworkServer.FinishPlayerForConnection(conn, networkIdentity, playerGameObject);
			if (networkIdentity.localPlayerAuthority)
			{
				networkIdentity.SetClientOwner(conn);
			}
			return true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00014484 File Offset: 0x00012684
		private static bool GetNetworkIdentity(GameObject go, out NetworkIdentity view)
		{
			view = go.GetComponent<NetworkIdentity>();
			if (view == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("UNET failure. GameObject doesn't have NetworkIdentity.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000144C0 File Offset: 0x000126C0
		public static void SetClientReady(NetworkConnection conn)
		{
			NetworkServer.instance.SetClientReadyInternal(conn);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000144D0 File Offset: 0x000126D0
		internal void SetClientReadyInternal(NetworkConnection conn)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("SetClientReadyInternal for conn:" + conn.connectionId);
			}
			if (conn.isReady)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("SetClientReady conn " + conn.connectionId + " already ready");
				}
				return;
			}
			if (conn.playerControllers.Count == 0 && LogFilter.logDebug)
			{
				Debug.LogWarning("Ready with no player object");
			}
			conn.isReady = true;
			ULocalConnectionToClient ulocalConnectionToClient = conn as ULocalConnectionToClient;
			if (ulocalConnectionToClient != null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkServer Ready handling ULocalConnectionToClient");
				}
				foreach (NetworkIdentity networkIdentity in NetworkServer.objects.Values)
				{
					if (networkIdentity != null && networkIdentity.gameObject != null)
					{
						bool flag = networkIdentity.OnCheckObserver(conn);
						if (flag)
						{
							networkIdentity.AddObserver(conn);
						}
						if (!networkIdentity.isClient)
						{
							if (LogFilter.logDev)
							{
								Debug.Log("LocalClient.SetSpawnObject calling OnStartClient");
							}
							networkIdentity.OnStartClient();
						}
					}
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Spawning ",
					NetworkServer.objects.Count,
					" objects for conn ",
					conn.connectionId
				}));
			}
			ObjectSpawnFinishedMessage objectSpawnFinishedMessage = new ObjectSpawnFinishedMessage();
			objectSpawnFinishedMessage.state = 0U;
			conn.Send(12, objectSpawnFinishedMessage);
			foreach (NetworkIdentity networkIdentity2 in NetworkServer.objects.Values)
			{
				if (networkIdentity2 == null)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("Invalid object found in server local object list (null NetworkIdentity).");
					}
				}
				else if (networkIdentity2.gameObject.activeSelf)
				{
					if (LogFilter.logDebug)
					{
						Debug.Log(string.Concat(new object[]
						{
							"Sending spawn message for current server objects name='",
							networkIdentity2.gameObject.name,
							"' netId=",
							networkIdentity2.netId
						}));
					}
					bool flag2 = networkIdentity2.OnCheckObserver(conn);
					if (flag2)
					{
						networkIdentity2.AddObserver(conn);
					}
				}
			}
			objectSpawnFinishedMessage.state = 1U;
			conn.Send(12, objectSpawnFinishedMessage);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001479C File Offset: 0x0001299C
		internal static void ShowForConnection(NetworkIdentity uv, NetworkConnection conn)
		{
			if (conn.isReady)
			{
				NetworkServer.instance.SendSpawnMessage(uv, conn);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000147B8 File Offset: 0x000129B8
		internal static void HideForConnection(NetworkIdentity uv, NetworkConnection conn)
		{
			conn.Send(13, new ObjectDestroyMessage
			{
				netId = uv.netId
			});
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000147E4 File Offset: 0x000129E4
		public static void SetAllClientsNotReady()
		{
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					NetworkServer.SetClientNotReady(networkConnection);
				}
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00014824 File Offset: 0x00012A24
		public static void SetClientNotReady(NetworkConnection conn)
		{
			NetworkServer.instance.InternalSetClientNotReady(conn);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00014834 File Offset: 0x00012A34
		internal void InternalSetClientNotReady(NetworkConnection conn)
		{
			if (conn.isReady)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("PlayerNotReady " + conn);
				}
				conn.isReady = false;
				conn.RemoveObservers();
				NotReadyMessage msg = new NotReadyMessage();
				conn.Send(36, msg);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014884 File Offset: 0x00012A84
		private static void OnClientReadyMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("Default handler for ready message from " + netMsg.conn);
			}
			NetworkServer.SetClientReady(netMsg.conn);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000148BC File Offset: 0x00012ABC
		private static void OnRemovePlayerMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<RemovePlayerMessage>(NetworkServer.s_RemovePlayerMessage);
			PlayerController playerController = null;
			netMsg.conn.GetPlayerController(NetworkServer.s_RemovePlayerMessage.playerControllerId, out playerController);
			if (playerController != null)
			{
				netMsg.conn.RemovePlayerController(NetworkServer.s_RemovePlayerMessage.playerControllerId);
				NetworkServer.Destroy(playerController.gameObject);
			}
			else if (LogFilter.logError)
			{
				Debug.LogError("Received remove player message but could not find the player ID: " + NetworkServer.s_RemovePlayerMessage.playerControllerId);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014944 File Offset: 0x00012B44
		private static void OnCommandMessage(NetworkMessage netMsg)
		{
			int cmdHash = (int)netMsg.reader.ReadPackedUInt32();
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			GameObject gameObject = NetworkServer.FindLocalObject(networkInstanceId);
			if (gameObject == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Instance not found when handling Command message [netId=" + networkInstanceId + "]");
				}
				return;
			}
			NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkIdentity deleted when handling Command message [netId=" + networkInstanceId + "]");
				}
				return;
			}
			bool flag = false;
			foreach (PlayerController playerController in netMsg.conn.playerControllers)
			{
				if (playerController.gameObject != null && playerController.gameObject.GetComponent<NetworkIdentity>().netId == component.netId)
				{
					flag = true;
					break;
				}
			}
			if (!flag && component.clientAuthorityOwner != netMsg.conn)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Command for object without authority [netId=" + networkInstanceId + "]");
				}
				return;
			}
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnCommandMessage for netId=",
					networkInstanceId,
					" conn=",
					netMsg.conn
				}));
			}
			component.HandleCommand(cmdHash, netMsg.reader);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014AF8 File Offset: 0x00012CF8
		internal void SpawnObject(GameObject obj)
		{
			if (!NetworkServer.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("SpawnObject for " + obj + ", NetworkServer is not active. Cannot spawn objects without an active server.");
				}
				return;
			}
			NetworkIdentity networkIdentity;
			if (!NetworkServer.GetNetworkIdentity(obj, out networkIdentity))
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"SpawnObject ",
						obj,
						" has no NetworkIdentity. Please add a NetworkIdentity to ",
						obj
					}));
				}
				return;
			}
			networkIdentity.OnStartServer(false);
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"SpawnObject instance ID ",
					networkIdentity.netId,
					" asset ID ",
					networkIdentity.assetId
				}));
			}
			networkIdentity.RebuildObservers(true);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014BC4 File Offset: 0x00012DC4
		internal void SendSpawnMessage(NetworkIdentity uv, NetworkConnection conn)
		{
			if (uv.serverOnly)
			{
				return;
			}
			if (uv.sceneId.IsEmpty())
			{
				ObjectSpawnMessage objectSpawnMessage = new ObjectSpawnMessage();
				objectSpawnMessage.netId = uv.netId;
				objectSpawnMessage.assetId = uv.assetId;
				objectSpawnMessage.position = uv.transform.position;
				NetworkWriter networkWriter = new NetworkWriter();
				uv.UNetSerializeAllVars(networkWriter);
				if (networkWriter.Position > 0)
				{
					objectSpawnMessage.payload = networkWriter.ToArray();
				}
				if (conn != null)
				{
					conn.Send(3, objectSpawnMessage);
				}
				else
				{
					NetworkServer.SendToReady(uv.gameObject, 3, objectSpawnMessage);
				}
			}
			else
			{
				ObjectSpawnSceneMessage objectSpawnSceneMessage = new ObjectSpawnSceneMessage();
				objectSpawnSceneMessage.netId = uv.netId;
				objectSpawnSceneMessage.sceneId = uv.sceneId;
				objectSpawnSceneMessage.position = uv.transform.position;
				NetworkWriter networkWriter2 = new NetworkWriter();
				uv.UNetSerializeAllVars(networkWriter2);
				if (networkWriter2.Position > 0)
				{
					objectSpawnSceneMessage.payload = networkWriter2.ToArray();
				}
				if (conn != null)
				{
					conn.Send(10, objectSpawnSceneMessage);
				}
				else
				{
					NetworkServer.SendToReady(uv.gameObject, 3, objectSpawnSceneMessage);
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014CE4 File Offset: 0x00012EE4
		public static void DestroyPlayersForConnection(NetworkConnection conn)
		{
			if (conn.playerControllers.Count == 0)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Empty player list given to NetworkServer.Destroy(), nothing to do.");
				}
				return;
			}
			if (conn.clientOwnedObjects != null)
			{
				HashSet<NetworkInstanceId> hashSet = new HashSet<NetworkInstanceId>(conn.clientOwnedObjects);
				foreach (NetworkInstanceId netId in hashSet)
				{
					GameObject gameObject = NetworkServer.FindLocalObject(netId);
					if (gameObject != null)
					{
						NetworkServer.DestroyObject(gameObject);
					}
				}
			}
			foreach (PlayerController playerController in conn.playerControllers)
			{
				if (playerController.IsValid)
				{
					if (!(playerController.unetView == null))
					{
						NetworkServer.DestroyObject(playerController.unetView, true);
					}
					playerController.gameObject = null;
				}
			}
			conn.playerControllers.Clear();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00014E28 File Offset: 0x00013028
		private static void UnSpawnObject(GameObject obj)
		{
			if (obj == null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkServer UnspawnObject is null");
				}
				return;
			}
			NetworkIdentity uv;
			if (!NetworkServer.GetNetworkIdentity(obj, out uv))
			{
				return;
			}
			NetworkServer.UnSpawnObject(uv);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014E6C File Offset: 0x0001306C
		private static void UnSpawnObject(NetworkIdentity uv)
		{
			NetworkServer.DestroyObject(uv, false);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014E78 File Offset: 0x00013078
		private static void DestroyObject(GameObject obj)
		{
			if (obj == null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkServer DestroyObject is null");
				}
				return;
			}
			NetworkIdentity uv;
			if (!NetworkServer.GetNetworkIdentity(obj, out uv))
			{
				return;
			}
			NetworkServer.DestroyObject(uv, true);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014EBC File Offset: 0x000130BC
		private static void DestroyObject(NetworkIdentity uv, bool destroyServerObject)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("DestroyObject instance:" + uv.netId);
			}
			if (NetworkServer.objects.ContainsKey(uv.netId))
			{
				NetworkServer.objects.Remove(uv.netId);
			}
			if (uv.clientAuthorityOwner != null)
			{
				uv.clientAuthorityOwner.RemoveOwnedObject(uv);
			}
			ObjectDestroyMessage objectDestroyMessage = new ObjectDestroyMessage();
			objectDestroyMessage.netId = uv.netId;
			NetworkServer.SendToObservers(uv.gameObject, 1, objectDestroyMessage);
			uv.ClearObservers();
			if (NetworkClient.active && NetworkServer.instance.m_LocalClientActive)
			{
				uv.OnNetworkDestroy();
				ClientScene.SetLocalObject(objectDestroyMessage.netId, null);
			}
			if (destroyServerObject)
			{
				Object.Destroy(uv.gameObject);
			}
			uv.SetNoServer();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014F94 File Offset: 0x00013194
		public static void ClearLocalObjects()
		{
			NetworkServer.objects.Clear();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00014FA0 File Offset: 0x000131A0
		public static void Spawn(GameObject obj)
		{
			NetworkServer.instance.SpawnObject(obj);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014FB0 File Offset: 0x000131B0
		public static bool SpawnWithClientAuthority(GameObject obj, GameObject player)
		{
			NetworkIdentity component = player.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				Debug.LogError("SpawnWithClientAuthority player object has no NetworkIdentity");
				return false;
			}
			if (component.connectionToClient == null)
			{
				Debug.LogError("SpawnWithClientAuthority player object is not a player.");
				return false;
			}
			return NetworkServer.SpawnWithClientAuthority(obj, component.connectionToClient);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00015000 File Offset: 0x00013200
		public static bool SpawnWithClientAuthority(GameObject obj, NetworkConnection conn)
		{
			NetworkServer.Spawn(obj);
			NetworkIdentity component = obj.GetComponent<NetworkIdentity>();
			return !(component == null) && component.isServer && component.AssignClientAuthority(conn);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001503C File Offset: 0x0001323C
		public static bool SpawnWithClientAuthority(GameObject obj, NetworkHash128 assetId, NetworkConnection conn)
		{
			NetworkServer.Spawn(obj, assetId);
			NetworkIdentity component = obj.GetComponent<NetworkIdentity>();
			return !(component == null) && component.isServer && component.AssignClientAuthority(conn);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015078 File Offset: 0x00013278
		public static void Spawn(GameObject obj, NetworkHash128 assetId)
		{
			NetworkIdentity networkIdentity;
			if (NetworkServer.GetNetworkIdentity(obj, out networkIdentity))
			{
				networkIdentity.SetDynamicAssetId(assetId);
			}
			NetworkServer.instance.SpawnObject(obj);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000150A4 File Offset: 0x000132A4
		public static void Destroy(GameObject obj)
		{
			NetworkServer.DestroyObject(obj);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000150AC File Offset: 0x000132AC
		public static void UnSpawn(GameObject obj)
		{
			NetworkServer.UnSpawnObject(obj);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000150B4 File Offset: 0x000132B4
		internal bool InvokeBytes(ULocalConnectionToServer conn, byte[] buffer, int numBytes, int channelId)
		{
			NetworkReader networkReader = new NetworkReader(buffer);
			networkReader.ReadInt16();
			short num = networkReader.ReadInt16();
			if (NetworkServer.handlers.ContainsKey(num) && this.m_LocalConnection != null)
			{
				this.m_LocalConnection.InvokeHandler(num, networkReader, channelId);
				return true;
			}
			return false;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015104 File Offset: 0x00013304
		internal bool InvokeHandlerOnServer(ULocalConnectionToServer conn, short msgType, MessageBase msg, int channelId)
		{
			if (NetworkServer.handlers.ContainsKey(msgType) && this.m_LocalConnection != null)
			{
				NetworkWriter writer = new NetworkWriter();
				msg.Serialize(writer);
				NetworkReader reader = new NetworkReader(writer);
				this.m_LocalConnection.InvokeHandler(msgType, reader, channelId);
				return true;
			}
			if (LogFilter.logError)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Local invoke: Failed to find local connection to invoke handler on [connectionId=",
					conn.connectionId,
					"] for MsgId:",
					msgType
				}));
			}
			return false;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00015194 File Offset: 0x00013394
		public static GameObject FindLocalObject(NetworkInstanceId netId)
		{
			return NetworkServer.instance.m_NetworkScene.FindLocalObject(netId);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000151A8 File Offset: 0x000133A8
		public static Dictionary<short, NetworkConnection.PacketStat> GetConnectionStats()
		{
			Dictionary<short, NetworkConnection.PacketStat> dictionary = new Dictionary<short, NetworkConnection.PacketStat>();
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					foreach (short key in networkConnection.packetStats.Keys)
					{
						if (dictionary.ContainsKey(key))
						{
							NetworkConnection.PacketStat packetStat = dictionary[key];
							packetStat.count += networkConnection.packetStats[key].count;
							packetStat.bytes += networkConnection.packetStats[key].bytes;
							dictionary[key] = packetStat;
						}
						else
						{
							dictionary[key] = networkConnection.packetStats[key];
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000152B8 File Offset: 0x000134B8
		public static void ResetConnectionStats()
		{
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					networkConnection.ResetStats();
				}
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000152F8 File Offset: 0x000134F8
		public static bool AddExternalConnection(NetworkConnection conn)
		{
			return NetworkServer.instance.AddExternalConnectionInternal(conn);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015308 File Offset: 0x00013508
		private bool AddExternalConnectionInternal(NetworkConnection conn)
		{
			if (conn.connectionId < 0)
			{
				return false;
			}
			if (conn.connectionId < NetworkServer.connections.Count && NetworkServer.connections[conn.connectionId] != null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AddExternalConnection failed, already connection for id:" + conn.connectionId);
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("AddExternalConnection external connection " + conn.connectionId);
			}
			this.m_SimpleServerSimple.SetConnectionAtIndex(conn);
			this.m_ExternalConnections.Add(conn.connectionId);
			conn.InvokeHandlerNoData(32);
			return true;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000153C0 File Offset: 0x000135C0
		public static void RemoveExternalConnection(int connectionId)
		{
			NetworkServer.instance.RemoveExternalConnectionInternal(connectionId);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000153D0 File Offset: 0x000135D0
		private bool RemoveExternalConnectionInternal(int connectionId)
		{
			if (!this.m_ExternalConnections.Contains(connectionId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RemoveExternalConnection failed, no connection for id:" + connectionId);
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("RemoveExternalConnection external connection " + connectionId);
			}
			NetworkConnection networkConnection = this.m_SimpleServerSimple.FindConnection(connectionId);
			if (networkConnection != null)
			{
				networkConnection.RemoveObservers();
			}
			this.m_SimpleServerSimple.RemoveConnectionAtIndex(connectionId);
			return true;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015458 File Offset: 0x00013658
		public static bool SpawnObjects()
		{
			if (NetworkServer.active)
			{
				NetworkIdentity[] array = Resources.FindObjectsOfTypeAll<NetworkIdentity>();
				foreach (NetworkIdentity networkIdentity in array)
				{
					if (networkIdentity.gameObject.hideFlags != HideFlags.NotEditable && networkIdentity.gameObject.hideFlags != HideFlags.HideAndDontSave)
					{
						if (!networkIdentity.sceneId.IsEmpty())
						{
							if (LogFilter.logDebug)
							{
								Debug.Log(string.Concat(new object[]
								{
									"SpawnObjects sceneId:",
									networkIdentity.sceneId,
									" name:",
									networkIdentity.gameObject.name
								}));
							}
							networkIdentity.gameObject.SetActive(true);
						}
					}
				}
				foreach (NetworkIdentity networkIdentity2 in array)
				{
					if (networkIdentity2.gameObject.hideFlags != HideFlags.NotEditable && networkIdentity2.gameObject.hideFlags != HideFlags.HideAndDontSave)
					{
						if (!networkIdentity2.sceneId.IsEmpty())
						{
							if (!networkIdentity2.isServer)
							{
								if (!(networkIdentity2.gameObject == null))
								{
									NetworkServer.Spawn(networkIdentity2.gameObject);
									networkIdentity2.ForceAuthority(true);
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000155C8 File Offset: 0x000137C8
		private static void SendCrc(NetworkConnection targetConnection)
		{
			if (NetworkCRC.singleton == null)
			{
				return;
			}
			if (!NetworkCRC.scriptCRCCheck)
			{
				return;
			}
			CRCMessage crcmessage = new CRCMessage();
			List<CRCMessageEntry> list = new List<CRCMessageEntry>();
			foreach (string text in NetworkCRC.singleton.scripts.Keys)
			{
				list.Add(new CRCMessageEntry
				{
					name = text,
					channel = (byte)NetworkCRC.singleton.scripts[text]
				});
			}
			crcmessage.scripts = list.ToArray();
			targetConnection.Send(14, crcmessage);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00015698 File Offset: 0x00013898
		[Obsolete("moved to NetworkMigrationManager")]
		public void SendNetworkInfo(NetworkConnection targetConnection)
		{
		}

		// Token: 0x04000191 RID: 401
		private const int k_RemoveListInterval = 100;

		// Token: 0x04000192 RID: 402
		private static bool s_Active;

		// Token: 0x04000193 RID: 403
		private static volatile NetworkServer s_Instance;

		// Token: 0x04000194 RID: 404
		private static object s_Sync = new Object();

		// Token: 0x04000195 RID: 405
		private static bool m_DontListen;

		// Token: 0x04000196 RID: 406
		private bool m_LocalClientActive;

		// Token: 0x04000197 RID: 407
		private List<NetworkConnection> m_LocalConnectionsFakeList = new List<NetworkConnection>();

		// Token: 0x04000198 RID: 408
		private ULocalConnectionToClient m_LocalConnection;

		// Token: 0x04000199 RID: 409
		private NetworkScene m_NetworkScene;

		// Token: 0x0400019A RID: 410
		private HashSet<int> m_ExternalConnections;

		// Token: 0x0400019B RID: 411
		private NetworkServer.ServerSimpleWrapper m_SimpleServerSimple;

		// Token: 0x0400019C RID: 412
		private float m_MaxDelay = 0.1f;

		// Token: 0x0400019D RID: 413
		private HashSet<NetworkInstanceId> m_RemoveList;

		// Token: 0x0400019E RID: 414
		private int m_RemoveListCount;

		// Token: 0x0400019F RID: 415
		internal static ushort maxPacketSize;

		// Token: 0x040001A0 RID: 416
		private static RemovePlayerMessage s_RemovePlayerMessage = new RemovePlayerMessage();

		// Token: 0x02000053 RID: 83
		private class ServerSimpleWrapper : NetworkServerSimple
		{
			// Token: 0x06000407 RID: 1031 RVA: 0x0001569C File Offset: 0x0001389C
			public ServerSimpleWrapper(NetworkServer server)
			{
				this.m_Server = server;
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x000156AC File Offset: 0x000138AC
			public override void OnConnectError(int connectionId, byte error)
			{
				this.m_Server.GenerateConnectError((int)error);
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x000156BC File Offset: 0x000138BC
			public override void OnDataError(NetworkConnection conn, byte error)
			{
				this.m_Server.GenerateDataError(conn, (int)error);
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x000156CC File Offset: 0x000138CC
			public override void OnConnected(NetworkConnection conn)
			{
				this.m_Server.OnConnected(conn);
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x000156DC File Offset: 0x000138DC
			public override void OnDisconnected(NetworkConnection conn)
			{
				this.m_Server.OnDisconnected(conn);
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x000156EC File Offset: 0x000138EC
			public override void OnData(NetworkConnection conn, int receivedSize, int channelId)
			{
				this.m_Server.OnData(conn, receivedSize, channelId);
			}

			// Token: 0x040001A1 RID: 417
			private NetworkServer m_Server;
		}
	}
}
