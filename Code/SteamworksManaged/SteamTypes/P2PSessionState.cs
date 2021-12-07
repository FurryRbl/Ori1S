using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200006E RID: 110
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionState
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00006DAF File Offset: 0x00004FAF
		internal static P2PSessionState Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<P2PSessionState>(data, dataSize);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public bool ConnectionActive
		{
			get
			{
				return this.connectionActive != 0;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00006DC6 File Offset: 0x00004FC6
		public bool Connecting
		{
			get
			{
				return this.connecting != 0;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public P2PSessionError P2PSessionError
		{
			get
			{
				return (P2PSessionError)this.p2pSessionError;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00006DDC File Offset: 0x00004FDC
		public bool UsingRelay
		{
			get
			{
				return this.usingRelay != 0;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00006DEA File Offset: 0x00004FEA
		public int BytesQueuedForSend
		{
			get
			{
				return this.bytesQueuedForSend;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public int PacketsQueuedForSend
		{
			get
			{
				return this.packetsQueuedForSend;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00006DFA File Offset: 0x00004FFA
		public uint RemoteIP
		{
			get
			{
				return this.remoteIP;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00006E02 File Offset: 0x00005002
		public ushort RemotePort
		{
			get
			{
				return this.remotePort;
			}
		}

		// Token: 0x040001EB RID: 491
		private byte connectionActive;

		// Token: 0x040001EC RID: 492
		private byte connecting;

		// Token: 0x040001ED RID: 493
		private byte p2pSessionError;

		// Token: 0x040001EE RID: 494
		private byte usingRelay;

		// Token: 0x040001EF RID: 495
		private int bytesQueuedForSend;

		// Token: 0x040001F0 RID: 496
		private int packetsQueuedForSend;

		// Token: 0x040001F1 RID: 497
		private uint remoteIP;

		// Token: 0x040001F2 RID: 498
		private ushort remotePort;
	}
}
