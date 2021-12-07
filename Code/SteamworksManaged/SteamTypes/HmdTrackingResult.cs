using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200001B RID: 27
	public enum HmdTrackingResult
	{
		// Token: 0x04000077 RID: 119
		Uninitialized = 1,
		// Token: 0x04000078 RID: 120
		CalibratingInProgress = 100,
		// Token: 0x04000079 RID: 121
		CalibratingOutOfRange,
		// Token: 0x0400007A RID: 122
		RunningOK = 200,
		// Token: 0x0400007B RID: 123
		RunningOutOfRange
	}
}
