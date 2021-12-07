using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class IsWebBuildCondition : Condition
{
	// Token: 0x06001564 RID: 5476 RVA: 0x0005F0CA File Offset: 0x0005D2CA
	public override bool Validate(IContext context)
	{
		return Application.isWebPlayer;
	}
}
