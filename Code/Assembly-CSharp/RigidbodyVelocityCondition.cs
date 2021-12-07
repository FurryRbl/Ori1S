using System;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class RigidbodyVelocityCondition : Condition
{
	// Token: 0x06001596 RID: 5526 RVA: 0x0005FE44 File Offset: 0x0005E044
	public override bool Validate(IContext context)
	{
		CollisionContext collisionContext = context as CollisionContext;
		return collisionContext.Collision.relativeVelocity.magnitude > this.MinVelocityMagnitude;
	}

	// Token: 0x04001295 RID: 4757
	public Rigidbody Rigidbody;

	// Token: 0x04001296 RID: 4758
	public float MinVelocityMagnitude;
}
