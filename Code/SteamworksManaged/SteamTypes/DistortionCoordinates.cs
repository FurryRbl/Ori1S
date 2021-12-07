using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000008 RID: 8
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public class DistortionCoordinates
	{
		// Token: 0x04000023 RID: 35
		public float[] Red = new float[2];

		// Token: 0x04000024 RID: 36
		public float[] Green = new float[2];

		// Token: 0x04000025 RID: 37
		public float[] Blue = new float[2];
	}
}
