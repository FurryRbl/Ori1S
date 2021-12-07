using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200007F RID: 127
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionRequest
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00007BA2 File Offset: 0x00005DA2
		internal static P2PSessionRequest Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<P2PSessionRequest>(data, dataSize);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00007BAB File Offset: 0x00005DAB
		public SteamID SteamIDRemote
		{
			get
			{
				return this.steamIDRemote;
			}
		}

		// Token: 0x0400020E RID: 526
		private SteamID steamIDRemote;
	}
}
