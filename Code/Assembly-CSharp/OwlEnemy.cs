using System;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class OwlEnemy : Enemy
{
	// Token: 0x06002255 RID: 8789 RVA: 0x00096880 File Offset: 0x00094A80
	public void StopSpeed()
	{
		this.FlyMovement.Velocity = Vector2.zero;
	}

	// Token: 0x04001CC8 RID: 7368
	public FlyMovement FlyMovement;

	// Token: 0x04001CC9 RID: 7369
	public SpriteRotationController SpriteRotation;
}
