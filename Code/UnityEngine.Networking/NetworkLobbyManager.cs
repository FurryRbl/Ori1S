using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace UnityEngine.Networking
{
	// Token: 0x02000042 RID: 66
	[AddComponentMenu("Network/NetworkLobbyManager")]
	public class NetworkLobbyManager : NetworkManager
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000BE84 File Offset: 0x0000A084
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public bool showLobbyGUI
		{
			get
			{
				return this.m_ShowLobbyGUI;
			}
			set
			{
				this.m_ShowLobbyGUI = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000BE98 File Offset: 0x0000A098
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public int maxPlayers
		{
			get
			{
				return this.m_MaxPlayers;
			}
			set
			{
				this.m_MaxPlayers = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public int maxPlayersPerConnection
		{
			get
			{
				return this.m_MaxPlayersPerConnection;
			}
			set
			{
				this.m_MaxPlayersPerConnection = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		public int minPlayers
		{
			get
			{
				return this.m_MinPlayers;
			}
			set
			{
				this.m_MinPlayers = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public NetworkLobbyPlayer lobbyPlayerPrefab
		{
			get
			{
				return this.m_LobbyPlayerPrefab;
			}
			set
			{
				this.m_LobbyPlayerPrefab = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public GameObject gamePlayerPrefab
		{
			get
			{
				return this.m_GamePlayerPrefab;
			}
			set
			{
				this.m_GamePlayerPrefab = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000BF04 File Offset: 0x0000A104
		public string lobbyScene
		{
			get
			{
				return this.m_LobbyScene;
			}
			set
			{
				this.m_LobbyScene = value;
				base.offlineScene = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000BF14 File Offset: 0x0000A114
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public string playScene
		{
			get
			{
				return this.m_PlayScene;
			}
			set
			{
				this.m_PlayScene = value;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000BF28 File Offset: 0x0000A128
		private void OnValidate()
		{
			if (this.m_MaxPlayers <= 0)
			{
				this.m_MaxPlayers = 1;
			}
			if (this.m_MaxPlayersPerConnection <= 0)
			{
				this.m_MaxPlayersPerConnection = 1;
			}
			if (this.m_MaxPlayersPerConnection > this.maxPlayers)
			{
				this.m_MaxPlayersPerConnection = this.maxPlayers;
			}
			if (this.m_MinPlayers < 0)
			{
				this.m_MinPlayers = 0;
			}
			if (this.m_MinPlayers > this.m_MaxPlayers)
			{
				this.m_MinPlayers = this.m_MaxPlayers;
			}
			if (this.m_LobbyPlayerPrefab != null)
			{
				NetworkIdentity component = this.m_LobbyPlayerPrefab.GetComponent<NetworkIdentity>();
				if (component == null)
				{
					this.m_LobbyPlayerPrefab = null;
					Debug.LogWarning("LobbyPlayer prefab must have a NetworkIdentity component.");
				}
			}
			if (this.m_GamePlayerPrefab != null)
			{
				NetworkIdentity component2 = this.m_GamePlayerPrefab.GetComponent<NetworkIdentity>();
				if (component2 == null)
				{
					this.m_GamePlayerPrefab = null;
					Debug.LogWarning("GamePlayer prefab must have a NetworkIdentity component.");
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C01C File Offset: 0x0000A21C
		private byte FindSlot()
		{
			byte b = 0;
			while ((int)b < this.maxPlayers)
			{
				if (this.lobbySlots[(int)b] == null)
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C05C File Offset: 0x0000A25C
		private void SceneLoadedForPlayer(NetworkConnection conn, GameObject lobbyPlayerGameObject)
		{
			NetworkLobbyPlayer component = lobbyPlayerGameObject.GetComponent<NetworkLobbyPlayer>();
			if (component == null)
			{
				return;
			}
			string name = SceneManager.GetSceneAt(0).name;
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"NetworkLobby SceneLoadedForPlayer scene:",
					name,
					" ",
					conn
				}));
			}
			if (name == this.m_LobbyScene)
			{
				NetworkLobbyManager.PendingPlayer item;
				item.conn = conn;
				item.lobbyPlayer = lobbyPlayerGameObject;
				this.m_PendingPlayers.Add(item);
				return;
			}
			short playerControllerId = lobbyPlayerGameObject.GetComponent<NetworkIdentity>().playerControllerId;
			GameObject gameObject = this.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
			if (gameObject == null)
			{
				Transform startPosition = base.GetStartPosition();
				if (startPosition != null)
				{
					gameObject = (GameObject)Object.Instantiate(this.gamePlayerPrefab, startPosition.position, startPosition.rotation);
				}
				else
				{
					gameObject = (GameObject)Object.Instantiate(this.gamePlayerPrefab, Vector3.zero, Quaternion.identity);
				}
			}
			if (!this.OnLobbyServerSceneLoadedForPlayer(lobbyPlayerGameObject, gameObject))
			{
				return;
			}
			NetworkServer.ReplacePlayerForConnection(conn, gameObject, playerControllerId);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C180 File Offset: 0x0000A380
		private static int CheckConnectionIsReadyToBegin(NetworkConnection conn)
		{
			int num = 0;
			foreach (PlayerController playerController in conn.playerControllers)
			{
				if (playerController.IsValid)
				{
					NetworkLobbyPlayer component = playerController.gameObject.GetComponent<NetworkLobbyPlayer>();
					if (component.readyToBegin)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C208 File Offset: 0x0000A408
		public void CheckReadyToBegin()
		{
			string name = SceneManager.GetSceneAt(0).name;
			if (name != this.m_LobbyScene)
			{
				return;
			}
			int num = 0;
			foreach (NetworkConnection networkConnection in NetworkServer.connections)
			{
				if (networkConnection != null)
				{
					num += NetworkLobbyManager.CheckConnectionIsReadyToBegin(networkConnection);
				}
			}
			if (this.m_MinPlayers > 0 && num < this.m_MinPlayers)
			{
				return;
			}
			this.m_PendingPlayers.Clear();
			this.OnLobbyServerPlayersReady();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		public void ServerReturnToLobby()
		{
			if (!NetworkServer.active)
			{
				Debug.Log("ServerReturnToLobby called on client");
				return;
			}
			this.ServerChangeScene(this.m_LobbyScene);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		private void CallOnClientEnterLobby()
		{
			this.OnLobbyClientEnter();
			foreach (NetworkLobbyPlayer networkLobbyPlayer in this.lobbySlots)
			{
				if (!(networkLobbyPlayer == null))
				{
					networkLobbyPlayer.readyToBegin = false;
					networkLobbyPlayer.OnClientEnterLobby();
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C33C File Offset: 0x0000A53C
		private void CallOnClientExitLobby()
		{
			this.OnLobbyClientExit();
			foreach (NetworkLobbyPlayer networkLobbyPlayer in this.lobbySlots)
			{
				if (!(networkLobbyPlayer == null))
				{
					networkLobbyPlayer.OnClientExitLobby();
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C388 File Offset: 0x0000A588
		public bool SendReturnToLobby()
		{
			if (this.client == null || !this.client.isConnected)
			{
				return false;
			}
			EmptyMessage msg = new EmptyMessage();
			this.client.Send(46, msg);
			return true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public override void OnServerConnect(NetworkConnection conn)
		{
			if (base.numPlayers >= this.maxPlayers)
			{
				conn.Disconnect();
				return;
			}
			string name = SceneManager.GetSceneAt(0).name;
			if (name != this.m_LobbyScene)
			{
				conn.Disconnect();
				return;
			}
			base.OnServerConnect(conn);
			this.OnLobbyServerConnect(conn);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C424 File Offset: 0x0000A624
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.OnServerDisconnect(conn);
			for (int i = 0; i < this.lobbySlots.Length; i++)
			{
				NetworkLobbyPlayer networkLobbyPlayer = this.lobbySlots[i];
				if (!(networkLobbyPlayer == null))
				{
					if (networkLobbyPlayer.connectionToClient == conn)
					{
						this.lobbySlots[i] = null;
						NetworkServer.Destroy(networkLobbyPlayer.gameObject);
					}
				}
			}
			this.OnLobbyServerDisconnect(conn);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C494 File Offset: 0x0000A694
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
			string name = SceneManager.GetSceneAt(0).name;
			if (name != this.m_LobbyScene)
			{
				return;
			}
			int num = 0;
			foreach (PlayerController playerController in conn.playerControllers)
			{
				if (playerController.IsValid)
				{
					num++;
				}
			}
			if (num >= this.maxPlayersPerConnection)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkLobbyManager no more players for this connection.");
				}
				EmptyMessage msg = new EmptyMessage();
				conn.Send(45, msg);
				return;
			}
			byte b = this.FindSlot();
			if (b == 255)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkLobbyManager no space for more players");
				}
				EmptyMessage msg2 = new EmptyMessage();
				conn.Send(45, msg2);
				return;
			}
			GameObject gameObject = this.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
			if (gameObject == null)
			{
				gameObject = (GameObject)Object.Instantiate(this.lobbyPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);
			}
			NetworkLobbyPlayer component = gameObject.GetComponent<NetworkLobbyPlayer>();
			component.slot = b;
			this.lobbySlots[(int)b] = component;
			NetworkServer.AddPlayerForConnection(conn, gameObject, playerControllerId);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
		{
			short playerControllerId = player.playerControllerId;
			byte slot = player.gameObject.GetComponent<NetworkLobbyPlayer>().slot;
			this.lobbySlots[(int)slot] = null;
			base.OnServerRemovePlayer(conn, player);
			foreach (NetworkLobbyPlayer networkLobbyPlayer in this.lobbySlots)
			{
				if (networkLobbyPlayer != null)
				{
					networkLobbyPlayer.GetComponent<NetworkLobbyPlayer>().readyToBegin = false;
					NetworkLobbyManager.s_LobbyReadyToBeginMessage.slotId = networkLobbyPlayer.slot;
					NetworkLobbyManager.s_LobbyReadyToBeginMessage.readyState = false;
					NetworkServer.SendToReady(null, 43, NetworkLobbyManager.s_LobbyReadyToBeginMessage);
				}
			}
			this.OnLobbyServerPlayerRemoved(conn, playerControllerId);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C698 File Offset: 0x0000A898
		public override void ServerChangeScene(string sceneName)
		{
			if (sceneName == this.m_LobbyScene)
			{
				foreach (NetworkLobbyPlayer networkLobbyPlayer in this.lobbySlots)
				{
					if (!(networkLobbyPlayer == null))
					{
						NetworkIdentity component = networkLobbyPlayer.GetComponent<NetworkIdentity>();
						PlayerController playerController;
						if (component.connectionToClient.GetPlayerController(component.playerControllerId, out playerController))
						{
							NetworkServer.Destroy(playerController.gameObject);
						}
						if (NetworkServer.active)
						{
							networkLobbyPlayer.GetComponent<NetworkLobbyPlayer>().readyToBegin = false;
							NetworkServer.ReplacePlayerForConnection(component.connectionToClient, networkLobbyPlayer.gameObject, component.playerControllerId);
						}
					}
				}
			}
			base.ServerChangeScene(sceneName);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C748 File Offset: 0x0000A948
		public override void OnServerSceneChanged(string sceneName)
		{
			if (sceneName != this.m_LobbyScene)
			{
				foreach (NetworkLobbyManager.PendingPlayer pendingPlayer in this.m_PendingPlayers)
				{
					this.SceneLoadedForPlayer(pendingPlayer.conn, pendingPlayer.lobbyPlayer);
				}
				this.m_PendingPlayers.Clear();
			}
			this.OnLobbyServerSceneChanged(sceneName);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		private void OnServerReadyToBeginMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyManager OnServerReadyToBeginMessage");
			}
			netMsg.ReadMessage<LobbyReadyToBeginMessage>(NetworkLobbyManager.s_ReadyToBeginMessage);
			PlayerController playerController;
			if (!netMsg.conn.GetPlayerController((short)NetworkLobbyManager.s_ReadyToBeginMessage.slotId, out playerController))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager OnServerReadyToBeginMessage invalid playerControllerId " + NetworkLobbyManager.s_ReadyToBeginMessage.slotId);
				}
				return;
			}
			NetworkLobbyPlayer component = playerController.gameObject.GetComponent<NetworkLobbyPlayer>();
			component.readyToBegin = NetworkLobbyManager.s_ReadyToBeginMessage.readyState;
			NetworkServer.SendToReady(null, 43, new LobbyReadyToBeginMessage
			{
				slotId = component.slot,
				readyState = NetworkLobbyManager.s_ReadyToBeginMessage.readyState
			});
			this.CheckReadyToBegin();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		private void OnServerSceneLoadedMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyManager OnSceneLoadedMessage");
			}
			netMsg.ReadMessage<IntegerMessage>(NetworkLobbyManager.s_SceneLoadedMessage);
			PlayerController playerController;
			if (!netMsg.conn.GetPlayerController((short)NetworkLobbyManager.s_SceneLoadedMessage.value, out playerController))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager OnServerSceneLoadedMessage invalid playerControllerId " + NetworkLobbyManager.s_SceneLoadedMessage.value);
				}
				return;
			}
			this.SceneLoadedForPlayer(netMsg.conn, playerController.gameObject);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C924 File Offset: 0x0000AB24
		private void OnServerReturnToLobbyMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyManager OnServerReturnToLobbyMessage");
			}
			this.ServerReturnToLobby();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C940 File Offset: 0x0000AB40
		public override void OnStartServer()
		{
			if (string.IsNullOrEmpty(this.m_LobbyScene))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager LobbyScene is empty. Set the LobbyScene in the inspector for the NetworkLobbyMangaer");
				}
				return;
			}
			if (string.IsNullOrEmpty(this.m_PlayScene))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager PlayScene is empty. Set the PlayScene in the inspector for the NetworkLobbyMangaer");
				}
				return;
			}
			if (this.lobbySlots.Length == 0)
			{
				this.lobbySlots = new NetworkLobbyPlayer[this.maxPlayers];
			}
			NetworkServer.RegisterHandler(43, new NetworkMessageDelegate(this.OnServerReadyToBeginMessage));
			NetworkServer.RegisterHandler(44, new NetworkMessageDelegate(this.OnServerSceneLoadedMessage));
			NetworkServer.RegisterHandler(46, new NetworkMessageDelegate(this.OnServerReturnToLobbyMessage));
			this.OnLobbyStartServer();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public override void OnStartHost()
		{
			this.OnLobbyStartHost();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public override void OnStopHost()
		{
			this.OnLobbyStopHost();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CA04 File Offset: 0x0000AC04
		public override void OnStartClient(NetworkClient lobbyClient)
		{
			if (this.lobbySlots.Length == 0)
			{
				this.lobbySlots = new NetworkLobbyPlayer[this.maxPlayers];
			}
			if (this.m_LobbyPlayerPrefab == null || this.m_LobbyPlayerPrefab.gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager no LobbyPlayer prefab is registered. Please add a LobbyPlayer prefab.");
				}
			}
			else
			{
				ClientScene.RegisterPrefab(this.m_LobbyPlayerPrefab.gameObject);
			}
			if (this.m_GamePlayerPrefab == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager no GamePlayer prefab is registered. Please add a GamePlayer prefab.");
				}
			}
			else
			{
				ClientScene.RegisterPrefab(this.m_GamePlayerPrefab);
			}
			lobbyClient.RegisterHandler(43, new NetworkMessageDelegate(this.OnClientReadyToBegin));
			lobbyClient.RegisterHandler(45, new NetworkMessageDelegate(this.OnClientAddPlayerFailedMessage));
			this.OnLobbyStartClient(lobbyClient);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public override void OnClientConnect(NetworkConnection conn)
		{
			this.OnLobbyClientConnect(conn);
			this.CallOnClientEnterLobby();
			base.OnClientConnect(conn);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		public override void OnClientDisconnect(NetworkConnection conn)
		{
			this.OnLobbyClientDisconnect(conn);
			base.OnClientDisconnect(conn);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public override void OnStopClient()
		{
			this.OnLobbyStopClient();
			this.CallOnClientExitLobby();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public override void OnClientSceneChanged(NetworkConnection conn)
		{
			string name = SceneManager.GetSceneAt(0).name;
			if (name == this.m_LobbyScene)
			{
				if (this.client.isConnected)
				{
					this.CallOnClientEnterLobby();
				}
			}
			else
			{
				this.CallOnClientExitLobby();
			}
			base.OnClientSceneChanged(conn);
			this.OnLobbyClientSceneChanged(conn);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CB78 File Offset: 0x0000AD78
		private void OnClientReadyToBegin(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<LobbyReadyToBeginMessage>(NetworkLobbyManager.s_LobbyReadyToBeginMessage);
			if ((int)NetworkLobbyManager.s_LobbyReadyToBeginMessage.slotId >= this.lobbySlots.Count<NetworkLobbyPlayer>())
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager OnClientReadyToBegin invalid lobby slot " + NetworkLobbyManager.s_LobbyReadyToBeginMessage.slotId);
				}
				return;
			}
			NetworkLobbyPlayer networkLobbyPlayer = this.lobbySlots[(int)NetworkLobbyManager.s_LobbyReadyToBeginMessage.slotId];
			if (networkLobbyPlayer == null || networkLobbyPlayer.gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkLobbyManager OnClientReadyToBegin no player at lobby slot " + NetworkLobbyManager.s_LobbyReadyToBeginMessage.slotId);
				}
				return;
			}
			networkLobbyPlayer.readyToBegin = NetworkLobbyManager.s_LobbyReadyToBeginMessage.readyState;
			networkLobbyPlayer.OnClientReady(NetworkLobbyManager.s_LobbyReadyToBeginMessage.readyState);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		private void OnClientAddPlayerFailedMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyManager Add Player failed.");
			}
			this.OnLobbyClientAddPlayerFailed();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public virtual void OnLobbyStartHost()
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public virtual void OnLobbyStopHost()
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CC70 File Offset: 0x0000AE70
		public virtual void OnLobbyStartServer()
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public virtual void OnLobbyServerConnect(NetworkConnection conn)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public virtual void OnLobbyServerDisconnect(NetworkConnection conn)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public virtual void OnLobbyServerSceneChanged(string sceneName)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public virtual GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
		{
			return null;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CC84 File Offset: 0x0000AE84
		public virtual GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
		{
			return null;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public virtual void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public virtual bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
		{
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public virtual void OnLobbyServerPlayersReady()
		{
			this.ServerChangeScene(this.m_PlayScene);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public virtual void OnLobbyClientEnter()
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public virtual void OnLobbyClientExit()
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public virtual void OnLobbyClientConnect(NetworkConnection conn)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public virtual void OnLobbyClientDisconnect(NetworkConnection conn)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		public virtual void OnLobbyStartClient(NetworkClient lobbyClient)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public virtual void OnLobbyStopClient()
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		public virtual void OnLobbyClientSceneChanged(NetworkConnection conn)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		public virtual void OnLobbyClientAddPlayerFailed()
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		private void OnGUI()
		{
			if (!this.showLobbyGUI)
			{
				return;
			}
			string name = SceneManager.GetSceneAt(0).name;
			if (name != this.m_LobbyScene)
			{
				return;
			}
			Rect position = new Rect(90f, 180f, 500f, 150f);
			GUI.Box(position, "Players:");
			if (NetworkClient.active)
			{
				Rect position2 = new Rect(100f, 300f, 120f, 20f);
				if (GUI.Button(position2, "Add Player"))
				{
					this.TryToAddPlayer();
				}
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public void TryToAddPlayer()
		{
			if (NetworkClient.active)
			{
				short num = -1;
				List<PlayerController> playerControllers = NetworkClient.allClients[0].connection.playerControllers;
				if (playerControllers.Count < this.maxPlayers)
				{
					num = (short)playerControllers.Count;
				}
				else
				{
					short num2 = 0;
					while ((int)num2 < this.maxPlayers)
					{
						if (!playerControllers[(int)num2].IsValid)
						{
							num = num2;
							break;
						}
						num2 += 1;
					}
				}
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"NetworkLobbyManager TryToAddPlayer controllerId ",
						num,
						" ready:",
						ClientScene.ready
					}));
				}
				if (num == -1)
				{
					if (LogFilter.logDebug)
					{
						Debug.Log("NetworkLobbyManager No Space!");
					}
					return;
				}
				if (ClientScene.ready)
				{
					ClientScene.AddPlayer(num);
				}
				else
				{
					ClientScene.AddPlayer(NetworkClient.allClients[0].connection, num);
				}
			}
			else if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyManager NetworkClient not active!");
			}
		}

		// Token: 0x04000120 RID: 288
		[SerializeField]
		private bool m_ShowLobbyGUI = true;

		// Token: 0x04000121 RID: 289
		[SerializeField]
		private int m_MaxPlayers = 4;

		// Token: 0x04000122 RID: 290
		[SerializeField]
		private int m_MaxPlayersPerConnection = 1;

		// Token: 0x04000123 RID: 291
		[SerializeField]
		private int m_MinPlayers;

		// Token: 0x04000124 RID: 292
		[SerializeField]
		private NetworkLobbyPlayer m_LobbyPlayerPrefab;

		// Token: 0x04000125 RID: 293
		[SerializeField]
		private GameObject m_GamePlayerPrefab;

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private string m_LobbyScene = string.Empty;

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private string m_PlayScene = string.Empty;

		// Token: 0x04000128 RID: 296
		private List<NetworkLobbyManager.PendingPlayer> m_PendingPlayers = new List<NetworkLobbyManager.PendingPlayer>();

		// Token: 0x04000129 RID: 297
		public NetworkLobbyPlayer[] lobbySlots;

		// Token: 0x0400012A RID: 298
		private static LobbyReadyToBeginMessage s_ReadyToBeginMessage = new LobbyReadyToBeginMessage();

		// Token: 0x0400012B RID: 299
		private static IntegerMessage s_SceneLoadedMessage = new IntegerMessage();

		// Token: 0x0400012C RID: 300
		private static LobbyReadyToBeginMessage s_LobbyReadyToBeginMessage = new LobbyReadyToBeginMessage();

		// Token: 0x02000043 RID: 67
		private struct PendingPlayer
		{
			// Token: 0x0400012D RID: 301
			public NetworkConnection conn;

			// Token: 0x0400012E RID: 302
			public GameObject lobbyPlayer;
		}
	}
}
