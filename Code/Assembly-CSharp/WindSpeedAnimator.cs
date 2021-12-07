using System;

// Token: 0x02000936 RID: 2358
public class WindSpeedAnimator : LegacyAnimator
{
	// Token: 0x0600342C RID: 13356 RVA: 0x000DB8B1 File Offset: 0x000D9AB1
	public override void Start()
	{
		this.m_windSpeed = this.Area.Speed;
		base.Start();
	}

	// Token: 0x0600342D RID: 13357 RVA: 0x000DB8CA File Offset: 0x000D9ACA
	protected override void AnimateIt(float value)
	{
		this.Area.Speed = this.m_windSpeed * value;
	}

	// Token: 0x0600342E RID: 13358 RVA: 0x000DB8DF File Offset: 0x000D9ADF
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(1f);
	}

	// Token: 0x04002F25 RID: 12069
	public WindArea Area;

	// Token: 0x04002F26 RID: 12070
	private float m_windSpeed;
}
