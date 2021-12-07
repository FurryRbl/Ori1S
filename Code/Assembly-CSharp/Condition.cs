using System;
using fsm;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public abstract class Condition : MonoBehaviour, ICondition
{
	// Token: 0x060009DA RID: 2522
	public abstract bool Validate(IContext context);

	// Token: 0x060009DB RID: 2523 RVA: 0x0002B1A6 File Offset: 0x000293A6
	public virtual string GetNiceName()
	{
		return StringUtility.AddSpaces(base.GetType().Name);
	}
}
