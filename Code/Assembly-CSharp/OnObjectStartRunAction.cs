using System;
using UnityEngine;

// Token: 0x0200036F RID: 879
[AddComponentMenu("Event Framework/Trigger/On Object Start Run Action")]
public class OnObjectStartRunAction : MonoBehaviour
{
	// Token: 0x06001923 RID: 6435 RVA: 0x0006B541 File Offset: 0x00069741
	public void Start()
	{
		if (SceneFPSTest.IsRunning())
		{
			return;
		}
		if (this.Condition == null || this.Condition.Validate(null))
		{
			this.ActionToRun.Perform(null);
		}
	}

	// Token: 0x04001589 RID: 5513
	public ActionMethod ActionToRun;

	// Token: 0x0400158A RID: 5514
	public Condition Condition;
}
