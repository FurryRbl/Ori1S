using System;

namespace UnityEngine.VR
{
	// Token: 0x02000263 RID: 611
	public enum VRDeviceType
	{
		// Token: 0x040009AE RID: 2478
		None,
		// Token: 0x040009AF RID: 2479
		Stereo,
		// Token: 0x040009B0 RID: 2480
		Split,
		// Token: 0x040009B1 RID: 2481
		Oculus,
		// Token: 0x040009B2 RID: 2482
		[Obsolete("Enum member VRDeviceType.Morpheus has been deprecated. Use VRDeviceType.PlayStationVR instead (UnityUpgradable) -> PlayStationVR", true)]
		Morpheus,
		// Token: 0x040009B3 RID: 2483
		PlayStationVR = 4
	}
}
