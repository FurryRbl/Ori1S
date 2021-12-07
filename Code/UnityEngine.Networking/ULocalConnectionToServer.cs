using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000013 RID: 19
	internal class ULocalConnectionToServer : NetworkConnection
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000491C File Offset: 0x00002B1C
		public ULocalConnectionToServer(NetworkServer localServer)
		{
			this.address = "localServer";
			this.m_LocalServer = localServer;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004938 File Offset: 0x00002B38
		public override bool Send(short msgType, MessageBase msg)
		{
			return this.m_LocalServer.InvokeHandlerOnServer(this, msgType, msg, 0);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000494C File Offset: 0x00002B4C
		public override bool SendUnreliable(short msgType, MessageBase msg)
		{
			return this.m_LocalServer.InvokeHandlerOnServer(this, msgType, msg, 1);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004960 File Offset: 0x00002B60
		public override bool SendByChannel(short msgType, MessageBase msg, int channelId)
		{
			return this.m_LocalServer.InvokeHandlerOnServer(this, msgType, msg, channelId);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004974 File Offset: 0x00002B74
		public override bool SendBytes(byte[] bytes, int numBytes, int channelId)
		{
			if (numBytes <= 0)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("LocalConnection:SendBytes cannot send zero bytes");
				}
				return false;
			}
			return this.m_LocalServer.InvokeBytes(this, bytes, numBytes, channelId);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000049B0 File Offset: 0x00002BB0
		public override bool SendWriter(NetworkWriter writer, int channelId)
		{
			return this.m_LocalServer.InvokeBytes(this, writer.AsArray(), (int)((short)writer.AsArray().Length), channelId);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000049DC File Offset: 0x00002BDC
		public override void GetStatsOut(out int numMsgs, out int numBufferedMsgs, out int numBytes, out int lastBufferedPerSecond)
		{
			numMsgs = 0;
			numBufferedMsgs = 0;
			numBytes = 0;
			lastBufferedPerSecond = 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000049EC File Offset: 0x00002BEC
		public override void GetStatsIn(out int numMsgs, out int numBytes)
		{
			numMsgs = 0;
			numBytes = 0;
		}

		// Token: 0x04000042 RID: 66
		private NetworkServer m_LocalServer;
	}
}
