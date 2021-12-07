using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x0200024A RID: 586
	public sealed class NetworkTransport
	{
		// Token: 0x0600232D RID: 9005 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		private NetworkTransport()
		{
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		internal static bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
		{
			if (endPoint.GetType().FullName == "UnityEngine.PS4.SceEndPoint")
			{
				SocketAddress socketAddress = endPoint.Serialize();
				if (socketAddress[8] != 0 || socketAddress[9] != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0002CA48 File Offset: 0x0002AC48
		public static int ConnectEndPoint(int hostId, EndPoint endPoint, int exceptionConnectionId, out byte error)
		{
			error = 0;
			byte[] array = new byte[]
			{
				95,
				36,
				19,
				246
			};
			if (endPoint == null)
			{
				throw new NullReferenceException("Null EndPoint provided");
			}
			if (endPoint.GetType().FullName != "UnityEngine.XboxOne.XboxOneEndPoint" && endPoint.GetType().FullName != "UnityEngine.PS4.SceEndPoint")
			{
				throw new ArgumentException("Endpoint of type XboxOneEndPoint or SceEndPoint  required");
			}
			if (endPoint.GetType().FullName == "UnityEngine.XboxOne.XboxOneEndPoint")
			{
				if (endPoint.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family");
				}
				SocketAddress socketAddress = endPoint.Serialize();
				if (socketAddress.Size != 14)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid size");
				}
				if (socketAddress[0] != 0 || socketAddress[1] != 0)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family signature");
				}
				if (socketAddress[2] != array[0] || socketAddress[3] != array[1] || socketAddress[4] != array[2] || socketAddress[5] != array[3])
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid signature");
				}
				byte[] array2 = new byte[8];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = socketAddress[6 + i];
				}
				IntPtr intPtr = new IntPtr(BitConverter.ToInt64(array2, 0));
				if (intPtr == IntPtr.Zero)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid SOCKET_STORAGE pointer");
				}
				byte[] array3 = new byte[2];
				Marshal.Copy(intPtr, array3, 0, array3.Length);
				AddressFamily addressFamily = (AddressFamily)(((int)array3[1] << 8) + (int)array3[0]);
				if (addressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ArgumentException("XboxOneEndPoint has corrupt or invalid SOCKET_STORAGE pointer");
				}
				return NetworkTransport.Internal_ConnectEndPoint(hostId, intPtr, 128, exceptionConnectionId, out error);
			}
			else
			{
				SocketAddress socketAddress2 = endPoint.Serialize();
				if (socketAddress2.Size != 16)
				{
					throw new ArgumentException("EndPoint has an invalid size");
				}
				if ((int)socketAddress2[0] != socketAddress2.Size)
				{
					throw new ArgumentException("EndPoint has an invalid size value");
				}
				if (socketAddress2[1] != 2)
				{
					throw new ArgumentException("EndPoint has an invalid family value");
				}
				byte[] array4 = new byte[16];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = socketAddress2[j];
				}
				IntPtr intPtr2 = Marshal.AllocHGlobal(array4.Length);
				Marshal.Copy(array4, 0, intPtr2, array4.Length);
				int result = NetworkTransport.Internal_ConnectEndPoint(hostId, intPtr2, 16, exceptionConnectionId, out error);
				Marshal.FreeHGlobal(intPtr2);
				return result;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public static void Init()
		{
			NetworkTransport.InitWithNoParameters();
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
		public static void Init(GlobalConfig config)
		{
			NetworkTransport.InitWithParameters(new GlobalConfigInternal(config));
		}

		// Token: 0x06002332 RID: 9010
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitWithNoParameters();

		// Token: 0x06002333 RID: 9011
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitWithParameters(GlobalConfigInternal config);

		// Token: 0x06002334 RID: 9012
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Shutdown();

		// Token: 0x06002335 RID: 9013
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetAssetId(GameObject go);

		// Token: 0x06002336 RID: 9014
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddSceneId(int id);

		// Token: 0x06002337 RID: 9015
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNextSceneId();

		// Token: 0x06002338 RID: 9016
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error);

		// Token: 0x06002339 RID: 9017
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisconnectNetworkHost(int hostId, out byte error);

		// Token: 0x0600233A RID: 9018
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error);

		// Token: 0x0600233B RID: 9019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, int bytesPerSec, float bucketSizeFactor, out byte error);

		// Token: 0x0600233C RID: 9020 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		public static int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
		{
			return NetworkTransport.ConnectToNetworkPeer(hostId, address, port, exceptionConnectionId, relaySlotId, network, source, node, 0, 0f, out error);
		}

		// Token: 0x0600233D RID: 9021
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentIncomingMessageAmount();

		// Token: 0x0600233E RID: 9022
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentOutgoingMessageAmount();

		// Token: 0x0600233F RID: 9023
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentRtt(int hostId, int connectionId, out byte error);

		// Token: 0x06002340 RID: 9024
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNetworkLostPacketNum(int hostId, int connectionId, out byte error);

		// Token: 0x06002341 RID: 9025
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPacketSentRate(int hostId, int connectionId, out byte error);

		// Token: 0x06002342 RID: 9026
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPacketReceivedRate(int hostId, int connectionId, out byte error);

		// Token: 0x06002343 RID: 9027
		[WrapperlessIcall]
		[Obsolete("GetRemotePacketReceivedRate has been made obsolete. Please do not use this function.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRemotePacketReceivedRate(int hostId, int connectionId, out byte error);

		// Token: 0x06002344 RID: 9028
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNetIOTimeuS();

		// Token: 0x06002345 RID: 9029 RVA: 0x0002CD20 File Offset: 0x0002AF20
		public static void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
		{
			ulong num;
			ushort num2;
			address = NetworkTransport.GetConnectionInfo(hostId, connectionId, out port, out num, out num2, out error);
			network = (NetworkID)num;
			dstNode = (NodeID)num2;
		}

		// Token: 0x06002346 RID: 9030
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetConnectionInfo(int hostId, int connectionId, out int port, out ulong network, out ushort dstNode, out byte error);

		// Token: 0x06002347 RID: 9031
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNetworkTimestamp();

		// Token: 0x06002348 RID: 9032
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRemoteDelayTimeMS(int hostId, int connectionId, int remoteTime, out byte error);

		// Token: 0x06002349 RID: 9033
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool StartSendMulticast(int hostId, int channelId, byte[] buffer, int size, out byte error);

		// Token: 0x0600234A RID: 9034
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendMulticast(int hostId, int connectionId, out byte error);

		// Token: 0x0600234B RID: 9035
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FinishSendMulticast(int hostId, out byte error);

		// Token: 0x0600234C RID: 9036
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxPacketSize();

		// Token: 0x0600234D RID: 9037 RVA: 0x0002CD48 File Offset: 0x0002AF48
		private static void CheckTopology(HostTopology topology)
		{
			int maxPacketSize = NetworkTransport.GetMaxPacketSize();
			if ((int)topology.DefaultConfig.PacketSize > maxPacketSize)
			{
				throw new ArgumentOutOfRangeException("Default config: packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
			}
			for (int i = 0; i < topology.SpecialConnectionConfigs.Count; i++)
			{
				if ((int)topology.SpecialConnectionConfigs[i].PacketSize > maxPacketSize)
				{
					throw new ArgumentOutOfRangeException("Special config " + i.ToString() + ": packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
				}
			}
		}

		// Token: 0x0600234E RID: 9038
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWsHostWrapper(HostTopologyInternal topologyInt, string ip, int port);

		// Token: 0x0600234F RID: 9039
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWsHostWrapperWithoutIp(HostTopologyInternal topologyInt, int port);

		// Token: 0x06002350 RID: 9040 RVA: 0x0002CDDC File Offset: 0x0002AFDC
		[ExcludeFromDocs]
		public static int AddWebsocketHost(HostTopology topology, int port)
		{
			string ip = null;
			return NetworkTransport.AddWebsocketHost(topology, port, ip);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public static int AddWebsocketHost(HostTopology topology, int port, [DefaultValue("null")] string ip)
		{
			if (topology == null)
			{
				throw new NullReferenceException("topology is not defined");
			}
			if (ip == null)
			{
				return NetworkTransport.AddWsHostWrapperWithoutIp(new HostTopologyInternal(topology), port);
			}
			return NetworkTransport.AddWsHostWrapper(new HostTopologyInternal(topology), ip, port);
		}

		// Token: 0x06002352 RID: 9042
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddHostWrapper(HostTopologyInternal topologyInt, string ip, int port, int minTimeout, int maxTimeout);

		// Token: 0x06002353 RID: 9043
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddHostWrapperWithoutIp(HostTopologyInternal topologyInt, int port, int minTimeout, int maxTimeout);

		// Token: 0x06002354 RID: 9044 RVA: 0x0002CE28 File Offset: 0x0002B028
		[ExcludeFromDocs]
		public static int AddHost(HostTopology topology, int port)
		{
			string ip = null;
			return NetworkTransport.AddHost(topology, port, ip);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0002CE40 File Offset: 0x0002B040
		[ExcludeFromDocs]
		public static int AddHost(HostTopology topology)
		{
			string ip = null;
			int port = 0;
			return NetworkTransport.AddHost(topology, port, ip);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x0002CE5C File Offset: 0x0002B05C
		public static int AddHost(HostTopology topology, [DefaultValue("0")] int port, [DefaultValue("null")] string ip)
		{
			if (topology == null)
			{
				throw new NullReferenceException("topology is not defined");
			}
			if (ip == null)
			{
				return NetworkTransport.AddHostWrapperWithoutIp(new HostTopologyInternal(topology), port, 0, 0);
			}
			return NetworkTransport.AddHostWrapper(new HostTopologyInternal(topology), ip, port, 0, 0);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0002CE94 File Offset: 0x0002B094
		[ExcludeFromDocs]
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
		{
			string ip = null;
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, port, ip);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		[ExcludeFromDocs]
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout)
		{
			string ip = null;
			int port = 0;
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, port, ip);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0002CECC File Offset: 0x0002B0CC
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, [DefaultValue("0")] int port, [DefaultValue("null")] string ip)
		{
			if (topology == null)
			{
				throw new NullReferenceException("topology is not defined");
			}
			if (ip == null)
			{
				return NetworkTransport.AddHostWrapperWithoutIp(new HostTopologyInternal(topology), port, minTimeout, maxTimeout);
			}
			return NetworkTransport.AddHostWrapper(new HostTopologyInternal(topology), ip, port, minTimeout, maxTimeout);
		}

		// Token: 0x0600235A RID: 9050
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RemoveHost(int hostId);

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600235B RID: 9051
		public static extern bool IsStarted { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600235C RID: 9052
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Connect(int hostId, string address, int port, int exeptionConnectionId, out byte error);

		// Token: 0x0600235D RID: 9053
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_ConnectEndPoint(int hostId, IntPtr sockAddrStorage, int sockAddrStorageLen, int exceptionConnectionId, out byte error);

		// Token: 0x0600235E RID: 9054
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ConnectWithSimulator(int hostId, string address, int port, int exeptionConnectionId, out byte error, ConnectionSimulatorConfig conf);

		// Token: 0x0600235F RID: 9055
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Disconnect(int hostId, int connectionId, out byte error);

		// Token: 0x06002360 RID: 9056 RVA: 0x0002CF08 File Offset: 0x0002B108
		public static bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
		{
			if (buffer == null)
			{
				throw new NullReferenceException("send buffer is not initialized");
			}
			return NetworkTransport.SendWrapper(hostId, connectionId, channelId, buffer, size, out error);
		}

		// Token: 0x06002361 RID: 9057
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendWrapper(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error);

		// Token: 0x06002362 RID: 9058
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		// Token: 0x06002363 RID: 9059
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		// Token: 0x06002364 RID: 9060
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes);

		// Token: 0x06002365 RID: 9061
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error);

		// Token: 0x06002366 RID: 9062
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopBroadcastDiscovery();

		// Token: 0x06002367 RID: 9063
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBroadcastDiscoveryRunning();

		// Token: 0x06002368 RID: 9064
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error);

		// Token: 0x06002369 RID: 9065
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetBroadcastConnectionInfo(int hostId, out int port, out byte error);

		// Token: 0x0600236A RID: 9066 RVA: 0x0002CF28 File Offset: 0x0002B128
		public static void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
		{
			address = NetworkTransport.GetBroadcastConnectionInfo(hostId, out port, out error);
		}

		// Token: 0x0600236B RID: 9067
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error);
	}
}
