using System;
using UnityEngine;

// Token: 0x0200025B RID: 603
public abstract class TimedActionExecuter : MonoBehaviour
{
	// Token: 0x17000398 RID: 920
	// (get) Token: 0x0600143F RID: 5183
	// (set) Token: 0x06001440 RID: 5184
	public abstract float StartTime { get; set; }

	// Token: 0x06001441 RID: 5185
	public abstract void ExecuteAction(IContext context);

	// Token: 0x06001442 RID: 5186
	public abstract void StopAction();

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06001443 RID: 5187
	public abstract ActionWithDuration ActionWithDuration { get; }

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06001444 RID: 5188
	public abstract ActionMethod ActionMethod { get; }
}
