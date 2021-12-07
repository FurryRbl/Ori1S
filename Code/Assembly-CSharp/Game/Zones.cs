using System;
using System.Collections.Generic;

namespace Game
{
	// Token: 0x020000C2 RID: 194
	internal static class Zones
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00023CAD File Offset: 0x00021EAD
		// Note: this type is marked as 'beforefieldinit'.
		static Zones()
		{
			Zones.ShorterHintZones = new List<ShorterHintZone>();
		}

		// Token: 0x040006A1 RID: 1697
		public static List<ShorterHintZone> ShorterHintZones;

		// Token: 0x040006A2 RID: 1698
		public static List<WaterZone> WaterZones = new List<WaterZone>();
	}
}
