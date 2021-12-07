using System;

// Token: 0x020000E7 RID: 231
public class TimerTransition : CutsceneTransition
{
	// Token: 0x06000930 RID: 2352 RVA: 0x00027908 File Offset: 0x00025B08
	public override bool ShouldTransition()
	{
		return base.enabled && base.ParentState.Parent.CurrentStateTime > this.Time;
	}

	// Token: 0x04000768 RID: 1896
	public float Time;
}
