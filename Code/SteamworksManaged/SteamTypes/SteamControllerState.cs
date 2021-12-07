using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200006B RID: 107
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamControllerState
	{
		// Token: 0x040001DE RID: 478
		private uint packetNumber;

		// Token: 0x040001DF RID: 479
		private SteamControllerButtons buttons;

		// Token: 0x040001E0 RID: 480
		private short LeftPadX;

		// Token: 0x040001E1 RID: 481
		private short LeftPadY;

		// Token: 0x040001E2 RID: 482
		private short sRightPadX;

		// Token: 0x040001E3 RID: 483
		private short sRightPadY;
	}
}
