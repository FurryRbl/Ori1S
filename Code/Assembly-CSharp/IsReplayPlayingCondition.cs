using System;

// Token: 0x02000291 RID: 657
public class IsReplayPlayingCondition : Condition
{
	// Token: 0x06001559 RID: 5465 RVA: 0x0005F06B File Offset: 0x0005D26B
	public override bool Validate(IContext context)
	{
		return Recorder.IsPlaying;
	}
}
