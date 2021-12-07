using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class WaitForConditionAction : ActionMethod
{
	// Token: 0x060014F3 RID: 5363 RVA: 0x0005E033 File Offset: 0x0005C233
	public override void Perform(IContext context)
	{
		base.StartCoroutine(this.WaitAndPerform(context));
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x0005E044 File Offset: 0x0005C244
	public IEnumerator WaitAndPerform(IContext context)
	{
		float time = 0f;
		while (!this.Condition.Validate(context))
		{
			time += Time.deltaTime;
			yield return new WaitForFixedUpdate();
			if (this.MaxWaitTime != 0f && time > this.MaxWaitTime)
			{
				yield break;
			}
		}
		this.Action.Perform(context);
		yield break;
	}

	// Token: 0x0400122F RID: 4655
	[NotNull]
	public ActionMethod Action;

	// Token: 0x04001230 RID: 4656
	[NotNull]
	public Condition Condition;

	// Token: 0x04001231 RID: 4657
	public float MaxWaitTime;
}
