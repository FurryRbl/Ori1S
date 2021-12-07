using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

namespace UnityEngine.Networking
{
	// Token: 0x02000046 RID: 70
	[AddComponentMenu("Network/NetworkManager")]
	public class NetworkManager : MonoBehaviour
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public int networkPort
		{
			get
			{
				return this.m_NetworkPort;
			}
			set
			{
				this.m_NetworkPort = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public bool serverBindToIP
		{
			get
			{
				return this.m_ServerBindToIP;
			}
			set
			{
				this.m_ServerBindToIP = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000D400 File Offset: 0x0000B600
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000D408 File Offset: 0x0000B608
		public string serverBindAddress
		{
			get
			{
				return this.m_ServerBindAddress;
			}
			set
			{
				this.m_ServerBindAddress = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000D414 File Offset: 0x0000B614
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000D41C File Offset: 0x0000B61C
		public string networkAddress
		{
			get
			{
				return this.m_NetworkAddress;
			}
			set
			{
				this.m_NetworkAddress = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000D428 File Offset: 0x0000B628
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000D430 File Offset: 0x0000B630
		public bool dontDestroyOnLoad
		{
			get
			{
				return this.m_DontDestroyOnLoad;
			}
			set
			{
				this.m_DontDestroyOnLoad = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000D43C File Offset: 0x0000B63C
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000D444 File Offset: 0x0000B644
		public bool runInBackground
		{
			get
			{
				return this.m_RunInBackground;
			}
			set
			{
				this.m_RunInBackground = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000D450 File Offset: 0x0000B650
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000D458 File Offset: 0x0000B658
		public bool scriptCRCCheck
		{
			get
			{
				return this.m_ScriptCRCCheck;
			}
			set
			{
				this.m_ScriptCRCCheck = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000D464 File Offset: 0x0000B664
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000D468 File Offset: 0x0000B668
		[Obsolete("moved to NetworkMigrationManager")]
		public bool sendPeerInfo
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000D46C File Offset: 0x0000B66C
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000D474 File Offset: 0x0000B674
		public float maxDelay
		{
			get
			{
				return this.m_MaxDelay;
			}
			set
			{
				this.m_MaxDelay = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000D480 File Offset: 0x0000B680
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000D488 File Offset: 0x0000B688
		public LogFilter.FilterLevel logLevel
		{
			get
			{
				return this.m_LogLevel;
			}
			set
			{
				this.m_LogLevel = value;
				LogFilter.currentLogLevel = (int)value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000D498 File Offset: 0x0000B698
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public GameObject playerPrefab
		{
			get
			{
				return this.m_PlayerPrefab;
			}
			set
			{
				this.m_PlayerPrefab = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000D4AC File Offset: 0x0000B6AC
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public bool autoCreatePlayer
		{
			get
			{
				return this.m_AutoCreatePlayer;
			}
			set
			{
				this.m_AutoCreatePlayer = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public PlayerSpawnMethod playerSpawnMethod
		{
			get
			{
				return this.m_PlayerSpawnMethod;
			}
			set
			{
				this.m_PlayerSpawnMethod = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		public string offlineScene
		{
			get
			{
				return this.m_OfflineScene;
			}
			set
			{
				this.m_OfflineScene = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		public string onlineScene
		{
			get
			{
				return this.m_OnlineScene;
			}
			set
			{
				this.m_OnlineScene = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public List<GameObject> spawnPrefabs
		{
			get
			{
				return this.m_SpawnPrefabs;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000D504 File Offset: 0x0000B704
		public List<Transform> startPositions
		{
			get
			{
				return NetworkManager.s_StartPositions;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000D50C File Offset: 0x0000B70C
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000D514 File Offset: 0x0000B714
		public bool customConfig
		{
			get
			{
				return this.m_CustomConfig;
			}
			set
			{
				this.m_CustomConfig = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000D520 File Offset: 0x0000B720
		public ConnectionConfig connectionConfig
		{
			get
			{
				if (this.m_ConnectionConfig == null)
				{
					this.m_ConnectionConfig = new ConnectionConfig();
				}
				return this.m_ConnectionConfig;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000D540 File Offset: 0x0000B740
		public GlobalConfig globalConfig
		{
			get
			{
				if (this.m_GlobalConfig == null)
				{
					this.m_GlobalConfig = new GlobalConfig();
				}
				return this.m_GlobalConfig;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000D560 File Offset: 0x0000B760
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000D568 File Offset: 0x0000B768
		public int maxConnections
		{
			get
			{
				return this.m_MaxConnections;
			}
			set
			{
				this.m_MaxConnections = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000D574 File Offset: 0x0000B774
		public List<QosType> channels
		{
			get
			{
				return this.m_Channels;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000D57C File Offset: 0x0000B77C
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000D584 File Offset: 0x0000B784
		public EndPoint secureTunnelEndpoint
		{
			get
			{
				return this.m_EndPoint;
			}
			set
			{
				this.m_EndPoint = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000D590 File Offset: 0x0000B790
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000D598 File Offset: 0x0000B798
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public bool useSimulator
		{
			get
			{
				return this.m_UseSimulator;
			}
			set
			{
				this.m_UseSimulator = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public int simulatedLatency
		{
			get
			{
				return this.m_SimulatedLatency;
			}
			set
			{
				this.m_SimulatedLatency = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public float packetLossPercentage
		{
			get
			{
				return this.m_PacketLossPercentage;
			}
			set
			{
				this.m_PacketLossPercentage = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public string matchHost
		{
			get
			{
				return this.m_MatchHost;
			}
			set
			{
				this.m_MatchHost = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		public int matchPort
		{
			get
			{
				return this.m_MatchPort;
			}
			set
			{
				this.m_MatchPort = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D608 File Offset: 0x0000B808
		public NetworkMigrationManager migrationManager
		{
			get
			{
				return this.m_MigrationManager;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000D610 File Offset: 0x0000B810
		public int numPlayers
		{
			get
			{
				int num = 0;
				foreach (NetworkConnection networkConnection in NetworkServer.connections)
				{
					if (networkConnection != null)
					{
						foreach (PlayerController playerController in networkConnection.playerControllers)
						{
							if (playerController.IsValid)
							{
								num++;
							}
						}
					}
				}
				return num;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		private void Awake()
		{
			this.InitializeSingleton();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		private void InitializeSingleton()
		{
			if (NetworkManager.singleton != null && NetworkManager.singleton == this)
			{
				return;
			}
			LogFilter.currentLogLevel = (int)this.m_LogLevel;
			if (this.m_DontDestroyOnLoad)
			{
				if (NetworkManager.singleton != null)
				{
					if (LogFilter.logDev)
					{
						Debug.Log("Multiple NetworkManagers detected in the scene. Only one NetworkManager can exist at a time. The duplicate NetworkManager will not be used.");
					}
					Object.Destroy(base.gameObject);
					return;
				}
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkManager created singleton (DontDestroyOnLoad)");
				}
				NetworkManager.singleton = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				if (LogFilter.logDev)
				{
					Debug.Log("NetworkManager created singleton (ForScene)");
				}
				NetworkManager.singleton = this;
			}
			if (this.m_NetworkAddress != string.Empty)
			{
				NetworkManager.s_Address = this.m_NetworkAddress;
			}
			else if (NetworkManager.s_Address != string.Empty)
			{
				this.m_NetworkAddress = NetworkManager.s_Address;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		private void OnValidate()
		{
			if (this.m_SimulatedLatency < 1)
			{
				this.m_SimulatedLatency = 1;
			}
			if (this.m_SimulatedLatency > 500)
			{
				this.m_SimulatedLatency = 500;
			}
			if (this.m_PacketLossPercentage < 0f)
			{
				this.m_PacketLossPercentage = 0f;
			}
			if (this.m_PacketLossPercentage > 99f)
			{
				this.m_PacketLossPercentage = 99f;
			}
			if (this.m_MaxConnections <= 0)
			{
				this.m_MaxConnections = 1;
			}
			if (this.m_MaxConnections > 32000)
			{
				this.m_MaxConnections = 32000;
			}
			if (this.m_PlayerPrefab != null && this.m_PlayerPrefab.GetComponent<NetworkIdentity>() == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkManager - playerPrefab must have a NetworkIdentity.");
				}
				this.m_PlayerPrefab = null;
			}
			if (this.m_ConnectionConfig.MinUpdateTimeout <= 0U)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkManager MinUpdateTimeout cannot be zero or less. The value will be reset to 1 millisecond");
				}
				this.m_ConnectionConfig.MinUpdateTimeout = 1U;
			}
			if (this.m_GlobalConfig != null && this.m_GlobalConfig.ThreadAwakeTimeout <= 0U)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkManager ThreadAwakeTimeout cannot be zero or less. The value will be reset to 1 millisecond");
				}
				this.m_GlobalConfig.ThreadAwakeTimeout = 1U;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D92C File Offset: 0x0000BB2C
		internal void RegisterServerMessages()
		{
			NetworkServer.RegisterHandler(32, new NetworkMessageDelegate(this.OnServerConnectInternal));
			NetworkServer.RegisterHandler(33, new NetworkMessageDelegate(this.OnServerDisconnectInternal));
			NetworkServer.RegisterHandler(35, new NetworkMessageDelegate(this.OnServerReadyMessageInternal));
			NetworkServer.RegisterHandler(37, new NetworkMessageDelegate(this.OnServerAddPlayerMessageInternal));
			NetworkServer.RegisterHandler(38, new NetworkMessageDelegate(this.OnServerRemovePlayerMessageInternal));
			NetworkServer.RegisterHandler(34, new NetworkMessageDelegate(this.OnServerErrorInternal));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public void SetupMigrationManager(NetworkMigrationManager man)
		{
			this.m_MigrationManager = man;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public bool StartServer(ConnectionConfig config, int maxConnections)
		{
			return this.StartServer(null, config, maxConnections);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public bool StartServer()
		{
			return this.StartServer(null);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		public bool StartServer(MatchInfo info)
		{
			return this.StartServer(info, null, -1);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private bool StartServer(MatchInfo info, ConnectionConfig config, int maxConnections)
		{
			this.InitializeSingleton();
			this.OnStartServer();
			if (this.m_RunInBackground)
			{
				Application.runInBackground = true;
			}
			NetworkCRC.scriptCRCCheck = this.scriptCRCCheck;
			NetworkServer.useWebSockets = this.m_UseWebSockets;
			if (this.m_GlobalConfig != null)
			{
				NetworkTransport.Init(this.m_GlobalConfig);
			}
			if (this.m_CustomConfig && this.m_ConnectionConfig != null && config == null)
			{
				this.m_ConnectionConfig.Channels.Clear();
				foreach (QosType value in this.m_Channels)
				{
					this.m_ConnectionConfig.AddChannel(value);
				}
				NetworkServer.Configure(this.m_ConnectionConfig, this.m_MaxConnections);
			}
			if (config != null)
			{
				NetworkServer.Configure(config, maxConnections);
			}
			if (info != null)
			{
				if (!NetworkServer.Listen(info, this.m_NetworkPort))
				{
					if (LogFilter.logError)
					{
						Debug.LogError("StartServer listen failed.");
					}
					return false;
				}
			}
			else if (this.m_ServerBindToIP && !string.IsNullOrEmpty(this.m_ServerBindAddress))
			{
				if (!NetworkServer.Listen(this.m_ServerBindAddress, this.m_NetworkPort))
				{
					if (LogFilter.logError)
					{
						Debug.LogError("StartServer listen on " + this.m_ServerBindAddress + " failed.");
					}
					return false;
				}
			}
			else if (!NetworkServer.Listen(this.m_NetworkPort))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("StartServer listen failed.");
				}
				return false;
			}
			this.RegisterServerMessages();
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager StartServer port:" + this.m_NetworkPort);
			}
			this.isNetworkActive = true;
			string name = SceneManager.GetSceneAt(0).name;
			if (this.m_OnlineScene != string.Empty && this.m_OnlineScene != name && this.m_OnlineScene != this.m_OfflineScene)
			{
				this.ServerChangeScene(this.m_OnlineScene);
			}
			else
			{
				NetworkServer.SpawnObjects();
			}
			return true;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DC28 File Offset: 0x0000BE28
		internal void RegisterClientMessages(NetworkClient client)
		{
			client.RegisterHandler(32, new NetworkMessageDelegate(this.OnClientConnectInternal));
			client.RegisterHandler(33, new NetworkMessageDelegate(this.OnClientDisconnectInternal));
			client.RegisterHandler(36, new NetworkMessageDelegate(this.OnClientNotReadyMessageInternal));
			client.RegisterHandler(34, new NetworkMessageDelegate(this.OnClientErrorInternal));
			client.RegisterHandler(39, new NetworkMessageDelegate(this.OnClientSceneInternal));
			if (this.m_PlayerPrefab != null)
			{
				ClientScene.RegisterPrefab(this.m_PlayerPrefab);
			}
			foreach (GameObject gameObject in this.m_SpawnPrefabs)
			{
				if (gameObject != null)
				{
					ClientScene.RegisterPrefab(gameObject);
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		public void UseExternalClient(NetworkClient externalClient)
		{
			if (this.m_RunInBackground)
			{
				Application.runInBackground = true;
			}
			if (externalClient != null)
			{
				this.client = externalClient;
				this.isNetworkActive = true;
				this.RegisterClientMessages(this.client);
				this.OnStartClient(this.client);
			}
			else
			{
				this.OnStopClient();
				ClientScene.DestroyAllClientObjects();
				ClientScene.HandleClientDisconnect(this.client.connection);
				this.client = null;
				if (this.m_OfflineScene != string.Empty)
				{
					this.ClientChangeScene(this.m_OfflineScene, false);
				}
			}
			NetworkManager.s_Address = this.m_NetworkAddress;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public NetworkClient StartClient(MatchInfo info, ConnectionConfig config)
		{
			this.InitializeSingleton();
			this.matchInfo = info;
			if (this.m_RunInBackground)
			{
				Application.runInBackground = true;
			}
			this.isNetworkActive = true;
			if (this.m_GlobalConfig != null)
			{
				NetworkTransport.Init(this.m_GlobalConfig);
			}
			this.client = new NetworkClient();
			if (config != null)
			{
				if (config.UsePlatformSpecificProtocols && Application.platform != RuntimePlatform.PS4)
				{
					throw new ArgumentOutOfRangeException("Platform specific protocols are not supported on this platform");
				}
				this.client.Configure(config, 1);
			}
			else if (this.m_CustomConfig && this.m_ConnectionConfig != null)
			{
				this.m_ConnectionConfig.Channels.Clear();
				foreach (QosType value in this.m_Channels)
				{
					this.m_ConnectionConfig.AddChannel(value);
				}
				if (this.m_ConnectionConfig.UsePlatformSpecificProtocols && Application.platform != RuntimePlatform.PS4)
				{
					throw new ArgumentOutOfRangeException("Platform specific protocols are not supported on this platform");
				}
				this.client.Configure(this.m_ConnectionConfig, this.m_MaxConnections);
			}
			this.RegisterClientMessages(this.client);
			if (this.matchInfo != null)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NetworkManager StartClient match: " + this.matchInfo);
				}
				this.client.Connect(this.matchInfo);
			}
			else if (this.m_EndPoint != null)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NetworkManager StartClient using provided SecureTunnel");
				}
				this.client.Connect(this.m_EndPoint);
			}
			else
			{
				if (string.IsNullOrEmpty(this.m_NetworkAddress))
				{
					if (LogFilter.logError)
					{
						Debug.LogError("Must set the Network Address field in the manager");
					}
					return null;
				}
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"NetworkManager StartClient address:",
						this.m_NetworkAddress,
						" port:",
						this.m_NetworkPort
					}));
				}
				if (this.m_UseSimulator)
				{
					this.client.ConnectWithSimulator(this.m_NetworkAddress, this.m_NetworkPort, this.m_SimulatedLatency, this.m_PacketLossPercentage);
				}
				else
				{
					this.client.Connect(this.m_NetworkAddress, this.m_NetworkPort);
				}
			}
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.Initialize(this.client, this.matchInfo);
			}
			this.OnStartClient(this.client);
			NetworkManager.s_Address = this.m_NetworkAddress;
			return this.client;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E088 File Offset: 0x0000C288
		public NetworkClient StartClient(MatchInfo matchInfo)
		{
			return this.StartClient(matchInfo, null);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E094 File Offset: 0x0000C294
		public NetworkClient StartClient()
		{
			return this.StartClient(null, null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public virtual NetworkClient StartHost(ConnectionConfig config, int maxConnections)
		{
			this.OnStartHost();
			if (this.StartServer(config, maxConnections))
			{
				NetworkClient networkClient = this.ConnectLocalClient();
				this.OnServerConnect(networkClient.connection);
				this.OnStartClient(networkClient);
				return networkClient;
			}
			return null;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public virtual NetworkClient StartHost(MatchInfo info)
		{
			this.OnStartHost();
			this.matchInfo = info;
			if (this.StartServer(info))
			{
				NetworkClient networkClient = this.ConnectLocalClient();
				this.OnServerConnect(networkClient.connection);
				this.OnStartClient(networkClient);
				return networkClient;
			}
			return null;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E124 File Offset: 0x0000C324
		public virtual NetworkClient StartHost()
		{
			this.OnStartHost();
			if (this.StartServer())
			{
				NetworkClient networkClient = this.ConnectLocalClient();
				this.OnServerConnect(networkClient.connection);
				this.OnStartClient(networkClient);
				return networkClient;
			}
			return null;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E160 File Offset: 0x0000C360
		private NetworkClient ConnectLocalClient()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager StartHost port:" + this.m_NetworkPort);
			}
			this.m_NetworkAddress = "localhost";
			this.client = ClientScene.ConnectLocalServer();
			this.RegisterClientMessages(this.client);
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.Initialize(this.client, this.matchInfo);
			}
			return this.client;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public void StopHost()
		{
			bool active = NetworkServer.active;
			this.OnStopHost();
			this.StopServer();
			this.StopClient();
			if (this.m_MigrationManager != null && active)
			{
				this.m_MigrationManager.LostHostOnHost();
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E22C File Offset: 0x0000C42C
		public void StopServer()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.OnStopServer();
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager StopServer");
			}
			this.isNetworkActive = false;
			NetworkServer.Shutdown();
			this.StopMatchMaker();
			if (this.m_OfflineScene != string.Empty)
			{
				this.ServerChangeScene(this.m_OfflineScene);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E294 File Offset: 0x0000C494
		public void StopClient()
		{
			this.OnStopClient();
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager StopClient");
			}
			this.isNetworkActive = false;
			if (this.client != null)
			{
				this.client.Disconnect();
				this.client.Shutdown();
				this.client = null;
			}
			this.StopMatchMaker();
			ClientScene.DestroyAllClientObjects();
			if (this.m_OfflineScene != string.Empty)
			{
				this.ClientChangeScene(this.m_OfflineScene, false);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E318 File Offset: 0x0000C518
		public virtual void ServerChangeScene(string newSceneName)
		{
			if (string.IsNullOrEmpty(newSceneName))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ServerChangeScene empty scene name");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("ServerChangeScene " + newSceneName);
			}
			NetworkServer.SetAllClientsNotReady();
			NetworkManager.networkSceneName = newSceneName;
			NetworkManager.s_LoadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);
			StringMessage msg = new StringMessage(NetworkManager.networkSceneName);
			NetworkServer.SendToAll(39, msg);
			NetworkManager.s_StartPositionIndex = 0;
			NetworkManager.s_StartPositions.Clear();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E39C File Offset: 0x0000C59C
		internal void ClientChangeScene(string newSceneName, bool forceReload)
		{
			if (string.IsNullOrEmpty(newSceneName))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ClientChangeScene empty scene name");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientChangeScene newSceneName:" + newSceneName + " networkSceneName:" + NetworkManager.networkSceneName);
			}
			if (newSceneName == NetworkManager.networkSceneName)
			{
				if (this.m_MigrationManager != null)
				{
					this.FinishLoadScene();
					return;
				}
				if (!forceReload)
				{
					this.FinishLoadScene();
					return;
				}
			}
			NetworkManager.s_LoadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);
			NetworkManager.networkSceneName = newSceneName;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E434 File Offset: 0x0000C634
		private void FinishLoadScene()
		{
			if (this.client != null)
			{
				if (NetworkManager.s_ClientReadyConnection != null)
				{
					this.OnClientConnect(NetworkManager.s_ClientReadyConnection);
					NetworkManager.s_ClientReadyConnection = null;
				}
			}
			else if (LogFilter.logDev)
			{
				Debug.Log("FinishLoadScene client is null");
			}
			if (NetworkServer.active)
			{
				NetworkServer.SpawnObjects();
				this.OnServerSceneChanged(NetworkManager.networkSceneName);
			}
			if (this.IsClientConnected() && this.client != null)
			{
				this.RegisterClientMessages(this.client);
				this.OnClientSceneChanged(this.client.connection);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		internal static void UpdateScene()
		{
			if (NetworkManager.singleton == null)
			{
				return;
			}
			if (NetworkManager.s_LoadingSceneAsync == null)
			{
				return;
			}
			if (!NetworkManager.s_LoadingSceneAsync.isDone)
			{
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientChangeScene done readyCon:" + NetworkManager.s_ClientReadyConnection);
			}
			NetworkManager.singleton.FinishLoadScene();
			NetworkManager.s_LoadingSceneAsync.allowSceneActivation = true;
			NetworkManager.s_LoadingSceneAsync = null;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E544 File Offset: 0x0000C744
		private void OnDestroy()
		{
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkManager destroyed");
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E55C File Offset: 0x0000C75C
		public static void RegisterStartPosition(Transform start)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("RegisterStartPosition:" + start);
			}
			NetworkManager.s_StartPositions.Add(start);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E584 File Offset: 0x0000C784
		public static void UnRegisterStartPosition(Transform start)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("UnRegisterStartPosition:" + start);
			}
			NetworkManager.s_StartPositions.Remove(start);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public bool IsClientConnected()
		{
			return this.client != null && this.client.isConnected;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public static void Shutdown()
		{
			if (NetworkManager.singleton == null)
			{
				return;
			}
			NetworkManager.s_StartPositions.Clear();
			NetworkManager.s_StartPositionIndex = 0;
			NetworkManager.s_ClientReadyConnection = null;
			NetworkManager.singleton.StopHost();
			NetworkManager.singleton = null;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E610 File Offset: 0x0000C810
		internal void OnServerConnectInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerConnectInternal");
			}
			netMsg.conn.SetMaxDelay(this.m_MaxDelay);
			if (NetworkManager.networkSceneName != string.Empty && NetworkManager.networkSceneName != this.m_OfflineScene)
			{
				StringMessage msg = new StringMessage(NetworkManager.networkSceneName);
				netMsg.conn.Send(39, msg);
			}
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.SendPeerInfo();
			}
			this.OnServerConnect(netMsg.conn);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		internal void OnServerDisconnectInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerDisconnectInternal");
			}
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.SendPeerInfo();
			}
			this.OnServerDisconnect(netMsg.conn);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		internal void OnServerReadyMessageInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerReadyMessageInternal");
			}
			this.OnServerReady(netMsg.conn);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E72C File Offset: 0x0000C92C
		internal void OnServerAddPlayerMessageInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerAddPlayerMessageInternal");
			}
			netMsg.ReadMessage<AddPlayerMessage>(NetworkManager.s_AddPlayerMessage);
			if (NetworkManager.s_AddPlayerMessage.msgSize != 0)
			{
				NetworkReader extraMessageReader = new NetworkReader(NetworkManager.s_AddPlayerMessage.msgData);
				this.OnServerAddPlayer(netMsg.conn, NetworkManager.s_AddPlayerMessage.playerControllerId, extraMessageReader);
			}
			else
			{
				this.OnServerAddPlayer(netMsg.conn, NetworkManager.s_AddPlayerMessage.playerControllerId);
			}
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.SendPeerInfo();
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		internal void OnServerRemovePlayerMessageInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerRemovePlayerMessageInternal");
			}
			netMsg.ReadMessage<RemovePlayerMessage>(NetworkManager.s_RemovePlayerMessage);
			PlayerController player;
			netMsg.conn.GetPlayerController(NetworkManager.s_RemovePlayerMessage.playerControllerId, out player);
			this.OnServerRemovePlayer(netMsg.conn, player);
			netMsg.conn.RemovePlayerController(NetworkManager.s_RemovePlayerMessage.playerControllerId);
			if (this.m_MigrationManager != null)
			{
				this.m_MigrationManager.SendPeerInfo();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E84C File Offset: 0x0000CA4C
		internal void OnServerErrorInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnServerErrorInternal");
			}
			netMsg.ReadMessage<ErrorMessage>(NetworkManager.s_ErrorMessage);
			this.OnServerError(netMsg.conn, NetworkManager.s_ErrorMessage.errorCode);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E884 File Offset: 0x0000CA84
		internal void OnClientConnectInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnClientConnectInternal");
			}
			netMsg.conn.SetMaxDelay(this.m_MaxDelay);
			if (string.IsNullOrEmpty(this.m_OnlineScene) || this.m_OnlineScene == this.m_OfflineScene)
			{
				this.OnClientConnect(netMsg.conn);
			}
			else
			{
				NetworkManager.s_ClientReadyConnection = netMsg.conn;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		internal void OnClientDisconnectInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnClientDisconnectInternal");
			}
			if (this.m_MigrationManager != null && this.m_MigrationManager.LostHostOnClient(netMsg.conn))
			{
				return;
			}
			if (this.m_OfflineScene != string.Empty)
			{
				this.ClientChangeScene(this.m_OfflineScene, false);
			}
			this.OnClientDisconnect(netMsg.conn);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E970 File Offset: 0x0000CB70
		internal void OnClientNotReadyMessageInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnClientNotReadyMessageInternal");
			}
			ClientScene.SetNotReady();
			this.OnClientNotReady(netMsg.conn);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E998 File Offset: 0x0000CB98
		internal void OnClientErrorInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnClientErrorInternal");
			}
			netMsg.ReadMessage<ErrorMessage>(NetworkManager.s_ErrorMessage);
			this.OnClientError(netMsg.conn, NetworkManager.s_ErrorMessage.errorCode);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		internal void OnClientSceneInternal(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager:OnClientSceneInternal");
			}
			string newSceneName = netMsg.reader.ReadString();
			if (this.IsClientConnected() && !NetworkServer.active)
			{
				this.ClientChangeScene(newSceneName, true);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public virtual void OnServerConnect(NetworkConnection conn)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public virtual void OnServerDisconnect(NetworkConnection conn)
		{
			NetworkServer.DestroyPlayersForConnection(conn);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EA28 File Offset: 0x0000CC28
		public virtual void OnServerReady(NetworkConnection conn)
		{
			if (conn.playerControllers.Count == 0 && LogFilter.logDebug)
			{
				Debug.Log("Ready with no player object");
			}
			NetworkServer.SetClientReady(conn);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
		{
			this.OnServerAddPlayerInternal(conn, playerControllerId);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
			this.OnServerAddPlayerInternal(conn, playerControllerId);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000EA78 File Offset: 0x0000CC78
		private void OnServerAddPlayerInternal(NetworkConnection conn, short playerControllerId)
		{
			if (this.m_PlayerPrefab == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
				}
				return;
			}
			if (this.m_PlayerPrefab.GetComponent<NetworkIdentity>() == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
				}
				return;
			}
			if ((int)playerControllerId < conn.playerControllers.Count && conn.playerControllers[(int)playerControllerId].IsValid && conn.playerControllers[(int)playerControllerId].gameObject != null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("There is already a player at that playerControllerId for this connections.");
				}
				return;
			}
			Transform startPosition = this.GetStartPosition();
			GameObject player;
			if (startPosition != null)
			{
				player = (GameObject)Object.Instantiate(this.m_PlayerPrefab, startPosition.position, startPosition.rotation);
			}
			else
			{
				player = (GameObject)Object.Instantiate(this.m_PlayerPrefab, Vector3.zero, Quaternion.identity);
			}
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public Transform GetStartPosition()
		{
			if (NetworkManager.s_StartPositions.Count > 0)
			{
				for (int i = NetworkManager.s_StartPositions.Count - 1; i >= 0; i--)
				{
					if (NetworkManager.s_StartPositions[i] == null)
					{
						NetworkManager.s_StartPositions.RemoveAt(i);
					}
				}
			}
			if (this.m_PlayerSpawnMethod == PlayerSpawnMethod.Random && NetworkManager.s_StartPositions.Count > 0)
			{
				int index = Random.Range(0, NetworkManager.s_StartPositions.Count);
				return NetworkManager.s_StartPositions[index];
			}
			if (this.m_PlayerSpawnMethod == PlayerSpawnMethod.RoundRobin && NetworkManager.s_StartPositions.Count > 0)
			{
				if (NetworkManager.s_StartPositionIndex >= NetworkManager.s_StartPositions.Count)
				{
					NetworkManager.s_StartPositionIndex = 0;
				}
				Transform result = NetworkManager.s_StartPositions[NetworkManager.s_StartPositionIndex];
				NetworkManager.s_StartPositionIndex++;
				return result;
			}
			return null;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public virtual void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
		{
			if (player.gameObject != null)
			{
				NetworkServer.Destroy(player.gameObject);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public virtual void OnServerError(NetworkConnection conn, int errorCode)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public virtual void OnServerSceneChanged(string sceneName)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000EC98 File Offset: 0x0000CE98
		public virtual void OnClientConnect(NetworkConnection conn)
		{
			if (string.IsNullOrEmpty(this.m_OnlineScene) || this.m_OnlineScene == this.m_OfflineScene)
			{
				ClientScene.Ready(conn);
				if (this.m_AutoCreatePlayer)
				{
					ClientScene.AddPlayer(0);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public virtual void OnClientDisconnect(NetworkConnection conn)
		{
			this.StopClient();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public virtual void OnClientError(NetworkConnection conn, int errorCode)
		{
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public virtual void OnClientNotReady(NetworkConnection conn)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public virtual void OnClientSceneChanged(NetworkConnection conn)
		{
			ClientScene.Ready(conn);
			if (!this.m_AutoCreatePlayer)
			{
				return;
			}
			bool flag = ClientScene.localPlayers.Count == 0;
			bool flag2 = false;
			foreach (PlayerController playerController in ClientScene.localPlayers)
			{
				if (playerController.gameObject != null)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				flag = true;
			}
			if (flag)
			{
				ClientScene.AddPlayer(0);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public void StartMatchMaker()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager StartMatchMaker");
			}
			this.SetMatchHost(this.m_MatchHost, this.m_MatchPort, true);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public void StopMatchMaker()
		{
			if (this.matchMaker != null)
			{
				Object.Destroy(this.matchMaker);
				this.matchMaker = null;
			}
			this.matchInfo = null;
			this.matches = null;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000EE04 File Offset: 0x0000D004
		public void SetMatchHost(string newHost, int port, bool https)
		{
			if (this.matchMaker == null)
			{
				this.matchMaker = base.gameObject.AddComponent<NetworkMatch>();
			}
			if (newHost == "localhost" || newHost == "127.0.0.1")
			{
				newHost = Environment.MachineName;
			}
			string text = "http://";
			if (https)
			{
				text = "https://";
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("SetMatchHost:" + newHost);
			}
			this.m_MatchHost = newHost;
			this.m_MatchPort = port;
			this.matchMaker.baseUri = new Uri(string.Concat(new object[]
			{
				text,
				this.m_MatchHost,
				":",
				this.m_MatchPort
			}));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public virtual void OnStartHost()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public virtual void OnStartServer()
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public virtual void OnStartClient(NetworkClient client)
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public virtual void OnStopServer()
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		public virtual void OnStopClient()
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public virtual void OnStopHost()
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EEEC File Offset: 0x0000D0EC
		public virtual void OnMatchCreate(CreateMatchResponse matchInfo)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager OnMatchCreate " + matchInfo);
			}
			if (matchInfo.success)
			{
				Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken(matchInfo.accessTokenString));
				this.StartHost(new MatchInfo(matchInfo));
			}
			else if (LogFilter.logError)
			{
				Debug.LogError("Create Failed:" + matchInfo);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EF60 File Offset: 0x0000D160
		public virtual void OnMatchList(ListMatchResponse matchList)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager OnMatchList ");
			}
			this.matches = matchList.matches;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EF90 File Offset: 0x0000D190
		public void OnMatchJoined(JoinMatchResponse matchInfo)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkManager OnMatchJoined ");
			}
			if (matchInfo.success)
			{
				Utility.SetAccessTokenForNetwork(matchInfo.networkId, new NetworkAccessToken(matchInfo.accessTokenString));
				this.StartClient(new MatchInfo(matchInfo));
			}
			else if (LogFilter.logError)
			{
				Debug.LogError("Join Failed:" + matchInfo);
			}
		}

		// Token: 0x04000135 RID: 309
		[SerializeField]
		private int m_NetworkPort = 7777;

		// Token: 0x04000136 RID: 310
		[SerializeField]
		private bool m_ServerBindToIP;

		// Token: 0x04000137 RID: 311
		[SerializeField]
		private string m_ServerBindAddress = string.Empty;

		// Token: 0x04000138 RID: 312
		[SerializeField]
		private string m_NetworkAddress = "localhost";

		// Token: 0x04000139 RID: 313
		[SerializeField]
		private bool m_DontDestroyOnLoad = true;

		// Token: 0x0400013A RID: 314
		[SerializeField]
		private bool m_RunInBackground = true;

		// Token: 0x0400013B RID: 315
		[SerializeField]
		private bool m_ScriptCRCCheck = true;

		// Token: 0x0400013C RID: 316
		[SerializeField]
		private float m_MaxDelay = 0.01f;

		// Token: 0x0400013D RID: 317
		[SerializeField]
		private LogFilter.FilterLevel m_LogLevel = LogFilter.FilterLevel.Info;

		// Token: 0x0400013E RID: 318
		[SerializeField]
		private GameObject m_PlayerPrefab;

		// Token: 0x0400013F RID: 319
		[SerializeField]
		private bool m_AutoCreatePlayer = true;

		// Token: 0x04000140 RID: 320
		[SerializeField]
		private PlayerSpawnMethod m_PlayerSpawnMethod;

		// Token: 0x04000141 RID: 321
		[SerializeField]
		private string m_OfflineScene = string.Empty;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		private string m_OnlineScene = string.Empty;

		// Token: 0x04000143 RID: 323
		[SerializeField]
		private List<GameObject> m_SpawnPrefabs = new List<GameObject>();

		// Token: 0x04000144 RID: 324
		[SerializeField]
		private bool m_CustomConfig;

		// Token: 0x04000145 RID: 325
		[SerializeField]
		private int m_MaxConnections = 4;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		private ConnectionConfig m_ConnectionConfig;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		private GlobalConfig m_GlobalConfig;

		// Token: 0x04000148 RID: 328
		[SerializeField]
		private List<QosType> m_Channels = new List<QosType>();

		// Token: 0x04000149 RID: 329
		[SerializeField]
		private bool m_UseWebSockets;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		private bool m_UseSimulator;

		// Token: 0x0400014B RID: 331
		[SerializeField]
		private int m_SimulatedLatency = 1;

		// Token: 0x0400014C RID: 332
		[SerializeField]
		private float m_PacketLossPercentage;

		// Token: 0x0400014D RID: 333
		[SerializeField]
		private string m_MatchHost = "mm.unet.unity3d.com";

		// Token: 0x0400014E RID: 334
		[SerializeField]
		private int m_MatchPort = 443;

		// Token: 0x0400014F RID: 335
		private NetworkMigrationManager m_MigrationManager;

		// Token: 0x04000150 RID: 336
		private EndPoint m_EndPoint;

		// Token: 0x04000151 RID: 337
		public static string networkSceneName = string.Empty;

		// Token: 0x04000152 RID: 338
		public bool isNetworkActive;

		// Token: 0x04000153 RID: 339
		public NetworkClient client;

		// Token: 0x04000154 RID: 340
		private static List<Transform> s_StartPositions = new List<Transform>();

		// Token: 0x04000155 RID: 341
		private static int s_StartPositionIndex;

		// Token: 0x04000156 RID: 342
		public MatchInfo matchInfo;

		// Token: 0x04000157 RID: 343
		public NetworkMatch matchMaker;

		// Token: 0x04000158 RID: 344
		public List<MatchDesc> matches;

		// Token: 0x04000159 RID: 345
		public string matchName = "default";

		// Token: 0x0400015A RID: 346
		public uint matchSize = 4U;

		// Token: 0x0400015B RID: 347
		public static NetworkManager singleton;

		// Token: 0x0400015C RID: 348
		private static AddPlayerMessage s_AddPlayerMessage = new AddPlayerMessage();

		// Token: 0x0400015D RID: 349
		private static RemovePlayerMessage s_RemovePlayerMessage = new RemovePlayerMessage();

		// Token: 0x0400015E RID: 350
		private static ErrorMessage s_ErrorMessage = new ErrorMessage();

		// Token: 0x0400015F RID: 351
		private static AsyncOperation s_LoadingSceneAsync;

		// Token: 0x04000160 RID: 352
		private static NetworkConnection s_ClientReadyConnection;

		// Token: 0x04000161 RID: 353
		private static string s_Address;
	}
}
