using System;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class RespawnPlaceholdersInHierarchyAction : ActionMethod
{
	// Token: 0x06001776 RID: 6006 RVA: 0x0006517C File Offset: 0x0006337C
	public override void Perform(IContext context)
	{
		foreach (RespawningPlaceholder respawningPlaceholder in this.RespawnHierarchy.GetComponentsInChildren<RespawningPlaceholder>())
		{
			if (respawningPlaceholder.EntityIsDead)
			{
				respawningPlaceholder.Spawn();
			}
		}
	}

	// Token: 0x0400141F RID: 5151
	public GameObject RespawnHierarchy;
}
