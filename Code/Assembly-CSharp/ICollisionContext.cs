using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
public interface ICollisionContext : IContext
{
	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x0600156F RID: 5487
	Collision Collision { get; }

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001570 RID: 5488
	Collider CollisionReciever { get; }
}
