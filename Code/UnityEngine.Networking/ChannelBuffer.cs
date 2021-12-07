using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	internal class ChannelBuffer : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public ChannelBuffer(NetworkConnection conn, int bufferSize, byte cid, bool isReliable)
		{
			this.m_Connection = conn;
			this.m_MaxPacketSize = bufferSize - 100;
			this.m_CurrentPacket = new ChannelPacket(this.m_MaxPacketSize, isReliable);
			this.m_ChannelId = cid;
			this.m_MaxPendingPacketCount = 16;
			this.m_IsReliable = isReliable;
			if (isReliable)
			{
				this.m_PendingPackets = new List<ChannelPacket>();
				if (ChannelBuffer.s_FreePackets == null)
				{
					ChannelBuffer.s_FreePackets = new List<ChannelPacket>();
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002184 File Offset: 0x00000384
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000218C File Offset: 0x0000038C
		public int numMsgsOut { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021A0 File Offset: 0x000003A0
		public int numBufferedMsgsOut { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		public int numBytesOut { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		public int numMsgsIn { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021DC File Offset: 0x000003DC
		public int numBytesIn { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021F0 File Offset: 0x000003F0
		public int numBufferedPerSecond { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021FC File Offset: 0x000003FC
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002204 File Offset: 0x00000404
		public int lastBufferedPerSecond { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002210 File Offset: 0x00000410
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_Disposed && disposing && this.m_PendingPackets != null)
			{
				foreach (ChannelPacket item in this.m_PendingPackets)
				{
					ChannelBuffer.pendingPacketCount--;
					if (ChannelBuffer.s_FreePackets.Count < 512)
					{
						ChannelBuffer.s_FreePackets.Add(item);
					}
				}
				this.m_PendingPackets.Clear();
			}
			this.m_Disposed = true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D8 File Offset: 0x000004D8
		public bool SetOption(ChannelOption option, int value)
		{
			if (option != ChannelOption.MaxPendingBuffers)
			{
				return false;
			}
			if (!this.m_IsReliable)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Cannot set MaxPendingBuffers on unreliable channel " + this.m_ChannelId);
				}
				return false;
			}
			if (value < 0 || value >= 512)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Invalid MaxPendingBuffers for channel ",
						this.m_ChannelId,
						". Must be greater than zero and less than ",
						512
					}));
				}
				return false;
			}
			this.m_MaxPendingPacketCount = value;
			return true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002388 File Offset: 0x00000588
		public void CheckInternalBuffer()
		{
			if (Time.realtimeSinceStartup - this.m_LastFlushTime > this.maxDelay && !this.m_CurrentPacket.IsEmpty())
			{
				this.SendInternalBuffer();
				this.m_LastFlushTime = Time.realtimeSinceStartup;
			}
			if (Time.realtimeSinceStartup - this.m_LastBufferedMessageCountTimer > 1f)
			{
				this.lastBufferedPerSecond = this.numBufferedPerSecond;
				this.numBufferedPerSecond = 0;
				this.m_LastBufferedMessageCountTimer = Time.realtimeSinceStartup;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002404 File Offset: 0x00000604
		public bool SendWriter(NetworkWriter writer)
		{
			return this.SendBytes(writer.AsArraySegment().Array, writer.AsArraySegment().Count);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002434 File Offset: 0x00000634
		public bool Send(short msgType, MessageBase msg)
		{
			ChannelBuffer.s_SendWriter.StartMessage(msgType);
			msg.Serialize(ChannelBuffer.s_SendWriter);
			ChannelBuffer.s_SendWriter.FinishMessage();
			this.numMsgsOut++;
			return this.SendWriter(ChannelBuffer.s_SendWriter);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000247C File Offset: 0x0000067C
		internal bool SendBytes(byte[] bytes, int bytesToSend)
		{
			if (bytesToSend <= 0)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("ChannelBuffer:SendBytes cannot send zero bytes");
				}
				return false;
			}
			if (bytesToSend > this.m_MaxPacketSize)
			{
				if (LogFilter.logError)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Failed to send big message of ",
						bytesToSend,
						" bytes. The maximum is ",
						this.m_MaxPacketSize,
						" bytes on this channel."
					}));
				}
				return false;
			}
			if (this.m_CurrentPacket.HasSpace(bytesToSend))
			{
				this.m_CurrentPacket.Write(bytes, bytesToSend);
				return this.maxDelay != 0f || this.SendInternalBuffer();
			}
			if (this.m_IsReliable)
			{
				if (this.m_PendingPackets.Count == 0)
				{
					if (!this.m_CurrentPacket.SendToTransport(this.m_Connection, (int)this.m_ChannelId))
					{
						this.QueuePacket();
					}
					this.m_CurrentPacket.Write(bytes, bytesToSend);
					return true;
				}
				if (this.m_PendingPackets.Count >= this.m_MaxPendingPacketCount)
				{
					if (!this.m_IsBroken && LogFilter.logError)
					{
						Debug.LogError("ChannelBuffer buffer limit of " + this.m_PendingPackets.Count + " packets reached.");
					}
					this.m_IsBroken = true;
					return false;
				}
				this.QueuePacket();
				this.m_CurrentPacket.Write(bytes, bytesToSend);
				return true;
			}
			else
			{
				if (!this.m_CurrentPacket.SendToTransport(this.m_Connection, (int)this.m_ChannelId))
				{
					if (LogFilter.logError)
					{
						Debug.Log("ChannelBuffer SendBytes no space on unreliable channel " + this.m_ChannelId);
					}
					return false;
				}
				this.m_CurrentPacket.Write(bytes, bytesToSend);
				return true;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002640 File Offset: 0x00000840
		private void QueuePacket()
		{
			ChannelBuffer.pendingPacketCount++;
			this.m_PendingPackets.Add(this.m_CurrentPacket);
			this.m_CurrentPacket = this.AllocPacket();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000266C File Offset: 0x0000086C
		private ChannelPacket AllocPacket()
		{
			if (ChannelBuffer.s_FreePackets.Count == 0)
			{
				return new ChannelPacket(this.m_MaxPacketSize, this.m_IsReliable);
			}
			ChannelPacket result = ChannelBuffer.s_FreePackets[0];
			ChannelBuffer.s_FreePackets.RemoveAt(0);
			result.Reset();
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026BC File Offset: 0x000008BC
		private static void FreePacket(ChannelPacket packet)
		{
			if (ChannelBuffer.s_FreePackets.Count >= 512)
			{
				return;
			}
			ChannelBuffer.s_FreePackets.Add(packet);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026EC File Offset: 0x000008EC
		public bool SendInternalBuffer()
		{
			if (this.m_IsReliable && this.m_PendingPackets.Count > 0)
			{
				while (this.m_PendingPackets.Count > 0)
				{
					ChannelPacket packet = this.m_PendingPackets[0];
					if (!packet.SendToTransport(this.m_Connection, (int)this.m_ChannelId))
					{
						break;
					}
					ChannelBuffer.pendingPacketCount--;
					this.m_PendingPackets.RemoveAt(0);
					ChannelBuffer.FreePacket(packet);
					if (this.m_IsBroken && this.m_PendingPackets.Count < this.m_MaxPendingPacketCount / 2)
					{
						if (LogFilter.logWarn)
						{
							Debug.LogWarning("ChannelBuffer recovered from overflow but data was lost.");
						}
						this.m_IsBroken = false;
					}
				}
				return true;
			}
			return this.m_CurrentPacket.SendToTransport(this.m_Connection, (int)this.m_ChannelId);
		}

		// Token: 0x04000001 RID: 1
		private const int k_MaxFreePacketCount = 512;

		// Token: 0x04000002 RID: 2
		private const int k_MaxPendingPacketCount = 16;

		// Token: 0x04000003 RID: 3
		private const int k_PacketHeaderReserveSize = 100;

		// Token: 0x04000004 RID: 4
		private NetworkConnection m_Connection;

		// Token: 0x04000005 RID: 5
		private ChannelPacket m_CurrentPacket;

		// Token: 0x04000006 RID: 6
		private float m_LastFlushTime;

		// Token: 0x04000007 RID: 7
		private byte m_ChannelId;

		// Token: 0x04000008 RID: 8
		private int m_MaxPacketSize;

		// Token: 0x04000009 RID: 9
		private bool m_IsReliable;

		// Token: 0x0400000A RID: 10
		private bool m_IsBroken;

		// Token: 0x0400000B RID: 11
		private int m_MaxPendingPacketCount;

		// Token: 0x0400000C RID: 12
		private List<ChannelPacket> m_PendingPackets;

		// Token: 0x0400000D RID: 13
		private static List<ChannelPacket> s_FreePackets;

		// Token: 0x0400000E RID: 14
		internal static int pendingPacketCount;

		// Token: 0x0400000F RID: 15
		public float maxDelay = 0.01f;

		// Token: 0x04000010 RID: 16
		private float m_LastBufferedMessageCountTimer = Time.realtimeSinceStartup;

		// Token: 0x04000011 RID: 17
		private static NetworkWriter s_SendWriter = new NetworkWriter();

		// Token: 0x04000012 RID: 18
		private bool m_Disposed;
	}
}
