using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000012 RID: 18
	internal class ULocalConnectionToClient : NetworkConnection
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000487C File Offset: 0x00002A7C
		public ULocalConnectionToClient(LocalClient localClient)
		{
			this.address = "localClient";
			this.m_LocalClient = localClient;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004898 File Offset: 0x00002A98
		public LocalClient localClient
		{
			get
			{
				return this.m_LocalClient;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000048A0 File Offset: 0x00002AA0
		public override bool Send(short msgType, MessageBase msg)
		{
			this.m_LocalClient.InvokeHandlerOnClient(msgType, msg, 0);
			return true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000048B4 File Offset: 0x00002AB4
		public override bool SendUnreliable(short msgType, MessageBase msg)
		{
			this.m_LocalClient.InvokeHandlerOnClient(msgType, msg, 1);
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000048C8 File Offset: 0x00002AC8
		public override bool SendByChannel(short msgType, MessageBase msg, int channelId)
		{
			this.m_LocalClient.InvokeHandlerOnClient(msgType, msg, channelId);
			return true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000048DC File Offset: 0x00002ADC
		public override bool SendBytes(byte[] bytes, int numBytes, int channelId)
		{
			this.m_LocalClient.InvokeBytesOnClient(bytes, channelId);
			return true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000048EC File Offset: 0x00002AEC
		public override bool SendWriter(NetworkWriter writer, int channelId)
		{
			this.m_LocalClient.InvokeBytesOnClient(writer.AsArray(), channelId);
			return true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004904 File Offset: 0x00002B04
		public override void GetStatsOut(out int numMsgs, out int numBufferedMsgs, out int numBytes, out int lastBufferedPerSecond)
		{
			numMsgs = 0;
			numBufferedMsgs = 0;
			numBytes = 0;
			lastBufferedPerSecond = 0;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004914 File Offset: 0x00002B14
		public override void GetStatsIn(out int numMsgs, out int numBytes)
		{
			numMsgs = 0;
			numBytes = 0;
		}

		// Token: 0x04000041 RID: 65
		private LocalClient m_LocalClient;
	}
}
