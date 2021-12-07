using System;

// Token: 0x02000780 RID: 1920
public class TimelineActionTrigger : BaseAnimator
{
	// Token: 0x06002C96 RID: 11414 RVA: 0x000BF583 File Offset: 0x000BD783
	public override void CacheOriginals()
	{
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x000BF588 File Offset: 0x000BD788
	public override void SampleValue(float value, bool forceSample)
	{
		value = base.TimeToAnimationCurveTime(value);
		if (!this.m_started && value > 0f && value < 0.1f)
		{
			this.m_started = true;
			this.Action.Perform(null);
		}
		if (value < 0f)
		{
			this.m_started = false;
		}
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000BF5E4 File Offset: 0x000BD7E4
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000BF5E7 File Offset: 0x000BD7E7
	public override float Duration
	{
		get
		{
			return 1f;
		}
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x000BF5EE File Offset: 0x000BD7EE
	public override void RestoreToOriginalState()
	{
	}

	// Token: 0x04002854 RID: 10324
	public ActionMethod Action;

	// Token: 0x04002855 RID: 10325
	private bool m_started;
}
