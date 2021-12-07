using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000CD RID: 205
	public enum AuthSessionResponse
	{
		// Token: 0x04000381 RID: 897
		OK,
		// Token: 0x04000382 RID: 898
		UserNotConnectedToSteam,
		// Token: 0x04000383 RID: 899
		NoLicenseOrExpired,
		// Token: 0x04000384 RID: 900
		VACBanned,
		// Token: 0x04000385 RID: 901
		LoggedInElseWhere,
		// Token: 0x04000386 RID: 902
		VACCheckTimedOut,
		// Token: 0x04000387 RID: 903
		AuthTicketCanceled,
		// Token: 0x04000388 RID: 904
		AuthTicketInvalidAlreadyUsed,
		// Token: 0x04000389 RID: 905
		AuthTicketInvalid
	}
}
