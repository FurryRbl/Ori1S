using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200002B RID: 43
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 20)]
	public struct ValidateAuthTicketResponse
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00003A6D File Offset: 0x00001C6D
		internal static ValidateAuthTicketResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ValidateAuthTicketResponse>(data, dataSize);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003A76 File Offset: 0x00001C76
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003A7E File Offset: 0x00001C7E
		public AuthSessionResponse AuthSessionResponseResponse
		{
			get
			{
				return (AuthSessionResponse)this.authSessionResponse;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003A86 File Offset: 0x00001C86
		public SteamID SteamIDOwner
		{
			get
			{
				return this.ownerSteamID;
			}
		}

		// Token: 0x040000DA RID: 218
		private SteamID steamID;

		// Token: 0x040000DB RID: 219
		private int authSessionResponse;

		// Token: 0x040000DC RID: 220
		private SteamID ownerSteamID;
	}
}
