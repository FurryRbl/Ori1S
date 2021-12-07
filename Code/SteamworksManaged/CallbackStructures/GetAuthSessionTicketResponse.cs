using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200002E RID: 46
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAuthSessionTicketResponse
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00003AC0 File Offset: 0x00001CC0
		internal static GetAuthSessionTicketResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GetAuthSessionTicketResponse>(data, dataSize);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public AuthTicketHandle AuthTicket
		{
			get
			{
				return this.authTicket;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003AD1 File Offset: 0x00001CD1
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040000E1 RID: 225
		private AuthTicketHandle authTicket;

		// Token: 0x040000E2 RID: 226
		private Result result;
	}
}
