using System;

namespace ManagedSteam
{
	// Token: 0x02000110 RID: 272
	internal enum ErrorCodes
	{
		// Token: 0x040004BA RID: 1210
		Ok,
		// Token: 0x040004BB RID: 1211
		StartOfNativeErrors,
		// Token: 0x040004BC RID: 1212
		AlreadyLoaded,
		// Token: 0x040004BD RID: 1213
		SteamInitializeFailed,
		// Token: 0x040004BE RID: 1214
		SteamInterfaceInitializeFailed,
		// Token: 0x040004BF RID: 1215
		EndOfNativeErrors = 4000,
		// Token: 0x040004C0 RID: 1216
		StartOfManagedErrors = 5000,
		// Token: 0x040004C1 RID: 1217
		InvalidInterfaceVersion,
		// Token: 0x040004C2 RID: 1218
		UsageAfterAPIShutdown,
		// Token: 0x040004C3 RID: 1219
		CallbackStructSizeMissmatch,
		// Token: 0x040004C4 RID: 1220
		NotAvailableInLite,
		// Token: 0x040004C5 RID: 1221
		NoCallbackEvent,
		// Token: 0x040004C6 RID: 1222
		NoResultEvent,
		// Token: 0x040004C7 RID: 1223
		CantChangeEncoding,
		// Token: 0x040004C8 RID: 1224
		SteamInstanceIsNull,
		// Token: 0x040004C9 RID: 1225
		MatchmakingServersIsNull,
		// Token: 0x040004CA RID: 1226
		StringIsToBig,
		// Token: 0x040004CB RID: 1227
		EndOfManagedErrors = 9000
	}
}
