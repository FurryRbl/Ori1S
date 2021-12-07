using System;
using UnityEngine;

// Token: 0x02000610 RID: 1552
public class FloatProviderAnimatorDriver : MonoBehaviour
{
	// Token: 0x060026A3 RID: 9891 RVA: 0x000A9668 File Offset: 0x000A7868
	public void Start()
	{
		this.Animator.Initialize();
		float floatValue = this.Value.GetFloatValue();
		this.Animator.SampleValue(floatValue, true);
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x000A969C File Offset: 0x000A789C
	public void FixedUpdate()
	{
		this.Animator.Initialize();
		float floatValue = this.Value.GetFloatValue();
		if (floatValue != this.m_lastValue)
		{
			this.m_lastValue = floatValue;
			this.Animator.SampleValue(floatValue, false);
		}
	}

	// Token: 0x04002153 RID: 8531
	public BaseAnimator Animator;

	// Token: 0x04002154 RID: 8532
	public FloatValueProvider Value;

	// Token: 0x04002155 RID: 8533
	private float m_lastValue = float.NaN;
}
