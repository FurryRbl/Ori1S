using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class TriggerWithConditions : MonoBehaviour
{
	// Token: 0x060019B5 RID: 6581 RVA: 0x0006E17C File Offset: 0x0006C37C
	public void FixedUpdate()
	{
		if (this.TriggerOnce && !this.m_triggered)
		{
			foreach (Condition condition in this.Conditions)
			{
				if (!condition.Validate(null))
				{
					return;
				}
			}
			this.Action.Perform(null);
			this.m_triggered = true;
		}
	}

	// Token: 0x0400160C RID: 5644
	public ActionMethod Action;

	// Token: 0x0400160D RID: 5645
	public List<Condition> Conditions;

	// Token: 0x0400160E RID: 5646
	public bool TriggerOnce = true;

	// Token: 0x0400160F RID: 5647
	private bool m_triggered;
}
