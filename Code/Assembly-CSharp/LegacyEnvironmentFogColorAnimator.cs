using System;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class LegacyEnvironmentFogColorAnimator : LegacyAnimator
{
	// Token: 0x06001A3A RID: 6714 RVA: 0x00070D50 File Offset: 0x0006EF50
	public override void Start()
	{
		this.m_initialFogColor = RenderSettings.ambientLight;
		base.Start();
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x00070D63 File Offset: 0x0006EF63
	protected override void AnimateIt(float value)
	{
		RenderSettings.ambientLight = Color.Lerp(this.m_initialFogColor, this.TargetFogColor, value);
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x00070D7C File Offset: 0x0006EF7C
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016A4 RID: 5796
	public Color TargetFogColor;

	// Token: 0x040016A5 RID: 5797
	private Color m_initialFogColor;
}
