using System;
using System.Runtime.InteropServices;

namespace ManagedSteam
{
	// Token: 0x020000AF RID: 175
	public class SteamEncryptedAppTicket
	{
		// Token: 0x0600053F RID: 1343
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SteamEncryptedAppTicket_BDecryptTicket(IntPtr encryptedTicketBuffer, uint encryptedTicketSize, IntPtr decryptedTicketBuffer, ref uint decryptedTicketSize, IntPtr keyBuffer, uint keySize);

		// Token: 0x06000540 RID: 1344
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SteamEncryptedAppTicket_BIsTicketForApp(IntPtr decryptedTicket, uint ticketSize, uint nAppID);

		// Token: 0x06000541 RID: 1345
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketIssueTime(IntPtr decryptedTicket, uint decryptedTicketSize);

		// Token: 0x06000542 RID: 1346
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SteamEncryptedAppTicket_GetTicketSteamID(IntPtr decryptedTicket, uint decryptedTicketSize, out ulong psteamID);

		// Token: 0x06000543 RID: 1347
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SteamEncryptedAppTicket_GetTicketAppID(IntPtr decryptedTicket, uint decryptedTicketSize);

		// Token: 0x06000544 RID: 1348
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SteamEncryptedAppTicket_BUserOwnsAppInTicket(IntPtr decryptedTicket, uint decryptedTicketSize, ulong nAppID);

		// Token: 0x06000545 RID: 1349
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern bool SteamEncryptedAppTicket_BUserIsVacBanned(IntPtr decryptedTicket, uint decryptedTicketSize);

		// Token: 0x06000546 RID: 1350
		[DllImport("sdkencryptedappticket", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SteamEncryptedAppTicket_GetUserVariableData(IntPtr decryptedTicket, uint decryptedTicketSize, out uint userDataSize);
	}
}
