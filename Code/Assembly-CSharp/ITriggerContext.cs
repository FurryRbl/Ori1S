using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public interface ITriggerContext : IContext
{
	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x060015B1 RID: 5553
	Collider Collider { get; }

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x060015B2 RID: 5554
	Collider TriggerReciever { get; }
}
