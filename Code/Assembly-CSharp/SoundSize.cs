using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
[Serializable]
public class SoundSize
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0004CDB8 File Offset: 0x0004AFB8
	public static SoundSize Everywhere
	{
		get
		{
			SoundSize result;
			if ((result = SoundSize.m_everywhere) == null)
			{
				result = (SoundSize.m_everywhere = new SoundSize
				{
					Radius = 0f,
					FalloffMargin = 0f
				});
			}
			return result;
		}
	}

	// Token: 0x04000E64 RID: 3684
	private static SoundSize m_everywhere;

	// Token: 0x04000E65 RID: 3685
	public float Radius = 5f;

	// Token: 0x04000E66 RID: 3686
	public float FalloffMargin = 10f;

	// Token: 0x04000E67 RID: 3687
	public AnimationCurve FalloffCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
}
