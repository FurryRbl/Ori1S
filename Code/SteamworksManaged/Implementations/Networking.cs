using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000057 RID: 87
	internal class Networking : SteamService, INetworking
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public Networking()
		{
			this.p2pSessionRequest = new List<P2PSessionRequest>();
			this.p2pSessionConnectFail = new List<P2PSessionConnectFail>();
			this.socketStatusCallback = new List<SocketStatusCallback>();
			SteamService.Callbacks[CallbackID.P2PSessionRequest] = delegate(IntPtr data, int size)
			{
				this.p2pSessionRequest.Add(ManagedSteam.CallbackStructures.P2PSessionRequest.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.P2PSessionConnectFail] = delegate(IntPtr data, int size)
			{
				this.p2pSessionConnectFail.Add(ManagedSteam.CallbackStructures.P2PSessionConnectFail.Create(data, size));
			};
			SteamService.Callbacks[CallbackID.SocketStatusCallback] = delegate(IntPtr data, int size)
			{
				this.socketStatusCallback.Add(ManagedSteam.CallbackStructures.SocketStatusCallback.Create(data, size));
			};
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000274 RID: 628 RVA: 0x00004C68 File Offset: 0x00002E68
		// (remove) Token: 0x06000275 RID: 629 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public event CallbackEvent<P2PSessionRequest> P2PSessionRequest;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000276 RID: 630 RVA: 0x00004CD8 File Offset: 0x00002ED8
		// (remove) Token: 0x06000277 RID: 631 RVA: 0x00004D10 File Offset: 0x00002F10
		public event CallbackEvent<P2PSessionConnectFail> P2PSessionConnectFail;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000278 RID: 632 RVA: 0x00004D48 File Offset: 0x00002F48
		// (remove) Token: 0x06000279 RID: 633 RVA: 0x00004D80 File Offset: 0x00002F80
		public event CallbackEvent<SocketStatusCallback> SocketStatusCallback;

		// Token: 0x0600027A RID: 634 RVA: 0x00004DB5 File Offset: 0x00002FB5
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00004DB7 File Offset: 0x00002FB7
		internal override void ReleaseManagedResources()
		{
			this.p2pSessionRequest = null;
			this.P2PSessionRequest = null;
			this.p2pSessionConnectFail = null;
			this.P2PSessionConnectFail = null;
			this.socketStatusCallback = null;
			this.SocketStatusCallback = null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00004DE3 File Offset: 0x00002FE3
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<P2PSessionRequest>(this.p2pSessionRequest, this.P2PSessionRequest);
			SteamService.InvokeEvents<P2PSessionConnectFail>(this.p2pSessionConnectFail, this.P2PSessionConnectFail);
			SteamService.InvokeEvents<SocketStatusCallback>(this.socketStatusCallback, this.SocketStatusCallback);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00004E18 File Offset: 0x00003018
		public bool SendP2PPacket(SteamID steamIDRemote, IntPtr data, uint dataSize, P2PSend p2pSendType, int channel = 0)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_SendP2PPacket(steamIDRemote.AsUInt64, data, dataSize, (int)p2pSendType, channel);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004E32 File Offset: 0x00003032
		public bool SendP2PPacket(SteamID steamIDRemote, IntPtr data, uint dataSize, uint dataOffset, P2PSend p2pSendType, int channel = 0)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_SendP2PPacketOffset(steamIDRemote.AsUInt64, data, dataSize, dataOffset, (int)p2pSendType, channel);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004E4E File Offset: 0x0000304E
		public bool IsP2PPacketAvailable(out uint msgSize, int channel = 0)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			return NativeMethods.Networking_IsP2PPacketAvailable(ref msgSize, channel);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004E60 File Offset: 0x00003060
		public NetworkingIsP2PPacketAvailableResult IsP2PPacketAvailable(int channel = 0)
		{
			NetworkingIsP2PPacketAvailableResult result = default(NetworkingIsP2PPacketAvailableResult);
			result.Result = this.IsP2PPacketAvailable(out result.MsgSize, channel);
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004E8C File Offset: 0x0000308C
		public bool ReadP2PPacket(byte[] dest, out uint msgSize, out SteamID steamIDRemote, int channel = 0)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(dest))
			{
				ulong value = 0UL;
				bool flag = NativeMethods.Networking_ReadP2PPacket(nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, ref msgSize, ref value, channel);
				steamIDRemote = new SteamID(value);
				nativeBuffer.ReadFromUnmanagedMemory((int)msgSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004EF8 File Offset: 0x000030F8
		public NetworkingReadP2PPacketResult ReadP2PPacket(byte[] dest, int channel = 0)
		{
			NetworkingReadP2PPacketResult result = default(NetworkingReadP2PPacketResult);
			result.Result = this.ReadP2PPacket(dest, out result.MsgSize, out result.SteamIDRemote, channel);
			return result;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00004F2B File Offset: 0x0000312B
		public bool AcceptP2PSessionWithUser(SteamID steamIDRemote)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_AcceptP2PSessionWithUser(steamIDRemote.AsUInt64);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00004F3F File Offset: 0x0000313F
		public bool CloseP2PSessionWithUser(SteamID steamIDRemote)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_CloseP2PSessionWithUser(steamIDRemote.AsUInt64);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00004F53 File Offset: 0x00003153
		public bool CloseP2PChannelWithUser(SteamID steamIDRemote, int channel)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_CloseP2PChannelWithUser(steamIDRemote.AsUInt64, channel);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00004F68 File Offset: 0x00003168
		public bool GetP2PSessionState(SteamID steamIDRemote, out P2PSessionState connectionState)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(Marshal.SizeOf(typeof(P2PSessionState))))
			{
				int num = NativeMethods.Networking_GetP2PSessionStateSize();
				if (num != nativeBuffer.UnmanagedSize)
				{
					Error.ThrowError(ErrorCodes.CallbackStructSizeMissmatch, new object[]
					{
						typeof(P2PSessionState).Name
					});
				}
				bool flag = NativeMethods.Networking_GetP2PSessionState(steamIDRemote.AsUInt64, nativeBuffer.UnmanagedMemory);
				connectionState = NativeHelpers.ConvertStruct<P2PSessionState>(nativeBuffer.UnmanagedMemory, nativeBuffer.UnmanagedSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005010 File Offset: 0x00003210
		public NetworkingGetP2PSessionStateResult GetP2PSessionState(SteamID steamIDRemote)
		{
			NetworkingGetP2PSessionStateResult result = default(NetworkingGetP2PSessionStateResult);
			result.Result = this.GetP2PSessionState(steamIDRemote, out result.P2PSessionState);
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000503B File Offset: 0x0000323B
		public bool AllowP2PPacketRelay(bool allow)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_AllowP2PPacketRelay(allow);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000504C File Offset: 0x0000324C
		public NetListenSocketHandle CreateListenSocket(int virtualP2PPort, uint ip, ushort port, bool allowUseOfPacketRelay)
		{
			base.CheckIfUsable();
			uint value = NativeMethods.Networking_CreateListenSocket(virtualP2PPort, ip, port, allowUseOfPacketRelay);
			return new NetListenSocketHandle(value);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00005070 File Offset: 0x00003270
		public NetSocketHandle CreateP2PConnectionSocket(SteamID steamIDTarget, int virtualPort, int timeoutSec, bool allowUseOfPacketRelay)
		{
			base.CheckIfUsable();
			uint value = NativeMethods.Networking_CreateP2PConnectionSocket(steamIDTarget.AsUInt64, virtualPort, timeoutSec, allowUseOfPacketRelay);
			return new NetSocketHandle(value);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000509C File Offset: 0x0000329C
		public NetSocketHandle CreateConnectionSocket(uint ip, ushort port, int timeoutSec)
		{
			base.CheckIfUsable();
			uint value = NativeMethods.Networking_CreateConnectionSocket(ip, port, timeoutSec);
			return new NetSocketHandle(value);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000050BE File Offset: 0x000032BE
		public bool DestroySocket(NetSocketHandle socket, bool notifyRemoteEnd)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_DestroySocket(socket.AsUInt32, notifyRemoteEnd);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000050D3 File Offset: 0x000032D3
		public bool DestroyListenSocket(NetListenSocketHandle socket, bool notifyRemoteEnd)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_DestroyListenSocket(socket.AsUInt32, notifyRemoteEnd);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000050E8 File Offset: 0x000032E8
		public bool SendDataOnSocket(NetSocketHandle socket, byte[] data, bool reliable)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(data))
			{
				nativeBuffer.WriteToUnmanagedMemory();
				result = NativeMethods.Networking_SendDataOnSocket(socket.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, reliable);
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00005140 File Offset: 0x00003340
		public bool IsDataAvailableOnSocket(NetSocketHandle socket, out uint msgSize)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			return NativeMethods.Networking_IsDataAvailableOnSocket(socket.AsUInt32, ref msgSize);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005158 File Offset: 0x00003358
		public NetworkingIsDataAvaibleOnSocketResult IsDataAvailableOnSocket(NetSocketHandle socket)
		{
			NetworkingIsDataAvaibleOnSocketResult result = default(NetworkingIsDataAvaibleOnSocketResult);
			result.Result = this.IsDataAvailableOnSocket(socket, out result.MsgSize);
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00005184 File Offset: 0x00003384
		public bool RetrieveDataFromSocket(NetSocketHandle socket, byte[] dest, out uint msgSize)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(dest))
			{
				bool flag = NativeMethods.Networking_RetrieveDataFromSocket(socket.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, ref msgSize);
				nativeBuffer.ReadFromUnmanagedMemory((int)msgSize);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000051E4 File Offset: 0x000033E4
		public NetworkingRetrieveDataFromSocketResult RetrieveDataFromSocket(NetSocketHandle socket, byte[] dest)
		{
			NetworkingRetrieveDataFromSocketResult result = default(NetworkingRetrieveDataFromSocketResult);
			result.Result = this.RetrieveDataFromSocket(socket, dest, out result.MsgSize);
			return result;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00005210 File Offset: 0x00003410
		public bool IsDataAvailable(NetListenSocketHandle listenSocket, out uint msgSize, out NetSocketHandle socket)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			uint value = 0U;
			bool result = NativeMethods.Networking_IsDataAvailable(listenSocket.AsUInt32, ref msgSize, ref value);
			socket = new NetSocketHandle(value);
			return result;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00005248 File Offset: 0x00003448
		public NetworkingIsDataAvailableResult IsDataAvailable(NetListenSocketHandle listenSocket)
		{
			NetworkingIsDataAvailableResult result = default(NetworkingIsDataAvailableResult);
			result.Result = this.IsDataAvailable(listenSocket, out result.MsgSize, out result.Socket);
			return result;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000527C File Offset: 0x0000347C
		public bool RetrieveData(NetListenSocketHandle listenSocket, byte[] dest, out uint msgSize, out NetSocketHandle socket)
		{
			base.CheckIfUsable();
			msgSize = 0U;
			uint value = 0U;
			bool result;
			using (NativeBuffer nativeBuffer = new NativeBuffer(dest))
			{
				bool flag = NativeMethods.Networking_RetrieveData(listenSocket.AsUInt32, nativeBuffer.UnmanagedMemory, (uint)nativeBuffer.UnmanagedSize, ref msgSize, ref value);
				nativeBuffer.ReadFromUnmanagedMemory((int)msgSize);
				socket = new NetSocketHandle(value);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000052EC File Offset: 0x000034EC
		public NetworkingRetrieveDataResult RetrieveData(NetListenSocketHandle listenSocket, byte[] dest)
		{
			NetworkingRetrieveDataResult result = default(NetworkingRetrieveDataResult);
			result.Result = this.RetrieveData(listenSocket, dest, out result.MsgSize, out result.Socket);
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00005320 File Offset: 0x00003520
		public bool GetSocketInfo(NetSocketHandle socket, out SteamID steamIDRemote, out SNetSocketState socketStatus, out uint ipRemote, out ushort portRemote)
		{
			base.CheckIfUsable();
			ulong value = 0UL;
			int num = 0;
			ipRemote = 0U;
			portRemote = 0;
			bool result = NativeMethods.Networking_GetSocketInfo(socket.AsUInt32, ref value, ref num, ref ipRemote, ref portRemote);
			steamIDRemote = new SteamID(value);
			socketStatus = (SNetSocketState)num;
			return result;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00005368 File Offset: 0x00003568
		public NetworkingGetSocketInfoResult GetSocketInfo(NetSocketHandle socket)
		{
			NetworkingGetSocketInfoResult result = default(NetworkingGetSocketInfoResult);
			result.Result = this.GetSocketInfo(socket, out result.SteamIDRemote, out result.SocketStatus, out result.IpRemote, out result.PortRemote);
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000053A8 File Offset: 0x000035A8
		public bool GetListenSocketInfo(NetListenSocketHandle listenSocket, out uint ip, out ushort port)
		{
			base.CheckIfUsable();
			ip = 0U;
			port = 0;
			return NativeMethods.Networking_GetListenSocketInfo(listenSocket.AsUInt32, ref ip, ref port);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000053C4 File Offset: 0x000035C4
		public NetworkingGetListenSocketInfoResult GetListenSocketInfo(NetListenSocketHandle listenSocket)
		{
			NetworkingGetListenSocketInfoResult result = default(NetworkingGetListenSocketInfoResult);
			result.Result = this.GetListenSocketInfo(listenSocket, out result.Ip, out result.Port);
			return result;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000053F8 File Offset: 0x000035F8
		public NetSocketConnectionType GetSocketConnectionType(NetSocketHandle socket)
		{
			base.CheckIfUsable();
			return (NetSocketConnectionType)NativeMethods.Networking_GetSocketConnectionType(socket.AsUInt32);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00005419 File Offset: 0x00003619
		public int GetMaxPacketSize(NetSocketHandle socket)
		{
			base.CheckIfUsable();
			return NativeMethods.Networking_GetMaxPacketSize(socket.AsUInt32);
		}

		// Token: 0x0400018C RID: 396
		private List<P2PSessionRequest> p2pSessionRequest;

		// Token: 0x0400018D RID: 397
		private List<P2PSessionConnectFail> p2pSessionConnectFail;

		// Token: 0x0400018E RID: 398
		private List<SocketStatusCallback> socketStatusCallback;
	}
}
