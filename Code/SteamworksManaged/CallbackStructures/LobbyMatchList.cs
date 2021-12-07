using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000ED RID: 237
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyMatchList
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00009AAB File Offset: 0x00007CAB
		internal static LobbyMatchList Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LobbyMatchList>(data, dataSize);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public uint LobbiesMatching
		{
			get
			{
				return this.lobbiesMatching;
			}
		}

		// Token: 0x040003F8 RID: 1016
		private uint lobbiesMatching;
	}
}
