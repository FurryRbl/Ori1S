using System;
using UnityEngine;

// Token: 0x020002AD RID: 685
public class TriggerContext : IContext, ITriggerContext
{
	// Token: 0x060015B3 RID: 5555 RVA: 0x000600E0 File Offset: 0x0005E2E0
	public TriggerContext(Collider collider, Collider triggerReciever)
	{
		this.Collider = collider;
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000600EF File Offset: 0x0005E2EF
	// (set) Token: 0x060015B5 RID: 5557 RVA: 0x000600F7 File Offset: 0x0005E2F7
	public Collider Collider { get; private set; }

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00060100 File Offset: 0x0005E300
	// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00060108 File Offset: 0x0005E308
	public Collider TriggerReciever { get; private set; }
}
