using System;

// Token: 0x02000257 RID: 599
public abstract class PerformingAction : ActionMethod
{
	// Token: 0x0600142B RID: 5163
	public abstract void Stop();

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x0600142C RID: 5164
	public abstract bool IsPerforming { get; }
}
