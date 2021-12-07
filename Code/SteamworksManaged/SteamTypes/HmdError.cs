using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000F5 RID: 245
	public enum HmdError
	{
		// Token: 0x0400040E RID: 1038
		None,
		// Token: 0x0400040F RID: 1039
		InitInstallationNotFound = 100,
		// Token: 0x04000410 RID: 1040
		InitInstallationCorrupt,
		// Token: 0x04000411 RID: 1041
		InitVRClientDLLNotFound,
		// Token: 0x04000412 RID: 1042
		InitFileNotFound,
		// Token: 0x04000413 RID: 1043
		InitFactoryNotFound,
		// Token: 0x04000414 RID: 1044
		InitInterfaceNotFound,
		// Token: 0x04000415 RID: 1045
		InitInvalidInterface,
		// Token: 0x04000416 RID: 1046
		InitUserConfigDirectoryInvalid,
		// Token: 0x04000417 RID: 1047
		InitHmdNotFound,
		// Token: 0x04000418 RID: 1048
		InitNotInitialized,
		// Token: 0x04000419 RID: 1049
		DriverFailed = 200,
		// Token: 0x0400041A RID: 1050
		IPCServerInitFailed = 300,
		// Token: 0x0400041B RID: 1051
		IPCConnectFailed,
		// Token: 0x0400041C RID: 1052
		IPCSharedStateInitFailed
	}
}
