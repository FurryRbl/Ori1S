using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class IsActiveCondition : Condition
{
	// Token: 0x060016B5 RID: 5813 RVA: 0x000634E3 File Offset: 0x000616E3
	public override bool Validate(IContext context)
	{
		if (this.ActivationType == IsActiveCondition.Mode.ActiveSelf)
		{
			return this.Active == this.Target.activeSelf;
		}
		return this.Active == this.Target.activeInHierarchy;
	}

	// Token: 0x04001394 RID: 5012
	public GameObject Target;

	// Token: 0x04001395 RID: 5013
	public bool Active = true;

	// Token: 0x04001396 RID: 5014
	public IsActiveCondition.Mode ActivationType;

	// Token: 0x020002F2 RID: 754
	public enum Mode
	{
		// Token: 0x04001398 RID: 5016
		ActiveSelf,
		// Token: 0x04001399 RID: 5017
		ActiveInHierarchy
	}
}
