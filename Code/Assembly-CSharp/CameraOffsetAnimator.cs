using System;
using Game;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class CameraOffsetAnimator : LegacyAnimator
{
	// Token: 0x06001192 RID: 4498 RVA: 0x00050E20 File Offset: 0x0004F020
	public override void Restart()
	{
		base.Restart();
		this.m_lastValue = 0f;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x00050E34 File Offset: 0x0004F034
	protected override void AnimateIt(float value)
	{
		UI.Cameras.Current.OffsetController.AdditiveDefaultOffset += new Vector3((!this.AnimateX) ? 0f : (value - this.m_lastValue), (!this.AnimateY) ? 0f : (value - this.m_lastValue), (!this.AnimateZ) ? 0f : (value - this.m_lastValue));
		this.m_lastValue = value;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00050EBE File Offset: 0x0004F0BE
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x04000F29 RID: 3881
	private float m_lastValue;

	// Token: 0x04000F2A RID: 3882
	public bool AnimateX;

	// Token: 0x04000F2B RID: 3883
	public bool AnimateY;

	// Token: 0x04000F2C RID: 3884
	public bool AnimateZ;
}
