using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000080 RID: 128
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct P2PSessionConnectFail
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00007BB3 File Offset: 0x00005DB3
		internal static P2PSessionConnectFail Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<P2PSessionConnectFail>(data, dataSize);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00007BBC File Offset: 0x00005DBC
		public SteamID SteamIDRemote
		{
			get
			{
				return this.steamIDRemote;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public P2PSessionError SessionError
		{
			get
			{
				return (P2PSessionError)this.p2pSessionError;
			}
		}

		// Token: 0x0400020F RID: 527
		private SteamID steamIDRemote;

		// Token: 0x04000210 RID: 528
		private byte p2pSessionError;
	}
}
