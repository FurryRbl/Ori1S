using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine.Networking
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("")]
	[RequireComponent(typeof(NetworkIdentity))]
	public class NetworkBehaviour : MonoBehaviour
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005FA8 File Offset: 0x000041A8
		public bool localPlayerAuthority
		{
			get
			{
				return this.myView.localPlayerAuthority;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005FB8 File Offset: 0x000041B8
		public bool isServer
		{
			get
			{
				return this.myView.isServer;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005FC8 File Offset: 0x000041C8
		public bool isClient
		{
			get
			{
				return this.myView.isClient;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005FD8 File Offset: 0x000041D8
		public bool isLocalPlayer
		{
			get
			{
				return this.myView.isLocalPlayer;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005FE8 File Offset: 0x000041E8
		public bool hasAuthority
		{
			get
			{
				return this.myView.hasAuthority;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005FF8 File Offset: 0x000041F8
		public NetworkInstanceId netId
		{
			get
			{
				return this.myView.netId;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006008 File Offset: 0x00004208
		public NetworkConnection connectionToServer
		{
			get
			{
				return this.myView.connectionToServer;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006018 File Offset: 0x00004218
		public NetworkConnection connectionToClient
		{
			get
			{
				return this.myView.connectionToClient;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006028 File Offset: 0x00004228
		public short playerControllerId
		{
			get
			{
				return this.myView.playerControllerId;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006038 File Offset: 0x00004238
		protected uint syncVarDirtyBits
		{
			get
			{
				return this.m_SyncVarDirtyBits;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006040 File Offset: 0x00004240
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00006048 File Offset: 0x00004248
		protected bool syncVarHookGuard
		{
			get
			{
				return this.m_SyncVarGuard;
			}
			set
			{
				this.m_SyncVarGuard = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006054 File Offset: 0x00004254
		private NetworkIdentity myView
		{
			get
			{
				if (this.m_MyView == null)
				{
					this.m_MyView = base.GetComponent<NetworkIdentity>();
					if (this.m_MyView == null && LogFilter.logError)
					{
						Debug.LogError("There is no NetworkIdentity on this object. Please add one.");
					}
					return this.m_MyView;
				}
				return this.m_MyView;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000060B0 File Offset: 0x000042B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SendCommandInternal(NetworkWriter writer, int channelId, string cmdName)
		{
			if (!this.isLocalPlayer && !this.hasAuthority)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Trying to send command for object without authority.");
				}
				return;
			}
			if (ClientScene.readyConnection == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Send command attempted with no client running [client=" + this.connectionToServer + "].");
				}
				return;
			}
			writer.FinishMessage();
			ClientScene.readyConnection.SendWriter(writer, channelId);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000612C File Offset: 0x0000432C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool InvokeCommand(int cmdHash, NetworkReader reader)
		{
			return this.InvokeCommandDelegate(cmdHash, reader);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006140 File Offset: 0x00004340
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SendRPCInternal(NetworkWriter writer, int channelId, string rpcName)
		{
			if (!this.isServer)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("ClientRpc call on un-spawned object");
				}
				return;
			}
			writer.FinishMessage();
			NetworkServer.SendWriterToReady(base.gameObject, writer, channelId);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006180 File Offset: 0x00004380
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool InvokeRPC(int cmdHash, NetworkReader reader)
		{
			return this.InvokeRpcDelegate(cmdHash, reader);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006194 File Offset: 0x00004394
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SendEventInternal(NetworkWriter writer, int channelId, string eventName)
		{
			if (!NetworkServer.active)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("SendEvent no server?");
				}
				return;
			}
			writer.FinishMessage();
			NetworkServer.SendWriterToReady(base.gameObject, writer, channelId);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000061D4 File Offset: 0x000043D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool InvokeSyncEvent(int cmdHash, NetworkReader reader)
		{
			return this.InvokeSyncEventDelegate(cmdHash, reader);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000061E8 File Offset: 0x000043E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool InvokeSyncList(int cmdHash, NetworkReader reader)
		{
			return this.InvokeSyncListDelegate(cmdHash, reader);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000061FC File Offset: 0x000043FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static void RegisterCommandDelegate(Type invokeClass, int cmdHash, NetworkBehaviour.CmdDelegate func)
		{
			if (NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return;
			}
			NetworkBehaviour.Invoker invoker = new NetworkBehaviour.Invoker();
			invoker.invokeType = NetworkBehaviour.UNetInvokeType.Command;
			invoker.invokeClass = invokeClass;
			invoker.invokeFunction = func;
			NetworkBehaviour.s_CmdHandlerDelegates[cmdHash] = invoker;
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterCommandDelegate hash:",
					cmdHash,
					" ",
					func.Method.Name
				}));
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006284 File Offset: 0x00004484
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static void RegisterRpcDelegate(Type invokeClass, int cmdHash, NetworkBehaviour.CmdDelegate func)
		{
			if (NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return;
			}
			NetworkBehaviour.Invoker invoker = new NetworkBehaviour.Invoker();
			invoker.invokeType = NetworkBehaviour.UNetInvokeType.ClientRpc;
			invoker.invokeClass = invokeClass;
			invoker.invokeFunction = func;
			NetworkBehaviour.s_CmdHandlerDelegates[cmdHash] = invoker;
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterRpcDelegate hash:",
					cmdHash,
					" ",
					func.Method.Name
				}));
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000630C File Offset: 0x0000450C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static void RegisterEventDelegate(Type invokeClass, int cmdHash, NetworkBehaviour.CmdDelegate func)
		{
			if (NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return;
			}
			NetworkBehaviour.Invoker invoker = new NetworkBehaviour.Invoker();
			invoker.invokeType = NetworkBehaviour.UNetInvokeType.SyncEvent;
			invoker.invokeClass = invokeClass;
			invoker.invokeFunction = func;
			NetworkBehaviour.s_CmdHandlerDelegates[cmdHash] = invoker;
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterEventDelegate hash:",
					cmdHash,
					" ",
					func.Method.Name
				}));
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006394 File Offset: 0x00004594
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected static void RegisterSyncListDelegate(Type invokeClass, int cmdHash, NetworkBehaviour.CmdDelegate func)
		{
			if (NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return;
			}
			NetworkBehaviour.Invoker invoker = new NetworkBehaviour.Invoker();
			invoker.invokeType = NetworkBehaviour.UNetInvokeType.SyncList;
			invoker.invokeClass = invokeClass;
			invoker.invokeFunction = func;
			NetworkBehaviour.s_CmdHandlerDelegates[cmdHash] = invoker;
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"RegisterSyncListDelegate hash:",
					cmdHash,
					" ",
					func.Method.Name
				}));
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000641C File Offset: 0x0000461C
		internal static string GetInvoker(int cmdHash)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return null;
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			return invoker.DebugString();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006450 File Offset: 0x00004650
		internal static bool GetInvokerForHashCommand(int cmdHash, out Type invokeClass, out NetworkBehaviour.CmdDelegate invokeFunction)
		{
			return NetworkBehaviour.GetInvokerForHash(cmdHash, NetworkBehaviour.UNetInvokeType.Command, out invokeClass, out invokeFunction);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000645C File Offset: 0x0000465C
		internal static bool GetInvokerForHashClientRpc(int cmdHash, out Type invokeClass, out NetworkBehaviour.CmdDelegate invokeFunction)
		{
			return NetworkBehaviour.GetInvokerForHash(cmdHash, NetworkBehaviour.UNetInvokeType.ClientRpc, out invokeClass, out invokeFunction);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006468 File Offset: 0x00004668
		internal static bool GetInvokerForHashSyncList(int cmdHash, out Type invokeClass, out NetworkBehaviour.CmdDelegate invokeFunction)
		{
			return NetworkBehaviour.GetInvokerForHash(cmdHash, NetworkBehaviour.UNetInvokeType.SyncList, out invokeClass, out invokeFunction);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006474 File Offset: 0x00004674
		internal static bool GetInvokerForHashSyncEvent(int cmdHash, out Type invokeClass, out NetworkBehaviour.CmdDelegate invokeFunction)
		{
			return NetworkBehaviour.GetInvokerForHash(cmdHash, NetworkBehaviour.UNetInvokeType.SyncEvent, out invokeClass, out invokeFunction);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006480 File Offset: 0x00004680
		private static bool GetInvokerForHash(int cmdHash, NetworkBehaviour.UNetInvokeType invokeType, out Type invokeClass, out NetworkBehaviour.CmdDelegate invokeFunction)
		{
			NetworkBehaviour.Invoker invoker = null;
			if (!NetworkBehaviour.s_CmdHandlerDelegates.TryGetValue(cmdHash, out invoker))
			{
				if (LogFilter.logDev)
				{
					Debug.Log("GetInvokerForHash hash:" + cmdHash + " not found");
				}
				invokeClass = null;
				invokeFunction = null;
				return false;
			}
			if (invoker == null)
			{
				if (LogFilter.logDev)
				{
					Debug.Log("GetInvokerForHash hash:" + cmdHash + " invoker null");
				}
				invokeClass = null;
				invokeFunction = null;
				return false;
			}
			if (invoker.invokeType != invokeType)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("GetInvokerForHash hash:" + cmdHash + " mismatched invokeType");
				}
				invokeClass = null;
				invokeFunction = null;
				return false;
			}
			invokeClass = invoker.invokeClass;
			invokeFunction = invoker.invokeFunction;
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006548 File Offset: 0x00004748
		internal static void DumpInvokers()
		{
			Debug.Log("DumpInvokers size:" + NetworkBehaviour.s_CmdHandlerDelegates.Count);
			foreach (KeyValuePair<int, NetworkBehaviour.Invoker> keyValuePair in NetworkBehaviour.s_CmdHandlerDelegates)
			{
				Debug.Log(string.Concat(new object[]
				{
					"  Invoker:",
					keyValuePair.Value.invokeClass,
					":",
					keyValuePair.Value.invokeFunction.Method.Name,
					" ",
					keyValuePair.Value.invokeType,
					" ",
					keyValuePair.Key
				}));
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006640 File Offset: 0x00004840
		internal bool ContainsCommandDelegate(int cmdHash)
		{
			return NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006650 File Offset: 0x00004850
		internal bool InvokeCommandDelegate(int cmdHash, NetworkReader reader)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return false;
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			if (invoker.invokeType != NetworkBehaviour.UNetInvokeType.Command)
			{
				return false;
			}
			if (base.GetType() != invoker.invokeClass)
			{
				if (!base.GetType().IsSubclassOf(invoker.invokeClass))
				{
					return false;
				}
			}
			invoker.invokeFunction(this, reader);
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000066C4 File Offset: 0x000048C4
		internal bool InvokeRpcDelegate(int cmdHash, NetworkReader reader)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return false;
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			if (invoker.invokeType != NetworkBehaviour.UNetInvokeType.ClientRpc)
			{
				return false;
			}
			if (base.GetType() != invoker.invokeClass)
			{
				if (!base.GetType().IsSubclassOf(invoker.invokeClass))
				{
					return false;
				}
			}
			invoker.invokeFunction(this, reader);
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000673C File Offset: 0x0000493C
		internal bool InvokeSyncEventDelegate(int cmdHash, NetworkReader reader)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return false;
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			if (invoker.invokeType != NetworkBehaviour.UNetInvokeType.SyncEvent)
			{
				return false;
			}
			invoker.invokeFunction(this, reader);
			return true;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006784 File Offset: 0x00004984
		internal bool InvokeSyncListDelegate(int cmdHash, NetworkReader reader)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return false;
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			if (invoker.invokeType != NetworkBehaviour.UNetInvokeType.SyncList)
			{
				return false;
			}
			if (base.GetType() != invoker.invokeClass)
			{
				return false;
			}
			invoker.invokeFunction(this, reader);
			return true;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000067E0 File Offset: 0x000049E0
		internal static string GetCmdHashHandlerName(int cmdHash)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return cmdHash.ToString();
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			return invoker.invokeType + ":" + invoker.invokeFunction.Method.Name;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006838 File Offset: 0x00004A38
		private static string GetCmdHashPrefixName(int cmdHash, string prefix)
		{
			if (!NetworkBehaviour.s_CmdHandlerDelegates.ContainsKey(cmdHash))
			{
				return cmdHash.ToString();
			}
			NetworkBehaviour.Invoker invoker = NetworkBehaviour.s_CmdHandlerDelegates[cmdHash];
			string text = invoker.invokeFunction.Method.Name;
			int num = text.IndexOf(prefix);
			if (num > -1)
			{
				text = text.Substring(prefix.Length);
			}
			return text;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006898 File Offset: 0x00004A98
		internal static string GetCmdHashCmdName(int cmdHash)
		{
			return NetworkBehaviour.GetCmdHashPrefixName(cmdHash, "InvokeCmd");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000068A8 File Offset: 0x00004AA8
		internal static string GetCmdHashRpcName(int cmdHash)
		{
			return NetworkBehaviour.GetCmdHashPrefixName(cmdHash, "InvokeRpc");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000068B8 File Offset: 0x00004AB8
		internal static string GetCmdHashEventName(int cmdHash)
		{
			return NetworkBehaviour.GetCmdHashPrefixName(cmdHash, "InvokeSyncEvent");
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000068C8 File Offset: 0x00004AC8
		internal static string GetCmdHashListName(int cmdHash)
		{
			return NetworkBehaviour.GetCmdHashPrefixName(cmdHash, "InvokeSyncList");
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000068D8 File Offset: 0x00004AD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SetSyncVarGameObject(GameObject newGameObject, ref GameObject gameObjectField, uint dirtyBit, ref NetworkInstanceId netIdField)
		{
			if (this.m_SyncVarGuard)
			{
				return;
			}
			NetworkInstanceId networkInstanceId = default(NetworkInstanceId);
			if (newGameObject != null)
			{
				NetworkIdentity component = newGameObject.GetComponent<NetworkIdentity>();
				if (component != null)
				{
					networkInstanceId = component.netId;
					if (networkInstanceId.IsEmpty() && LogFilter.logWarn)
					{
						Debug.LogWarning("SetSyncVarGameObject GameObject " + newGameObject + " has a zero netId. Maybe it is not spawned yet?");
					}
				}
			}
			NetworkInstanceId networkInstanceId2 = default(NetworkInstanceId);
			if (gameObjectField != null)
			{
				networkInstanceId2 = gameObjectField.GetComponent<NetworkIdentity>().netId;
			}
			if (networkInstanceId != networkInstanceId2)
			{
				if (LogFilter.logDev)
				{
					Debug.Log(string.Concat(new object[]
					{
						"SetSyncVar GameObject ",
						base.GetType().Name,
						" bit [",
						dirtyBit,
						"] netfieldId:",
						networkInstanceId2,
						"->",
						networkInstanceId
					}));
				}
				this.SetDirtyBit(dirtyBit);
				gameObjectField = newGameObject;
				netIdField = networkInstanceId;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000069F0 File Offset: 0x00004BF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void SetSyncVar<T>(T value, ref T fieldValue, uint dirtyBit)
		{
			if (!value.Equals(fieldValue))
			{
				if (LogFilter.logDev)
				{
					Debug.Log(string.Concat(new object[]
					{
						"SetSyncVar ",
						base.GetType().Name,
						" bit [",
						dirtyBit,
						"] ",
						fieldValue,
						"->",
						value
					}));
				}
				this.SetDirtyBit(dirtyBit);
				fieldValue = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006A90 File Offset: 0x00004C90
		public void SetDirtyBit(uint dirtyBit)
		{
			this.m_SyncVarDirtyBits |= dirtyBit;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public void ClearAllDirtyBits()
		{
			this.m_LastSendTime = Time.time;
			this.m_SyncVarDirtyBits = 0U;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006AB4 File Offset: 0x00004CB4
		internal int GetDirtyChannel()
		{
			if (Time.time - this.m_LastSendTime > this.GetNetworkSendInterval() && this.m_SyncVarDirtyBits != 0U)
			{
				return this.GetNetworkChannel();
			}
			return -1;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006AEC File Offset: 0x00004CEC
		public virtual bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			if (!initialState)
			{
				writer.WritePackedUInt32(0U);
			}
			return false;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006AFC File Offset: 0x00004CFC
		public virtual void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (!initialState)
			{
				reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006B0C File Offset: 0x00004D0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void PreStartClient()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006B10 File Offset: 0x00004D10
		public virtual void OnNetworkDestroy()
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006B14 File Offset: 0x00004D14
		public virtual void OnStartServer()
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006B18 File Offset: 0x00004D18
		public virtual void OnStartClient()
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006B1C File Offset: 0x00004D1C
		public virtual void OnStartLocalPlayer()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006B20 File Offset: 0x00004D20
		public virtual void OnStartAuthority()
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006B24 File Offset: 0x00004D24
		public virtual void OnStopAuthority()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006B28 File Offset: 0x00004D28
		public virtual bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
		{
			return false;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006B2C File Offset: 0x00004D2C
		public virtual void OnSetLocalVisibility(bool vis)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006B30 File Offset: 0x00004D30
		public virtual bool OnCheckObserver(NetworkConnection conn)
		{
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006B34 File Offset: 0x00004D34
		public virtual int GetNetworkChannel()
		{
			return 0;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006B38 File Offset: 0x00004D38
		public virtual float GetNetworkSendInterval()
		{
			return 0.1f;
		}

		// Token: 0x04000099 RID: 153
		private const float k_DefaultSendInterval = 0.1f;

		// Token: 0x0400009A RID: 154
		private uint m_SyncVarDirtyBits;

		// Token: 0x0400009B RID: 155
		private float m_LastSendTime;

		// Token: 0x0400009C RID: 156
		private bool m_SyncVarGuard;

		// Token: 0x0400009D RID: 157
		private NetworkIdentity m_MyView;

		// Token: 0x0400009E RID: 158
		private static Dictionary<int, NetworkBehaviour.Invoker> s_CmdHandlerDelegates = new Dictionary<int, NetworkBehaviour.Invoker>();

		// Token: 0x02000033 RID: 51
		protected enum UNetInvokeType
		{
			// Token: 0x040000A0 RID: 160
			Command,
			// Token: 0x040000A1 RID: 161
			ClientRpc,
			// Token: 0x040000A2 RID: 162
			SyncEvent,
			// Token: 0x040000A3 RID: 163
			SyncList
		}

		// Token: 0x02000034 RID: 52
		protected class Invoker
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00006B48 File Offset: 0x00004D48
			public string DebugString()
			{
				return string.Concat(new object[]
				{
					this.invokeType,
					":",
					this.invokeClass,
					":",
					this.invokeFunction.Method.Name
				});
			}

			// Token: 0x040000A4 RID: 164
			public NetworkBehaviour.UNetInvokeType invokeType;

			// Token: 0x040000A5 RID: 165
			public Type invokeClass;

			// Token: 0x040000A6 RID: 166
			public NetworkBehaviour.CmdDelegate invokeFunction;
		}

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x0600052A RID: 1322
		public delegate void CmdDelegate(NetworkBehaviour obj, NetworkReader reader);

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x0600052E RID: 1326
		protected delegate void EventDelegate(List<Delegate> targets, NetworkReader reader);
	}
}
