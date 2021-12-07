using System;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	public class ClientScene
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000029BC File Offset: 0x00000BBC
		public static void SetReconnectId(int newReconnectId, PeerInfoMessage[] peers)
		{
			ClientScene.s_ReconnectId = newReconnectId;
			ClientScene.s_Peers = peers;
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::SetReconnectId: " + newReconnectId);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029EC File Offset: 0x00000BEC
		internal static void SetNotReady()
		{
			ClientScene.s_IsReady = false;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static List<PlayerController> localPlayers
		{
			get
			{
				return ClientScene.s_LocalPlayers;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000029FC File Offset: 0x00000BFC
		public static bool ready
		{
			get
			{
				return ClientScene.s_IsReady;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002A04 File Offset: 0x00000C04
		public static NetworkConnection readyConnection
		{
			get
			{
				return ClientScene.s_ReadyConnection;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A0C File Offset: 0x00000C0C
		public static int reconnectId
		{
			get
			{
				return ClientScene.s_ReconnectId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A14 File Offset: 0x00000C14
		public static Dictionary<NetworkInstanceId, NetworkIdentity> objects
		{
			get
			{
				return ClientScene.s_NetworkScene.localObjects;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A20 File Offset: 0x00000C20
		public static Dictionary<NetworkHash128, GameObject> prefabs
		{
			get
			{
				return NetworkScene.guidToPrefab;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A28 File Offset: 0x00000C28
		public static Dictionary<NetworkSceneId, NetworkIdentity> spawnableObjects
		{
			get
			{
				return ClientScene.s_SpawnableObjects;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A30 File Offset: 0x00000C30
		internal static void Shutdown()
		{
			ClientScene.s_NetworkScene.Shutdown();
			ClientScene.s_LocalPlayers = new List<PlayerController>();
			ClientScene.s_PendingOwnerIds = new List<ClientScene.PendingOwner>();
			ClientScene.s_SpawnableObjects = null;
			ClientScene.s_ReadyConnection = null;
			ClientScene.s_IsReady = false;
			ClientScene.s_IsSpawnFinished = false;
			ClientScene.s_ReconnectId = -1;
			NetworkTransport.Shutdown();
			NetworkTransport.Init();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A84 File Offset: 0x00000C84
		internal static bool GetPlayerController(short playerControllerId, out PlayerController player)
		{
			player = null;
			if ((int)playerControllerId >= ClientScene.localPlayers.Count)
			{
				if (LogFilter.logWarn)
				{
					Debug.Log("ClientScene::GetPlayer: no local player found for: " + playerControllerId);
				}
				return false;
			}
			if (ClientScene.localPlayers[(int)playerControllerId] == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ClientScene::GetPlayer: local player is null for: " + playerControllerId);
				}
				return false;
			}
			player = ClientScene.localPlayers[(int)playerControllerId];
			return player.gameObject != null;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B10 File Offset: 0x00000D10
		internal static void InternalAddPlayer(NetworkIdentity view, short playerControllerId)
		{
			if (LogFilter.logDebug)
			{
				Debug.LogWarning("ClientScene::InternalAddPlayer: playerControllerId : " + playerControllerId);
			}
			if ((int)playerControllerId >= ClientScene.s_LocalPlayers.Count)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ClientScene::InternalAddPlayer: playerControllerId higher than expected: " + playerControllerId);
				}
				while ((int)playerControllerId >= ClientScene.s_LocalPlayers.Count)
				{
					ClientScene.s_LocalPlayers.Add(new PlayerController());
				}
			}
			PlayerController playerController = new PlayerController
			{
				gameObject = view.gameObject,
				playerControllerId = playerControllerId,
				unetView = view
			};
			ClientScene.s_LocalPlayers[(int)playerControllerId] = playerController;
			ClientScene.s_ReadyConnection.SetPlayerController(playerController);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static bool AddPlayer(short playerControllerId)
		{
			return ClientScene.AddPlayer(null, playerControllerId);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static bool AddPlayer(NetworkConnection readyConn, short playerControllerId)
		{
			return ClientScene.AddPlayer(readyConn, playerControllerId, null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public static bool AddPlayer(NetworkConnection readyConn, short playerControllerId, MessageBase extraMessage)
		{
			if (playerControllerId < 0)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ClientScene::AddPlayer: playerControllerId of " + playerControllerId + " is negative");
				}
				return false;
			}
			if (playerControllerId > 32)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"ClientScene::AddPlayer: playerControllerId of ",
						playerControllerId,
						" is too high, max is ",
						32
					}));
				}
				return false;
			}
			if (playerControllerId > 16 && LogFilter.logWarn)
			{
				Debug.LogWarning("ClientScene::AddPlayer: playerControllerId of " + playerControllerId + " is unusually high");
			}
			while ((int)playerControllerId >= ClientScene.s_LocalPlayers.Count)
			{
				ClientScene.s_LocalPlayers.Add(new PlayerController());
			}
			if (readyConn == null)
			{
				if (!ClientScene.s_IsReady)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("Must call AddPlayer() with a connection the first time to become ready.");
					}
					return false;
				}
			}
			else
			{
				ClientScene.s_IsReady = true;
				ClientScene.s_ReadyConnection = readyConn;
			}
			PlayerController playerController;
			if (ClientScene.s_ReadyConnection.GetPlayerController(playerControllerId, out playerController) && playerController.IsValid && playerController.gameObject != null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ClientScene::AddPlayer: playerControllerId of " + playerControllerId + " already in use.");
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::AddPlayer() for ID ",
					playerControllerId,
					" called with connection [",
					ClientScene.s_ReadyConnection,
					"]"
				}));
			}
			if (ClientScene.s_ReconnectId == -1)
			{
				AddPlayerMessage addPlayerMessage = new AddPlayerMessage();
				addPlayerMessage.playerControllerId = playerControllerId;
				if (extraMessage != null)
				{
					NetworkWriter networkWriter = new NetworkWriter();
					extraMessage.Serialize(networkWriter);
					addPlayerMessage.msgData = networkWriter.ToArray();
					addPlayerMessage.msgSize = (int)networkWriter.Position;
				}
				ClientScene.s_ReadyConnection.Send(37, addPlayerMessage);
			}
			else
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("ClientScene::AddPlayer reconnect " + ClientScene.s_ReconnectId);
				}
				if (ClientScene.s_Peers == null)
				{
					ClientScene.SetReconnectId(-1, null);
					if (LogFilter.logError)
					{
						Debug.LogError("ClientScene::AddPlayer: reconnecting, but no peers.");
					}
					return false;
				}
				foreach (PeerInfoMessage peerInfoMessage in ClientScene.s_Peers)
				{
					if (peerInfoMessage.playerIds != null)
					{
						if (peerInfoMessage.connectionId == ClientScene.s_ReconnectId)
						{
							foreach (PeerInfoPlayer peerInfoPlayer in peerInfoMessage.playerIds)
							{
								ReconnectMessage reconnectMessage = new ReconnectMessage();
								reconnectMessage.oldConnectionId = ClientScene.s_ReconnectId;
								reconnectMessage.netId = peerInfoPlayer.netId;
								reconnectMessage.playerControllerId = peerInfoPlayer.playerControllerId;
								if (extraMessage != null)
								{
									NetworkWriter networkWriter2 = new NetworkWriter();
									extraMessage.Serialize(networkWriter2);
									reconnectMessage.msgData = networkWriter2.ToArray();
									reconnectMessage.msgSize = (int)networkWriter2.Position;
								}
								ClientScene.s_ReadyConnection.Send(47, reconnectMessage);
							}
						}
					}
				}
				ClientScene.SetReconnectId(-1, null);
			}
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F10 File Offset: 0x00001110
		public static bool RemovePlayer(short playerControllerId)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::RemovePlayer() for ID ",
					playerControllerId,
					" called with connection [",
					ClientScene.s_ReadyConnection,
					"]"
				}));
			}
			PlayerController playerController;
			if (ClientScene.s_ReadyConnection.GetPlayerController(playerControllerId, out playerController))
			{
				RemovePlayerMessage removePlayerMessage = new RemovePlayerMessage();
				removePlayerMessage.playerControllerId = playerControllerId;
				ClientScene.s_ReadyConnection.Send(38, removePlayerMessage);
				ClientScene.s_ReadyConnection.RemovePlayerController(playerControllerId);
				ClientScene.s_LocalPlayers[(int)playerControllerId] = new PlayerController();
				Object.Destroy(playerController.gameObject);
				return true;
			}
			if (LogFilter.logError)
			{
				Debug.LogError("Failed to find player ID " + playerControllerId);
			}
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static bool Ready(NetworkConnection conn)
		{
			if (ClientScene.s_IsReady)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("A connection has already been set as ready. There can only be one.");
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::Ready() called with connection [" + conn + "]");
			}
			if (conn != null)
			{
				ReadyMessage msg = new ReadyMessage();
				conn.Send(35, msg);
				ClientScene.s_IsReady = true;
				ClientScene.s_ReadyConnection = conn;
				ClientScene.s_ReadyConnection.isReady = true;
				return true;
			}
			if (LogFilter.logError)
			{
				Debug.LogError("Ready() called with invalid connection object: conn=null");
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003068 File Offset: 0x00001268
		public static NetworkClient ConnectLocalServer()
		{
			LocalClient localClient = new LocalClient();
			NetworkServer.instance.ActivateLocalClientScene();
			localClient.InternalConnectLocalServer(true);
			return localClient;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003090 File Offset: 0x00001290
		internal static NetworkClient ReconnectLocalServer()
		{
			LocalClient localClient = new LocalClient();
			NetworkServer.instance.ActivateLocalClientScene();
			localClient.InternalConnectLocalServer(false);
			return localClient;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000030B8 File Offset: 0x000012B8
		internal static void ClearLocalPlayers()
		{
			ClientScene.s_LocalPlayers.Clear();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030C4 File Offset: 0x000012C4
		internal static void HandleClientDisconnect(NetworkConnection conn)
		{
			if (ClientScene.s_ReadyConnection == conn && ClientScene.s_IsReady)
			{
				ClientScene.s_IsReady = false;
				ClientScene.s_ReadyConnection = null;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030E8 File Offset: 0x000012E8
		internal static void PrepareToSpawnSceneObjects()
		{
			ClientScene.s_SpawnableObjects = new Dictionary<NetworkSceneId, NetworkIdentity>();
			foreach (NetworkIdentity networkIdentity in Resources.FindObjectsOfTypeAll<NetworkIdentity>())
			{
				if (!networkIdentity.gameObject.activeSelf)
				{
					if (networkIdentity.gameObject.hideFlags != HideFlags.NotEditable && networkIdentity.gameObject.hideFlags != HideFlags.HideAndDontSave)
					{
						if (!networkIdentity.sceneId.IsEmpty())
						{
							ClientScene.s_SpawnableObjects[networkIdentity.sceneId] = networkIdentity;
							if (LogFilter.logDebug)
							{
								Debug.Log("ClientScene::PrepareSpawnObjects sceneId:" + networkIdentity.sceneId);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000031A8 File Offset: 0x000013A8
		internal static NetworkIdentity SpawnSceneObject(NetworkSceneId sceneId)
		{
			if (ClientScene.s_SpawnableObjects.ContainsKey(sceneId))
			{
				NetworkIdentity result = ClientScene.s_SpawnableObjects[sceneId];
				ClientScene.s_SpawnableObjects.Remove(sceneId);
				return result;
			}
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000031E0 File Offset: 0x000013E0
		internal static void RegisterSystemHandlers(NetworkClient client, bool localClient)
		{
			if (localClient)
			{
				client.RegisterHandlerSafe(1, new NetworkMessageDelegate(ClientScene.OnLocalClientObjectDestroy));
				client.RegisterHandlerSafe(13, new NetworkMessageDelegate(ClientScene.OnLocalClientObjectHide));
				client.RegisterHandlerSafe(3, new NetworkMessageDelegate(ClientScene.OnLocalClientObjectSpawn));
				client.RegisterHandlerSafe(10, new NetworkMessageDelegate(ClientScene.OnLocalClientObjectSpawnScene));
				client.RegisterHandlerSafe(15, new NetworkMessageDelegate(ClientScene.OnClientAuthority));
			}
			else
			{
				client.RegisterHandlerSafe(3, new NetworkMessageDelegate(ClientScene.OnObjectSpawn));
				client.RegisterHandlerSafe(10, new NetworkMessageDelegate(ClientScene.OnObjectSpawnScene));
				client.RegisterHandlerSafe(12, new NetworkMessageDelegate(ClientScene.OnObjectSpawnFinished));
				client.RegisterHandlerSafe(1, new NetworkMessageDelegate(ClientScene.OnObjectDestroy));
				client.RegisterHandlerSafe(13, new NetworkMessageDelegate(ClientScene.OnObjectDestroy));
				client.RegisterHandlerSafe(8, new NetworkMessageDelegate(ClientScene.OnUpdateVarsMessage));
				client.RegisterHandlerSafe(4, new NetworkMessageDelegate(ClientScene.OnOwnerMessage));
				client.RegisterHandlerSafe(9, new NetworkMessageDelegate(ClientScene.OnSyncListMessage));
				client.RegisterHandlerSafe(40, new NetworkMessageDelegate(NetworkAnimator.OnAnimationClientMessage));
				client.RegisterHandlerSafe(41, new NetworkMessageDelegate(NetworkAnimator.OnAnimationParametersClientMessage));
				client.RegisterHandlerSafe(15, new NetworkMessageDelegate(ClientScene.OnClientAuthority));
			}
			client.RegisterHandlerSafe(2, new NetworkMessageDelegate(ClientScene.OnRPCMessage));
			client.RegisterHandlerSafe(7, new NetworkMessageDelegate(ClientScene.OnSyncEventMessage));
			client.RegisterHandlerSafe(42, new NetworkMessageDelegate(NetworkAnimator.OnAnimationTriggerClientMessage));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000336C File Offset: 0x0000156C
		internal static string GetStringForAssetId(NetworkHash128 assetId)
		{
			GameObject gameObject;
			if (NetworkScene.GetPrefab(assetId, out gameObject))
			{
				return gameObject.name;
			}
			SpawnDelegate spawnDelegate;
			if (NetworkScene.GetSpawnHandler(assetId, out spawnDelegate))
			{
				return spawnDelegate.Method.Name;
			}
			return "unknown";
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000033AC File Offset: 0x000015AC
		public static void RegisterPrefab(GameObject prefab, NetworkHash128 newAssetId)
		{
			NetworkScene.RegisterPrefab(prefab, newAssetId);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void RegisterPrefab(GameObject prefab)
		{
			NetworkScene.RegisterPrefab(prefab);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000033C0 File Offset: 0x000015C0
		public static void RegisterPrefab(GameObject prefab, SpawnDelegate spawnHandler, UnSpawnDelegate unspawnHandler)
		{
			NetworkScene.RegisterPrefab(prefab, spawnHandler, unspawnHandler);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000033CC File Offset: 0x000015CC
		public static void UnregisterPrefab(GameObject prefab)
		{
			NetworkScene.UnregisterPrefab(prefab);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000033D4 File Offset: 0x000015D4
		public static void RegisterSpawnHandler(NetworkHash128 assetId, SpawnDelegate spawnHandler, UnSpawnDelegate unspawnHandler)
		{
			NetworkScene.RegisterSpawnHandler(assetId, spawnHandler, unspawnHandler);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000033E0 File Offset: 0x000015E0
		public static void UnregisterSpawnHandler(NetworkHash128 assetId)
		{
			NetworkScene.UnregisterSpawnHandler(assetId);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000033E8 File Offset: 0x000015E8
		public static void ClearSpawners()
		{
			NetworkScene.ClearSpawners();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000033F0 File Offset: 0x000015F0
		public static void DestroyAllClientObjects()
		{
			ClientScene.s_NetworkScene.DestroyAllClientObjects();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000033FC File Offset: 0x000015FC
		public static void SetLocalObject(NetworkInstanceId netId, GameObject obj)
		{
			ClientScene.s_NetworkScene.SetLocalObject(netId, obj, ClientScene.s_IsSpawnFinished, false);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003410 File Offset: 0x00001610
		public static GameObject FindLocalObject(NetworkInstanceId netId)
		{
			return ClientScene.s_NetworkScene.FindLocalObject(netId);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003420 File Offset: 0x00001620
		private static void ApplySpawnPayload(NetworkIdentity uv, Vector3 position, byte[] payload, NetworkInstanceId netId, GameObject newGameObject)
		{
			if (!uv.gameObject.activeSelf)
			{
				uv.gameObject.SetActive(true);
			}
			uv.transform.position = position;
			if (payload != null && payload.Length > 0)
			{
				NetworkReader reader = new NetworkReader(payload);
				uv.OnUpdateVars(reader, true);
			}
			if (newGameObject == null)
			{
				return;
			}
			newGameObject.SetActive(true);
			uv.SetNetworkInstanceId(netId);
			ClientScene.SetLocalObject(netId, newGameObject);
			if (ClientScene.s_IsSpawnFinished)
			{
				uv.OnStartClient();
				ClientScene.CheckForOwner(uv);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000034B0 File Offset: 0x000016B0
		private static void OnObjectSpawn(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectSpawnMessage>(ClientScene.s_ObjectSpawnMessage);
			if (!ClientScene.s_ObjectSpawnMessage.assetId.IsValid())
			{
				if (LogFilter.logError)
				{
					Debug.LogError("OnObjSpawn netId: " + ClientScene.s_ObjectSpawnMessage.netId + " has invalid asset Id");
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Client spawn handler instantiating [netId:",
					ClientScene.s_ObjectSpawnMessage.netId,
					" asset ID:",
					ClientScene.s_ObjectSpawnMessage.assetId,
					" pos:",
					ClientScene.s_ObjectSpawnMessage.position,
					"]"
				}));
			}
			NetworkIdentity component;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectSpawnMessage.netId, out component))
			{
				ClientScene.ApplySpawnPayload(component, ClientScene.s_ObjectSpawnMessage.position, ClientScene.s_ObjectSpawnMessage.payload, ClientScene.s_ObjectSpawnMessage.netId, null);
				return;
			}
			GameObject original;
			SpawnDelegate spawnDelegate;
			if (NetworkScene.GetPrefab(ClientScene.s_ObjectSpawnMessage.assetId, out original))
			{
				GameObject gameObject = (GameObject)Object.Instantiate(original, ClientScene.s_ObjectSpawnMessage.position, Quaternion.identity);
				component = gameObject.GetComponent<NetworkIdentity>();
				if (component == null)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("Client object spawned for " + ClientScene.s_ObjectSpawnMessage.assetId + " does not have a NetworkIdentity");
					}
					return;
				}
				ClientScene.ApplySpawnPayload(component, ClientScene.s_ObjectSpawnMessage.position, ClientScene.s_ObjectSpawnMessage.payload, ClientScene.s_ObjectSpawnMessage.netId, gameObject);
			}
			else if (NetworkScene.GetSpawnHandler(ClientScene.s_ObjectSpawnMessage.assetId, out spawnDelegate))
			{
				GameObject gameObject2 = spawnDelegate(ClientScene.s_ObjectSpawnMessage.position, ClientScene.s_ObjectSpawnMessage.assetId);
				if (gameObject2 == null)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("Client spawn handler for " + ClientScene.s_ObjectSpawnMessage.assetId + " returned null");
					}
					return;
				}
				component = gameObject2.GetComponent<NetworkIdentity>();
				if (component == null)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("Client object spawned for " + ClientScene.s_ObjectSpawnMessage.assetId + " does not have a network identity");
					}
					return;
				}
				component.SetDynamicAssetId(ClientScene.s_ObjectSpawnMessage.assetId);
				ClientScene.ApplySpawnPayload(component, ClientScene.s_ObjectSpawnMessage.position, ClientScene.s_ObjectSpawnMessage.payload, ClientScene.s_ObjectSpawnMessage.netId, gameObject2);
			}
			else if (LogFilter.logError)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Failed to spawn server object, assetId=",
					ClientScene.s_ObjectSpawnMessage.assetId,
					" netId=",
					ClientScene.s_ObjectSpawnMessage.netId
				}));
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003790 File Offset: 0x00001990
		private static void OnObjectSpawnScene(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectSpawnSceneMessage>(ClientScene.s_ObjectSpawnSceneMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Client spawn scene handler instantiating [netId:",
					ClientScene.s_ObjectSpawnSceneMessage.netId,
					" sceneId:",
					ClientScene.s_ObjectSpawnSceneMessage.sceneId,
					" pos:",
					ClientScene.s_ObjectSpawnSceneMessage.position
				}));
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectSpawnSceneMessage.netId, out networkIdentity))
			{
				ClientScene.ApplySpawnPayload(networkIdentity, ClientScene.s_ObjectSpawnSceneMessage.position, ClientScene.s_ObjectSpawnSceneMessage.payload, ClientScene.s_ObjectSpawnSceneMessage.netId, networkIdentity.gameObject);
				return;
			}
			NetworkIdentity networkIdentity2 = ClientScene.SpawnSceneObject(ClientScene.s_ObjectSpawnSceneMessage.sceneId);
			if (networkIdentity2 == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Spawn scene object not found for " + ClientScene.s_ObjectSpawnSceneMessage.sceneId);
				}
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Client spawn for [netId:",
					ClientScene.s_ObjectSpawnSceneMessage.netId,
					"] [sceneId:",
					ClientScene.s_ObjectSpawnSceneMessage.sceneId,
					"] obj:",
					networkIdentity2.gameObject.name
				}));
			}
			ClientScene.ApplySpawnPayload(networkIdentity2, ClientScene.s_ObjectSpawnSceneMessage.position, ClientScene.s_ObjectSpawnSceneMessage.payload, ClientScene.s_ObjectSpawnSceneMessage.netId, networkIdentity2.gameObject);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000392C File Offset: 0x00001B2C
		private static void OnObjectSpawnFinished(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectSpawnFinishedMessage>(ClientScene.s_ObjectSpawnFinishedMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log("SpawnFinished:" + ClientScene.s_ObjectSpawnFinishedMessage.state);
			}
			if (ClientScene.s_ObjectSpawnFinishedMessage.state == 0U)
			{
				ClientScene.PrepareToSpawnSceneObjects();
				ClientScene.s_IsSpawnFinished = false;
				return;
			}
			foreach (NetworkIdentity networkIdentity in ClientScene.objects.Values)
			{
				if (!networkIdentity.isClient)
				{
					networkIdentity.OnStartClient();
					ClientScene.CheckForOwner(networkIdentity);
				}
			}
			ClientScene.s_IsSpawnFinished = true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039FC File Offset: 0x00001BFC
		private static void OnObjectDestroy(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectDestroyMessage>(ClientScene.s_ObjectDestroyMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::OnObjDestroy netId:" + ClientScene.s_ObjectDestroyMessage.netId);
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectDestroyMessage.netId, out networkIdentity))
			{
				networkIdentity.OnNetworkDestroy();
				if (!NetworkScene.InvokeUnSpawnHandler(networkIdentity.assetId, networkIdentity.gameObject))
				{
					if (networkIdentity.sceneId.IsEmpty())
					{
						Object.Destroy(networkIdentity.gameObject);
					}
					else
					{
						networkIdentity.gameObject.SetActive(false);
						ClientScene.s_SpawnableObjects[networkIdentity.sceneId] = networkIdentity;
					}
				}
				ClientScene.s_NetworkScene.RemoveLocalObject(ClientScene.s_ObjectDestroyMessage.netId);
			}
			else if (LogFilter.logDebug)
			{
				Debug.LogWarning("Did not find target for destroy message for " + ClientScene.s_ObjectDestroyMessage.netId);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003AF8 File Offset: 0x00001CF8
		private static void OnLocalClientObjectDestroy(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectDestroyMessage>(ClientScene.s_ObjectDestroyMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::OnLocalObjectObjDestroy netId:" + ClientScene.s_ObjectDestroyMessage.netId);
			}
			ClientScene.s_NetworkScene.RemoveLocalObject(ClientScene.s_ObjectDestroyMessage.netId);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B50 File Offset: 0x00001D50
		private static void OnLocalClientObjectHide(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectDestroyMessage>(ClientScene.s_ObjectDestroyMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::OnLocalObjectObjHide netId:" + ClientScene.s_ObjectDestroyMessage.netId);
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectDestroyMessage.netId, out networkIdentity))
			{
				networkIdentity.OnSetLocalVisibility(false);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BB4 File Offset: 0x00001DB4
		private static void OnLocalClientObjectSpawn(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectSpawnMessage>(ClientScene.s_ObjectSpawnMessage);
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectSpawnMessage.netId, out networkIdentity))
			{
				networkIdentity.OnSetLocalVisibility(true);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private static void OnLocalClientObjectSpawnScene(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ObjectSpawnSceneMessage>(ClientScene.s_ObjectSpawnSceneMessage);
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ObjectSpawnSceneMessage.netId, out networkIdentity))
			{
				networkIdentity.OnSetLocalVisibility(true);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003C2C File Offset: 0x00001E2C
		private static void OnUpdateVarsMessage(NetworkMessage netMsg)
		{
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::OnUpdateVarsMessage ",
					networkInstanceId,
					" channel:",
					netMsg.channelId
				}));
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(networkInstanceId, out networkIdentity))
			{
				networkIdentity.OnUpdateVars(netMsg.reader, false);
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("Did not find target for sync message for " + networkInstanceId);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003CCC File Offset: 0x00001ECC
		private static void OnRPCMessage(NetworkMessage netMsg)
		{
			int num = (int)netMsg.reader.ReadPackedUInt32();
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::OnRPCMessage hash:",
					num,
					" netId:",
					networkInstanceId
				}));
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(networkInstanceId, out networkIdentity))
			{
				networkIdentity.HandleRPC(num, netMsg.reader);
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("Did not find target for RPC message for " + networkInstanceId);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D70 File Offset: 0x00001F70
		private static void OnSyncEventMessage(NetworkMessage netMsg)
		{
			int cmdHash = (int)netMsg.reader.ReadPackedUInt32();
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::OnSyncEventMessage " + networkInstanceId);
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(networkInstanceId, out networkIdentity))
			{
				networkIdentity.HandleSyncEvent(cmdHash, netMsg.reader);
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("Did not find target for SyncEvent message for " + networkInstanceId);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003DF8 File Offset: 0x00001FF8
		private static void OnSyncListMessage(NetworkMessage netMsg)
		{
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			int cmdHash = (int)netMsg.reader.ReadPackedUInt32();
			if (LogFilter.logDebug)
			{
				Debug.Log("ClientScene::OnSyncListMessage " + networkInstanceId);
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(networkInstanceId, out networkIdentity))
			{
				networkIdentity.HandleSyncList(cmdHash, netMsg.reader);
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("Did not find target for SyncList message for " + networkInstanceId);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E80 File Offset: 0x00002080
		private static void OnClientAuthority(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<ClientAuthorityMessage>(ClientScene.s_ClientAuthorityMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::OnClientAuthority for  connectionId=",
					netMsg.conn.connectionId,
					" netId: ",
					ClientScene.s_ClientAuthorityMessage.netId
				}));
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_ClientAuthorityMessage.netId, out networkIdentity))
			{
				networkIdentity.HandleClientAuthority(ClientScene.s_ClientAuthorityMessage.authority);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003F14 File Offset: 0x00002114
		private static void OnOwnerMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<OwnerMessage>(ClientScene.s_OwnerMessage);
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ClientScene::OnOwnerMessage - connectionId=",
					netMsg.conn.connectionId,
					" netId: ",
					ClientScene.s_OwnerMessage.netId
				}));
			}
			PlayerController playerController;
			if (netMsg.conn.GetPlayerController(ClientScene.s_OwnerMessage.playerControllerId, out playerController))
			{
				playerController.unetView.SetNotLocalPlayer();
			}
			NetworkIdentity networkIdentity;
			if (ClientScene.s_NetworkScene.GetNetworkIdentity(ClientScene.s_OwnerMessage.netId, out networkIdentity))
			{
				networkIdentity.SetConnectionToServer(netMsg.conn);
				networkIdentity.SetLocalPlayer(ClientScene.s_OwnerMessage.playerControllerId);
				ClientScene.InternalAddPlayer(networkIdentity, ClientScene.s_OwnerMessage.playerControllerId);
			}
			else
			{
				ClientScene.PendingOwner item = new ClientScene.PendingOwner
				{
					netId = ClientScene.s_OwnerMessage.netId,
					playerControllerId = ClientScene.s_OwnerMessage.playerControllerId
				};
				ClientScene.s_PendingOwnerIds.Add(item);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004028 File Offset: 0x00002228
		private static void CheckForOwner(NetworkIdentity uv)
		{
			int i = 0;
			while (i < ClientScene.s_PendingOwnerIds.Count)
			{
				ClientScene.PendingOwner pendingOwner = ClientScene.s_PendingOwnerIds[i];
				if (pendingOwner.netId == uv.netId)
				{
					uv.SetConnectionToServer(ClientScene.s_ReadyConnection);
					uv.SetLocalPlayer(pendingOwner.playerControllerId);
					if (LogFilter.logDev)
					{
						Debug.Log("ClientScene::OnOwnerMessage - player=" + uv.gameObject.name);
					}
					if (ClientScene.s_ReadyConnection.connectionId < 0)
					{
						if (LogFilter.logError)
						{
							Debug.LogError("Owner message received on a local client.");
						}
						return;
					}
					ClientScene.InternalAddPlayer(uv, pendingOwner.playerControllerId);
					ClientScene.s_PendingOwnerIds.RemoveAt(i);
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0400001D RID: 29
		public const int ReconnectIdInvalid = -1;

		// Token: 0x0400001E RID: 30
		public const int ReconnectIdHost = 0;

		// Token: 0x0400001F RID: 31
		private static List<PlayerController> s_LocalPlayers = new List<PlayerController>();

		// Token: 0x04000020 RID: 32
		private static NetworkConnection s_ReadyConnection;

		// Token: 0x04000021 RID: 33
		private static Dictionary<NetworkSceneId, NetworkIdentity> s_SpawnableObjects;

		// Token: 0x04000022 RID: 34
		private static bool s_IsReady;

		// Token: 0x04000023 RID: 35
		private static bool s_IsSpawnFinished;

		// Token: 0x04000024 RID: 36
		private static NetworkScene s_NetworkScene = new NetworkScene();

		// Token: 0x04000025 RID: 37
		private static ObjectSpawnSceneMessage s_ObjectSpawnSceneMessage = new ObjectSpawnSceneMessage();

		// Token: 0x04000026 RID: 38
		private static ObjectSpawnFinishedMessage s_ObjectSpawnFinishedMessage = new ObjectSpawnFinishedMessage();

		// Token: 0x04000027 RID: 39
		private static ObjectDestroyMessage s_ObjectDestroyMessage = new ObjectDestroyMessage();

		// Token: 0x04000028 RID: 40
		private static ObjectSpawnMessage s_ObjectSpawnMessage = new ObjectSpawnMessage();

		// Token: 0x04000029 RID: 41
		private static OwnerMessage s_OwnerMessage = new OwnerMessage();

		// Token: 0x0400002A RID: 42
		private static ClientAuthorityMessage s_ClientAuthorityMessage = new ClientAuthorityMessage();

		// Token: 0x0400002B RID: 43
		private static int s_ReconnectId = -1;

		// Token: 0x0400002C RID: 44
		private static PeerInfoMessage[] s_Peers;

		// Token: 0x0400002D RID: 45
		private static List<ClientScene.PendingOwner> s_PendingOwnerIds = new List<ClientScene.PendingOwner>();

		// Token: 0x02000005 RID: 5
		private struct PendingOwner
		{
			// Token: 0x0400002E RID: 46
			public NetworkInstanceId netId;

			// Token: 0x0400002F RID: 47
			public short playerControllerId;
		}
	}
}
