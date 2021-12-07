using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000081 RID: 129
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SocketStatusCallback
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00007BCC File Offset: 0x00005DCC
		internal static SocketStatusCallback Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SocketStatusCallback>(data, dataSize);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00007BD5 File Offset: 0x00005DD5
		public uint Socket
		{
			get
			{
				return this.socket;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00007BDD File Offset: 0x00005DDD
		public uint ListenSocket
		{
			get
			{
				return this.listenSocket;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00007BE5 File Offset: 0x00005DE5
		public SteamID SteamIDRemote
		{
			get
			{
				return this.steamIDRemote;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00007BED File Offset: 0x00005DED
		public SNetSocketState NetSocketState
		{
			get
			{
				return (SNetSocketState)this.netSocketState;
			}
		}

		// Token: 0x04000211 RID: 529
		private uint socket;

		// Token: 0x04000212 RID: 530
		private uint listenSocket;

		// Token: 0x04000213 RID: 531
		private SteamID steamIDRemote;

		// Token: 0x04000214 RID: 532
		private int netSocketState;
	}
}
