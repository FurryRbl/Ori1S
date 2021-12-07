using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class CollisionContext : ICollisionContext, IContext
{
	// Token: 0x060010B1 RID: 4273 RVA: 0x0004C651 File Offset: 0x0004A851
	public CollisionContext(Collision collision, Collider collisionReciever)
	{
		this.Collision = collision;
		this.CollisionReciever = collisionReciever;
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0004C667 File Offset: 0x0004A867
	// (set) Token: 0x060010B3 RID: 4275 RVA: 0x0004C66F File Offset: 0x0004A86F
	public Collision Collision { get; private set; }

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0004C678 File Offset: 0x0004A878
	// (set) Token: 0x060010B5 RID: 4277 RVA: 0x0004C680 File Offset: 0x0004A880
	public Collider CollisionReciever { get; private set; }
}
