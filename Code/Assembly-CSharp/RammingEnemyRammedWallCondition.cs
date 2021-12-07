using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class RammingEnemyRammedWallCondition : Condition
{
	// Token: 0x0600156E RID: 5486 RVA: 0x0005F1EC File Offset: 0x0005D3EC
	public override bool Validate(IContext context)
	{
		ICollisionContext collisionContext = context as ICollisionContext;
		if (collisionContext != null)
		{
			Collision collision = collisionContext.Collision;
			Collider collider = collision.collider;
			if (collider)
			{
				RammingEnemy component = collider.GetComponent<RammingEnemy>();
				if (component)
				{
					return collision.relativeVelocity.magnitude > this.MinMagnitudeToBreak;
				}
			}
		}
		return false;
	}

	// Token: 0x0400128D RID: 4749
	public float MinMagnitudeToBreak = 10f;
}
