using System;
using UnityEngine;

// Token: 0x02000264 RID: 612
public static class ActionHelper
{
	// Token: 0x0600149E RID: 5278 RVA: 0x0005D25B File Offset: 0x0005B45B
	public static string GetName(ActionMethod action)
	{
		return (!(action == null)) ? action.GetNiceName() : "unknown";
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x0005D279 File Offset: 0x0005B479
	public static string GetName(Condition condition)
	{
		return (!(condition == null)) ? condition.GetNiceName() : "unknown";
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x0005D297 File Offset: 0x0005B497
	public static string GetName(UnityEngine.Object target)
	{
		return (!(target == null)) ? target.name : "unknown";
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x0005D2B5 File Offset: 0x0005B4B5
	public static string GetName(string s)
	{
		return s;
	}
}
