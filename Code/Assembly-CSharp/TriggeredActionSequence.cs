using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class TriggeredActionSequence : MonoBehaviour
{
	// Token: 0x060014EF RID: 5359 RVA: 0x0005DFD8 File Offset: 0x0005C1D8
	public void Trigger()
	{
		this.ActionsWithDuration[this.m_count % this.ActionsWithDuration.Count].Perform(null);
		this.m_count++;
	}

	// Token: 0x0400122D RID: 4653
	public List<ActionMethod> ActionsWithDuration = new List<ActionMethod>();

	// Token: 0x0400122E RID: 4654
	private int m_count;
}
