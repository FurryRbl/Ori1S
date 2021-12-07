using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000056 RID: 86
	public interface INetworking
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600024D RID: 589
		// (remove) Token: 0x0600024E RID: 590
		event CallbackEvent<P2PSessionRequest> P2PSessionRequest;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600024F RID: 591
		// (remove) Token: 0x06000250 RID: 592
		event CallbackEvent<P2PSessionConnectFail> P2PSessionConnectFail;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000251 RID: 593
		// (remove) Token: 0x06000252 RID: 594
		event CallbackEvent<SocketStatusCallback> SocketStatusCallback;

		// Token: 0x06000253 RID: 595
		bool SendP2PPacket(SteamID steamIDRemote, IntPtr data, uint dataSize, P2PSend p2pSendType, int channel = 0);

		// Token: 0x06000254 RID: 596
		bool SendP2PPacket(SteamID steamIDRemote, IntPtr data, uint dataSize, uint dataOffset, P2PSend p2pSendType, int channel = 0);

		// Token: 0x06000255 RID: 597
		bool IsP2PPacketAvailable(out uint msgSize, int channel = 0);

		// Token: 0x06000256 RID: 598
		NetworkingIsP2PPacketAvailableResult IsP2PPacketAvailable(int channel = 0);

		// Token: 0x06000257 RID: 599
		bool ReadP2PPacket(byte[] dest, out uint msgSize, out SteamID steamIDRemote, int channel = 0);

		// Token: 0x06000258 RID: 600
		NetworkingReadP2PPacketResult ReadP2PPacket(byte[] dest, int channel = 0);

		// Token: 0x06000259 RID: 601
		bool AcceptP2PSessionWithUser(SteamID steamIDRemote);

		// Token: 0x0600025A RID: 602
		bool CloseP2PSessionWithUser(SteamID steamIDRemote);

		// Token: 0x0600025B RID: 603
		bool CloseP2PChannelWithUser(SteamID steamIDRemote, int channel);

		// Token: 0x0600025C RID: 604
		bool GetP2PSessionState(SteamID steamIDRemote, out P2PSessionState connectionState);

		// Token: 0x0600025D RID: 605
		NetworkingGetP2PSessionStateResult GetP2PSessionState(SteamID steamIDRemote);

		// Token: 0x0600025E RID: 606
		bool AllowP2PPacketRelay(bool allow);

		// Token: 0x0600025F RID: 607
		NetListenSocketHandle CreateListenSocket(int virtualP2PPort, uint ip, ushort port, bool allowUseOfPacketRelay);

		// Token: 0x06000260 RID: 608
		NetSocketHandle CreateP2PConnectionSocket(SteamID steamIDTarget, int virtualPort, int timeoutSec, bool allowUseOfPacketRelay);

		// Token: 0x06000261 RID: 609
		NetSocketHandle CreateConnectionSocket(uint ip, ushort port, int timeoutSec);

		// Token: 0x06000262 RID: 610
		bool DestroySocket(NetSocketHandle socket, bool notifyRemoteEnd);

		// Token: 0x06000263 RID: 611
		bool DestroyListenSocket(NetListenSocketHandle socket, bool notifyRemoteEnd);

		// Token: 0x06000264 RID: 612
		bool SendDataOnSocket(NetSocketHandle socket, byte[] data, bool reliable);

		// Token: 0x06000265 RID: 613
		bool IsDataAvailableOnSocket(NetSocketHandle socket, out uint msgSize);

		// Token: 0x06000266 RID: 614
		NetworkingIsDataAvaibleOnSocketResult IsDataAvailableOnSocket(NetSocketHandle socket);

		// Token: 0x06000267 RID: 615
		bool RetrieveDataFromSocket(NetSocketHandle socket, byte[] dest, out uint msgSize);

		// Token: 0x06000268 RID: 616
		NetworkingRetrieveDataFromSocketResult RetrieveDataFromSocket(NetSocketHandle socket, byte[] dest);

		// Token: 0x06000269 RID: 617
		bool IsDataAvailable(NetListenSocketHandle listenSocket, out uint msgSize, out NetSocketHandle socket);

		// Token: 0x0600026A RID: 618
		NetworkingIsDataAvailableResult IsDataAvailable(NetListenSocketHandle listenSocket);

		// Token: 0x0600026B RID: 619
		bool RetrieveData(NetListenSocketHandle listenSocket, byte[] dest, out uint msgSize, out NetSocketHandle socket);

		// Token: 0x0600026C RID: 620
		NetworkingRetrieveDataResult RetrieveData(NetListenSocketHandle listenSocket, byte[] dest);

		// Token: 0x0600026D RID: 621
		bool GetSocketInfo(NetSocketHandle socket, out SteamID steamIDRemote, out SNetSocketState socketStatus, out uint ipRemote, out ushort portRemote);

		// Token: 0x0600026E RID: 622
		NetworkingGetSocketInfoResult GetSocketInfo(NetSocketHandle socket);

		// Token: 0x0600026F RID: 623
		bool GetListenSocketInfo(NetListenSocketHandle listenSocket, out uint ip, out ushort port);

		// Token: 0x06000270 RID: 624
		NetworkingGetListenSocketInfoResult GetListenSocketInfo(NetListenSocketHandle listenSocket);

		// Token: 0x06000271 RID: 625
		NetSocketConnectionType GetSocketConnectionType(NetSocketHandle socket);

		// Token: 0x06000272 RID: 626
		int GetMaxPacketSize(NetSocketHandle socket);
	}
}
