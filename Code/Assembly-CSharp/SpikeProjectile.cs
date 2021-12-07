using System;
using UnityEngine;

// Token: 0x020005C4 RID: 1476
public class SpikeProjectile : Projectile
{
	// Token: 0x06002555 RID: 9557 RVA: 0x000A2F08 File Offset: 0x000A1108
	public new void FixedUpdate()
	{
		base.FixedUpdate();
		if (!this.IsSuspended)
		{
			this.Rigidbody.velocity = this.SpeedOverTimeCurve.Evaluate(this.CurrentTime) * this.Direction * this.Speed;
		}
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x000A2F58 File Offset: 0x000A1158
	public override bool CanBeBashed()
	{
		return false;
	}

	// Token: 0x04001FE6 RID: 8166
	public AnimationCurve SpeedOverTimeCurve;
}
