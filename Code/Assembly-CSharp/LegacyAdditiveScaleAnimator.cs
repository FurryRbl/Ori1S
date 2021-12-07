using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class LegacyAdditiveScaleAnimator : LegacyAnimator
{
	// Token: 0x060019F1 RID: 6641 RVA: 0x0006F772 File Offset: 0x0006D972
	public override void Start()
	{
		if (!this.m_initialized)
		{
			this.m_originalScale = base.transform.localScale;
			this.m_initialized = true;
		}
		base.Start();
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x0006F7A0 File Offset: 0x0006D9A0
	protected override void AnimateIt(float value)
	{
		if (!this.m_initialized)
		{
			this.m_originalScale = base.transform.localScale;
			this.m_initialized = true;
		}
		base.transform.localScale = this.m_originalScale + this.AddScale * value;
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x0006F7F2 File Offset: 0x0006D9F2
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x04001651 RID: 5713
	private bool m_initialized;

	// Token: 0x04001652 RID: 5714
	private Vector3 m_originalScale;

	// Token: 0x04001653 RID: 5715
	public Vector3 AddScale = Vector3.one;
}
