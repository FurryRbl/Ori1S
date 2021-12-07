using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	internal struct ChannelPacket
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000027CC File Offset: 0x000009CC
		public ChannelPacket(int packetSize, bool isReliable)
		{
			this.m_Position = 0;
			this.m_Buffer = new byte[packetSize];
			this.m_IsReliable = isReliable;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Reset()
		{
			this.m_Position = 0;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027F4 File Offset: 0x000009F4
		public bool IsEmpty()
		{
			return this.m_Position == 0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002800 File Offset: 0x00000A00
		public void Write(byte[] bytes, int numBytes)
		{
			Array.Copy(bytes, 0, this.m_Buffer, this.m_Position, numBytes);
			this.m_Position += numBytes;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002830 File Offset: 0x00000A30
		public bool HasSpace(int numBytes)
		{
			return this.m_Position + numBytes <= this.m_Buffer.Length;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002848 File Offset: 0x00000A48
		public bool SendToTransport(NetworkConnection conn, int channelId)
		{
			bool result = true;
			byte b;
			if (!conn.TransportSend(this.m_Buffer, (int)((ushort)this.m_Position), channelId, out b))
			{
				if (!this.m_IsReliable || b != 4)
				{
					if (LogFilter.logError)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"Failed to send internal buffer channel:",
							channelId,
							" bytesToSend:",
							this.m_Position
						}));
					}
					result = false;
				}
			}
			if (b != 0)
			{
				if (this.m_IsReliable && b == 4)
				{
					return false;
				}
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Send Error: ",
						b,
						" channel:",
						channelId,
						" bytesToSend:",
						this.m_Position
					}));
				}
				result = false;
			}
			this.m_Position = 0;
			return result;
		}

		// Token: 0x0400001A RID: 26
		private int m_Position;

		// Token: 0x0400001B RID: 27
		private byte[] m_Buffer;

		// Token: 0x0400001C RID: 28
		private bool m_IsReliable;
	}
}
