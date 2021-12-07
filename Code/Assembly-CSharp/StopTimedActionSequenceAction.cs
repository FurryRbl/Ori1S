using System;

// Token: 0x02000356 RID: 854
public class StopTimedActionSequenceAction : ActionMethod
{
	// Token: 0x0600185A RID: 6234 RVA: 0x0006872A File Offset: 0x0006692A
	public override void Perform(IContext context)
	{
		this.TimedActionSequence.Stop();
	}

	// Token: 0x040014E6 RID: 5350
	[NotNull]
	public TimedActionSequence TimedActionSequence;
}
