using System;
using UnityEngine;

// Token: 0x020003ED RID: 1005
[Serializable]
public class ColorCorrectionSettings
{
	// Token: 0x06001B6D RID: 7021 RVA: 0x000763C4 File Offset: 0x000745C4
	public ColorCorrectionSettings Clone()
	{
		ColorCorrectionSettings colorCorrectionSettings = (ColorCorrectionSettings)base.MemberwiseClone();
		colorCorrectionSettings.Red = new AnimationCurve(this.Red.keys);
		colorCorrectionSettings.Blue = new AnimationCurve(this.Blue.keys);
		colorCorrectionSettings.Green = new AnimationCurve(this.Green.keys);
		return colorCorrectionSettings;
	}

	// Token: 0x040017D6 RID: 6102
	public AnimationCurve Red = new AnimationCurve();

	// Token: 0x040017D7 RID: 6103
	public AnimationCurve Blue = new AnimationCurve();

	// Token: 0x040017D8 RID: 6104
	public AnimationCurve Green = new AnimationCurve();
}
