using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class NaruSpriteRotationController : Suspendable
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000BC82 File Offset: 0x00009E82
	// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000BC8A File Offset: 0x00009E8A
	public override bool IsSuspended { get; set; }

	// Token: 0x060002D9 RID: 729 RVA: 0x0000BC93 File Offset: 0x00009E93
	public new void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000BCA8 File Offset: 0x00009EA8
	public void Start()
	{
		PlatformMovement platformMovement = this.Naru.PlatformBehaviour.PlatformMovement;
		this.Angle = Mathf.Clamp(platformMovement.GroundAngle * 2f, -10f, 10f);
		this.m_transform.eulerAngles = new Vector3(0f, 0f, this.Angle);
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000BD08 File Offset: 0x00009F08
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		PlatformMovement platformMovement = this.Naru.PlatformBehaviour.PlatformMovement;
		float b = 0f;
		if (platformMovement.IsOnGround)
		{
			b = Mathf.Clamp(platformMovement.GroundAngle * 2f, -10f, 10f);
		}
		this.Angle = Mathf.LerpAngle(this.Angle, b, 0.05f);
		this.m_transform.eulerAngles = new Vector3(0f, 0f, this.Angle);
	}

	// Token: 0x04000209 RID: 521
	public float Angle;

	// Token: 0x0400020A RID: 522
	public Naru Naru;

	// Token: 0x0400020B RID: 523
	private Transform m_transform;
}
