using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public abstract class Suspendable : MonoBehaviour, ISuspendable
{
	// Token: 0x0600025C RID: 604 RVA: 0x0000A282 File Offset: 0x00008482
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000A28A File Offset: 0x0000848A
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600025E RID: 606
	// (set) Token: 0x0600025F RID: 607
	public abstract bool IsSuspended { get; set; }
}
