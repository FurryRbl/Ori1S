using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000664 RID: 1636
[ExecuteInEditMode]
public class TimelineSequenceLabel : MonoBehaviour
{
	// Token: 0x060027E1 RID: 10209 RVA: 0x000AD550 File Offset: 0x000AB750
	public void Awake()
	{
		TimelineSequenceLabel.All.Add(this);
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x000AD55D File Offset: 0x000AB75D
	public void OnEnable()
	{
		if (!TimelineSequenceLabel.All.Contains(this))
		{
			TimelineSequenceLabel.All.Add(this);
		}
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x000AD57A File Offset: 0x000AB77A
	public void OnDestroy()
	{
		TimelineSequenceLabel.All.Remove(this);
	}

	// Token: 0x04002279 RID: 8825
	public static List<TimelineSequenceLabel> All = new List<TimelineSequenceLabel>();
}
