using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Networking
{
	// Token: 0x0200003A RID: 58
	public class NetworkConnection : IDisposable
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00008624 File Offset: 0x00006824
		public NetworkConnection()
		{
			this.m_Writer = new NetworkWriter();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008688 File Offset: 0x00006888
		internal HashSet<NetworkIdentity> visList
		{
			get
			{
				return this.m_VisList;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008690 File Offset: 0x00006890
		public List<PlayerController> playerControllers
		{
			get
			{
				return this.m_PlayerControllers;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008698 File Offset: 0x00006898
		public HashSet<NetworkInstanceId> clientOwnedObjects
		{
			get
			{
				return this.m_ClientOwnedObjects;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000086A0 File Offset: 0x000068A0
		public bool isConnected
		{
			get
			{
				return this.hostId != -1;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000086B0 File Offset: 0x000068B0
		internal Dictionary<short, NetworkConnection.PacketStat> packetStats
		{
			get
			{
				return this.m_PacketStats;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000086B8 File Offset: 0x000068B8
		public virtual void Initialize(string networkAddress, int networkHostId, int networkConnectionId, HostTopology hostTopology)
		{
			this.m_Writer = new NetworkWriter();
			this.address = networkAddress;
			this.hostId = networkHostId;
			this.connectionId = networkConnectionId;
			int channelCount = hostTopology.DefaultConfig.ChannelCount;
			int packetSize = (int)hostTopology.DefaultConfig.PacketSize;
			if (hostTopology.DefaultConfig.UsePlatformSpecificProtocols && Application.platform != RuntimePlatform.PS4)
			{
				throw new ArgumentOutOfRangeException("Platform specific protocols are not supported on this platform");
			}
			this.m_Channels = new ChannelBuffer[channelCount];
			for (int i = 0; i < channelCount; i++)
			{
				ChannelQOS channelQOS = hostTopology.DefaultConfig.Channels[i];
				int bufferSize = packetSize;
				if (channelQOS.QOS == QosType.ReliableFragmented || channelQOS.QOS == QosType.UnreliableFragmented)
				{
					bufferSize = (int)(hostTopology.DefaultConfig.FragmentSize * 128);
				}
				this.m_Channels[i] = new ChannelBuffer(this, bufferSize, (byte)i, NetworkConnection.IsReliableQoS(channelQOS.QOS));
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000087A4 File Offset: 0x000069A4
		~NetworkConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000087E0 File Offset: 0x000069E0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000087F0 File Offset: 0x000069F0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_Disposed && this.m_Channels != null)
			{
				for (int i = 0; i < this.m_Channels.Length; i++)
				{
					this.m_Channels[i].Dispose();
				}
			}
			this.m_Channels = null;
			if (this.m_ClientOwnedObjects != null)
			{
				foreach (NetworkInstanceId netId in this.m_ClientOwnedObjects)
				{
					GameObject gameObject = NetworkServer.FindLocalObject(netId);
					if (gameObject != null)
					{
						gameObject.GetComponent<NetworkIdentity>().ClearClientOwner();
					}
				}
			}
			this.m_ClientOwnedObjects = null;
			this.m_Disposed = true;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000088CC File Offset: 0x00006ACC
		private static bool IsReliableQoS(QosType qos)
		{
			return qos == QosType.Reliable || qos == QosType.ReliableFragmented || qos == QosType.ReliableSequenced || qos == QosType.ReliableStateUpdate;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000088EC File Offset: 0x00006AEC
		public bool SetChannelOption(int channelId, ChannelOption option, int value)
		{
			return this.m_Channels != null && channelId >= 0 && channelId < this.m_Channels.Length && this.m_Channels[channelId].SetOption(option, value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008924 File Offset: 0x00006B24
		public void Disconnect()
		{
			this.address = string.Empty;
			this.isReady = false;
			ClientScene.HandleClientDisconnect(this);
			if (this.hostId == -1)
			{
				return;
			}
			byte b;
			NetworkTransport.Disconnect(this.hostId, this.connectionId, out b);
			this.RemoveObservers();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008970 File Offset: 0x00006B70
		internal void SetHandlers(NetworkMessageHandlers handlers)
		{
			this.m_MessageHandlers = handlers;
			this.m_MessageHandlersDict = handlers.GetHandlers();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008988 File Offset: 0x00006B88
		public bool CheckHandler(short msgType)
		{
			return this.m_MessageHandlersDict.ContainsKey(msgType);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008998 File Offset: 0x00006B98
		public bool InvokeHandlerNoData(short msgType)
		{
			return this.InvokeHandler(msgType, null, 0);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000089A4 File Offset: 0x00006BA4
		public bool InvokeHandler(short msgType, NetworkReader reader, int channelId)
		{
			if (!this.m_MessageHandlersDict.ContainsKey(msgType))
			{
				return false;
			}
			this.m_MessageInfo.msgType = msgType;
			this.m_MessageInfo.conn = this;
			this.m_MessageInfo.reader = reader;
			this.m_MessageInfo.channelId = channelId;
			NetworkMessageDelegate networkMessageDelegate = this.m_MessageHandlersDict[msgType];
			if (networkMessageDelegate == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkConnection InvokeHandler no handler for " + msgType);
				}
				return false;
			}
			networkMessageDelegate(this.m_MessageInfo);
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008A38 File Offset: 0x00006C38
		public bool InvokeHandler(NetworkMessage netMsg)
		{
			if (this.m_MessageHandlersDict.ContainsKey(netMsg.msgType))
			{
				NetworkMessageDelegate networkMessageDelegate = this.m_MessageHandlersDict[netMsg.msgType];
				networkMessageDelegate(netMsg);
				return true;
			}
			return false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008A78 File Offset: 0x00006C78
		public void RegisterHandler(short msgType, NetworkMessageDelegate handler)
		{
			this.m_MessageHandlers.RegisterHandler(msgType, handler);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008A88 File Offset: 0x00006C88
		public void UnregisterHandler(short msgType)
		{
			this.m_MessageHandlers.UnregisterHandler(msgType);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008A98 File Offset: 0x00006C98
		internal void SetPlayerController(PlayerController player)
		{
			while ((int)player.playerControllerId >= this.m_PlayerControllers.Count)
			{
				this.m_PlayerControllers.Add(new PlayerController());
			}
			this.m_PlayerControllers[(int)player.playerControllerId] = player;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008AD8 File Offset: 0x00006CD8
		internal void RemovePlayerController(short playerControllerId)
		{
			for (int i = this.m_PlayerControllers.Count; i >= 0; i--)
			{
				if ((int)playerControllerId == i && playerControllerId == this.m_PlayerControllers[i].playerControllerId)
				{
					this.m_PlayerControllers[i] = new PlayerController();
					return;
				}
			}
			if (LogFilter.logError)
			{
				Debug.LogError("RemovePlayer player at playerControllerId " + playerControllerId + " not found");
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008B58 File Offset: 0x00006D58
		internal bool GetPlayerController(short playerControllerId, out PlayerController playerController)
		{
			playerController = null;
			if (this.playerControllers.Count > 0)
			{
				for (int i = 0; i < this.playerControllers.Count; i++)
				{
					if (this.playerControllers[i].IsValid && this.playerControllers[i].playerControllerId == playerControllerId)
					{
						playerController = this.playerControllers[i];
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public void FlushChannels()
		{
			if (this.m_Channels == null)
			{
				return;
			}
			foreach (ChannelBuffer channelBuffer in this.m_Channels)
			{
				channelBuffer.CheckInternalBuffer();
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008C18 File Offset: 0x00006E18
		public void SetMaxDelay(float seconds)
		{
			if (this.m_Channels == null)
			{
				return;
			}
			foreach (ChannelBuffer channelBuffer in this.m_Channels)
			{
				channelBuffer.maxDelay = seconds;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008C58 File Offset: 0x00006E58
		public virtual bool Send(short msgType, MessageBase msg)
		{
			return this.SendByChannel(msgType, msg, 0);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008C64 File Offset: 0x00006E64
		public virtual bool SendUnreliable(short msgType, MessageBase msg)
		{
			return this.SendByChannel(msgType, msg, 1);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008C70 File Offset: 0x00006E70
		public virtual bool SendByChannel(short msgType, MessageBase msg, int channelId)
		{
			this.m_Writer.StartMessage(msgType);
			msg.Serialize(this.m_Writer);
			this.m_Writer.FinishMessage();
			return this.SendWriter(this.m_Writer, channelId);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public virtual bool SendBytes(byte[] bytes, int numBytes, int channelId)
		{
			if (this.logNetworkMessages)
			{
				this.LogSend(bytes);
			}
			return this.CheckChannel(channelId) && this.m_Channels[channelId].SendBytes(bytes, numBytes);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008CF0 File Offset: 0x00006EF0
		public virtual bool SendWriter(NetworkWriter writer, int channelId)
		{
			if (this.logNetworkMessages)
			{
				this.LogSend(writer.ToArray());
			}
			return this.CheckChannel(channelId) && this.m_Channels[channelId].SendWriter(writer);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008D34 File Offset: 0x00006F34
		private void LogSend(byte[] bytes)
		{
			NetworkReader networkReader = new NetworkReader(bytes);
			ushort num = networkReader.ReadUInt16();
			ushort num2 = networkReader.ReadUInt16();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 4; i < (int)(4 + num); i++)
			{
				stringBuilder.AppendFormat("{0:X2}", bytes[i]);
				if (i > 150)
				{
					break;
				}
			}
			Debug.Log(string.Concat(new object[]
			{
				"ConnectionSend con:",
				this.connectionId,
				" bytes:",
				num,
				" msgId:",
				num2,
				" ",
				stringBuilder
			}));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008DF4 File Offset: 0x00006FF4
		private bool CheckChannel(int channelId)
		{
			if (this.m_Channels == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Channels not initialized sending on id '" + channelId);
				}
				return false;
			}
			if (channelId < 0 || channelId >= this.m_Channels.Length)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Invalid channel when sending buffered data, '",
						channelId,
						"'. Current channel count is ",
						this.m_Channels.Length
					}));
				}
				return false;
			}
			return true;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008E88 File Offset: 0x00007088
		public void ResetStats()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008E8C File Offset: 0x0000708C
		protected void HandleBytes(byte[] buffer, int receivedSize, int channelId)
		{
			NetworkReader reader = new NetworkReader(buffer);
			this.HandleReader(reader, receivedSize, channelId);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008EAC File Offset: 0x000070AC
		protected void HandleReader(NetworkReader reader, int receivedSize, int channelId)
		{
			while ((ulong)reader.Position < (ulong)((long)receivedSize))
			{
				ushort num = reader.ReadUInt16();
				short num2 = reader.ReadInt16();
				byte[] array = reader.ReadBytes((int)num);
				NetworkReader reader2 = new NetworkReader(array);
				if (this.logNetworkMessages)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < (int)num; i++)
					{
						stringBuilder.AppendFormat("{0:X2}", array[i]);
						if (i > 150)
						{
							break;
						}
					}
					Debug.Log(string.Concat(new object[]
					{
						"ConnectionRecv con:",
						this.connectionId,
						" bytes:",
						num,
						" msgId:",
						num2,
						" ",
						stringBuilder
					}));
				}
				NetworkMessageDelegate networkMessageDelegate = null;
				if (this.m_MessageHandlersDict.ContainsKey(num2))
				{
					networkMessageDelegate = this.m_MessageHandlersDict[num2];
				}
				if (networkMessageDelegate == null)
				{
					if (LogFilter.logError)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"Unknown message ID ",
							num2,
							" connId:",
							this.connectionId
						}));
					}
					break;
				}
				this.m_NetMsg.msgType = num2;
				this.m_NetMsg.reader = reader2;
				this.m_NetMsg.conn = this;
				this.m_NetMsg.channelId = channelId;
				networkMessageDelegate(this.m_NetMsg);
				this.lastMessageTime = Time.time;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000904C File Offset: 0x0000724C
		public virtual void GetStatsOut(out int numMsgs, out int numBufferedMsgs, out int numBytes, out int lastBufferedPerSecond)
		{
			numMsgs = 0;
			numBufferedMsgs = 0;
			numBytes = 0;
			lastBufferedPerSecond = 0;
			foreach (ChannelBuffer channelBuffer in this.m_Channels)
			{
				numMsgs += channelBuffer.numMsgsOut;
				numBufferedMsgs += channelBuffer.numBufferedMsgsOut;
				numBytes += channelBuffer.numBytesOut;
				lastBufferedPerSecond += channelBuffer.lastBufferedPerSecond;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000090B4 File Offset: 0x000072B4
		public virtual void GetStatsIn(out int numMsgs, out int numBytes)
		{
			numMsgs = 0;
			numBytes = 0;
			foreach (ChannelBuffer channelBuffer in this.m_Channels)
			{
				numMsgs += channelBuffer.numMsgsIn;
				numBytes += channelBuffer.numBytesIn;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000090FC File Offset: 0x000072FC
		public override string ToString()
		{
			return string.Format("hostId: {0} connectionId: {1} isReady: {2} channel count: {3}", new object[]
			{
				this.hostId,
				this.connectionId,
				this.isReady,
				(this.m_Channels == null) ? 0 : this.m_Channels.Length
			});
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00009164 File Offset: 0x00007364
		internal void AddToVisList(NetworkIdentity uv)
		{
			this.m_VisList.Add(uv);
			NetworkServer.ShowForConnection(uv, this);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000917C File Offset: 0x0000737C
		internal void RemoveFromVisList(NetworkIdentity uv, bool isDestroyed)
		{
			this.m_VisList.Remove(uv);
			if (!isDestroyed)
			{
				NetworkServer.HideForConnection(uv, this);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009198 File Offset: 0x00007398
		internal void RemoveObservers()
		{
			foreach (NetworkIdentity networkIdentity in this.m_VisList)
			{
				networkIdentity.RemoveObserverInternal(this);
			}
			this.m_VisList.Clear();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000920C File Offset: 0x0000740C
		public virtual void TransportRecieve(byte[] bytes, int numBytes, int channelId)
		{
			this.HandleBytes(bytes, numBytes, channelId);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009218 File Offset: 0x00007418
		public virtual bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
		{
			return NetworkTransport.Send(this.hostId, this.connectionId, channelId, bytes, numBytes, out error);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009230 File Offset: 0x00007430
		internal void AddOwnedObject(NetworkIdentity obj)
		{
			if (this.m_ClientOwnedObjects == null)
			{
				this.m_ClientOwnedObjects = new HashSet<NetworkInstanceId>();
			}
			this.m_ClientOwnedObjects.Add(obj.netId);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009268 File Offset: 0x00007468
		internal void RemoveOwnedObject(NetworkIdentity obj)
		{
			if (this.m_ClientOwnedObjects == null)
			{
				return;
			}
			this.m_ClientOwnedObjects.Remove(obj.netId);
		}

		// Token: 0x040000CD RID: 205
		private const int k_MaxMessageLogSize = 150;

		// Token: 0x040000CE RID: 206
		private ChannelBuffer[] m_Channels;

		// Token: 0x040000CF RID: 207
		private List<PlayerController> m_PlayerControllers = new List<PlayerController>();

		// Token: 0x040000D0 RID: 208
		private NetworkMessage m_NetMsg = new NetworkMessage();

		// Token: 0x040000D1 RID: 209
		private HashSet<NetworkIdentity> m_VisList = new HashSet<NetworkIdentity>();

		// Token: 0x040000D2 RID: 210
		private NetworkWriter m_Writer;

		// Token: 0x040000D3 RID: 211
		private Dictionary<short, NetworkMessageDelegate> m_MessageHandlersDict;

		// Token: 0x040000D4 RID: 212
		private NetworkMessageHandlers m_MessageHandlers;

		// Token: 0x040000D5 RID: 213
		private HashSet<NetworkInstanceId> m_ClientOwnedObjects;

		// Token: 0x040000D6 RID: 214
		private NetworkMessage m_MessageInfo = new NetworkMessage();

		// Token: 0x040000D7 RID: 215
		public int hostId = -1;

		// Token: 0x040000D8 RID: 216
		public int connectionId = -1;

		// Token: 0x040000D9 RID: 217
		public bool isReady;

		// Token: 0x040000DA RID: 218
		public string address;

		// Token: 0x040000DB RID: 219
		public float lastMessageTime;

		// Token: 0x040000DC RID: 220
		public bool logNetworkMessages;

		// Token: 0x040000DD RID: 221
		private Dictionary<short, NetworkConnection.PacketStat> m_PacketStats = new Dictionary<short, NetworkConnection.PacketStat>();

		// Token: 0x040000DE RID: 222
		private bool m_Disposed;

		// Token: 0x0200003B RID: 59
		public class PacketStat
		{
			// Token: 0x060001B5 RID: 437 RVA: 0x00009290 File Offset: 0x00007490
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					MsgType.MsgTypeToString(this.msgType),
					": count=",
					this.count,
					" bytes=",
					this.bytes
				});
			}

			// Token: 0x040000DF RID: 223
			public short msgType;

			// Token: 0x040000E0 RID: 224
			public int count;

			// Token: 0x040000E1 RID: 225
			public int bytes;
		}
	}
}
