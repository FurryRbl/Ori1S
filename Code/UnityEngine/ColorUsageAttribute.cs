using System;

namespace UnityEngine
{
	// Token: 0x020002FC RID: 764
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x000364C4 File Offset: 0x000346C4
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000364FC File Offset: 0x000346FC
		public ColorUsageAttribute(bool showAlpha, bool hdr, float minBrightness, float maxBrightness, float minExposureValue, float maxExposureValue)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
			this.minBrightness = minBrightness;
			this.maxBrightness = maxBrightness;
			this.minExposureValue = minExposureValue;
			this.maxExposureValue = maxExposureValue;
		}

		// Token: 0x04000BF6 RID: 3062
		public readonly bool showAlpha = true;

		// Token: 0x04000BF7 RID: 3063
		public readonly bool hdr;

		// Token: 0x04000BF8 RID: 3064
		public readonly float minBrightness;

		// Token: 0x04000BF9 RID: 3065
		public readonly float maxBrightness = 8f;

		// Token: 0x04000BFA RID: 3066
		public readonly float minExposureValue = 0.125f;

		// Token: 0x04000BFB RID: 3067
		public readonly float maxExposureValue = 3f;
	}
}
