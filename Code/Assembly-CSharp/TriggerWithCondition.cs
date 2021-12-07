using System;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class TriggerWithCondition : MonoBehaviour, ISuspendable
{
	// Token: 0x060019AF RID: 6575 RVA: 0x0006E11E File Offset: 0x0006C31E
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x0006E126 File Offset: 0x0006C326
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x0006E12E File Offset: 0x0006C32E
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.Condition.Validate(null))
		{
			this.Action.Perform(null);
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0006E159 File Offset: 0x0006C359
	// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0006E161 File Offset: 0x0006C361
	public bool IsSuspended { get; set; }

	// Token: 0x04001609 RID: 5641
	public ActionMethod Action;

	// Token: 0x0400160A RID: 5642
	public Condition Condition;
}
