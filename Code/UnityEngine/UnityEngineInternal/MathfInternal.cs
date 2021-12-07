using System;

namespace UnityEngineInternal
{
	// Token: 0x02000067 RID: 103
	public struct MathfInternal
	{
		// Token: 0x04000105 RID: 261
		public static volatile float FloatMinNormal = 1.1754944E-38f;

		// Token: 0x04000106 RID: 262
		public static volatile float FloatMinDenormal = float.Epsilon;

		// Token: 0x04000107 RID: 263
		public static bool IsFlushToZeroEnabled = MathfInternal.FloatMinDenormal == 0f;
	}
}
