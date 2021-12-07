using System;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
public class SlugEnemy : Enemy
{
	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x0600253F RID: 9535 RVA: 0x000A2AB8 File Offset: 0x000A0CB8
	public float BendValue
	{
		get
		{
			return this.AnimationFromBend.Evaluate((float)((!base.FaceLeft) ? -1 : 1) * this.Movement.CurrentAngularVelocity);
		}
	}

	// Token: 0x04001FCF RID: 8143
	public TraceGroundMovement Movement;

	// Token: 0x04001FD0 RID: 8144
	public AnimationCurve AnimationFromBend;
}
