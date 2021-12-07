using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Networking.NetworkSystem;

namespace UnityEngine.Networking
{
	// Token: 0x02000040 RID: 64
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Network/NetworkIdentity")]
	public sealed class NetworkIdentity : MonoBehaviour
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A524 File Offset: 0x00008724
		public bool isClient
		{
			get
			{
				return this.m_IsClient;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000A52C File Offset: 0x0000872C
		public bool isServer
		{
			get
			{
				return this.m_IsServer && NetworkServer.active && this.m_IsServer;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A55C File Offset: 0x0000875C
		public bool hasAuthority
		{
			get
			{
				return this.m_HasAuthority;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000A564 File Offset: 0x00008764
		public NetworkInstanceId netId
		{
			get
			{
				return this.m_NetId;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A56C File Offset: 0x0000876C
		public NetworkSceneId sceneId
		{
			get
			{
				return this.m_SceneId;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A574 File Offset: 0x00008774
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000A57C File Offset: 0x0000877C
		public bool serverOnly
		{
			get
			{
				return this.m_ServerOnly;
			}
			set
			{
				this.m_ServerOnly = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A588 File Offset: 0x00008788
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000A590 File Offset: 0x00008790
		public bool localPlayerAuthority
		{
			get
			{
				return this.m_LocalPlayerAuthority;
			}
			set
			{
				this.m_LocalPlayerAuthority = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A59C File Offset: 0x0000879C
		public NetworkConnection clientAuthorityOwner
		{
			get
			{
				return this.m_ClientAuthorityOwner;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public NetworkHash128 assetId
		{
			get
			{
				return this.m_AssetId;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A5AC File Offset: 0x000087AC
		internal void SetDynamicAssetId(NetworkHash128 newAssetId)
		{
			if (!this.m_AssetId.IsValid() || this.m_AssetId.Equals(newAssetId))
			{
				this.m_AssetId = newAssetId;
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("SetDynamicAssetId object already has an assetId <" + this.m_AssetId + ">");
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A61C File Offset: 0x0000881C
		internal void SetClientOwner(NetworkConnection conn)
		{
			if (this.m_ClientAuthorityOwner != null && LogFilter.logError)
			{
				Debug.LogError("SetClientOwner m_ClientAuthorityOwner already set!");
			}
			this.m_ClientAuthorityOwner = conn;
			this.m_ClientAuthorityOwner.AddOwnedObject(this);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A65C File Offset: 0x0000885C
		internal void ClearClientOwner()
		{
			this.m_ClientAuthorityOwner = null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A668 File Offset: 0x00008868
		internal void ForceAuthority(bool authority)
		{
			if (this.m_HasAuthority == authority)
			{
				return;
			}
			this.m_HasAuthority = authority;
			if (authority)
			{
				this.OnStartAuthority();
			}
			else
			{
				this.OnStopAuthority();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A698 File Offset: 0x00008898
		public bool isLocalPlayer
		{
			get
			{
				return this.m_IsLocalPlayer;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public short playerControllerId
		{
			get
			{
				return this.m_PlayerId;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public NetworkConnection connectionToServer
		{
			get
			{
				return this.m_ConnectionToServer;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public NetworkConnection connectionToClient
		{
			get
			{
				return this.m_ConnectionToClient;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public ReadOnlyCollection<NetworkConnection> observers
		{
			get
			{
				if (this.m_Observers == null)
				{
					return null;
				}
				return new ReadOnlyCollection<NetworkConnection>(this.m_Observers);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A6D4 File Offset: 0x000088D4
		internal static NetworkInstanceId GetNextNetworkId()
		{
			uint value = NetworkIdentity.s_NextNetworkId;
			NetworkIdentity.s_NextNetworkId += 1U;
			return new NetworkInstanceId(value);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A6FC File Offset: 0x000088FC
		private void CacheBehaviours()
		{
			if (this.m_NetworkBehaviours == null)
			{
				this.m_NetworkBehaviours = base.GetComponents<NetworkBehaviour>();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A718 File Offset: 0x00008918
		internal static void AddNetworkId(uint id)
		{
			if (id >= NetworkIdentity.s_NextNetworkId)
			{
				NetworkIdentity.s_NextNetworkId = id + 1U;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A730 File Offset: 0x00008930
		internal void SetNetworkInstanceId(NetworkInstanceId newNetId)
		{
			this.m_NetId = newNetId;
			if (newNetId.Value == 0U)
			{
				this.m_IsServer = false;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A74C File Offset: 0x0000894C
		public void ForceSceneId(int newSceneId)
		{
			this.m_SceneId = new NetworkSceneId((uint)newSceneId);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A75C File Offset: 0x0000895C
		internal void UpdateClientServer(bool isClientFlag, bool isServerFlag)
		{
			this.m_IsClient = (this.m_IsClient || isClientFlag);
			this.m_IsServer = (this.m_IsServer || isServerFlag);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A77C File Offset: 0x0000897C
		internal void SetNoServer()
		{
			this.m_IsServer = false;
			this.SetNetworkInstanceId(NetworkInstanceId.Zero);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A790 File Offset: 0x00008990
		internal void SetNotLocalPlayer()
		{
			this.m_IsLocalPlayer = false;
			if (NetworkServer.active && NetworkServer.localClientActive)
			{
				return;
			}
			this.m_HasAuthority = false;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A7B8 File Offset: 0x000089B8
		internal void RemoveObserverInternal(NetworkConnection conn)
		{
			if (this.m_Observers != null)
			{
				this.m_Observers.Remove(conn);
				this.m_ObserverConnections.Remove(conn.connectionId);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A7F0 File Offset: 0x000089F0
		private void OnDestroy()
		{
			if (this.m_IsServer && NetworkServer.active)
			{
				NetworkServer.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A820 File Offset: 0x00008A20
		internal void OnStartServer(bool allowNonZeroNetId)
		{
			if (this.m_IsServer)
			{
				return;
			}
			this.m_IsServer = true;
			if (this.m_LocalPlayerAuthority)
			{
				this.m_HasAuthority = false;
			}
			else
			{
				this.m_HasAuthority = true;
			}
			this.m_Observers = new List<NetworkConnection>();
			this.m_ObserverConnections = new HashSet<int>();
			this.CacheBehaviours();
			if (this.netId.IsEmpty())
			{
				this.m_NetId = NetworkIdentity.GetNextNetworkId();
			}
			else if (!allowNonZeroNetId)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Object has non-zero netId ",
						this.netId,
						" for ",
						base.gameObject
					}));
				}
				return;
			}
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnStartServer ",
					base.gameObject,
					" GUID:",
					this.netId
				}));
			}
			NetworkServer.instance.SetLocalObjectOnServer(this.netId, base.gameObject);
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					networkBehaviour.OnStartServer();
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnStartServer:" + ex.Message + " " + ex.StackTrace);
				}
			}
			if (NetworkClient.active && NetworkServer.localClientActive)
			{
				ClientScene.SetLocalObject(this.netId, base.gameObject);
				this.OnStartClient();
			}
			if (this.m_HasAuthority)
			{
				this.OnStartAuthority();
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A9EC File Offset: 0x00008BEC
		internal void OnStartClient()
		{
			if (!this.m_IsClient)
			{
				this.m_IsClient = true;
			}
			this.CacheBehaviours();
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnStartClient ",
					base.gameObject,
					" GUID:",
					this.netId,
					" localPlayerAuthority:",
					this.localPlayerAuthority
				}));
			}
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					networkBehaviour.PreStartClient();
					networkBehaviour.OnStartClient();
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnStartClient:" + ex.Message + " " + ex.StackTrace);
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		internal void OnStartAuthority()
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					networkBehaviour.OnStartAuthority();
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnStartAuthority:" + ex.Message + " " + ex.StackTrace);
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000AB5C File Offset: 0x00008D5C
		internal void OnStopAuthority()
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					networkBehaviour.OnStopAuthority();
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnStopAuthority:" + ex.Message + " " + ex.StackTrace);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		internal void OnSetLocalVisibility(bool vis)
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					networkBehaviour.OnSetLocalVisibility(vis);
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnSetLocalVisibility:" + ex.Message + " " + ex.StackTrace);
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000AC58 File Offset: 0x00008E58
		internal bool OnCheckObserver(NetworkConnection conn)
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				try
				{
					if (!networkBehaviour.OnCheckObserver(conn))
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnCheckObserver:" + ex.Message + " " + ex.StackTrace);
				}
			}
			return true;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		internal void UNetSerializeAllVars(NetworkWriter writer)
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				networkBehaviour.OnSerialize(writer, true);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000AD1C File Offset: 0x00008F1C
		internal void HandleClientAuthority(bool authority)
		{
			if (!this.localPlayerAuthority)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleClientAuthority " + base.gameObject + " does not have localPlayerAuthority");
				}
				return;
			}
			this.ForceAuthority(authority);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AD60 File Offset: 0x00008F60
		private bool GetInvokeComponent(int cmdHash, Type invokeClass, out NetworkBehaviour invokeComponent)
		{
			NetworkBehaviour networkBehaviour = null;
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour2 = this.m_NetworkBehaviours[i];
				if (networkBehaviour2.GetType() == invokeClass || networkBehaviour2.GetType().IsSubclassOf(invokeClass))
				{
					networkBehaviour = networkBehaviour2;
					break;
				}
			}
			if (networkBehaviour == null)
			{
				string cmdHashHandlerName = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Found no behaviour for incoming [",
						cmdHashHandlerName,
						"] on ",
						base.gameObject,
						",  the server and client should have the same NetworkBehaviour instances [netId=",
						this.netId,
						"]."
					}));
				}
				invokeComponent = null;
				return false;
			}
			invokeComponent = networkBehaviour;
			return true;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AE28 File Offset: 0x00009028
		internal void HandleSyncEvent(int cmdHash, NetworkReader reader)
		{
			if (base.gameObject == null)
			{
				string cmdHashHandlerName = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"SyncEvent [",
						cmdHashHandlerName,
						"] received for deleted object [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			Type invokeClass;
			NetworkBehaviour.CmdDelegate cmdDelegate;
			if (!NetworkBehaviour.GetInvokerForHashSyncEvent(cmdHash, out invokeClass, out cmdDelegate))
			{
				string cmdHashHandlerName2 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Found no receiver for incoming [",
						cmdHashHandlerName2,
						"] on ",
						base.gameObject,
						",  the server and client should have the same NetworkBehaviour instances [netId=",
						this.netId,
						"]."
					}));
				}
				return;
			}
			NetworkBehaviour obj;
			if (!this.GetInvokeComponent(cmdHash, invokeClass, out obj))
			{
				string cmdHashHandlerName3 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"SyncEvent [",
						cmdHashHandlerName3,
						"] handler not found [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			cmdDelegate(obj, reader);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000AF68 File Offset: 0x00009168
		internal void HandleSyncList(int cmdHash, NetworkReader reader)
		{
			if (base.gameObject == null)
			{
				string cmdHashHandlerName = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"SyncList [",
						cmdHashHandlerName,
						"] received for deleted object [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			Type invokeClass;
			NetworkBehaviour.CmdDelegate cmdDelegate;
			if (!NetworkBehaviour.GetInvokerForHashSyncList(cmdHash, out invokeClass, out cmdDelegate))
			{
				string cmdHashHandlerName2 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Found no receiver for incoming [",
						cmdHashHandlerName2,
						"] on ",
						base.gameObject,
						",  the server and client should have the same NetworkBehaviour instances [netId=",
						this.netId,
						"]."
					}));
				}
				return;
			}
			NetworkBehaviour obj;
			if (!this.GetInvokeComponent(cmdHash, invokeClass, out obj))
			{
				string cmdHashHandlerName3 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"SyncList [",
						cmdHashHandlerName3,
						"] handler not found [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			cmdDelegate(obj, reader);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B0A8 File Offset: 0x000092A8
		internal void HandleCommand(int cmdHash, NetworkReader reader)
		{
			if (base.gameObject == null)
			{
				string cmdHashHandlerName = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Command [",
						cmdHashHandlerName,
						"] received for deleted object [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			Type invokeClass;
			NetworkBehaviour.CmdDelegate cmdDelegate;
			if (!NetworkBehaviour.GetInvokerForHashCommand(cmdHash, out invokeClass, out cmdDelegate))
			{
				string cmdHashHandlerName2 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Found no receiver for incoming [",
						cmdHashHandlerName2,
						"] on ",
						base.gameObject,
						",  the server and client should have the same NetworkBehaviour instances [netId=",
						this.netId,
						"]."
					}));
				}
				return;
			}
			NetworkBehaviour obj;
			if (!this.GetInvokeComponent(cmdHash, invokeClass, out obj))
			{
				string cmdHashHandlerName3 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Command [",
						cmdHashHandlerName3,
						"] handler not found [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			cmdDelegate(obj, reader);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B1E8 File Offset: 0x000093E8
		internal void HandleRPC(int cmdHash, NetworkReader reader)
		{
			if (base.gameObject == null)
			{
				string cmdHashHandlerName = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"ClientRpc [",
						cmdHashHandlerName,
						"] received for deleted object [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			Type invokeClass;
			NetworkBehaviour.CmdDelegate cmdDelegate;
			if (!NetworkBehaviour.GetInvokerForHashClientRpc(cmdHash, out invokeClass, out cmdDelegate))
			{
				string cmdHashHandlerName2 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Found no receiver for incoming [",
						cmdHashHandlerName2,
						"] on ",
						base.gameObject,
						",  the server and client should have the same NetworkBehaviour instances [netId=",
						this.netId,
						"]."
					}));
				}
				return;
			}
			NetworkBehaviour obj;
			if (!this.GetInvokeComponent(cmdHash, invokeClass, out obj))
			{
				string cmdHashHandlerName3 = NetworkBehaviour.GetCmdHashHandlerName(cmdHash);
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"ClientRpc [",
						cmdHashHandlerName3,
						"] handler not found [netId=",
						this.netId,
						"]"
					}));
				}
				return;
			}
			cmdDelegate(obj, reader);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B328 File Offset: 0x00009528
		internal void UNetUpdate()
		{
			uint num = 0U;
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				int dirtyChannel = networkBehaviour.GetDirtyChannel();
				if (dirtyChannel != -1)
				{
					num |= 1U << dirtyChannel;
				}
			}
			if (num == 0U)
			{
				return;
			}
			for (int j = 0; j < NetworkServer.numChannels; j++)
			{
				if ((num & 1U << j) != 0U)
				{
					NetworkIdentity.s_UpdateWriter.StartMessage(8);
					NetworkIdentity.s_UpdateWriter.Write(this.netId);
					bool flag = false;
					for (int k = 0; k < this.m_NetworkBehaviours.Length; k++)
					{
						short position = NetworkIdentity.s_UpdateWriter.Position;
						NetworkBehaviour networkBehaviour2 = this.m_NetworkBehaviours[k];
						if (networkBehaviour2.GetDirtyChannel() != j)
						{
							networkBehaviour2.OnSerialize(NetworkIdentity.s_UpdateWriter, false);
						}
						else
						{
							if (networkBehaviour2.OnSerialize(NetworkIdentity.s_UpdateWriter, false))
							{
								networkBehaviour2.ClearAllDirtyBits();
								flag = true;
							}
							if (NetworkIdentity.s_UpdateWriter.Position - position > (short)NetworkServer.maxPacketSize && LogFilter.logWarn)
							{
								Debug.LogWarning(string.Concat(new object[]
								{
									"Large state update of ",
									(int)(NetworkIdentity.s_UpdateWriter.Position - position),
									" bytes for netId:",
									this.netId,
									" from script:",
									networkBehaviour2
								}));
							}
						}
					}
					if (flag)
					{
						NetworkIdentity.s_UpdateWriter.FinishMessage();
						NetworkServer.SendWriterToReady(base.gameObject, NetworkIdentity.s_UpdateWriter, j);
					}
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B4C8 File Offset: 0x000096C8
		internal void OnUpdateVars(NetworkReader reader, bool initialState)
		{
			if (initialState && this.m_NetworkBehaviours == null)
			{
				this.m_NetworkBehaviours = base.GetComponents<NetworkBehaviour>();
			}
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				networkBehaviour.OnDeserialize(reader, initialState);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B51C File Offset: 0x0000971C
		internal void SetLocalPlayer(short localPlayerControllerId)
		{
			this.m_IsLocalPlayer = true;
			this.m_PlayerId = localPlayerControllerId;
			if (this.localPlayerAuthority)
			{
				this.m_HasAuthority = true;
			}
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				networkBehaviour.OnStartLocalPlayer();
				if (this.localPlayerAuthority)
				{
					networkBehaviour.OnStartAuthority();
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B584 File Offset: 0x00009784
		internal void SetConnectionToServer(NetworkConnection conn)
		{
			this.m_ConnectionToServer = conn;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B590 File Offset: 0x00009790
		internal void SetConnectionToClient(NetworkConnection conn, short newPlayerControllerId)
		{
			this.m_PlayerId = newPlayerControllerId;
			this.m_ConnectionToClient = conn;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B5A0 File Offset: 0x000097A0
		internal void OnNetworkDestroy()
		{
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				networkBehaviour.OnNetworkDestroy();
			}
			this.m_IsServer = false;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B5DC File Offset: 0x000097DC
		internal void ClearObservers()
		{
			if (this.m_Observers != null)
			{
				int count = this.m_Observers.Count;
				for (int i = 0; i < count; i++)
				{
					NetworkConnection networkConnection = this.m_Observers[i];
					networkConnection.RemoveFromVisList(this, true);
				}
				this.m_Observers.Clear();
				this.m_ObserverConnections.Clear();
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B640 File Offset: 0x00009840
		internal void AddObserver(NetworkConnection conn)
		{
			if (this.m_Observers == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AddObserver for " + base.gameObject + " observer list is null");
				}
				return;
			}
			if (this.m_ObserverConnections.Contains(conn.connectionId))
			{
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Duplicate observer ",
						conn.address,
						" added for ",
						base.gameObject
					}));
				}
				return;
			}
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Added observer ",
					conn.address,
					" added for ",
					base.gameObject
				}));
			}
			this.m_Observers.Add(conn);
			this.m_ObserverConnections.Add(conn.connectionId);
			conn.AddToVisList(this);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B734 File Offset: 0x00009934
		internal void RemoveObserver(NetworkConnection conn)
		{
			if (this.m_Observers == null)
			{
				return;
			}
			this.m_Observers.Remove(conn);
			this.m_ObserverConnections.Remove(conn.connectionId);
			conn.RemoveFromVisList(this, false);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B76C File Offset: 0x0000996C
		public void RebuildObservers(bool initialize)
		{
			if (this.m_Observers == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			HashSet<NetworkConnection> hashSet = new HashSet<NetworkConnection>();
			HashSet<NetworkConnection> hashSet2 = new HashSet<NetworkConnection>(this.m_Observers);
			for (int i = 0; i < this.m_NetworkBehaviours.Length; i++)
			{
				NetworkBehaviour networkBehaviour = this.m_NetworkBehaviours[i];
				flag2 |= networkBehaviour.OnRebuildObservers(hashSet, initialize);
			}
			if (!flag2)
			{
				if (initialize)
				{
					foreach (NetworkConnection networkConnection in NetworkServer.connections)
					{
						if (networkConnection != null)
						{
							if (networkConnection.isReady)
							{
								this.AddObserver(networkConnection);
							}
						}
					}
					foreach (NetworkConnection networkConnection2 in NetworkServer.localConnections)
					{
						if (networkConnection2 != null)
						{
							if (networkConnection2.isReady)
							{
								this.AddObserver(networkConnection2);
							}
						}
					}
				}
				return;
			}
			foreach (NetworkConnection networkConnection3 in hashSet)
			{
				if (networkConnection3 != null)
				{
					if (!networkConnection3.isReady)
					{
						if (LogFilter.logWarn)
						{
							Debug.LogWarning(string.Concat(new object[]
							{
								"Observer is not ready for ",
								base.gameObject,
								" ",
								networkConnection3
							}));
						}
					}
					else if (initialize || !hashSet2.Contains(networkConnection3))
					{
						networkConnection3.AddToVisList(this);
						if (LogFilter.logDebug)
						{
							Debug.Log(string.Concat(new object[]
							{
								"New Observer for ",
								base.gameObject,
								" ",
								networkConnection3
							}));
						}
						flag = true;
					}
				}
			}
			foreach (NetworkConnection networkConnection4 in hashSet2)
			{
				if (!hashSet.Contains(networkConnection4))
				{
					networkConnection4.RemoveFromVisList(this, false);
					if (LogFilter.logDebug)
					{
						Debug.Log(string.Concat(new object[]
						{
							"Removed Observer for ",
							base.gameObject,
							" ",
							networkConnection4
						}));
					}
					flag = true;
				}
			}
			if (initialize)
			{
				foreach (NetworkConnection item in NetworkServer.localConnections)
				{
					if (!hashSet.Contains(item))
					{
						this.OnSetLocalVisibility(false);
					}
				}
			}
			if (!flag)
			{
				return;
			}
			this.m_Observers = new List<NetworkConnection>(hashSet);
			this.m_ObserverConnections.Clear();
			foreach (NetworkConnection networkConnection5 in this.m_Observers)
			{
				this.m_ObserverConnections.Add(networkConnection5.connectionId);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BB40 File Offset: 0x00009D40
		public bool RemoveClientAuthority(NetworkConnection conn)
		{
			if (!this.isServer)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RemoveClientAuthority can only be call on the server for spawned objects.");
				}
				return false;
			}
			if (this.connectionToClient != null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RemoveClientAuthority cannot remove authority for a player object");
				}
				return false;
			}
			if (this.m_ClientAuthorityOwner == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RemoveClientAuthority for " + base.gameObject + " has no clientAuthority owner.");
				}
				return false;
			}
			if (this.m_ClientAuthorityOwner != conn)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("RemoveClientAuthority for " + base.gameObject + " has different owner.");
				}
				return false;
			}
			this.m_ClientAuthorityOwner.RemoveOwnedObject(this);
			this.m_ClientAuthorityOwner = null;
			this.ForceAuthority(true);
			conn.Send(15, new ClientAuthorityMessage
			{
				netId = this.netId,
				authority = false
			});
			if (NetworkIdentity.clientAuthorityCallback != null)
			{
				NetworkIdentity.clientAuthorityCallback(conn, this, false);
			}
			return true;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BC48 File Offset: 0x00009E48
		public bool AssignClientAuthority(NetworkConnection conn)
		{
			if (!this.isServer)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AssignClientAuthority can only be call on the server for spawned objects.");
				}
				return false;
			}
			if (!this.localPlayerAuthority)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AssignClientAuthority can only be used for NetworkIdentity component with LocalPlayerAuthority set.");
				}
				return false;
			}
			if (this.m_ClientAuthorityOwner != null && conn != this.m_ClientAuthorityOwner)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AssignClientAuthority for " + base.gameObject + " already has an owner. Use RemoveClientAuthority() first.");
				}
				return false;
			}
			if (conn == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("AssignClientAuthority for " + base.gameObject + " owner cannot be null. Use RemoveClientAuthority() instead.");
				}
				return false;
			}
			this.m_ClientAuthorityOwner = conn;
			this.m_ClientAuthorityOwner.AddOwnedObject(this);
			this.ForceAuthority(false);
			conn.Send(15, new ClientAuthorityMessage
			{
				netId = this.netId,
				authority = true
			});
			if (NetworkIdentity.clientAuthorityCallback != null)
			{
				NetworkIdentity.clientAuthorityCallback(conn, this, true);
			}
			return true;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BD58 File Offset: 0x00009F58
		internal static void UNetStaticUpdate()
		{
			NetworkServer.Update();
			NetworkClient.UpdateClients();
			NetworkManager.UpdateScene();
		}

		// Token: 0x0400010A RID: 266
		[SerializeField]
		private NetworkSceneId m_SceneId;

		// Token: 0x0400010B RID: 267
		[SerializeField]
		private NetworkHash128 m_AssetId;

		// Token: 0x0400010C RID: 268
		[SerializeField]
		private bool m_ServerOnly;

		// Token: 0x0400010D RID: 269
		[SerializeField]
		private bool m_LocalPlayerAuthority;

		// Token: 0x0400010E RID: 270
		private bool m_IsClient;

		// Token: 0x0400010F RID: 271
		private bool m_IsServer;

		// Token: 0x04000110 RID: 272
		private bool m_HasAuthority;

		// Token: 0x04000111 RID: 273
		private NetworkInstanceId m_NetId;

		// Token: 0x04000112 RID: 274
		private bool m_IsLocalPlayer;

		// Token: 0x04000113 RID: 275
		private NetworkConnection m_ConnectionToServer;

		// Token: 0x04000114 RID: 276
		private NetworkConnection m_ConnectionToClient;

		// Token: 0x04000115 RID: 277
		private short m_PlayerId = -1;

		// Token: 0x04000116 RID: 278
		private NetworkBehaviour[] m_NetworkBehaviours;

		// Token: 0x04000117 RID: 279
		private HashSet<int> m_ObserverConnections;

		// Token: 0x04000118 RID: 280
		private List<NetworkConnection> m_Observers;

		// Token: 0x04000119 RID: 281
		private NetworkConnection m_ClientAuthorityOwner;

		// Token: 0x0400011A RID: 282
		private static uint s_NextNetworkId = 1U;

		// Token: 0x0400011B RID: 283
		private static NetworkWriter s_UpdateWriter = new NetworkWriter();

		// Token: 0x0400011C RID: 284
		public static NetworkIdentity.ClientAuthorityCallback clientAuthorityCallback;

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x06000532 RID: 1330
		public delegate void ClientAuthorityCallback(NetworkConnection conn, NetworkIdentity uv, bool authorityState);
	}
}
