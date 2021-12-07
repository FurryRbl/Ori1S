using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200002D RID: 45
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EncryptedAppTicketResponse
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00003AAF File Offset: 0x00001CAF
		internal static EncryptedAppTicketResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<EncryptedAppTicketResponse>(data, dataSize);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040000E0 RID: 224
		private Result result;
	}
}
