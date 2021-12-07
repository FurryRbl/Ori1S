using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
[Serializable]
public class LowPassFilterSettings
{
	// Token: 0x170002FB RID: 763
	// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0004CE45 File Offset: 0x0004B045
	public static LowPassFilterSettings StandardSetting
	{
		get
		{
			LowPassFilterSettings result;
			if ((result = LowPassFilterSettings.m_standardSetting) == null)
			{
				result = (LowPassFilterSettings.m_standardSetting = new LowPassFilterSettings());
			}
			return result;
		}
	}

	// Token: 0x04000E68 RID: 3688
	private static LowPassFilterSettings m_standardSetting;

	// Token: 0x04000E69 RID: 3689
	public AnimationCurve CutoffFrequency = AnimationCurve.Linear(0f, 0f, 30f, 22000f);

	// Token: 0x04000E6A RID: 3690
	public AnimationCurve LowpassResonance = AnimationCurve.Linear(0f, 1f, 30f, 10f);

	// Token: 0x04000E6B RID: 3691
	public bool Active;
}
