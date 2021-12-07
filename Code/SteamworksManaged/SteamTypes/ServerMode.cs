using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000F0 RID: 240
	[Flags]
	public enum ServerMode
	{
		// Token: 0x040003FF RID: 1023
		eServerModeInvalid = 0,
		// Token: 0x04000400 RID: 1024
		eServerModeNoAuthentication = 1,
		// Token: 0x04000401 RID: 1025
		eServerModeAuthentication = 2,
		// Token: 0x04000402 RID: 1026
		eServerModeAuthenticationAndSecure = 3
	}
}
