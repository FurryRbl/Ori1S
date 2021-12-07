using System;
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x02000049 RID: 73
	[AddComponentMenu("Network/NetworkMigrationManager")]
	public class NetworkMigrationManager : MonoBehaviour
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000FB24 File Offset: 0x0000DD24
		private void AddPendingPlayer(GameObject obj, int connectionId, NetworkInstanceId netId, short playerControllerId)
		{
			if (!this.m_PendingPlayers.ContainsKey(connectionId))
			{
				NetworkMigrationManager.ConnectionPendingPlayers value = default(NetworkMigrationManager.ConnectionPendingPlayers);
				value.players = new List<NetworkMigrationManager.PendingPlayerInfo>();
				this.m_PendingPlayers[connectionId] = value;
			}
			NetworkMigrationManager.PendingPlayerInfo item = default(NetworkMigrationManager.PendingPlayerInfo);
			item.netId = netId;
			item.playerControllerId = playerControllerId;
			item.obj = obj;
			this.m_PendingPlayers[connectionId].players.Add(item);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		private GameObject FindPendingPlayer(int connectionId, NetworkInstanceId netId, short playerControllerId)
		{
			if (this.m_PendingPlayers.ContainsKey(connectionId))
			{
				foreach (NetworkMigrationManager.PendingPlayerInfo pendingPlayerInfo in this.m_PendingPlayers[connectionId].players)
				{
					if (pendingPlayerInfo.netId == netId && pendingPlayerInfo.playerControllerId == playerControllerId)
					{
						return pendingPlayerInfo.obj;
					}
				}
			}
			return null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000FC50 File Offset: 0x0000DE50
		private void RemovePendingPlayer(int connectionId)
		{
			this.m_PendingPlayers.Remove(connectionId);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000FC60 File Offset: 0x0000DE60
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public bool hostMigration
		{
			get
			{
				return this.m_HostMigration;
			}
			set
			{
				this.m_HostMigration = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000FC74 File Offset: 0x0000DE74
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public bool showGUI
		{
			get
			{
				return this.m_ShowGUI;
			}
			set
			{
				this.m_ShowGUI = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000FC88 File Offset: 0x0000DE88
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public int offsetX
		{
			get
			{
				return this.m_OffsetX;
			}
			set
			{
				this.m_OffsetX = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public int offsetY
		{
			get
			{
				return this.m_OffsetY;
			}
			set
			{
				this.m_OffsetY = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		public NetworkClient client
		{
			get
			{
				return this.m_Client;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public bool waitingToBecomeNewHost
		{
			get
			{
				return this.m_WaitingToBecomeNewHost;
			}
			set
			{
				this.m_WaitingToBecomeNewHost = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000FCCC File Offset: 0x0000DECC
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public bool waitingReconnectToNewHost
		{
			get
			{
				return this.m_WaitingReconnectToNewHost;
			}
			set
			{
				this.m_WaitingReconnectToNewHost = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public bool disconnectedFromHost
		{
			get
			{
				return this.m_DisconnectedFromHost;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public bool hostWasShutdown
		{
			get
			{
				return this.m_HostWasShutdown;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public MatchInfo matchInfo
		{
			get
			{
				return this.m_MatchInfo;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		public int oldServerConnectionId
		{
			get
			{
				return this.m_OldServerConnectionId;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000FD00 File Offset: 0x0000DF00
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000FD08 File Offset: 0x0000DF08
		public string newHostAddress
		{
			get
			{
				return this.m_NewHostAddress;
			}
			set
			{
				this.m_NewHostAddress = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public PeerInfoMessage[] peers
		{
			get
			{
				return this.m_Peers;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> pendingPlayers
		{
			get
			{
				return this.m_PendingPlayers;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000FD24 File Offset: 0x0000DF24
		private void Start()
		{
			this.Reset(-1);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public void Reset(int reconnectId)
		{
			this.m_OldServerConnectionId = -1;
			this.m_WaitingToBecomeNewHost = false;
			this.m_WaitingReconnectToNewHost = false;
			this.m_DisconnectedFromHost = false;
			this.m_HostWasShutdown = false;
			ClientScene.SetReconnectId(reconnectId, this.m_Peers);
			if (NetworkManager.singleton != null)
			{
				NetworkManager.singleton.SetupMigrationManager(this);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000FD88 File Offset: 0x0000DF88
		internal void AssignAuthorityCallback(NetworkConnection conn, NetworkIdentity uv, bool authorityState)
		{
			PeerAuthorityMessage peerAuthorityMessage = new PeerAuthorityMessage();
			peerAuthorityMessage.connectionId = conn.connectionId;
			peerAuthorityMessage.netId = uv.netId;
			peerAuthorityMessage.authorityState = authorityState;
			if (LogFilter.logDebug)
			{
				Debug.Log("AssignAuthorityCallback send for netId" + uv.netId);
			}
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					networkConnection.Send(17, peerAuthorityMessage);
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000FE18 File Offset: 0x0000E018
		public void Initialize(NetworkClient newClient, MatchInfo newMatchInfo)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkMigrationManager initialize");
			}
			this.m_Client = newClient;
			this.m_MatchInfo = newMatchInfo;
			newClient.RegisterHandlerSafe(11, new NetworkMessageDelegate(this.OnPeerInfo));
			newClient.RegisterHandlerSafe(17, new NetworkMessageDelegate(this.OnPeerClientAuthority));
			NetworkIdentity.clientAuthorityCallback = new NetworkIdentity.ClientAuthorityCallback(this.AssignAuthorityCallback);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000FE80 File Offset: 0x0000E080
		public void DisablePlayerObjects()
		{
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkMigrationManager DisablePlayerObjects");
			}
			if (this.m_Peers == null)
			{
				return;
			}
			foreach (PeerInfoMessage peerInfoMessage in this.m_Peers)
			{
				if (peerInfoMessage.playerIds != null)
				{
					foreach (PeerInfoPlayer peerInfoPlayer in peerInfoMessage.playerIds)
					{
						if (LogFilter.logDev)
						{
							Debug.Log(string.Concat(new object[]
							{
								"DisablePlayerObjects disable player for ",
								peerInfoMessage.address,
								" netId:",
								peerInfoPlayer.netId,
								" control:",
								peerInfoPlayer.playerControllerId
							}));
						}
						GameObject gameObject = ClientScene.FindLocalObject(peerInfoPlayer.netId);
						if (gameObject != null)
						{
							gameObject.SetActive(false);
							this.AddPendingPlayer(gameObject, peerInfoMessage.connectionId, peerInfoPlayer.netId, peerInfoPlayer.playerControllerId);
						}
						else if (LogFilter.logWarn)
						{
							Debug.LogWarning(string.Concat(new object[]
							{
								"DisablePlayerObjects didnt find player Conn:",
								peerInfoMessage.connectionId,
								" NetId:",
								peerInfoPlayer.netId
							}));
						}
					}
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public void SendPeerInfo()
		{
			if (!this.m_HostMigration)
			{
				return;
			}
			PeerListMessage peerListMessage = new PeerListMessage();
			List<PeerInfoMessage> list = new List<PeerInfoMessage>();
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					PeerInfoMessage peerInfoMessage = new PeerInfoMessage();
					string address;
					int port;
					NetworkID networkID;
					NodeID nodeID;
					byte b;
					NetworkTransport.GetConnectionInfo(NetworkServer.serverHostId, networkConnection.connectionId, out address, out port, out networkID, out nodeID, out b);
					peerInfoMessage.connectionId = networkConnection.connectionId;
					peerInfoMessage.port = port;
					if (i == 0)
					{
						peerInfoMessage.port = NetworkServer.listenPort;
						peerInfoMessage.isHost = true;
						peerInfoMessage.address = "<host>";
					}
					else
					{
						peerInfoMessage.address = address;
						peerInfoMessage.isHost = false;
					}
					List<PeerInfoPlayer> list2 = new List<PeerInfoPlayer>();
					foreach (PlayerController playerController in networkConnection.playerControllers)
					{
						if (playerController != null && playerController.unetView != null)
						{
							PeerInfoPlayer item;
							item.netId = playerController.unetView.netId;
							item.playerControllerId = playerController.unetView.playerControllerId;
							list2.Add(item);
						}
					}
					if (networkConnection.clientOwnedObjects != null)
					{
						foreach (NetworkInstanceId netId in networkConnection.clientOwnedObjects)
						{
							GameObject gameObject = NetworkServer.FindLocalObject(netId);
							if (!(gameObject == null))
							{
								NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
								if (component.playerControllerId == -1)
								{
									PeerInfoPlayer item2;
									item2.netId = netId;
									item2.playerControllerId = -1;
									list2.Add(item2);
								}
							}
						}
					}
					if (list2.Count > 0)
					{
						peerInfoMessage.playerIds = list2.ToArray();
					}
					list.Add(peerInfoMessage);
				}
			}
			peerListMessage.peers = list.ToArray();
			for (int j = 0; j < NetworkServer.connections.Count; j++)
			{
				NetworkConnection networkConnection2 = NetworkServer.connections[j];
				if (networkConnection2 != null)
				{
					peerListMessage.oldServerConnectionId = networkConnection2.connectionId;
					networkConnection2.Send(11, peerListMessage);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001027C File Offset: 0x0000E47C
		private void OnPeerClientAuthority(NetworkMessage netMsg)
		{
			PeerAuthorityMessage peerAuthorityMessage = netMsg.ReadMessage<PeerAuthorityMessage>();
			if (LogFilter.logDebug)
			{
				Debug.Log("OnPeerClientAuthority for netId:" + peerAuthorityMessage.netId);
			}
			if (this.m_Peers == null)
			{
				return;
			}
			foreach (PeerInfoMessage peerInfoMessage in this.m_Peers)
			{
				if (peerInfoMessage.connectionId == peerAuthorityMessage.connectionId)
				{
					if (peerInfoMessage.playerIds == null)
					{
						peerInfoMessage.playerIds = new PeerInfoPlayer[0];
					}
					if (peerAuthorityMessage.authorityState)
					{
						foreach (PeerInfoPlayer peerInfoPlayer in peerInfoMessage.playerIds)
						{
							if (peerInfoPlayer.netId == peerAuthorityMessage.netId)
							{
								return;
							}
						}
						PeerInfoPlayer item = default(PeerInfoPlayer);
						item.netId = peerAuthorityMessage.netId;
						item.playerControllerId = -1;
						peerInfoMessage.playerIds = new List<PeerInfoPlayer>(peerInfoMessage.playerIds)
						{
							item
						}.ToArray();
					}
					else
					{
						for (int k = 0; k < peerInfoMessage.playerIds.Length; k++)
						{
							if (peerInfoMessage.playerIds[k].netId == peerAuthorityMessage.netId)
							{
								List<PeerInfoPlayer> list = new List<PeerInfoPlayer>(peerInfoMessage.playerIds);
								list.RemoveAt(k);
								peerInfoMessage.playerIds = list.ToArray();
								break;
							}
						}
					}
				}
			}
			GameObject go = ClientScene.FindLocalObject(peerAuthorityMessage.netId);
			this.OnAuthorityUpdated(go, peerAuthorityMessage.connectionId, peerAuthorityMessage.authorityState);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010428 File Offset: 0x0000E628
		private void OnPeerInfo(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("OnPeerInfo");
			}
			netMsg.ReadMessage<PeerListMessage>(this.m_PeerListMessage);
			this.m_Peers = this.m_PeerListMessage.peers;
			this.m_OldServerConnectionId = this.m_PeerListMessage.oldServerConnectionId;
			for (int i = 0; i < this.m_Peers.Length; i++)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"peer conn ",
						this.m_Peers[i].connectionId,
						" your conn ",
						this.m_PeerListMessage.oldServerConnectionId
					}));
				}
				if (this.m_Peers[i].connectionId == this.m_PeerListMessage.oldServerConnectionId)
				{
					this.m_Peers[i].isYou = true;
					break;
				}
			}
			this.OnPeersUpdated(this.m_PeerListMessage);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010520 File Offset: 0x0000E720
		private void OnServerReconnectPlayerMessage(NetworkMessage netMsg)
		{
			ReconnectMessage reconnectMessage = netMsg.ReadMessage<ReconnectMessage>();
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnReconnectMessage: connId=",
					reconnectMessage.oldConnectionId,
					" playerControllerId:",
					reconnectMessage.playerControllerId,
					" netId:",
					reconnectMessage.netId
				}));
			}
			GameObject gameObject = this.FindPendingPlayer(reconnectMessage.oldConnectionId, reconnectMessage.netId, reconnectMessage.playerControllerId);
			if (gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"OnReconnectMessage connId=",
						reconnectMessage.oldConnectionId,
						" player null for netId:",
						reconnectMessage.netId,
						" msg.playerControllerId:",
						reconnectMessage.playerControllerId
					}));
				}
				return;
			}
			if (gameObject.activeSelf)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("OnReconnectMessage connId=" + reconnectMessage.oldConnectionId + " player already active?");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("OnReconnectMessage: player=" + gameObject);
			}
			NetworkReader networkReader = null;
			if (reconnectMessage.msgSize != 0)
			{
				networkReader = new NetworkReader(reconnectMessage.msgData);
			}
			if (reconnectMessage.playerControllerId != -1)
			{
				if (networkReader == null)
				{
					this.OnServerReconnectPlayer(netMsg.conn, gameObject, reconnectMessage.oldConnectionId, reconnectMessage.playerControllerId);
				}
				else
				{
					this.OnServerReconnectPlayer(netMsg.conn, gameObject, reconnectMessage.oldConnectionId, reconnectMessage.playerControllerId, networkReader);
				}
			}
			else
			{
				this.OnServerReconnectObject(netMsg.conn, gameObject, reconnectMessage.oldConnectionId);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000106DC File Offset: 0x0000E8DC
		public bool ReconnectObjectForConnection(NetworkConnection newConnection, GameObject oldObject, int oldConnectionId)
		{
			if (!NetworkServer.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectObjectForConnection must have active server");
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ReconnectObjectForConnection: oldConnId=",
					oldConnectionId,
					" obj=",
					oldObject,
					" conn:",
					newConnection
				}));
			}
			if (!this.m_PendingPlayers.ContainsKey(oldConnectionId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectObjectForConnection oldConnId=" + oldConnectionId + " not found.");
				}
				return false;
			}
			oldObject.SetActive(true);
			oldObject.GetComponent<NetworkIdentity>().SetNetworkInstanceId(new NetworkInstanceId(0U));
			if (!NetworkServer.SpawnWithClientAuthority(oldObject, newConnection))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectObjectForConnection oldConnId=" + oldConnectionId + " SpawnWithClientAuthority failed.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000107D0 File Offset: 0x0000E9D0
		public bool ReconnectPlayerForConnection(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
		{
			if (!NetworkServer.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectPlayerForConnection must have active server");
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ReconnectPlayerForConnection: oldConnId=",
					oldConnectionId,
					" player=",
					oldPlayer,
					" conn:",
					newConnection
				}));
			}
			if (!this.m_PendingPlayers.ContainsKey(oldConnectionId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectPlayerForConnection oldConnId=" + oldConnectionId + " not found.");
				}
				return false;
			}
			oldPlayer.SetActive(true);
			NetworkServer.Spawn(oldPlayer);
			if (!NetworkServer.AddPlayerForConnection(newConnection, oldPlayer, playerControllerId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ReconnectPlayerForConnection oldConnId=" + oldConnectionId + " AddPlayerForConnection failed.");
				}
				return false;
			}
			if (NetworkServer.localClientActive)
			{
				this.SendPeerInfo();
			}
			return true;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000108CC File Offset: 0x0000EACC
		public bool LostHostOnClient(NetworkConnection conn)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkMigrationManager client OnDisconnectedFromHost");
			}
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("LostHostOnClient: Host migration not supported on WebGL");
				}
				return false;
			}
			if (this.m_Client == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkMigrationManager LostHostOnHost client was never initialized.");
				}
				return false;
			}
			if (!this.m_HostMigration)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkMigrationManager LostHostOnHost migration not enabled.");
				}
				return false;
			}
			this.m_DisconnectedFromHost = true;
			this.DisablePlayerObjects();
			byte b;
			NetworkTransport.Disconnect(this.m_Client.hostId, this.m_Client.connection.connectionId, out b);
			if (this.m_OldServerConnectionId != -1)
			{
				NetworkMigrationManager.SceneChangeOption sceneChangeOption;
				this.OnClientDisconnectedFromHost(conn, out sceneChangeOption);
				return sceneChangeOption == NetworkMigrationManager.SceneChangeOption.StayInOnlineScene;
			}
			return false;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001099C File Offset: 0x0000EB9C
		public void LostHostOnHost()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkMigrationManager LostHostOnHost");
			}
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("LostHostOnHost: Host migration not supported on WebGL");
				}
				return;
			}
			this.OnServerHostShutdown();
			if (this.m_Peers == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkMigrationManager LostHostOnHost no peers");
				}
				return;
			}
			if (this.m_Peers.Length != 1)
			{
				this.m_HostWasShutdown = true;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public bool BecomeNewHost(int port)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkMigrationManager BecomeNewHost " + this.m_MatchInfo);
			}
			NetworkServer.RegisterHandler(47, new NetworkMessageDelegate(this.OnServerReconnectPlayerMessage));
			NetworkClient networkClient = NetworkServer.BecomeHost(this.m_Client, port, this.m_MatchInfo, this.oldServerConnectionId, this.peers);
			if (networkClient != null)
			{
				if (NetworkManager.singleton != null)
				{
					NetworkManager.singleton.RegisterServerMessages();
					NetworkManager.singleton.UseExternalClient(networkClient);
				}
				else
				{
					Debug.LogWarning("MigrationManager BecomeNewHost - No NetworkManager.");
				}
				networkClient.RegisterHandlerSafe(11, new NetworkMessageDelegate(this.OnPeerInfo));
				this.RemovePendingPlayer(this.m_OldServerConnectionId);
				this.Reset(-1);
				this.SendPeerInfo();
				return true;
			}
			if (LogFilter.logError)
			{
				Debug.LogError("NetworkServer.BecomeHost failed");
			}
			return false;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		protected virtual void OnClientDisconnectedFromHost(NetworkConnection conn, out NetworkMigrationManager.SceneChangeOption sceneChange)
		{
			sceneChange = NetworkMigrationManager.SceneChangeOption.StayInOnlineScene;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010B00 File Offset: 0x0000ED00
		protected virtual void OnServerHostShutdown()
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010B04 File Offset: 0x0000ED04
		protected virtual void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
		{
			this.ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010B14 File Offset: 0x0000ED14
		protected virtual void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId, NetworkReader extraMessageReader)
		{
			this.ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010B24 File Offset: 0x0000ED24
		protected virtual void OnServerReconnectObject(NetworkConnection newConnection, GameObject oldObject, int oldConnectionId)
		{
			this.ReconnectObjectForConnection(newConnection, oldObject, oldConnectionId);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010B30 File Offset: 0x0000ED30
		protected virtual void OnPeersUpdated(PeerListMessage peers)
		{
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkMigrationManager NumPeers " + peers.peers.Length);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010B64 File Offset: 0x0000ED64
		protected virtual void OnAuthorityUpdated(GameObject go, int connectionId, bool authorityState)
		{
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"NetworkMigrationManager OnAuthorityUpdated for ",
					go,
					" conn:",
					connectionId,
					" state:",
					authorityState
				}));
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010BBC File Offset: 0x0000EDBC
		public virtual bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
		{
			if (this.m_Peers == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkMigrationManager FindLowestHost no peers");
				}
				newHostInfo = null;
				youAreNewHost = false;
				return false;
			}
			if (LogFilter.logDev)
			{
				Debug.Log("NetworkMigrationManager FindLowestHost");
			}
			newHostInfo = new PeerInfoMessage();
			newHostInfo.connectionId = 50000;
			newHostInfo.address = string.Empty;
			newHostInfo.port = 0;
			int num = -1;
			youAreNewHost = false;
			if (this.m_Peers == null)
			{
				return false;
			}
			foreach (PeerInfoMessage peerInfoMessage in this.m_Peers)
			{
				if (peerInfoMessage.connectionId != 0)
				{
					if (!peerInfoMessage.isHost)
					{
						if (peerInfoMessage.isYou)
						{
							num = peerInfoMessage.connectionId;
						}
						if (peerInfoMessage.connectionId < newHostInfo.connectionId)
						{
							newHostInfo = peerInfoMessage;
						}
					}
				}
			}
			if (newHostInfo.connectionId == 50000)
			{
				return false;
			}
			if (newHostInfo.connectionId == num)
			{
				youAreNewHost = true;
			}
			if (LogFilter.logDev)
			{
				Debug.Log("FindNewHost new host is " + newHostInfo.address);
			}
			return true;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private void OnGUIHost()
		{
			int num = this.m_OffsetY;
			GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "Host Was Shutdown ID(" + this.m_OldServerConnectionId + ")");
			num += 25;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "Host Migration not supported for WebGL");
				return;
			}
			if (this.m_WaitingReconnectToNewHost)
			{
				if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Reconnect as Client"))
				{
					this.Reset(0);
					if (NetworkManager.singleton != null)
					{
						NetworkManager.singleton.networkAddress = GUI.TextField(new Rect((float)(this.m_OffsetX + 100), (float)num, 95f, 20f), NetworkManager.singleton.networkAddress);
						NetworkManager.singleton.StartClient();
					}
					else
					{
						Debug.LogWarning("MigrationManager Old Host Reconnect - No NetworkManager.");
					}
				}
				num += 25;
			}
			else
			{
				bool flag;
				if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Pick New Host") && this.FindNewHost(out this.m_NewHostInfo, out flag))
				{
					this.m_NewHostAddress = this.m_NewHostInfo.address;
					if (flag)
					{
						Debug.LogWarning("MigrationManager FindNewHost - new host is self?");
					}
					else
					{
						this.m_WaitingReconnectToNewHost = true;
					}
				}
				num += 25;
			}
			if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Leave Game"))
			{
				if (NetworkManager.singleton != null)
				{
					NetworkManager.singleton.SetupMigrationManager(null);
					NetworkManager.singleton.StopHost();
				}
				else
				{
					Debug.LogWarning("MigrationManager Old Host LeaveGame - No NetworkManager.");
				}
				this.Reset(-1);
			}
			num += 25;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		private void OnGUIClient()
		{
			int num = this.m_OffsetY;
			GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "Lost Connection To Host ID(" + this.m_OldServerConnectionId + ")");
			num += 25;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "Host Migration not supported for WebGL");
				return;
			}
			if (this.m_WaitingToBecomeNewHost)
			{
				GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "You are the new host");
				num += 25;
				if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Start As Host"))
				{
					if (NetworkManager.singleton != null)
					{
						this.BecomeNewHost(NetworkManager.singleton.networkPort);
					}
					else
					{
						Debug.LogWarning("MigrationManager Client BecomeNewHost - No NetworkManager.");
					}
				}
				num += 25;
			}
			else if (this.m_WaitingReconnectToNewHost)
			{
				GUI.Label(new Rect((float)this.m_OffsetX, (float)num, 200f, 40f), "New host is " + this.m_NewHostAddress);
				num += 25;
				if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Reconnect To New Host"))
				{
					this.Reset(this.m_OldServerConnectionId);
					if (NetworkManager.singleton != null)
					{
						NetworkManager.singleton.networkAddress = this.m_NewHostAddress;
						NetworkManager.singleton.client.ReconnectToNewHost(this.m_NewHostAddress, NetworkManager.singleton.networkPort);
					}
					else
					{
						Debug.LogWarning("MigrationManager Client reconnect - No NetworkManager.");
					}
				}
				num += 25;
			}
			else
			{
				bool flag;
				if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Pick New Host") && this.FindNewHost(out this.m_NewHostInfo, out flag))
				{
					this.m_NewHostAddress = this.m_NewHostInfo.address;
					if (flag)
					{
						this.m_WaitingToBecomeNewHost = true;
					}
					else
					{
						this.m_WaitingReconnectToNewHost = true;
					}
				}
				num += 25;
			}
			if (GUI.Button(new Rect((float)this.m_OffsetX, (float)num, 200f, 20f), "Leave Game"))
			{
				if (NetworkManager.singleton != null)
				{
					NetworkManager.singleton.SetupMigrationManager(null);
					NetworkManager.singleton.StopHost();
				}
				else
				{
					Debug.LogWarning("MigrationManager Client LeaveGame - No NetworkManager.");
				}
				this.Reset(-1);
			}
			num += 25;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001118C File Offset: 0x0000F38C
		private void OnGUI()
		{
			if (!this.m_ShowGUI)
			{
				return;
			}
			if (this.m_HostWasShutdown)
			{
				this.OnGUIHost();
				return;
			}
			if (!this.m_DisconnectedFromHost)
			{
				return;
			}
			this.OnGUIClient();
		}

		// Token: 0x04000168 RID: 360
		[SerializeField]
		private bool m_HostMigration = true;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private bool m_ShowGUI = true;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private int m_OffsetX = 10;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private int m_OffsetY = 300;

		// Token: 0x0400016C RID: 364
		private NetworkClient m_Client;

		// Token: 0x0400016D RID: 365
		private bool m_WaitingToBecomeNewHost;

		// Token: 0x0400016E RID: 366
		private bool m_WaitingReconnectToNewHost;

		// Token: 0x0400016F RID: 367
		private bool m_DisconnectedFromHost;

		// Token: 0x04000170 RID: 368
		private bool m_HostWasShutdown;

		// Token: 0x04000171 RID: 369
		private MatchInfo m_MatchInfo;

		// Token: 0x04000172 RID: 370
		private int m_OldServerConnectionId = -1;

		// Token: 0x04000173 RID: 371
		private string m_NewHostAddress;

		// Token: 0x04000174 RID: 372
		private PeerInfoMessage m_NewHostInfo = new PeerInfoMessage();

		// Token: 0x04000175 RID: 373
		private PeerListMessage m_PeerListMessage = new PeerListMessage();

		// Token: 0x04000176 RID: 374
		private PeerInfoMessage[] m_Peers;

		// Token: 0x04000177 RID: 375
		private Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers> m_PendingPlayers = new Dictionary<int, NetworkMigrationManager.ConnectionPendingPlayers>();

		// Token: 0x0200004A RID: 74
		public enum SceneChangeOption
		{
			// Token: 0x04000179 RID: 377
			StayInOnlineScene,
			// Token: 0x0400017A RID: 378
			SwitchToOfflineScene
		}

		// Token: 0x0200004B RID: 75
		public struct PendingPlayerInfo
		{
			// Token: 0x0400017B RID: 379
			public NetworkInstanceId netId;

			// Token: 0x0400017C RID: 380
			public short playerControllerId;

			// Token: 0x0400017D RID: 381
			public GameObject obj;
		}

		// Token: 0x0200004C RID: 76
		public struct ConnectionPendingPlayers
		{
			// Token: 0x0400017E RID: 382
			public List<NetworkMigrationManager.PendingPlayerInfo> players;
		}
	}
}
