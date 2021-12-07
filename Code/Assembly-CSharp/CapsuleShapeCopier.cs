using System;
using UnityEngine;

// Token: 0x020006DB RID: 1755
public class CapsuleShapeCopier : MonoBehaviour
{
	// Token: 0x060029F9 RID: 10745 RVA: 0x000B4C4A File Offset: 0x000B2E4A
	private void Awake()
	{
		this.m_capsuleCollider = base.GetComponent<CapsuleCollider>();
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x000B4C58 File Offset: 0x000B2E58
	private void FixedUpdate()
	{
		if (this.m_oldRadius != this.CapsuleTarget.radius || this.m_oldHeight != this.CapsuleTarget.height || this.m_oldCenter != this.CapsuleTarget.center)
		{
			this.m_oldRadius = this.CapsuleTarget.radius;
			this.m_oldHeight = this.CapsuleTarget.height;
			this.m_oldCenter = this.CapsuleTarget.center;
			this.m_capsuleCollider.radius = this.CapsuleTarget.radius - this.ShrinkDistance;
			this.m_capsuleCollider.height = this.CapsuleTarget.height - this.ShrinkDistance * 2f;
			this.m_capsuleCollider.center = this.CapsuleTarget.center;
		}
	}

	// Token: 0x04002586 RID: 9606
	public CapsuleCollider CapsuleTarget;

	// Token: 0x04002587 RID: 9607
	public float ShrinkDistance = 0.15f;

	// Token: 0x04002588 RID: 9608
	private float m_oldRadius;

	// Token: 0x04002589 RID: 9609
	private float m_oldHeight;

	// Token: 0x0400258A RID: 9610
	private Vector3 m_oldCenter;

	// Token: 0x0400258B RID: 9611
	private CapsuleCollider m_capsuleCollider;
}
