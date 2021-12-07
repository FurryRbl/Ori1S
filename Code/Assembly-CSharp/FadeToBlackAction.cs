using System;
using Game;
using UnityEngine;

// Token: 0x020002E4 RID: 740
[Category("Camera")]
public class FadeToBlackAction : ActionWithDuration
{
	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x06001681 RID: 5761 RVA: 0x00062C78 File Offset: 0x00060E78
	// (set) Token: 0x06001682 RID: 5762 RVA: 0x00062C80 File Offset: 0x00060E80
	public override float Duration
	{
		get
		{
			return this.DurationOfFade;
		}
		set
		{
			this.DurationOfFade = value;
		}
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x00062C89 File Offset: 0x00060E89
	public override void Perform(IContext context)
	{
		UI.Fader.FadeIn(this.Duration);
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x00062C9B File Offset: 0x00060E9B
	public override void Stop()
	{
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x00062C9D File Offset: 0x00060E9D
	public override bool IsPerforming
	{
		get
		{
			return UI.Fader.IsFadingIn();
		}
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x00062CAC File Offset: 0x00060EAC
	public override string GetNiceName()
	{
		if (this.FadeStayTime > 0f)
		{
			return string.Concat(new object[]
			{
				"Fade to black over ",
				this.DurationOfFade,
				" seconds and stay for ",
				this.FadeStayTime,
				" seconds"
			});
		}
		return "Fade to black over " + this.DurationOfFade + " seconds ";
	}

	// Token: 0x04001367 RID: 4967
	[NotNull]
	public GameObject FaderToUse;

	// Token: 0x04001368 RID: 4968
	public float DurationOfFade;

	// Token: 0x04001369 RID: 4969
	public float FadeStayTime;

	// Token: 0x0400136A RID: 4970
	private Fader m_fader;
}
