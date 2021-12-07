using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;

namespace UnityEngine.Networking
{
	// Token: 0x02000038 RID: 56
	public class NetworkClient
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00007120 File Offset: 0x00005320
		public NetworkClient()
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Client created version " + Version.Current);
			}
			this.m_MsgBuffer = new byte[65535];
			this.m_MsgReader = new NetworkReader(this.m_MsgBuffer);
			NetworkClient.AddClient(this);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000071B8 File Offset: 0x000053B8
		public NetworkClient(NetworkConnection conn)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("Client created version " + Version.Current);
			}
			this.m_MsgBuffer = new byte[65535];
			this.m_MsgReader = new NetworkReader(this.m_MsgBuffer);
			NetworkClient.AddClient(this);
			NetworkClient.SetActive(true);
			this.m_Connection = conn;
			this.m_AsyncConnect = NetworkClient.ConnectState.Connected;
			conn.SetHandlers(this.m_MessageHandlers);
			this.RegisterSystemHandlers(false);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007290 File Offset: 0x00005490
		public static List<NetworkClient> allClients
		{
			get
			{
				return NetworkClient.s_Clients;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007298 File Offset: 0x00005498
		public static bool active
		{
			get
			{
				return NetworkClient.s_IsActive;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000072A0 File Offset: 0x000054A0
		internal void SetHandlers(NetworkConnection conn)
		{
			conn.SetHandlers(this.m_MessageHandlers);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000072B0 File Offset: 0x000054B0
		public string serverIp
		{
			get
			{
				return this.m_ServerIp;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000072B8 File Offset: 0x000054B8
		public int serverPort
		{
			get
			{
				return this.m_ServerPort;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000072C0 File Offset: 0x000054C0
		public NetworkConnection connection
		{
			get
			{
				return this.m_Connection;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000072C8 File Offset: 0x000054C8
		[Obsolete("Moved to NetworkMigrationManager.")]
		public PeerInfoMessage[] peers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000072CC File Offset: 0x000054CC
		internal int hostId
		{
			get
			{
				return this.m_ClientId;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000072D4 File Offset: 0x000054D4
		public Dictionary<short, NetworkMessageDelegate> handlers
		{
			get
			{
				return this.m_MessageHandlers.GetHandlers();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000072E4 File Offset: 0x000054E4
		public int numChannels
		{
			get
			{
				return this.m_HostTopology.DefaultConfig.ChannelCount;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000072F8 File Offset: 0x000054F8
		public HostTopology hostTopology
		{
			get
			{
				return this.m_HostTopology;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007300 File Offset: 0x00005500
		public bool isConnected
		{
			get
			{
				return this.m_AsyncConnect == NetworkClient.ConnectState.Connected;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000730C File Offset: 0x0000550C
		public Type networkConnectionClass
		{
			get
			{
				return this.m_NetworkConnectionClass;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007314 File Offset: 0x00005514
		public void SetNetworkConnectionClass<T>() where T : NetworkConnection
		{
			this.m_NetworkConnectionClass = typeof(T);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007328 File Offset: 0x00005528
		public bool Configure(ConnectionConfig config, int maxConnections)
		{
			HostTopology topology = new HostTopology(config, maxConnections);
			return this.Configure(topology);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007344 File Offset: 0x00005544
		public bool Configure(HostTopology topology)
		{
			this.m_HostTopology = topology;
			return true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007350 File Offset: 0x00005550
		public void Connect(MatchInfo matchInfo)
		{
			this.PrepareForConnect();
			this.ConnectWithRelay(matchInfo);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007360 File Offset: 0x00005560
		public bool ReconnectToNewHost(string serverIp, int serverPort)
		{
			if (!NetworkClient.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Reconnect - NetworkClient must be active");
				}
				return false;
			}
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Reconnect - no old connection exists");
				}
				return false;
			}
			if (LogFilter.logInfo)
			{
				Debug.Log(string.Concat(new object[]
				{
					"NetworkClient Reconnect ",
					serverIp,
					":",
					serverPort
				}));
			}
			ClientScene.HandleClientDisconnect(this.m_Connection);
			ClientScene.ClearLocalPlayers();
			this.m_Connection.Disconnect();
			this.m_Connection = null;
			this.m_ClientId = NetworkTransport.AddHost(this.m_HostTopology, 0);
			this.m_ServerPort = serverPort;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				this.m_ServerIp = serverIp;
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
			}
			else if (serverIp.Equals("127.0.0.1") || serverIp.Equals("localhost"))
			{
				this.m_ServerIp = "127.0.0.1";
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
			}
			else
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("Async DNS START:" + serverIp);
				}
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolving;
				Dns.BeginGetHostAddresses(serverIp, new AsyncCallback(NetworkClient.GetHostAddressesCallback), this);
			}
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000074B0 File Offset: 0x000056B0
		public void ConnectWithSimulator(string serverIp, int serverPort, int latency, float packetLoss)
		{
			this.m_UseSimulator = true;
			this.m_SimulatedLatency = latency;
			this.m_PacketLoss = packetLoss;
			this.Connect(serverIp, serverPort);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000074D0 File Offset: 0x000056D0
		private static bool IsValidIpV6(string address)
		{
			foreach (char c in address)
			{
				if (c != ':' && (c < '0' || c > '9') && (c < 'a' || c > 'f') && (c < 'A' || c > 'F'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007540 File Offset: 0x00005740
		public void Connect(string serverIp, int serverPort)
		{
			this.PrepareForConnect();
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Client Connect: ",
					serverIp,
					":",
					serverPort
				}));
			}
			this.m_ServerPort = serverPort;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				this.m_ServerIp = serverIp;
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
			}
			else if (serverIp.Equals("127.0.0.1") || serverIp.Equals("localhost"))
			{
				this.m_ServerIp = "127.0.0.1";
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
			}
			else if (serverIp.IndexOf(":") != -1 && NetworkClient.IsValidIpV6(serverIp))
			{
				this.m_ServerIp = serverIp;
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
			}
			else
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("Async DNS START:" + serverIp);
				}
				this.m_RequestedServerHost = serverIp;
				this.m_AsyncConnect = NetworkClient.ConnectState.Resolving;
				Dns.BeginGetHostAddresses(serverIp, new AsyncCallback(NetworkClient.GetHostAddressesCallback), this);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007654 File Offset: 0x00005854
		public void Connect(EndPoint secureTunnelEndPoint)
		{
			bool usePlatformSpecificProtocols = NetworkTransport.DoesEndPointUsePlatformProtocols(secureTunnelEndPoint);
			this.PrepareForConnect(usePlatformSpecificProtocols);
			if (LogFilter.logDebug)
			{
				Debug.Log("Client Connect to remoteSockAddr");
			}
			if (secureTunnelEndPoint == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Connect failed: null endpoint passed in");
				}
				this.m_AsyncConnect = NetworkClient.ConnectState.Failed;
				return;
			}
			if (secureTunnelEndPoint.AddressFamily != AddressFamily.InterNetwork && secureTunnelEndPoint.AddressFamily != AddressFamily.InterNetworkV6)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Connect failed: Endpoint AddressFamily must be either InterNetwork or InterNetworkV6");
				}
				this.m_AsyncConnect = NetworkClient.ConnectState.Failed;
				return;
			}
			string fullName = secureTunnelEndPoint.GetType().FullName;
			if (fullName == "System.Net.IPEndPoint")
			{
				IPEndPoint ipendPoint = (IPEndPoint)secureTunnelEndPoint;
				this.Connect(ipendPoint.Address.ToString(), ipendPoint.Port);
				return;
			}
			if (fullName != "UnityEngine.XboxOne.XboxOneEndPoint" && fullName != "UnityEngine.PS4.SceEndPoint")
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Connect failed: invalid Endpoint (not IPEndPoint or XboxOneEndPoint or SceEndPoint)");
				}
				this.m_AsyncConnect = NetworkClient.ConnectState.Failed;
				return;
			}
			byte b = 0;
			this.m_RemoteEndPoint = secureTunnelEndPoint;
			this.m_AsyncConnect = NetworkClient.ConnectState.Connecting;
			try
			{
				this.m_ClientConnectionId = NetworkTransport.ConnectEndPoint(this.m_ClientId, this.m_RemoteEndPoint, 0, out b);
			}
			catch (Exception arg)
			{
				Debug.LogError("Connect failed: Exception when trying to connect to EndPoint: " + arg);
			}
			if (this.m_ClientConnectionId == 0 && LogFilter.logError)
			{
				Debug.LogError("Connect failed: Unable to connect to EndPoint (" + b + ")");
			}
			this.m_Connection = (NetworkConnection)Activator.CreateInstance(this.m_NetworkConnectionClass);
			this.m_Connection.SetHandlers(this.m_MessageHandlers);
			this.m_Connection.Initialize(this.m_ServerIp, this.m_ClientId, this.m_ClientConnectionId, this.m_HostTopology);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007830 File Offset: 0x00005A30
		private void PrepareForConnect()
		{
			this.PrepareForConnect(false);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000783C File Offset: 0x00005A3C
		private void PrepareForConnect(bool usePlatformSpecificProtocols)
		{
			NetworkClient.SetActive(true);
			this.RegisterSystemHandlers(false);
			if (this.m_HostTopology == null)
			{
				ConnectionConfig connectionConfig = new ConnectionConfig();
				connectionConfig.AddChannel(QosType.Reliable);
				connectionConfig.AddChannel(QosType.Unreliable);
				connectionConfig.UsePlatformSpecificProtocols = usePlatformSpecificProtocols;
				this.m_HostTopology = new HostTopology(connectionConfig, 8);
			}
			if (this.m_UseSimulator)
			{
				int num = this.m_SimulatedLatency / 3 - 1;
				if (num < 1)
				{
					num = 1;
				}
				int num2 = this.m_SimulatedLatency * 3;
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"AddHost Using Simulator ",
						num,
						"/",
						num2
					}));
				}
				this.m_ClientId = NetworkTransport.AddHostWithSimulator(this.m_HostTopology, num, num2, 0);
			}
			else
			{
				this.m_ClientId = NetworkTransport.AddHost(this.m_HostTopology, 0);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000791C File Offset: 0x00005B1C
		internal static void GetHostAddressesCallback(IAsyncResult ar)
		{
			try
			{
				IPAddress[] array = Dns.EndGetHostAddresses(ar);
				NetworkClient networkClient = (NetworkClient)ar.AsyncState;
				if (array.Length == 0)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("DNS lookup failed for:" + networkClient.m_RequestedServerHost);
					}
					networkClient.m_AsyncConnect = NetworkClient.ConnectState.Failed;
				}
				else
				{
					networkClient.m_ServerIp = array[0].ToString();
					networkClient.m_AsyncConnect = NetworkClient.ConnectState.Resolved;
					if (LogFilter.logDebug)
					{
						Debug.Log(string.Concat(new string[]
						{
							"Async DNS Result:",
							networkClient.m_ServerIp,
							" for ",
							networkClient.m_RequestedServerHost,
							": ",
							networkClient.m_ServerIp
						}));
					}
				}
			}
			catch (SocketException ex)
			{
				NetworkClient networkClient2 = (NetworkClient)ar.AsyncState;
				if (LogFilter.logError)
				{
					Debug.LogError("DNS resolution failed: " + ex.ErrorCode);
				}
				if (LogFilter.logDebug)
				{
					Debug.Log("Exception:" + ex);
				}
				networkClient2.m_AsyncConnect = NetworkClient.ConnectState.Failed;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007A4C File Offset: 0x00005C4C
		internal void ContinueConnect()
		{
			if (this.m_UseSimulator)
			{
				int num = this.m_SimulatedLatency / 3;
				if (num < 1)
				{
					num = 1;
				}
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Connect Using Simulator ",
						this.m_SimulatedLatency / 3,
						"/",
						this.m_SimulatedLatency
					}));
				}
				ConnectionSimulatorConfig conf = new ConnectionSimulatorConfig(num, this.m_SimulatedLatency, num, this.m_SimulatedLatency, this.m_PacketLoss);
				byte b;
				this.m_ClientConnectionId = NetworkTransport.ConnectWithSimulator(this.m_ClientId, this.m_ServerIp, this.m_ServerPort, 0, out b, conf);
			}
			else
			{
				byte b;
				this.m_ClientConnectionId = NetworkTransport.Connect(this.m_ClientId, this.m_ServerIp, this.m_ServerPort, 0, out b);
			}
			this.m_Connection = (NetworkConnection)Activator.CreateInstance(this.m_NetworkConnectionClass);
			this.m_Connection.SetHandlers(this.m_MessageHandlers);
			this.m_Connection.Initialize(this.m_ServerIp, this.m_ClientId, this.m_ClientConnectionId, this.m_HostTopology);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007B68 File Offset: 0x00005D68
		private void ConnectWithRelay(MatchInfo info)
		{
			this.m_AsyncConnect = NetworkClient.ConnectState.Connecting;
			this.Update();
			byte b;
			this.m_ClientConnectionId = NetworkTransport.ConnectToNetworkPeer(this.m_ClientId, info.address, info.port, 0, 0, info.networkId, Utility.GetSourceID(), info.nodeId, out b);
			this.m_Connection = (NetworkConnection)Activator.CreateInstance(this.m_NetworkConnectionClass);
			this.m_Connection.SetHandlers(this.m_MessageHandlers);
			this.m_Connection.Initialize(info.address, this.m_ClientId, this.m_ClientConnectionId, this.m_HostTopology);
			if (b != 0)
			{
				Debug.LogError("ConnectToNetworkPeer Error: " + b);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007C1C File Offset: 0x00005E1C
		public virtual void Disconnect()
		{
			this.m_AsyncConnect = NetworkClient.ConnectState.Disconnected;
			ClientScene.HandleClientDisconnect(this.m_Connection);
			if (this.m_Connection != null)
			{
				this.m_Connection.Disconnect();
				this.m_Connection.Dispose();
				this.m_Connection = null;
				NetworkTransport.RemoveHost(this.m_ClientId);
				this.m_ClientId = -1;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007C78 File Offset: 0x00005E78
		public bool Send(short msgType, MessageBase msg)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient Send with no connection");
				}
				return false;
			}
			if (this.m_AsyncConnect != NetworkClient.ConnectState.Connected)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient Send when not connected to a server");
				}
				return false;
			}
			return this.m_Connection.Send(msgType, msg);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public bool SendWriter(NetworkWriter writer, int channelId)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendWriter with no connection");
				}
				return false;
			}
			if (this.m_AsyncConnect != NetworkClient.ConnectState.Connected)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendWriter when not connected to a server");
				}
				return false;
			}
			return this.m_Connection.SendWriter(writer, channelId);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007D38 File Offset: 0x00005F38
		public bool SendBytes(byte[] data, int numBytes, int channelId)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendBytes with no connection");
				}
				return false;
			}
			if (this.m_AsyncConnect != NetworkClient.ConnectState.Connected)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendBytes when not connected to a server");
				}
				return false;
			}
			return this.m_Connection.SendBytes(data, numBytes, channelId);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007D98 File Offset: 0x00005F98
		public bool SendUnreliable(short msgType, MessageBase msg)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendUnreliable with no connection");
				}
				return false;
			}
			if (this.m_AsyncConnect != NetworkClient.ConnectState.Connected)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendUnreliable when not connected to a server");
				}
				return false;
			}
			return this.m_Connection.SendUnreliable(msgType, msg);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007DF8 File Offset: 0x00005FF8
		public bool SendByChannel(short msgType, MessageBase msg, int channelId)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendByChannel with no connection");
				}
				return false;
			}
			if (this.m_AsyncConnect != NetworkClient.ConnectState.Connected)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkClient SendByChannel when not connected to a server");
				}
				return false;
			}
			return this.m_Connection.SendByChannel(msgType, msg, channelId);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007E58 File Offset: 0x00006058
		public void SetMaxDelay(float seconds)
		{
			if (this.m_Connection == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("SetMaxDelay failed, not connected.");
				}
				return;
			}
			this.m_Connection.SetMaxDelay(seconds);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007E94 File Offset: 0x00006094
		public void Shutdown()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("Shutting down client " + this.m_ClientId);
			}
			if (this.m_ClientId != -1)
			{
				NetworkTransport.RemoveHost(this.m_ClientId);
				this.m_ClientId = -1;
			}
			NetworkClient.RemoveClient(this);
			if (NetworkClient.s_Clients.Count == 0)
			{
				NetworkClient.SetActive(false);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007F00 File Offset: 0x00006100
		internal virtual void Update()
		{
			if (this.m_ClientId == -1)
			{
				return;
			}
			switch (this.m_AsyncConnect)
			{
			case NetworkClient.ConnectState.None:
			case NetworkClient.ConnectState.Resolving:
			case NetworkClient.ConnectState.Disconnected:
				return;
			case NetworkClient.ConnectState.Resolved:
				this.m_AsyncConnect = NetworkClient.ConnectState.Connecting;
				this.ContinueConnect();
				return;
			case NetworkClient.ConnectState.Failed:
				this.GenerateConnectError(11);
				this.m_AsyncConnect = NetworkClient.ConnectState.Disconnected;
				return;
			}
			if (this.m_Connection != null && (int)Time.time != this.m_StatResetTime)
			{
				this.m_Connection.ResetStats();
				this.m_StatResetTime = (int)Time.time;
			}
			byte b;
			for (;;)
			{
				int num = 0;
				int num2;
				int channelId;
				int numBytes;
				NetworkEventType networkEventType = NetworkTransport.ReceiveFromHost(this.m_ClientId, out num2, out channelId, this.m_MsgBuffer, (int)((ushort)this.m_MsgBuffer.Length), out numBytes, out b);
				if (networkEventType != NetworkEventType.Nothing && LogFilter.logDev)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Client event: host=",
						this.m_ClientId,
						" event=",
						networkEventType,
						" error=",
						b
					}));
				}
				switch (networkEventType)
				{
				case NetworkEventType.DataEvent:
					if (b != 0)
					{
						goto Block_10;
					}
					this.m_MsgReader.SeekZero();
					this.m_Connection.TransportRecieve(this.m_MsgBuffer, numBytes, channelId);
					break;
				case NetworkEventType.ConnectEvent:
					if (LogFilter.logDebug)
					{
						Debug.Log("Client connected");
					}
					if (b != 0)
					{
						goto Block_9;
					}
					this.m_AsyncConnect = NetworkClient.ConnectState.Connected;
					this.m_Connection.InvokeHandlerNoData(32);
					break;
				case NetworkEventType.DisconnectEvent:
					if (LogFilter.logDebug)
					{
						Debug.Log("Client disconnected");
					}
					this.m_AsyncConnect = NetworkClient.ConnectState.Disconnected;
					if (b != 0 && b != 6)
					{
						this.GenerateDisconnectError((int)b);
					}
					ClientScene.HandleClientDisconnect(this.m_Connection);
					if (this.m_Connection != null)
					{
						this.m_Connection.InvokeHandlerNoData(33);
					}
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
				if (num + 1 >= 500)
				{
					goto Block_16;
				}
				if (this.m_ClientId == -1)
				{
					goto Block_18;
				}
				if (networkEventType == NetworkEventType.Nothing)
				{
					goto IL_27E;
				}
			}
			Block_9:
			this.GenerateConnectError((int)b);
			return;
			Block_10:
			this.GenerateDataError((int)b);
			return;
			Block_16:
			if (LogFilter.logDebug)
			{
				Debug.Log("MaxEventsPerFrame hit (" + 500 + ")");
			}
			Block_18:
			IL_27E:
			if (this.m_Connection != null && this.m_AsyncConnect == NetworkClient.ConnectState.Connected)
			{
				this.m_Connection.FlushChannels();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000081B0 File Offset: 0x000063B0
		private void GenerateConnectError(int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError("UNet Client Error Connect Error: " + error);
			}
			this.GenerateError(error);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000081E4 File Offset: 0x000063E4
		private void GenerateDataError(int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError("UNet Client Data Error: " + (NetworkError)error);
			}
			this.GenerateError(error);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000821C File Offset: 0x0000641C
		private void GenerateDisconnectError(int error)
		{
			if (LogFilter.logError)
			{
				Debug.LogError("UNet Client Disconnect Error: " + (NetworkError)error);
			}
			this.GenerateError(error);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008254 File Offset: 0x00006454
		private void GenerateError(int error)
		{
			NetworkMessageDelegate handler = this.m_MessageHandlers.GetHandler(34);
			if (handler == null)
			{
				handler = this.m_MessageHandlers.GetHandler(34);
			}
			if (handler != null)
			{
				ErrorMessage errorMessage = new ErrorMessage();
				errorMessage.errorCode = error;
				byte[] buffer = new byte[200];
				NetworkWriter writer = new NetworkWriter(buffer);
				errorMessage.Serialize(writer);
				NetworkReader reader = new NetworkReader(buffer);
				handler(new NetworkMessage
				{
					msgType = 34,
					reader = reader,
					conn = this.m_Connection,
					channelId = 0
				});
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000082F0 File Offset: 0x000064F0
		public void GetStatsOut(out int numMsgs, out int numBufferedMsgs, out int numBytes, out int lastBufferedPerSecond)
		{
			numMsgs = 0;
			numBufferedMsgs = 0;
			numBytes = 0;
			lastBufferedPerSecond = 0;
			if (this.m_Connection != null)
			{
				this.m_Connection.GetStatsOut(out numMsgs, out numBufferedMsgs, out numBytes, out lastBufferedPerSecond);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008328 File Offset: 0x00006528
		public void GetStatsIn(out int numMsgs, out int numBytes)
		{
			numMsgs = 0;
			numBytes = 0;
			if (this.m_Connection != null)
			{
				this.m_Connection.GetStatsIn(out numMsgs, out numBytes);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008348 File Offset: 0x00006548
		public Dictionary<short, NetworkConnection.PacketStat> GetConnectionStats()
		{
			if (this.m_Connection == null)
			{
				return null;
			}
			return this.m_Connection.packetStats;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008364 File Offset: 0x00006564
		public void ResetConnectionStats()
		{
			if (this.m_Connection == null)
			{
				return;
			}
			this.m_Connection.ResetStats();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008380 File Offset: 0x00006580
		public int GetRTT()
		{
			if (this.m_ClientId == -1)
			{
				return 0;
			}
			byte b;
			return NetworkTransport.GetCurrentRtt(this.m_ClientId, this.m_ClientConnectionId, out b);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000083B0 File Offset: 0x000065B0
		internal void RegisterSystemHandlers(bool localClient)
		{
			ClientScene.RegisterSystemHandlers(this, localClient);
			this.RegisterHandlerSafe(14, new NetworkMessageDelegate(this.OnCRC));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000083D0 File Offset: 0x000065D0
		private void OnCRC(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<CRCMessage>(NetworkClient.s_CRCMessage);
			NetworkCRC.Validate(NetworkClient.s_CRCMessage.scripts, this.numChannels);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000083F4 File Offset: 0x000065F4
		public void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			this.m_MessageHandlers.RegisterHandler(msgType, handler);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008404 File Offset: 0x00006604
		public void RegisterHandlerSafe(short msgType, NetworkMessageDelegate handler)
		{
			this.m_MessageHandlers.RegisterHandlerSafe(msgType, handler);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008414 File Offset: 0x00006614
		public void UnregisterHandler(short msgType)
		{
			this.m_MessageHandlers.UnregisterHandler(msgType);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008424 File Offset: 0x00006624
		public static Dictionary<short, NetworkConnection.PacketStat> GetTotalConnectionStats()
		{
			Dictionary<short, NetworkConnection.PacketStat> dictionary = new Dictionary<short, NetworkConnection.PacketStat>();
			foreach (NetworkClient networkClient in NetworkClient.s_Clients)
			{
				Dictionary<short, NetworkConnection.PacketStat> connectionStats = networkClient.GetConnectionStats();
				foreach (short key in connectionStats.Keys)
				{
					if (dictionary.ContainsKey(key))
					{
						NetworkConnection.PacketStat packetStat = dictionary[key];
						packetStat.count += connectionStats[key].count;
						packetStat.bytes += connectionStats[key].bytes;
						dictionary[key] = packetStat;
					}
					else
					{
						dictionary[key] = connectionStats[key];
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008550 File Offset: 0x00006750
		internal static void AddClient(NetworkClient client)
		{
			NetworkClient.s_Clients.Add(client);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008560 File Offset: 0x00006760
		internal static bool RemoveClient(NetworkClient client)
		{
			return NetworkClient.s_Clients.Remove(client);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008570 File Offset: 0x00006770
		internal static void UpdateClients()
		{
			for (int i = 0; i < NetworkClient.s_Clients.Count; i++)
			{
				if (NetworkClient.s_Clients[i] != null)
				{
					NetworkClient.s_Clients[i].Update();
				}
				else
				{
					NetworkClient.s_Clients.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000085C8 File Offset: 0x000067C8
		public static void ShutdownAll()
		{
			while (NetworkClient.s_Clients.Count != 0)
			{
				NetworkClient.s_Clients[0].Shutdown();
			}
			NetworkClient.s_Clients = new List<NetworkClient>();
			NetworkClient.s_IsActive = false;
			ClientScene.Shutdown();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008604 File Offset: 0x00006804
		internal static void SetActive(bool state)
		{
			if (!NetworkClient.s_IsActive && state)
			{
				NetworkTransport.Init();
			}
			NetworkClient.s_IsActive = state;
		}

		// Token: 0x040000B0 RID: 176
		private const int k_MaxEventsPerFrame = 500;

		// Token: 0x040000B1 RID: 177
		private Type m_NetworkConnectionClass = typeof(NetworkConnection);

		// Token: 0x040000B2 RID: 178
		private static List<NetworkClient> s_Clients = new List<NetworkClient>();

		// Token: 0x040000B3 RID: 179
		private static bool s_IsActive;

		// Token: 0x040000B4 RID: 180
		private HostTopology m_HostTopology;

		// Token: 0x040000B5 RID: 181
		private bool m_UseSimulator;

		// Token: 0x040000B6 RID: 182
		private int m_SimulatedLatency;

		// Token: 0x040000B7 RID: 183
		private float m_PacketLoss;

		// Token: 0x040000B8 RID: 184
		private string m_ServerIp = string.Empty;

		// Token: 0x040000B9 RID: 185
		private int m_ServerPort;

		// Token: 0x040000BA RID: 186
		private int m_ClientId = -1;

		// Token: 0x040000BB RID: 187
		private int m_ClientConnectionId = -1;

		// Token: 0x040000BC RID: 188
		private int m_StatResetTime;

		// Token: 0x040000BD RID: 189
		private EndPoint m_RemoteEndPoint;

		// Token: 0x040000BE RID: 190
		private static CRCMessage s_CRCMessage = new CRCMessage();

		// Token: 0x040000BF RID: 191
		private NetworkMessageHandlers m_MessageHandlers = new NetworkMessageHandlers();

		// Token: 0x040000C0 RID: 192
		protected NetworkConnection m_Connection;

		// Token: 0x040000C1 RID: 193
		private byte[] m_MsgBuffer;

		// Token: 0x040000C2 RID: 194
		private NetworkReader m_MsgReader;

		// Token: 0x040000C3 RID: 195
		protected NetworkClient.ConnectState m_AsyncConnect;

		// Token: 0x040000C4 RID: 196
		private string m_RequestedServerHost = string.Empty;

		// Token: 0x02000039 RID: 57
		protected enum ConnectState
		{
			// Token: 0x040000C6 RID: 198
			None,
			// Token: 0x040000C7 RID: 199
			Resolving,
			// Token: 0x040000C8 RID: 200
			Resolved,
			// Token: 0x040000C9 RID: 201
			Connecting,
			// Token: 0x040000CA RID: 202
			Connected,
			// Token: 0x040000CB RID: 203
			Disconnected,
			// Token: 0x040000CC RID: 204
			Failed
		}
	}
}
