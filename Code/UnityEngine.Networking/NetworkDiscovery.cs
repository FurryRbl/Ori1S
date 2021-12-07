using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x0200003E RID: 62
	[AddComponentMenu("Network/NetworkDiscovery")]
	[DisallowMultipleComponent]
	public class NetworkDiscovery : MonoBehaviour
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000096C8 File Offset: 0x000078C8
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000096D0 File Offset: 0x000078D0
		public int broadcastPort
		{
			get
			{
				return this.m_BroadcastPort;
			}
			set
			{
				this.m_BroadcastPort = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000096DC File Offset: 0x000078DC
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000096E4 File Offset: 0x000078E4
		public int broadcastKey
		{
			get
			{
				return this.m_BroadcastKey;
			}
			set
			{
				this.m_BroadcastKey = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000096F0 File Offset: 0x000078F0
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000096F8 File Offset: 0x000078F8
		public int broadcastVersion
		{
			get
			{
				return this.m_BroadcastVersion;
			}
			set
			{
				this.m_BroadcastVersion = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009704 File Offset: 0x00007904
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000970C File Offset: 0x0000790C
		public int broadcastSubVersion
		{
			get
			{
				return this.m_BroadcastSubVersion;
			}
			set
			{
				this.m_BroadcastSubVersion = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009718 File Offset: 0x00007918
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00009720 File Offset: 0x00007920
		public int broadcastInterval
		{
			get
			{
				return this.m_BroadcastInterval;
			}
			set
			{
				this.m_BroadcastInterval = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000972C File Offset: 0x0000792C
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00009734 File Offset: 0x00007934
		public bool useNetworkManager
		{
			get
			{
				return this.m_UseNetworkManager;
			}
			set
			{
				this.m_UseNetworkManager = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009740 File Offset: 0x00007940
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00009748 File Offset: 0x00007948
		public string broadcastData
		{
			get
			{
				return this.m_BroadcastData;
			}
			set
			{
				this.m_BroadcastData = value;
				this.m_MsgOutBuffer = NetworkDiscovery.StringToBytes(this.m_BroadcastData);
				if (this.m_UseNetworkManager && LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkDiscovery broadcast data changed while using NetworkManager. This can prevent clients from finding the server. The format of the broadcast data must be 'NetworkManager:IPAddress:Port'.");
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00009784 File Offset: 0x00007984
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000978C File Offset: 0x0000798C
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00009798 File Offset: 0x00007998
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000097A0 File Offset: 0x000079A0
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000097AC File Offset: 0x000079AC
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000097B4 File Offset: 0x000079B4
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000097C0 File Offset: 0x000079C0
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000097C8 File Offset: 0x000079C8
		public int hostId
		{
			get
			{
				return this.m_HostId;
			}
			set
			{
				this.m_HostId = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000097D4 File Offset: 0x000079D4
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000097DC File Offset: 0x000079DC
		public bool running
		{
			get
			{
				return this.m_Running;
			}
			set
			{
				this.m_Running = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000097E8 File Offset: 0x000079E8
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000097F0 File Offset: 0x000079F0
		public bool isServer
		{
			get
			{
				return this.m_IsServer;
			}
			set
			{
				this.m_IsServer = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000097FC File Offset: 0x000079FC
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00009804 File Offset: 0x00007A04
		public bool isClient
		{
			get
			{
				return this.m_IsClient;
			}
			set
			{
				this.m_IsClient = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00009810 File Offset: 0x00007A10
		public Dictionary<string, NetworkBroadcastResult> broadcastsReceived
		{
			get
			{
				return this.m_BroadcastsReceived;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009818 File Offset: 0x00007A18
		private static byte[] StringToBytes(string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009848 File Offset: 0x00007A48
		private static string BytesToString(byte[] bytes)
		{
			char[] array = new char[bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			return new string(array);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009874 File Offset: 0x00007A74
		public bool Initialize()
		{
			if (this.m_BroadcastData.Length >= 1024)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkDiscovery Initialize - data too large. max is " + 1024);
				}
				return false;
			}
			if (!NetworkTransport.IsStarted)
			{
				NetworkTransport.Init();
			}
			if (this.m_UseNetworkManager && NetworkManager.singleton != null)
			{
				this.m_BroadcastData = string.Concat(new object[]
				{
					"NetworkManager:",
					NetworkManager.singleton.networkAddress,
					":",
					NetworkManager.singleton.networkPort
				});
				if (LogFilter.logInfo)
				{
					Debug.Log("NetwrokDiscovery set broadbast data to:" + this.m_BroadcastData);
				}
			}
			this.m_MsgOutBuffer = NetworkDiscovery.StringToBytes(this.m_BroadcastData);
			this.m_MsgInBuffer = new byte[1024];
			this.m_BroadcastsReceived = new Dictionary<string, NetworkBroadcastResult>();
			ConnectionConfig connectionConfig = new ConnectionConfig();
			connectionConfig.AddChannel(QosType.Unreliable);
			this.m_DefaultTopology = new HostTopology(connectionConfig, 1);
			if (this.m_IsServer)
			{
				this.StartAsServer();
			}
			if (this.m_IsClient)
			{
				this.StartAsClient();
			}
			return true;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000099B0 File Offset: 0x00007BB0
		public bool StartAsClient()
		{
			if (this.m_HostId != -1 || this.m_Running)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkDiscovery StartAsClient already started");
				}
				return false;
			}
			this.m_HostId = NetworkTransport.AddHost(this.m_DefaultTopology, this.m_BroadcastPort);
			if (this.m_HostId == -1)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkDiscovery StartAsClient - addHost failed");
				}
				return false;
			}
			byte b;
			NetworkTransport.SetBroadcastCredentials(this.m_HostId, this.m_BroadcastKey, this.m_BroadcastVersion, this.m_BroadcastSubVersion, out b);
			this.m_Running = true;
			this.m_IsClient = true;
			if (LogFilter.logDebug)
			{
				Debug.Log("StartAsClient Discovery listening");
			}
			return true;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009A68 File Offset: 0x00007C68
		public bool StartAsServer()
		{
			if (this.m_HostId != -1 || this.m_Running)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkDiscovery StartAsServer already started");
				}
				return false;
			}
			this.m_HostId = NetworkTransport.AddHost(this.m_DefaultTopology, 0);
			if (this.m_HostId == -1)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkDiscovery StartAsServer - addHost failed");
				}
				return false;
			}
			byte b;
			if (!NetworkTransport.StartBroadcastDiscovery(this.m_HostId, this.m_BroadcastPort, this.m_BroadcastKey, this.m_BroadcastVersion, this.m_BroadcastSubVersion, this.m_MsgOutBuffer, this.m_MsgOutBuffer.Length, this.m_BroadcastInterval, out b))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkDiscovery StartBroadcast failed err: " + b);
				}
				return false;
			}
			this.m_Running = true;
			this.m_IsServer = true;
			if (LogFilter.logDebug)
			{
				Debug.Log("StartAsServer Discovery broadcasting");
			}
			Object.DontDestroyOnLoad(base.gameObject);
			return true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009B64 File Offset: 0x00007D64
		public void StopBroadcast()
		{
			if (this.m_HostId == -1)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkDiscovery StopBroadcast not initialized");
				}
				return;
			}
			if (!this.m_Running)
			{
				Debug.LogWarning("NetworkDiscovery StopBroadcast not started");
				return;
			}
			if (this.m_IsServer)
			{
				NetworkTransport.StopBroadcastDiscovery();
			}
			NetworkTransport.RemoveHost(this.m_HostId);
			this.m_HostId = -1;
			this.m_Running = false;
			this.m_IsServer = false;
			this.m_IsClient = false;
			this.m_MsgInBuffer = null;
			this.m_BroadcastsReceived = null;
			if (LogFilter.logDebug)
			{
				Debug.Log("Stopped Discovery broadcasting");
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009C04 File Offset: 0x00007E04
		private void Update()
		{
			if (this.m_HostId == -1)
			{
				return;
			}
			if (this.m_IsServer)
			{
				return;
			}
			NetworkEventType networkEventType;
			do
			{
				int num;
				int num2;
				int num3;
				byte b;
				networkEventType = NetworkTransport.ReceiveFromHost(this.m_HostId, out num, out num2, this.m_MsgInBuffer, 1024, out num3, out b);
				if (networkEventType == NetworkEventType.BroadcastEvent)
				{
					NetworkTransport.GetBroadcastConnectionMessage(this.m_HostId, this.m_MsgInBuffer, 1024, out num3, out b);
					string text;
					int num4;
					NetworkTransport.GetBroadcastConnectionInfo(this.m_HostId, out text, out num4, out b);
					NetworkBroadcastResult value = default(NetworkBroadcastResult);
					value.serverAddress = text;
					value.broadcastData = new byte[num3];
					Buffer.BlockCopy(this.m_MsgInBuffer, 0, value.broadcastData, 0, num3);
					this.m_BroadcastsReceived[text] = value;
					this.OnReceivedBroadcast(text, NetworkDiscovery.BytesToString(this.m_MsgInBuffer));
				}
			}
			while (networkEventType != NetworkEventType.Nothing);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private void OnDestroy()
		{
			if (this.m_IsServer && this.m_Running && this.m_HostId != -1)
			{
				NetworkTransport.StopBroadcastDiscovery();
				NetworkTransport.RemoveHost(this.m_HostId);
			}
			if (this.m_IsClient && this.m_Running && this.m_HostId != -1)
			{
				NetworkTransport.RemoveHost(this.m_HostId);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009D48 File Offset: 0x00007F48
		public virtual void OnReceivedBroadcast(string fromAddress, string data)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009D4C File Offset: 0x00007F4C
		private void OnGUI()
		{
			if (!this.m_ShowGUI)
			{
				return;
			}
			int num = 10 + this.m_OffsetX;
			int num2 = 40 + this.m_OffsetY;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				GUI.Box(new Rect((float)num, (float)num2, 200f, 20f), "( WebGL cannot broadcast )");
				return;
			}
			if (this.m_MsgInBuffer == null)
			{
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Initialize Broadcast"))
				{
					this.Initialize();
				}
				return;
			}
			string str = string.Empty;
			if (this.m_IsServer)
			{
				str = " (server)";
			}
			if (this.m_IsClient)
			{
				str = " (client)";
			}
			GUI.Label(new Rect((float)num, (float)num2, 200f, 20f), "initialized" + str);
			num2 += 24;
			if (this.m_Running)
			{
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Stop"))
				{
					this.StopBroadcast();
				}
				num2 += 24;
				if (this.m_BroadcastsReceived != null)
				{
					foreach (string text in this.m_BroadcastsReceived.Keys)
					{
						NetworkBroadcastResult networkBroadcastResult = this.m_BroadcastsReceived[text];
						if (GUI.Button(new Rect((float)num, (float)(num2 + 20), 200f, 20f), "Game at " + text) && this.m_UseNetworkManager)
						{
							string text2 = NetworkDiscovery.BytesToString(networkBroadcastResult.broadcastData);
							string[] array = text2.Split(new char[]
							{
								':'
							});
							if (array.Length == 3 && array[0] == "NetworkManager" && NetworkManager.singleton != null && NetworkManager.singleton.client == null)
							{
								NetworkManager.singleton.networkAddress = array[1];
								NetworkManager.singleton.networkPort = Convert.ToInt32(array[2]);
								NetworkManager.singleton.StartClient();
							}
						}
						num2 += 24;
					}
				}
			}
			else
			{
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Start Broadcasting"))
				{
					this.StartAsServer();
				}
				num2 += 24;
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Listen for Broadcast"))
				{
					this.StartAsClient();
				}
				num2 += 24;
			}
		}

		// Token: 0x040000E7 RID: 231
		private const int k_MaxBroadcastMsgSize = 1024;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		private int m_BroadcastPort = 47777;

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private int m_BroadcastKey = 2222;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		private int m_BroadcastVersion = 1;

		// Token: 0x040000EB RID: 235
		[SerializeField]
		private int m_BroadcastSubVersion = 1;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		private int m_BroadcastInterval = 1000;

		// Token: 0x040000ED RID: 237
		[SerializeField]
		private bool m_UseNetworkManager = true;

		// Token: 0x040000EE RID: 238
		[SerializeField]
		private string m_BroadcastData = "HELLO";

		// Token: 0x040000EF RID: 239
		[SerializeField]
		private bool m_ShowGUI = true;

		// Token: 0x040000F0 RID: 240
		[SerializeField]
		private int m_OffsetX;

		// Token: 0x040000F1 RID: 241
		[SerializeField]
		private int m_OffsetY;

		// Token: 0x040000F2 RID: 242
		private int m_HostId = -1;

		// Token: 0x040000F3 RID: 243
		private bool m_Running;

		// Token: 0x040000F4 RID: 244
		private bool m_IsServer;

		// Token: 0x040000F5 RID: 245
		private bool m_IsClient;

		// Token: 0x040000F6 RID: 246
		private byte[] m_MsgOutBuffer;

		// Token: 0x040000F7 RID: 247
		private byte[] m_MsgInBuffer;

		// Token: 0x040000F8 RID: 248
		private HostTopology m_DefaultTopology;

		// Token: 0x040000F9 RID: 249
		private Dictionary<string, NetworkBroadcastResult> m_BroadcastsReceived;
	}
}
