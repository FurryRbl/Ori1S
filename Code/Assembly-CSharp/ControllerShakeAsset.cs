using System;
using UnityEngine;

// Token: 0x020003D4 RID: 980
public class ControllerShakeAsset : ScriptableObject
{
	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0007389E File Offset: 0x00071A9E
	public float Duration
	{
		get
		{
			if (this.m_duration == 0f)
			{
				this.m_duration = this.ShakeCurve.CurveDuration();
			}
			return this.m_duration;
		}
	}

	// Token: 0x04001766 RID: 5990
	public AnimationCurve ShakeCurve;

	// Token: 0x04001767 RID: 5991
	private float m_duration;
}
