using System;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class LegacyTwoPointTransformAnimator : LegacyAnimator
{
	// Token: 0x06001A83 RID: 6787 RVA: 0x0007231F File Offset: 0x0007051F
	public override void Start()
	{
		this.m_transform = base.transform;
		this.m_originalLocalPosition = this.m_transform.localPosition;
		base.Start();
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x00072344 File Offset: 0x00070544
	protected override void AnimateIt(float value)
	{
		this.m_transform.localPosition = Vector3.Lerp(this.m_originalLocalPosition, this.Target.transform.localPosition, value);
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x00072378 File Offset: 0x00070578
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x04001703 RID: 5891
	public GameObject Target;

	// Token: 0x04001704 RID: 5892
	private Transform m_transform;

	// Token: 0x04001705 RID: 5893
	private Vector3 m_originalLocalPosition;
}
