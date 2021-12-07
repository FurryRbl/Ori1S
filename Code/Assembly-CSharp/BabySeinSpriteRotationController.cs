using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class BabySeinSpriteRotationController : Suspendable
{
	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A1DB File Offset: 0x000083DB
	// (set) Token: 0x06000259 RID: 601 RVA: 0x0000A1E3 File Offset: 0x000083E3
	public override bool IsSuspended { get; set; }

	// Token: 0x0600025A RID: 602 RVA: 0x0000A1EC File Offset: 0x000083EC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		PlatformMovement platformMovement = this.BabySein.PlatformBehaviour.PlatformMovement;
		float b = 0f;
		if (platformMovement.IsOnGround)
		{
			b = Mathf.Clamp(platformMovement.GroundAngle * 2f, -15f, 15f);
		}
		this.Angle = Mathf.LerpAngle(this.Angle, b, 0.05f);
		base.transform.eulerAngles = new Vector3(0f, 0f, this.Angle);
	}

	// Token: 0x040001D8 RID: 472
	public float Angle;

	// Token: 0x040001D9 RID: 473
	public BabySein BabySein;
}
